using DataAccess_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_layer
{
    public class clsProductImages
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Image { get; set; }
        

        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        public clsProductImages()
        {
            Id = -1;
            Image = string.Empty;
            ProductId = -1;
            _Mode = enMode.AddNew;
        }

        private clsProductImages(int Id, string ImageUrl, int ProductId)
        {
            this.Id = Id;
            this.Image = ImageUrl;
            this.ProductId = ProductId;
            _Mode = enMode.Update;
        }

        private bool _Add()
        {
            this.Id = clsProductImagesData.InsertImage(ProductId, Image);
            return (this.Id != -1);
        }

        private bool _Update()
        {
            return clsProductImagesData.UpdateImage(Id, Image, ProductId);
        }

        //public static clsProductImages Find(int Id)
        //{

        //    string ImageUrl = string.Empty;
        //    int ProductId = -1;


        //    bool IsFound = clsProductImagesData.Find(Id, ref ImageUrl, ref ProductId);

        //    if (IsFound)
        //    {
        //        return new clsProductImages(Id, ImageUrl, ProductId);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
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
        public static bool DeleteAllProductImages(int ProductId)
        {
            return clsProductImagesData.DeleteAllProductImages(ProductId);
        }


        /// <summary>
        /// Gets all images of a specific product by his id from database.
        /// </summary>
        /// <param name="Product Id"></param>
        /// <returns></returns>
        public static List<string> GetAllProductImages(int ProductId)
        {
            return clsProductImagesData.GetAllProductImages(ProductId);
        }
    }
}
