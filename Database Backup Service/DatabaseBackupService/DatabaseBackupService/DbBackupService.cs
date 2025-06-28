using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace DatabaseBackupService
{
    public partial class DbBackupService : ServiceBase
    {
        private string logFilePath;
        private string connectionString;
        private string BackupFolder;
        private string BackupIntervalMinutes;
        private Timer timer;
        public DbBackupService()
        {
            InitializeComponent();
            logFilePath = ConfigurationManager.AppSettings["LogFolder"];
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            BackupFolder = ConfigurationManager.AppSettings["BackupFolder"];
            BackupIntervalMinutes = ConfigurationManager.AppSettings["BackupIntervalMinutes"];

            CheckLogFile();
            CheckFolders(BackupFolder);
        }
        protected override void OnStart(string[] args)
        {
            LogServiceEvent("Service started");
            SetHourlyTimer();
        }
        protected override void OnStop()
        {
            timer.Dispose();
            LogServiceEvent("Service stopped");
        }
        private void SetHourlyTimer()
        {
            timer = new Timer();
            timer.Interval = int.Parse(BackupIntervalMinutes) * 60 * 1000; 
            timer.Elapsed += (sender, e) => TakeBackup();
            timer.AutoReset = true;
            timer.Start();
        }
        private void TakeBackup()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string timestamp = DateTime.Now.ToString("yyyy.MM.dd_HH-mm-ss");
                    string databaseName = "MtuSetsDB";

                    string backupQuery = $@"
                    BACKUP DATABASE [{databaseName}]
                    TO DISK = '{BackupFolder}\{timestamp}.bak'
                    WITH FORMAT,
                    MEDIANAME = 'SQLServerBackups',
                    NAME = 'Full Database Backup';";


                    using (SqlCommand command = new SqlCommand(backupQuery, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        LogServiceEvent($"Backup Taked  -> {BackupFolder}\\{timestamp}.bak");
                    }
                }
            }
            catch (Exception ex)
            {
                LogServiceEvent("Error : " +  ex.Message );
            }
        }
        private void LogServiceEvent(string message)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n";
            File.AppendAllText(logFilePath, logMessage);

            // Write to console if running interactively
            if (Environment.UserInteractive)
            {
                Console.WriteLine(logMessage);
            }
        }
        private void CheckLogFile()
        {
            // Validate and create directory if it doesn't exist
            if (string.IsNullOrWhiteSpace(logFilePath))
            {
                throw new ConfigurationErrorsException("LogDirectory is not specified in the configuration file.");
            }

            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }

            logFilePath = Path.Combine(logFilePath, "ServiceStateLog.txt");
        }
        private void CheckFolders(string path)
        {
            if (string.IsNullOrEmpty(path))
                LogServiceEvent("Path null or empty");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                LogServiceEvent($"Folder created : {path}");
            }
        }
        public void StartInConsole()
        {
            OnStart(null); // Trigger OnStart logic
            Console.WriteLine("Press Enter to stop the service...");
            Console.ReadLine(); // Wait for user input to simulate service stopping
            OnStop(); // Trigger OnStop logic
            Console.ReadKey();

        }
    }
}