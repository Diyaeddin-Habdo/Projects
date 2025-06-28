using DataAccess_layer.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_layer
{
    public class clsProductData
    {        
        // =============== Handle images ===============
        private static int InsertImage(int productId, string imagePath, SqlConnection connection, SqlTransaction transaction)
        {
            int AddedID = -1;

            try
            {
                using (SqlCommand Command = new SqlCommand("SP_AddNewProductImage", connection, transaction))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@productId", productId);
                    Command.Parameters.AddWithValue("@image", imagePath);
                    SqlParameter outputIdParam = new SqlParameter("@addedId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputIdParam);
                    Command.ExecuteNonQuery();

                    AddedID = (int)Command.Parameters["@addedId"].Value;
                }
            }
            catch
            {
                AddedID = -1;
            }
            return AddedID;
        }
        public static bool DeleteAllProductImages(int productId, SqlConnection connection, SqlTransaction transaction)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlCommand Command = new SqlCommand("SP_DeleteAllProductImages", connection, transaction))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@id", productId);
                    rowsAffected = Command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }
            return rowsAffected > 0;
        }

        // =============================================

        public static int Add(int CategoryId, string Title, string Description, int Rating, decimal Price, decimal Discount, string About, List<string> Images)
        {
            int AddedID = -1;
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                Connection.Open();
                using (SqlTransaction transaction = Connection.BeginTransaction())
                {
                    try
                    {

                        using SqlCommand Command = new SqlCommand("SP_AddNewProduct", Connection, transaction);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@categoryId", CategoryId);
                        Command.Parameters.AddWithValue("@title", Title);
                        Command.Parameters.AddWithValue("@description", Description);
                        Command.Parameters.AddWithValue("@rating", Rating);
                        Command.Parameters.AddWithValue("@price", Price);
                        Command.Parameters.AddWithValue("@discount", Discount);
                        Command.Parameters.AddWithValue("@about", About);

                        SqlParameter outputIdParam = new SqlParameter("@addedId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outputIdParam);
                        Command.ExecuteNonQuery();

                        AddedID = (int)Command.Parameters["@addedId"].Value;

                        foreach (var image in Images)
                        {
                            if (InsertImage(AddedID, image, Connection, transaction) == -1)
                                throw new Exception("Image adding failde.");
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        AddedID = -1;
                    }
                }
            }
            return AddedID;
        }
        public static bool Update(int id,int CategoryId, string Title, string Description, int Rating, decimal Price, decimal Discount, string About, List<string> Images)
        {
            bool success = false;
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                Connection.Open();
                using (SqlTransaction transaction = Connection.BeginTransaction())
                {
                    try
                    {
                        using SqlCommand Command = new SqlCommand("SP_UpdateProduct", Connection, transaction);
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@id", id);
                        Command.Parameters.AddWithValue("@categoryId", CategoryId);
                        Command.Parameters.AddWithValue("@title", Title);
                        Command.Parameters.AddWithValue("@description", Description);
                        Command.Parameters.AddWithValue("@rating", Rating);
                        Command.Parameters.AddWithValue("@price", Price);
                        Command.Parameters.AddWithValue("@discount", Discount);
                        Command.Parameters.AddWithValue("@about", About);

                        int rowsAffected = Command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            if (!DeleteAllProductImages(id, Connection, transaction))
                                throw new Exception("Image deleting failde.");

                            foreach (var image in Images)
                            {
                                if (InsertImage(id, image, Connection, transaction) == -1)
                                    throw new Exception("Image adding failde.");
                            }
                        }

                        transaction.Commit();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return success;
        }
        public static bool Find(int id, ref int CategoryId,ref string Title, ref string Description,ref int Rating,ref decimal Price,ref decimal Discount,ref string About,ref List<string> Images)
        {
            bool IsFound = false;
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                Connection.Open();
                using (SqlTransaction transaction = Connection.BeginTransaction())
                {
                    try
                    {

                        using SqlCommand Command = new SqlCommand("SP_GetProduct", Connection, transaction);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@id", id);

                        using SqlDataReader reader = Command.ExecuteReader();

                        if (reader.Read())
                        {
                            IsFound = true;

                            CategoryId = (int)reader["categoryId"];
                            Title = (string)reader["title"];
                            Description = (string)reader["description"];
                            Rating = (int)reader["rating"];
                            Price = (decimal)reader["price"];
                            Discount = (decimal)reader["discount"];
                            About = (string)reader["about"];
                            Images.AddRange(clsProductImagesData.GetAllProductImages(id));
                        }
                        else
                            IsFound = false;

                        reader.Close();
                        transaction.Commit();
                        
                    }
                    catch
                    {
                        IsFound = false;
                        transaction.Rollback();
                    }
                }
            }
            return IsFound;
        }
        public static bool Delete(int Id)
        {
            bool success = false;
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                Connection.Open();
                using (SqlTransaction transaction = Connection.BeginTransaction())
                {
                    try
                    {
                        if (!clsProductImagesData.DeleteAllProductImages(Id))
                            throw new Exception("Images deleting failde");

                        SqlCommand Command = new SqlCommand("SP_DeleteProduct", Connection, transaction);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@id", Id);

                        int rowsAffected = Command.ExecuteNonQuery();

                        transaction.Commit();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return success;
        }
        public static bool IsExists(int Id)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            //string query = @"select Found = 1 From Products WHERE Id = @Id";
            //SqlCommand Command = new SqlCommand(query, Connection);
            SqlCommand Command = new SqlCommand("SP_CheckExistsProduct", Connection);
            Command.Parameters.AddWithValue("@Id", Id);

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


        public static List<dtoGetAllProducts> GetAll()
        {
            List<dtoGetAllProducts> products = new List<dtoGetAllProducts>();

            using SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            using SqlCommand Command = new SqlCommand("SP_GetAllProducts", Connection);

            try
            {
                Connection.Open();
                using SqlDataReader reader = Command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new dtoGetAllProducts()
                    {
                        Id = (int)reader["id"],
                        CategoryId = (int)reader["categoryId"],
                        Title = (string)reader["title"],
                        Description = (string)reader["description"],
                        Price = (decimal)reader["price"],
                        Rating = (int)reader["rating"],
                        Discount = (decimal)reader["discount"],
                        About = (string)reader["about"],
                        Images = new List<string>()
                    });
                }

                List<clsProductImages> Images = clsProductImagesData.GetAllProductImages();

                products.ForEach(p =>
                {
                    p.Images.AddRange(Images
                        .Where(I => I.productId == p.Id)
                        .Select(I => I.image) 
                        .ToList());
                });
            }
            catch (Exception ex)
            {
                // Loglama veya hata yönetimi
                Console.WriteLine($"Hata: {ex.Message}");
            }
            return products;
        }
        public static List<dtoGetAllProducts> GetProductsPage(int PageNumber, int PageSize)
        {
            List<dtoGetAllProducts> products = new List<dtoGetAllProducts>();

            using SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            using SqlCommand Command = new SqlCommand("SP_GetProductsPage", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.AddWithValue("@PageNumber", PageNumber);
            Command.Parameters.AddWithValue("@PageSize", PageSize);

            try
            {
                Connection.Open();
                using SqlDataReader reader = Command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new dtoGetAllProducts()
                    {
                        Id = (int)reader["id"],
                        CategoryId = (int)reader["categoryId"],
                        Title = (string)reader["title"],
                        Description = (string)reader["description"],
                        Price = (decimal)reader["price"],
                        Rating = (int)reader["rating"],
                        Discount = (decimal)reader["discount"],
                        About = (string)reader["about"],
                        Images = new List<string>()
                    });
                }

                List<clsProductImages> Images = clsProductImagesData.GetAllProductImages();

                products.ForEach(p =>
                {
                    p.Images.AddRange(Images
                        .Where(I => I.productId == p.Id)
                        .Select(I => I.image) 
                        .ToList());
                });
            }
            catch (Exception ex)
            {
                // Loglama veya hata yönetimi
                Console.WriteLine($"Hata: {ex.Message}");
            }
            return products;
        }

        public static List<dtoGetAllProducts> GetLatest_N_Sales(int count)
        {
            List<dtoGetAllProducts> products = new List<dtoGetAllProducts>();

            using SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            using SqlCommand Command = new SqlCommand($"SELECT * FROM dbo.FN_GetLatestSales(@TopN)", Connection);
            Command.CommandType = CommandType.Text;  // Text olarak değiştirdik
            Command.Parameters.AddWithValue("@TopN", count);

            try
            {
                Connection.Open();
                using SqlDataReader reader = Command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new dtoGetAllProducts()
                    {
                        Id = (int)reader["id"],
                        CategoryId = (int)reader["categoryId"],
                        Title = (string)reader["title"],
                        Description = (string)reader["description"],
                        Price = (decimal)reader["price"],
                        Rating = (int)reader["rating"],
                        Discount = (decimal)reader["discount"],
                        About = (string)reader["about"],
                        Images = new List<string>()
                    });
                }

                // Resimleri ekleme işlemi
                List<clsProductImages> Images = clsProductImagesData.GetAllProductImages();

                products.ForEach(p =>
                {
                    p.Images.AddRange(Images
                        .Where(I => I.productId == p.Id)
                        .Select(I => I.image)
                        .ToList());
                });
            }
            catch (Exception ex)
            {
                // Loglama veya hata yönetimi
                Console.WriteLine($"Hata: {ex.Message}");
            }
            return products;
        }
         public static List<dtoGetAllProducts> LatestProducts(int count)
        {
            List<dtoGetAllProducts> products = new List<dtoGetAllProducts>();

            using SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            using SqlCommand Command = new SqlCommand($"SP_GetLatestProducts", Connection);
            Command.CommandType = CommandType.StoredProcedure; 

            try
            {
                Connection.Open();
                using SqlDataReader reader = Command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new dtoGetAllProducts()
                    {
                        Id = (int)reader["id"],
                        CategoryId = (int)reader["categoryId"],
                        Title = (string)reader["title"],
                        Description = (string)reader["description"],
                        Price = (decimal)reader["price"],
                        Rating = (int)reader["rating"],
                        Discount = (decimal)reader["discount"],
                        About = (string)reader["about"],
                        Images = new List<string>()
                    });
                }

                // Resimleri ekleme işlemi
                List<clsProductImages> Images = clsProductImagesData.GetAllProductImages();

                products.ForEach(p =>
                {
                    p.Images.AddRange(Images
                        .Where(I => I.productId == p.Id)
                        .Select(I => I.image)
                        .ToList());
                });
            }
            catch (Exception ex)
            {
                // Loglama veya hata yönetimi
                Console.WriteLine($"Hata: {ex.Message}");
            }
            return products;
        }
        
        public static List<dtoGetAllProducts> Get_N_TopRated(int count)
        {
            List<dtoGetAllProducts> products = new List<dtoGetAllProducts>();

            using SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            using SqlCommand Command = new SqlCommand($"SELECT * FROM dbo.FN_GetTopRated(@TopN)", Connection);
            Command.CommandType = CommandType.Text;  // Text olarak değiştirdik
            Command.Parameters.AddWithValue("@TopN", count);

            try
            {
                Connection.Open();
                using SqlDataReader reader = Command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new dtoGetAllProducts()
                    {
                        Id = (int)reader["id"],
                        CategoryId = (int)reader["categoryId"],
                        Title = (string)reader["title"],
                        Description = (string)reader["description"],
                        Price = (decimal)reader["price"],
                        Rating = (int)reader["rating"],
                        Discount = (decimal)reader["discount"],
                        About = (string)reader["about"],
                        Images = new List<string>()
                    });
                }

                // Resimleri ekleme işlemi
                List<clsProductImages> Images = clsProductImagesData.GetAllProductImages();

                products.ForEach(p =>
                {
                    p.Images.AddRange(Images
                        .Where(I => I.productId == p.Id)
                        .Select(I => I.image)
                        .ToList());
                });
            }
            catch (Exception ex)
            {
                // Loglama veya hata yönetimi
                Console.WriteLine($"Hata: {ex.Message}");
            }
            return products;
        }
    }
}
