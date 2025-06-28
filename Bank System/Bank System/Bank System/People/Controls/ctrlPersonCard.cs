using Bank_System.Properties;
using Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank_System.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        private int _PersonID = -1;
        private string _IDNumber ="";
        private clsPerson _Person = new clsPerson();
        private enum enGender { Male = 0,Female = 1 }

        public int PersonID
        {
            get { return _PersonID; }
        }

        
        public clsPerson PersonInfo
        {
            get
            {
                return _Person;
            }
        }
        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private bool IsEditPersonEnable;
        public bool EnableEditPerson
        {
            get { return IsEditPersonEnable; }

            set { IsEditPersonEnable = value;
                llEditPersonInfo.Enabled = IsEditPersonEnable;
            }
        }

        private void _LoadData()
        {
            llEditPersonInfo.Enabled = true;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblFirstName.Text = _Person.FirstName.ToString();
            lblLastName.Text = _Person.LastName.ToString();
            lblIDNumber.Text = _Person.IDNumber.ToString();

            if (_Person.Gender == (byte)enGender.Male)
            {
                lblGender.Text = "Male";
                pbGender.Image = Resources.Man_32;
                pbImage.Image = Resources.Male_512;
            }
            else
            {
                lblGender.Text = "Female";
                pbGender.Image = Resources.Woman_32;
                pbImage.Image = Resources.Female_512;

            }

            lblAddress.Text = _Person.Address.ToString();
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd.MM.yyyy");
            lblPhone.Text = _Person.Phone.ToString();

            if (_Person.Email != "")
                lblEmail.Text = _Person.Email;
            else
                lblEmail.Text = "";
            lblCountry.Text = _Person.CountryInfo.CountryName;

            if(_Person.ImagePath != "")
                pbImage.ImageLocation = _Person.ImagePath;

        }
        private void _ResetDefaultValues()
        {
            llEditPersonInfo.Enabled = false;
            lblPersonID.Text = "???";
            lblFirstName.Text = "???";
            lblLastName.Text = "???";
            lblIDNumber.Text = "???";
            lblGender.Text = "???";
            lblAddress.Text = "???";
            lblDateOfBirth.Text = "???";
            lblPhone.Text = "???";
            lblEmail.Text = "???";
            lblCountry.Text = "???";
            pbImage.ImageLocation = null;
        }

        public void ResetDefaultValues()
        {
            _ResetDefaultValues();
        }
        public void LoadPersonData(int PersonID)
        {
            _Person = clsPerson.FindByPersonID(PersonID);
            if(_Person == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("No Person Found With ID = " + PersonID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _PersonID = PersonID;
            _IDNumber = _Person.IDNumber;
            _LoadData();
        }

        public void LoadPersonData(string IDNumber)
        {
            _Person = clsPerson.FindByIDNumber(IDNumber);
            if (_Person == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("No Person Found With ID Number = " + IDNumber, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _IDNumber = IDNumber;
            _PersonID = _Person.PersonID;
            _LoadData();
        }
        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_Person.PersonID);
            frm.ShowDialog();
            this.LoadPersonData(_Person.PersonID);
        }
    }
}
