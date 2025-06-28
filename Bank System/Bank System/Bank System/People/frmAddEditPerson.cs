using Bank_System.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Layer;
using Bank_System.Properties;

namespace Bank_System
{
    public partial class frmAddEditPerson : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // declare an event using the delegate
        public event DataBackEventHandler DataBack;
        public enum enMode { AddNew = 1, Edit = 2 }
        public enum enGender { Male = 0, Female = 1 }
        private enMode _Mode = enMode.AddNew;
        private int _PerosnID = -1;
        private clsPerson _Person;
        public frmAddEditPerson()
        {
            InitializeComponent();
            _PerosnID = -1;
            _Mode = enMode.AddNew;
        }
        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();
            _PerosnID = PersonID;
            _Mode = enMode.Edit;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
                return;

            if(!clsValidations.ValidateEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Email Format Is Wrong!");
            }
            else
                errorProvider1.SetError(txtEmail, null);
        }
        private void _FillCountriesInComboBox()
        {
            DataTable dt = clsCountry.GetAllCountries();

            foreach (DataRow row in dt.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }
        private void _ResetDefaultValues()
        {
            if(_Mode == enMode.AddNew)
            {
                this.Text = "Add New Person";
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                this.Text = "Update Person Info";
                lblTitle.Text = "Update Person Info";
            }

            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtIDNumber.Text = "";
            rbMale.Checked = true;
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            if(rbMale.Checked)
                pbImage.Image = Resources.Male_512;
            else
                pbImage.Image = Resources.Female_512;

            llRemoveImage.Visible = (pbImage.ImageLocation != null);

            _FillCountriesInComboBox();
            cbCountry.SelectedIndex = cbCountry.FindString("Turkey");
        }
        private void _LoadData()
        {
            _Person = clsPerson.FindByPersonID(_PerosnID);

            if( _Person == null )
            {
                MessageBox.Show("No Person with ID = " + _PerosnID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtLastName.Text = _Person.LastName;
            txtIDNumber.Text = _Person.IDNumber;

            if (_Person.Gender == (byte)enGender.Male)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            txtAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            txtPhone.Text = _Person.Phone;
            if(_Person.Email != "")
                txtEmail.Text = _Person.Email;
            else
                txtEmail.Text = "";

            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);
            
            if(_Person.ImagePath != "")
                pbImage.ImageLocation = _Person.ImagePath;

            llRemoveImage.Visible = (_Person.ImagePath != "");
        }
        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Edit)
                _LoadData();
        }
        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.ImageLocation = null;



            if (rbMale.Checked)
                pbImage.Image = Resources.Male_512;
            else
                pbImage.Image = Resources.Female_512;

            llRemoveImage.Visible = false;
        }
        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbImage.ImageLocation = selectedFilePath;
                llRemoveImage.Visible = true;
                // ...
            }
        }
        private void rbMale_Click(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation == null)
                pbImage.Image = Resources.Male_512;
        }
        private void rbFemale_Click(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation == null)
                pbImage.Image = Resources.Female_512;
        }
        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This Feild Is Required!");
            }
            else
                errorProvider1.SetError(Temp, null);
        }
        private void txtIDNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtIDNumber, "This Feild Is Required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtIDNumber, null);
            };

            if(txtIDNumber.Text.Trim() != _Person.IDNumber && clsPerson.IsPersonExists(txtIDNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtIDNumber, "This ID Number Is Invalid!");
            }
            else
            {
                errorProvider1.SetError(txtIDNumber, null);
            };
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("Please Fill All Required Feilds.", "Required Feilds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }


            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.IDNumber = txtIDNumber.Text.Trim();

            if(rbMale.Checked)
                _Person.Gender = (byte)enGender.Male;
            else
                _Person.Gender = (byte)enGender.Female;

            _Person.Address = txtAddress.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Phone = txtPhone.Text.Trim();

            if (txtEmail.Text.Trim() != "")
                _Person.Email = txtEmail.Text.Trim();
            else
                _Person.Email = "";

            _Person.NationalityCountryID = cbCountry.SelectedIndex + 1;

            if (pbImage.ImageLocation != null)
                _Person.ImagePath = pbImage.ImageLocation;
            else
                _Person.ImagePath = "";


            if(_Person.Save())
            {
                this.Text = "Update Person Info";
                lblTitle.Text = "Update Person Info";
                _Mode = enMode.Edit;
                DataBack?.Invoke(this, _Person.PersonID);
                MessageBox.Show("Data Saved Successfully.","Done",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data Is Not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            lblPersonID.Text = _Person.PersonID.ToString();
        }

        private void txtIDNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
