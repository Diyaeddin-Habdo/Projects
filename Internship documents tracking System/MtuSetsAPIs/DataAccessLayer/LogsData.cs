using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LogsDTO
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int DocumentsId { get; set; }
        public string Status { get; set; }
        public DateTime Time { get; set; }

        public LogsDTO(int id,int teacherId,int documentsId,string status,DateTime time)
        {
            this.Id = id;
            this.TeacherId = teacherId;
            this.DocumentsId = documentsId;
            this.Status = status;
            this.Time = time;
        }
    }
    public class TeacherApprovalLogsDTO
    {
        public string Status { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string SNo { get; set; }
        public string ImagePath { get; set; }
        public DateTime Time { get; set; }

        public TeacherApprovalLogsDTO(string status,string name,string email,string sNo,string imagePath,DateTime time)
        {
            this.Status = status;
            this.Name = name;
            this.Email = email;
            this.SNo = sNo;
            this.ImagePath = imagePath;
            this.Time = time;    
        }
    }
    public class TeacherApprovalLogsForStudentDTO
    {
        public string Status { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImagePath { get; set; }
        public DateTime Time { get; set; }

        public TeacherApprovalLogsForStudentDTO(string status, string name, string email, string Phone, string imagePath, DateTime time)
        {
            this.Status = status;
            this.Name = name;
            this.Email = email;
            this.Phone = Phone;
            this.ImagePath = imagePath;
            this.Time = time;
        }
    }
    public class LogsData
    {
        static string _connectionString = "Server=localhost;Database=MtuSETS;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";

        /// <summary>
        /// Retrieves a list of teacher approval logs based on the specified teacher's ID.
        /// </summary>
        /// <param name="teacherId">The ID of the teacher whose approval logs are to be retrieved.</param>
        /// <returns>
        /// A list of <see cref="TeacherApprovalLogsDTO"/> objects containing the approval log details for the specified teacher.
        /// </returns>
        public static List<TeacherApprovalLogsDTO> GetTeacherApprovalLogs(int teacherId)
        {
            var LogsList = new List<TeacherApprovalLogsDTO>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetTeacherApprovalHistory", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LogsList.Add(new TeacherApprovalLogsDTO
                                (
                                    reader.GetString(reader.GetOrdinal("Status")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetString(reader.GetOrdinal("Email")),
                                    reader.GetString(reader.GetOrdinal("SNo")),
                                    reader.GetString(reader.GetOrdinal("ImagePath")),
                                    reader.GetDateTime(reader.GetOrdinal("Time"))
                                ));
                            }
                        }
                    }
                    catch { }
                }


                return LogsList;
            }

        }

        /// <summary>
        /// Retrieves a list of teacher approval logs for a specific student based on the student's ID.
        /// </summary>
        /// <param name="studentId">The ID of the student whose approval logs are to be retrieved.</param>
        /// <returns>
        /// A list of <see cref="TeacherApprovalLogsForStudentDTO"/> objects containing the approval log details for the specified student.
        /// </returns>
        public static List<TeacherApprovalLogsForStudentDTO> GetTeacherApprovalLogsByStudentId(int studentId)
        {
            var LogsList = new List<TeacherApprovalLogsForStudentDTO>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetTeacherApprovalHistoryByStudentId", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LogsList.Add(new TeacherApprovalLogsForStudentDTO
                                (
                                    reader.GetString(reader.GetOrdinal("Status")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetString(reader.GetOrdinal("Email")),
                                    reader.GetString(reader.GetOrdinal("Phone")),
                                    reader.GetString(reader.GetOrdinal("ImagePath")),
                                    reader.GetDateTime(reader.GetOrdinal("Time"))
                                ));
                            }
                        }
                    }
                    catch { }
                }


                return LogsList;
            }

        }

    }
}
