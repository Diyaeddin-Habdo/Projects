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
    public class clsProductImagesData
    {
        public static List<string> GetAllProductImages(int productId)
        {
            List<string> images = new List<string>();
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                try
                {
                    using SqlCommand Command = new SqlCommand("SP_GetAllProductImages", Connection);
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@id", productId);

                    Connection.Open();
                    using SqlDataReader reader = Command.ExecuteReader();

                    while (reader.Read())
                    {
                        images.Add((string)reader["image"]);
                    }
                }
                catch (Exception ex)
                {
                    return images;
                }
            }

            return images;
        }



        public static List<clsProductImages> GetAllProductImages()
        {
            List<clsProductImages> images = new List<clsProductImages>();
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                try
                {
                    using SqlCommand Command = new SqlCommand("SP_GetAllImages", Connection);
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using SqlDataReader reader = Command.ExecuteReader();

                    while (reader.Read())
                    {
                        images.Add(new clsProductImages((int)reader["id"], (string)reader["image"], (int)reader["productId"]));
                    }
                }
                catch (Exception ex)
                {
                    return images;
                }
            }

            return images;
        }


        public static bool DeleteAllProductImages(int productId)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteAllProductImages", connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@id", productId);
                        rowsAffected = Command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }
            return rowsAffected > 0;
        }
        public static int InsertImage(int productId, string imagePath)
        {
            int AddedID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand Command = new SqlCommand("SP_AddNewProductImage", connection))
                    {
                        Command.CommandTimeout = 180;
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@ProductId", productId);
                        Command.Parameters.AddWithValue("@ImageUrl", imagePath);
                        SqlParameter outputIdParam = new SqlParameter("@NewID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);
                        Command.ExecuteNonQuery();

                        AddedID = (int)Command.Parameters["@NewID"].Value;
                    }
                }
            }
            catch
            {
                AddedID = -1;
            }
            return AddedID;
        }
        public static bool UpdateImage(int Id, string imagePath, int productId)
        {
            int rowsAffected = 0;

            try
            {
                using SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
                using SqlCommand Command = new SqlCommand("SP_UpdateProductImage", connection);
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@Id", Id);
                Command.Parameters.AddWithValue("@ProductId", productId);
                Command.Parameters.AddWithValue("@ImageUrl", imagePath);
                SqlParameter outputIdParam = new SqlParameter("@NewID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                Command.Parameters.Add(outputIdParam);

                rowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                rowsAffected = -1;
            }
            return rowsAffected > 0;
        }
    }
}
