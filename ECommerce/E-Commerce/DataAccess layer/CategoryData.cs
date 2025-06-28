using DataAccess_layer.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_layer
{
    public class CategoryData
    {
        public static int AddCategory(string name,string image)
        {
            int AddedID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand("SP_AddNewCategory", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@name", name);
                        Command.Parameters.AddWithValue("@image", image);

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

        public static bool UpdateCategory(int id, string name, string image)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (SqlCommand Command = new SqlCommand("SP_UpdateCategory", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@id", id);
                        Command.Parameters.AddWithValue("@name", name);
                        Command.Parameters.AddWithValue("@image", image);

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

        public static bool Find(int Id, ref string name, ref string image)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand("SP_GetCategoryById", Connection))
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
                                image = (string)reader["image"];
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
                    using (SqlCommand Command = new SqlCommand("SP_DeleteCategory", Connection))
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
                    using (SqlCommand Command = new SqlCommand("SP_CheckExistsCategory", Connection))
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

        public static List<dtoCategory> GetAllCategories()
        {
            var result = new List<dtoCategory>();

            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllCategories", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new dtoCategory
                                (
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetString(reader.GetOrdinal("name")),
                                    reader.GetString(reader.GetOrdinal("image"))
                                ));
                            }
                        }
                    }


                    return result;
                }
            }
            catch
            {
                result = new List<dtoCategory>();
            }

            return result.ToList();
        }
        
        public static List<dtoCategory> GetCategoriesPage(int PageNumber,int PageSize)
        {
            var result = new List<dtoCategory>();

            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetCategoriesPage", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageNumber", PageNumber);
                        cmd.Parameters.AddWithValue("@PageSize", PageSize);

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new dtoCategory
                                (
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetString(reader.GetOrdinal("name")),
                                    reader.GetString(reader.GetOrdinal("image"))
                                ));
                            }
                        }
                    }


                    return result;
                }
            }
            catch
            {
                result = new List<dtoCategory>();
            }

            return result.ToList();
        }
    }
}
