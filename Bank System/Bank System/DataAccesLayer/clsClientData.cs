using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class clsClientData
    {
        public static int AddNewClient(int PersonID, string AccountNumber, string PinCode, decimal AccountBalance)
        {
            int ClientID = -1;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Clients (PersonID,AccountNumber,PinCode,AccountBalance)
                        VALUES (@PersonID,@AccountNumber,@PinCode,@AccountBalance);
                        Select SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@AccountNumber", AccountNumber);
            Command.Parameters.AddWithValue("@PinCode", PinCode);
            Command.Parameters.AddWithValue("@AccountBalance", AccountBalance);

            
            try
            {
                Connection.Open();
                object obj = Command.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int InsertedID))
                {
                    ClientID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                ClientID = -1;
            }
            finally
            {
                Connection.Close();
            }
            return ClientID;
        }

        public static bool UpdateClient(int ClientID, int PersonID, string AccountNumber, string PinCode, decimal AccountBalance)
        {
            int rowsAffected = 0;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE Clients
                           SET PersonID = @PersonID
                              ,AccountNumber = @AccountNumber
                              ,PinCode = @PinCode
                              ,AccountBalance = @AccountBalance
                         WHERE ClientID = @ClientID";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@ClientID", ClientID);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@AccountNumber", AccountNumber);
            Command.Parameters.AddWithValue("@PinCode", PinCode);
            Command.Parameters.AddWithValue("@AccountBalance", AccountBalance);

            try
            {
                Connection.Open();
                rowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }
            finally
            {
                Connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool FindByPersonID(int PersonID,ref int ClientID,ref string AccountNumber,ref string PinCode,ref decimal AccountBalance)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * From Clients WHERE PersonID = @PersonID";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    ClientID = (int)reader["ClientID"];
                    AccountNumber = (string)reader["AccountNumber"];
                    PinCode = (string)reader["PinCode"];
                    AccountBalance = (decimal)reader["AccountBalance"];
                }
                else
                    IsFound = false;
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }

        public static bool FindByClientID(int ClientID, ref int PersonID, ref string AccountNumber, ref string PinCode, ref decimal AccountBalance)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * From Clients WHERE ClientID = @ClientID";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@ClientID", ClientID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    PersonID = (int)reader["PersonID"];
                    AccountNumber = (string)reader["AccountNumber"];
                    PinCode = (string)reader["PinCode"];
                    AccountBalance = (decimal)reader["AccountBalance"];
                }
                else
                    IsFound = false;
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }
       
        public static bool FindByAccountNumber(string AccountNumber, ref int ClientID, ref int PersonID, ref string PinCode, ref decimal AccountBalance)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select *from Clients where AccountNumber =@AccountNumber";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@AccountNumber", AccountNumber);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    ClientID = (int)reader["ClientID"];
                    PersonID = (int)reader["PersonID"];
                    PinCode = (string)reader["PinCode"];
                    AccountBalance = (decimal)reader["AccountBalance"];
                }
                else
                    IsFound = false;

                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }

        public static bool DeleteClient(int ClientID)
        {
            int rowsAffected = 0;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Delete from Clients Where ClientID = @ClientID";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@ClientID", ClientID);


            try
            {
                Connection.Open();
                rowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }
            finally
            {
                Connection.Close();
            }
            return (rowsAffected > 0);
        }

        public static bool IsClientExists(int ClientID)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select Found = 1 From Clients WHERE ClientID = @ClientID";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@ClientID", ClientID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                IsFound = reader.HasRows;
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        } 

        public static bool IsClientExistsForPersonID(int PersonID)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select Found = 1 From Clients WHERE PersonID = @PersonID";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                IsFound = reader.HasRows;
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }

        public static bool Withdraw(string AccountNumber,decimal Amount)
        {
            int rowsAffected = 0;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update Clients set AccountBalance -= @Amount where AccountNumber = @AccountNumber AND AccountBalance >= @Amount;";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@AccountNumber", AccountNumber);
            Command.Parameters.AddWithValue("@Amount", Amount);

            try
            {
                Connection.Open();
                rowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }
            finally
            {
                Connection.Close();
            }
            return (rowsAffected > 0);
        }

        public static bool Deposit(string AccountNumber,decimal Amount)
        {
            int rowsAffected = 0;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update Clients set AccountBalance += @Amount where AccountNumber = @AccountNumber;";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@AccountNumber", AccountNumber);
            Command.Parameters.AddWithValue("@Amount", Amount);

            try
            {
                Connection.Open();
                rowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }
            finally
            {
                Connection.Close();
            }
            return (rowsAffected > 0);
        }
        
        public static bool Transfer(string FromAccountNumber,string ToAccountNumber,decimal Amount)
        {
            int rowsAffected = 0;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update Clients set AccountBalance -= @Amount where AccountNumber = @FromAccountNumber and AccountBalance >= @Amount;
                             update Clients set AccountBalance += @Amount where AccountNumber = @ToAccountNumber";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@FromAccountNumber", FromAccountNumber);
            Command.Parameters.AddWithValue("@ToAccountNumber", ToAccountNumber);
            Command.Parameters.AddWithValue("@Amount", Amount);

            try
            {
                Connection.Open();
                rowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }
            finally
            {
                Connection.Close();
            }
            return (rowsAffected > 0);
        }
        
        public static bool IsClientExists(string AccountNumber)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select Found = 1 From Clients WHERE AccountNumber = @AccountNumber";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@AccountNumber", AccountNumber);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                IsFound = reader.HasRows;
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }

        public static DataTable GetAllClients()
        {
            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT Clients.ClientID, Clients.PersonID, People.IDNumber, People.FirstName + ' ' + People.LastName as FullName,
                            CASE WHEN
                            People.Gender = 0 then 'Male'
                            else 'Female'
                            end as GenderCaption, Clients.AccountBalance
                            FROM     Clients INNER JOIN
                                              People ON Clients.PersonID = People.PersonID";
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
