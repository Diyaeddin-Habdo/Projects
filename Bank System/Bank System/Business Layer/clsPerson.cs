using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer;

namespace Business_Layer
{
    public class clsPerson
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IDNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte Gender { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }

        public int NationalityCountryID { get; set; }

        public clsCountry CountryInfo;
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        public clsPerson()
        {
            PersonID = -1;
            FirstName = "";
            LastName = "";
            IDNumber = "";
            DateOfBirth = DateTime.Now;
            Email = "";
            Phone = "";
            Gender = 0; // Male = 0, Female = 1
            Address = "";
            ImagePath = "";
            NationalityCountryID = -1;
            _Mode = enMode.AddNew;
        }

        private clsPerson(int PersonID, string FirstName, string LastName, string IDNumber,
            DateTime DateOfBirth, string Email, string Phone, byte Gender, string Address,
            string ImagePath, int NationalityCountryID)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.IDNumber = IDNumber;
            this.DateOfBirth = DateOfBirth;
            this.Email = Email;
            this.Phone = Phone;
            this.Gender = Gender;
            this.Address = Address;
            this.ImagePath = ImagePath;
            this.NationalityCountryID = NationalityCountryID;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);
            this._Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(IDNumber, FirstName, LastName,
             DateOfBirth, Gender, Address, Phone, NationalityCountryID,
              Email, ImagePath);
            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(PersonID,IDNumber, FirstName, LastName,
             DateOfBirth, Gender, Address, Phone, NationalityCountryID,
              Email, ImagePath);
        }

        public static clsPerson FindByPersonID(int PersonID)
        {
            string FirstName = "", LastName = "", IdNumber = "", Email = "", Phone = "", Address = "", ImagePath = "";
            int NationalityCountryID = -1;
            byte Gender = 0;
            DateTime DateOfBirth = DateTime.Now;

            bool IsFound = clsPersonData.FindByPersonID(PersonID, ref IdNumber, ref FirstName, ref LastName,
            ref DateOfBirth, ref Gender, ref Address, ref Phone, ref NationalityCountryID,
            ref Email, ref ImagePath);

            if (IsFound)
            {
                return new clsPerson(PersonID, FirstName, LastName, IdNumber,
                                      DateOfBirth, Email, Phone, Gender, Address,
                                      ImagePath, NationalityCountryID
                                      );
            }
            else
            {
                return null;
            }
        }

        public static clsPerson FindByIDNumber(string IdNumber)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            int NationalityCountryID = -1;
            byte Gender = 0;
            DateTime DateOfBirth = DateTime.Now;
            int PersonID = -1;

            bool IsFound = clsPersonData.FindByIDNumber(IdNumber, ref PersonID, ref FirstName, ref LastName,
            ref DateOfBirth, ref Gender, ref Address, ref Phone, ref NationalityCountryID,
            ref Email, ref ImagePath);

            if (IsFound)
            {
                return new clsPerson(PersonID, FirstName, LastName, IdNumber,
              DateOfBirth, Email, Phone, Gender, Address,
              ImagePath, NationalityCountryID);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdatePerson();

            }

            return false;
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static bool IsPersonExists(int PersonID)
        {
            return clsPersonData.IsPersonExists(PersonID);
        }

        public static bool IsPersonExists(string IDNumber)
        {
            return clsPersonData.IsPersonExists(IDNumber);
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        
    }
}
