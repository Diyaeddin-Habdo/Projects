using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsLoginLog
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int UserID { get; set; }
        public clsUser CreatedByUserInfo;

        public clsLoginLog()
        {
            ID = -1;
            Date = DateTime.Now;
            UserID = -1;
            _Mode = enMode.AddNew;
        }
        public static bool AddNewLoginRecord(DateTime Date, int UserID)
        {
            int ID = clsLoginData.AddNewLoginRecord(Date,UserID);
            return (ID != -1);
        }
        public static DataTable GetAllLoginRecords()
        {
            return clsLoginData.GetAllLoginRecords();
        }
    }
}
