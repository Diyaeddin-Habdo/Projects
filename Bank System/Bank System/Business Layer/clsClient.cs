using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsClient
    {
        public int ClientID { get; set; }
        public int PersonID { get; set; }
        clsPerson PersonInfo;

        public string AccountNumber { get; set; }
        public string PinCode { get; set; }
        public decimal AccountBalance { get; set; }
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        public clsClient()
        {
            ClientID = -1;
            PersonID = -1;
            AccountBalance = 0m;
            PinCode = "";
            AccountNumber = "";
            _Mode = enMode.AddNew;
        }

        private clsClient(int ClientID,int PersonID,string AccountNumber,string PinCode,decimal AccountBalance)
        {
            this.ClientID = ClientID;

            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.FindByPersonID(PersonID);

            this.AccountBalance = AccountBalance;

            this.PinCode = PinCode;

            this.AccountNumber = AccountNumber;

            this._Mode = enMode.Update;
        }
        
        private bool _AddNewClient()
        {
            this.ClientID = clsClientData.AddNewClient(PersonID, AccountNumber, PinCode,AccountBalance);
            return (this.ClientID != -1);
        }
        private bool _UpdateClient()
        {
            return clsClientData.UpdateClient(ClientID, PersonID, AccountNumber, PinCode, AccountBalance);
        }
        public static clsClient FindByPersonID(int PersonID)
        {
            int ClientID = -1;
            string AccountNumber = "", PinCode = "";
            decimal AccountBalance = 0;

            bool IsFound = clsClientData.FindByPersonID(PersonID, ref ClientID, ref AccountNumber, ref PinCode, ref AccountBalance);

            if (IsFound)
            {
                return new clsClient(ClientID,PersonID,AccountNumber,PinCode,AccountBalance);
            }
            else
            {
                return null;
            }
        }
        public static clsClient FindByClientID(int ClientID)
        {
            int PersonID = -1;
            string AccountNumber = "", PinCode = "";
            decimal AccountBalance = 0;

            bool IsFound = clsClientData.FindByClientID(ClientID, ref PersonID, ref AccountNumber, ref PinCode, ref AccountBalance);

            if (IsFound)
            {
                return new clsClient(ClientID, PersonID, AccountNumber, PinCode, AccountBalance);
            }
            else
            {
                return null;
            }
        }
       
        public static clsClient FindForAccountNumber(string AccountNumber)
        {
            int ClientID = -1;
            int PersonID = -1;
            string PinCode = "";
            decimal AccountBalance = 0;
            bool IsFound = clsClientData.FindByAccountNumber(AccountNumber,ref ClientID,ref PersonID,ref PinCode,ref AccountBalance);

            if(IsFound)
                return new clsClient(ClientID,PersonID,AccountNumber,PinCode,AccountBalance);
            else
                return null;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewClient())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateClient();

            }

            return false;
        }
        public static bool DeleteClient(int ClientID)
        {
            return clsClientData.DeleteClient(ClientID);
        }
        public static bool IsClientExists(int ClientID)
        {
            return clsClientData.IsClientExists(ClientID);
        }
        public static bool IsClientExistsForPersonID(int PersonID)
        {
            return clsClientData.IsClientExistsForPersonID(PersonID);
        }
        public static bool IsClientExists(string AccountNumber)
        {
            return clsClientData.IsClientExists(AccountNumber);
        }
        public static DataTable GetAllClients()
        {
            return clsClientData.GetAllClients();
        }
        public static bool Withdraw(string AccountNumber,decimal Amount)
        {
            if(FindForAccountNumber(AccountNumber).AccountBalance < Amount)
            {
                return false;
            }
            else
            {
                return clsClientData.Withdraw(AccountNumber, Amount);
            }
        }
        public static bool Deposit(string AccountNumber,decimal Amount)
        {
            return clsClientData.Deposit(AccountNumber,Amount);
        }
        public static bool Transfer(string FromAccountNumber,string ToAccountNumber,decimal Amount)
        {
            if (FindForAccountNumber(FromAccountNumber).AccountBalance < Amount)
            {
                return false;
            }
            else
            {
                return clsClientData.Transfer(FromAccountNumber,ToAccountNumber, Amount);
            }
        }
    }
}
