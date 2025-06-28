using Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank_System.Clients
{
    public partial class frmAddUpdateClient : Form
    {
        public enum enMode { AddNew = 0, Edit = 1 }
        public enMode _Mode = enMode.AddNew;
        private int _ClientID = -1;
        private clsClient _Client;
        public frmAddUpdateClient()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _Client = new clsClient();
        }
        public frmAddUpdateClient(int ClientID)
        {
            InitializeComponent();
            _ClientID = ClientID;
            _Mode = enMode.Edit;
        }

        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                this.Text = "Add New Client";
                lblTitle.Text = this.Text;
                tpAccountInfo.Enabled = false;
                btnSave.Enabled = false;
                tabControl1.SelectedIndex = 0;
            }
            else
            {
                this.Text = "Update Client Info";
                lblTitle.Text = this.Text;
                tpAccountInfo.Enabled = true;
                btnSave.Enabled = true;
                tabControl1.SelectedIndex = 0;
            }
            lblClientID.Text = "????";
            lblAccountBalance.Text = "0000";
            txtAccountNumber.Text = "";
            txtPinCode.Text = "";

        }
        private void _LoadData()
        {
            _Client = clsClient.FindByClientID(_ClientID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            if (_Client == null)
            {
                MessageBox.Show("Client Not Found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            lblClientID.Text = _Client.ClientID.ToString();
            lblAccountBalance.Text = _Client.AccountBalance.ToString();
            txtPinCode.Text = _Client.PinCode.ToString();
            txtAccountNumber.Text = _Client.AccountNumber.ToString();

            ctrlPersonCardWithFilter1.LoadPersonInfo(_Client.PersonID);

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateClient_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Edit)
                _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

           
            _Client.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _Client.AccountBalance = Convert.ToDecimal(lblAccountBalance.Text);
            _Client.PinCode = txtPinCode.Text.Trim();
            _Client.AccountNumber = txtAccountNumber.Text.Trim();

            if (_Client.Save())
            {
                lblClientID.Text = _Client.ClientID.ToString();
                //change form mode to update.
                _Mode = enMode.Edit;
                lblTitle.Text = "Update Client Info";
                this.Text = "Update Client Info";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(_Client.ClientID.ToString());
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAccountNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccountNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAccountNumber, "Account Number cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtAccountNumber, null);
            };


            if (_Mode == enMode.AddNew)
            {

                if (clsClient.IsClientExists(txtAccountNumber.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtAccountNumber, "Account Number is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtAccountNumber, null);
                };
            }
            else
            {
                //incase update make sure not to use anothers user name
                if (_Client.AccountNumber != txtAccountNumber.Text.Trim())
                {
                    if (clsClient.IsClientExists(txtAccountNumber.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtAccountNumber, "Account Number is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtAccountNumber, null);
                    };
                }
            }
        }

        private void txtPinCode_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPinCode.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPinCode, "Pin Code cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtPinCode, null);
            };
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (_Mode == enMode.Edit)
            {
                btnSave.Enabled = true;
                tpAccountInfo.Enabled = true;
                tabControl1.SelectedTab = tabControl1.TabPages["tpAccountInfo"];
                return;
            }

            //incase of add new mode.
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {

                if (clsClient.IsClientExistsForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {

                    MessageBox.Show("Selected Person already has a Client, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tpAccountInfo.Enabled = true;
                    tabControl1.SelectedTab = tabControl1.TabPages["tpAccountInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }
    }
}
