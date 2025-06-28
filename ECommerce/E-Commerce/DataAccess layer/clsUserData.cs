using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_layer.Models;
using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;


namespace DataAccess_layer
{
    public class clsUserData
    {
        public static int AddUser(string name, string email, string password,string roles)
        {
            int AddedID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand("SP_AddNewUser", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@name", name);
                        Command.Parameters.AddWithValue("@email", email);
                        Command.Parameters.AddWithValue("@password", password);
                        Command.Parameters.AddWithValue("@roles", roles);

                        var outputIdParam = new SqlParameter("@addedId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);

                        Connection.Open();
                        Command.ExecuteNonQuery();

                        AddedID = (int)Command.Parameters["@addedId"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                AddedID = -1;
            }

            return AddedID;
        }

        public static bool UpdateUser(int id, string name, string email,string roles)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (SqlCommand Command = new SqlCommand("SP_UpdateUser", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@id", id);
                        Command.Parameters.AddWithValue("@name", name);
                        Command.Parameters.AddWithValue("@email", email);
                        Command.Parameters.AddWithValue("@roles", roles);

                        Connection.Open();
                        rowsAffected = Command.ExecuteNonQuery();
                        return true;

                    }


                }
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }
            return (rowsAffected > 0);
        }

        public static bool Find(int Id, ref string name, ref string email,ref string roles)
        {
            bool IsFound = false;         
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand("SP_GetUserById", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@id", Id);

                        Connection.Open();
                        using (SqlDataReader reader = Command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;


                                name = (string)reader["name"];
                                email = (string)reader["email"];
                                roles = (string)reader["roles"];
                            }
                            else
                                IsFound = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            return IsFound;
        }

        public static bool FindById(int Id, ref string name, ref string email,ref string password, ref string roles)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand("SP_GetAllUserInfo", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@id", Id);

                        Connection.Open();
                        using (SqlDataReader reader = Command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;


                                name = (string)reader["name"];
                                email = (string)reader["email"];
                                password = (string)reader["password"];
                                roles = (string)reader["roles"];
                            }
                            else
                                IsFound = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            return IsFound;
        }

        public static bool FindByCredentials(string email,string password, ref string name, ref int id, ref string roles)
        {
            bool IsFound = false;
            try 
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand("SP_GetByCredentials", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@email", email);
                        Command.Parameters.AddWithValue("@password", password);

                        Connection.Open();
                        using (SqlDataReader reader = Command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;


                                email = (string)reader["email"];
                                password = (string)reader["password"];

                                name = (string)reader["name"];
                                id = (int)reader["id"];
                                roles = (string)reader["roles"];
                            }
                            else
                                IsFound = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            return IsFound;
        }
        public static bool Delete(int Id)
        {
            int rowsAffected = 0;
            
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand("SP_DeleteUser", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@id", Id);

                        Connection.Open();
                        rowsAffected = Command.ExecuteNonQuery();
                    }


                }
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }
            return (rowsAffected > 0);
        }

        public static bool IsExists(int Id)
        {
            bool IsFound = false;
           
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_CheckExistsUser", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@id", (object)Id ?? DBNull.Value);

                        SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };

                        Command.Parameters.Add(returnParameter);
                        Command.ExecuteNonQuery();

                        IsFound = (int)returnParameter.Value == 1;
                    }

                }
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            return IsFound;
        }
        public static List<dtoUser> GetAllUsers()
        {
            var result = new List<dtoUser>();

            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllUsers", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new dtoUser
                                (
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetString(reader.GetOrdinal("name")),
                                    reader.GetString(reader.GetOrdinal("email")),
                                    reader.GetString(reader.GetOrdinal("roles"))
                                ));
                            }
                        }
                    }


                    return result;
                }
            }
            catch
            {
                result = new List<dtoUser>();
            }

            return result.ToList();
        }

    }
}
