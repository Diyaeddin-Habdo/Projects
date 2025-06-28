using DataAccess_layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_layer
{
    public class clsProduct
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public clsCategory CategoryInfo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public decimal Price { get; set; }

        public decimal Discount { get; set; }
        public string About { get; set; }

        public List<string> Images { get; set; } = new List<string>();
       
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        public clsProduct()
        {

            Id = -1;
            CategoryId = -1;
            CategoryInfo = new clsCategory();
            Title = "";
            Description = "";
            Rating = 0;
            Price = 0;
            Discount = 0;
            About = "";
            Images = new List<string>();
            _Mode = enMode.AddNew;
        }

        private clsProduct(int id, int categoryId, string title, string description, int rating, decimal price, decimal discount, string about, List<string> images)
        {
            Id = id;
            CategoryId = categoryId;
            CategoryInfo = clsCategory.Find(this.CategoryId);
            Title = title;
            Description = description;
            Rating = rating;
            Price = price;
            Discount = discount;
            About = about;
            Images.AddRange(images);
            _Mode = enMode.Update;
        }

        private bool _Add()
        {
            this.Id = clsProductData.Add(CategoryId,Title,Description,Rating,Price,Discount,About,Images);
            return (this.Id != -1);
        }

        private bool _Update()
        {
            return clsProductData.Update(Id, CategoryId, Title, Description, Rating, Price, Discount, About, Images);
        }

        public static clsProduct Find(int Id)
        {
            int CategoryId = 0;
            string Title = "";
            string Description = "";
            int Rating = 0;
            decimal Price = 0m;
            decimal Discount = 0m;
            string About = "";     
            List<string> Images = new List<string>();


            bool IsFound = clsProductData.Find(Id,ref CategoryId, ref Title, ref Description, ref Rating, ref Price, ref Discount, ref About, ref Images);

            if (IsFound)
            {
                return new clsProduct(Id, CategoryId, Title, Description, Rating, Price, Discount, About, Images);
            }
            else
            {
                return null;
            }
        }



        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_Add())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _Update();

            }

            return false;
        }

        public static bool Delete(int Id)
        {
            return clsProductData.Delete(Id);
        }

        public static bool IsExists(int Id)
        {
            return clsProductData.IsExists(Id);
        }

        public static List<DataAccess_layer.Models.dtoGetAllProducts> GetAll()
        {
            return clsProductData.GetAll();
        }
        public static List<DataAccess_layer.Models.dtoGetAllProducts> GetProductsPage(int PageNumber = 1,int PageSize = 10)
        {
            return clsProductData.GetProductsPage(PageNumber,PageSize);
        }


        public static List<DataAccess_layer.Models.dtoGetAllProducts> GetLatest_N_Sales(int N)
        {
            return clsProductData.GetLatest_N_Sales(N);
        }
         public static List<DataAccess_layer.Models.dtoGetAllProducts> LatestProducts(int N)
        {
            return clsProductData.LatestProducts(N);
        }

          public static List<DataAccess_layer.Models.dtoGetAllProducts> Get_N_TopRated(int N)
        {
            return clsProductData.Get_N_TopRated(N);
        }



    }
}
