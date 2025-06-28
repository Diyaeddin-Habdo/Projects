using DataAccess_layer.Models;
using DataAccess_layer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Business_layer
{
    public class clsCategory
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public clsCategory()
        {
            this.id = -1;
            this.name = string.Empty;
            this.image = string.Empty;

            this.Mode = enMode.AddNew;
        }
        private clsCategory(int id, string name, string image)
        {
            this.id = id;
            this.name = name;
            this.image= image;

            this.Mode = enMode.Update;
        }
        private bool _AddNewCategory()
        {
            this.id = CategoryData.AddCategory(name, image);
            return (this.id != -1);
        }

        private bool _UpdateCategory()
        {
            return CategoryData.UpdateCategory(id,name, image);
        }

        public static List<DataAccess_layer.Models.dtoCategory> GetAllCategories()
        {
            return CategoryData.GetAllCategories();
        }
        public static List<DataAccess_layer.Models.dtoCategory> GetCategoriesPage(int PagesNumber=1,int PageSize=10)
        {
            return CategoryData.GetCategoriesPage(PagesNumber,PageSize);
        }

        public static clsCategory Find(int ID)
        {
            string name = "", image = "";

            bool isFound = CategoryData.Find(ID, ref name, ref image);

            if (isFound)
            {
                return new clsCategory(ID,name, image);
            }
            else
                return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCategory())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateCategory();

            }

            return false;
        }

        public static bool DeleteCategory(int ID)
        {
            return CategoryData.Delete(ID);
        }

        public static bool IsExists(int ID)
        {
            return CategoryData.IsExists(ID);
        }
    }
}
