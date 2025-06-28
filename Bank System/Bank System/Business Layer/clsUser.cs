using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsUser
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }

        public clsPerson PersonInfo;

        public string UserName { get; set; }
        public string Password { get; set; }

        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            _Mode = enMode.AddNew;
        }

        private clsUser(int UserID, int PersonID, string UserName, string Password)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.FindByPersonID(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this._Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(PersonID, UserName, Password);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(UserID, PersonID, UserName, Password);
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";

            bool IsFound = clsUserData.FindByPersonID(PersonID,ref UserID,ref UserName,ref Password);

            if (IsFound)
            {
                return new clsUser(UserID,PersonID,UserName,Password);
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindByUserID(int UserID)
        {

            int PersonID = -1;
            string UserName = "", Password = "";

            bool IsFound = clsUserData.FindByUserID(UserID,ref PersonID,ref UserName,ref Password);

            if (IsFound)
            {
                return new clsUser(UserID,PersonID,UserName,Password);
            }
            else
                return null;
        }

        public static clsUser FindByUsernameAndPassword(string UserName, string Password)
        {
            int UserID = -1;
            int PersonID = -1;

            bool IsFound = clsUserData.GetUserInfoByUserNameAndPassword(UserName, Password, ref PersonID, ref UserID);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsUser(UserID, PersonID, UserName, Password);
            else
                return null;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateUser();

            }

            return false;
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool IsUserExists(int UserID)
        {
            return clsUserData.IsUserExists(UserID);
        }

        public static bool IsUserExistsForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistsForPersonID(PersonID);
        }
        public static bool IsUserExists(string UserName)
        {
            return clsUserData.IsUserExists(UserName);
        }
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

    }
}
