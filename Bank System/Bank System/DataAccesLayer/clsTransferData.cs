using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class clsTransferData
    {

        public static int AddNewTransferRecord(DateTime Date, decimal Amount, int FromAccountNumber,
            int ToAccountNumber, int CreatedByUserID)
        {
            int ID = -1;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO TransferLog
           (Date,Amount,FromAccountNumber,ToAccountNumber,CreatedByUserID)
           VALUES (@Date,@Amount,@FromAccountNumber,@ToAccountNumber,@CreatedByUserID);
             Select SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@Date", Date);
            Command.Parameters.AddWithValue("@Amount", Amount);
            Command.Parameters.AddWithValue("@FromAccountNumber", FromAccountNumber);
            Command.Parameters.AddWithValue("@ToAccountNumber", ToAccountNumber);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            

            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int InsertedID))
                {
                    ID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                ID = -1;
            }
            finally
            {
                Connection.Close();
            }
            return ID;
        }
        public static DataTable GetAllTransferRecords()
        {
            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Select * from TransferLog";
            SqlCommand Command = new SqlCommand(query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return dt;
        }

    }
}
