using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsTransfer
    {
        public enum enMode { AddNew = 0, Update =  1 }
        private enMode _Mode = enMode.AddNew;
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string FromAccNumber { get; set; }
        public clsClient FromClientInfo;
        public string ToAccNumber { get; set; }
        public clsClient ToClientInfo;
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;

        public clsTransfer()
        {
            ID = -1;
            Date = DateTime.Now;
            Amount = 0;
            FromAccNumber = "";
            ToAccNumber = "";
            CreatedByUserID = -1;
            _Mode = enMode.AddNew;
        }
        public static bool AddNewTransferRecord(DateTime Date,decimal Amount,int FromAccID,int ToAccID,int CreatedByUserID)
        {
             int ID = clsTransferData.AddNewTransferRecord(Date, Amount, FromAccID,
             ToAccID, CreatedByUserID);
            return (ID != -1);
        }
        public static DataTable GetAllTransferRecords()
        {
            return clsTransferData.GetAllTransferRecords();
        }

    }
}
