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

namespace Bank_System.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        public clsPerson PersonInfo
        {
            get
            {
                return ctrlPersonCard1.PersonInfo;
            }
        }
        public int PersonID
        {
            get
            {
                return ctrlPersonCard1.PersonID;
            }
        }
        public void LoadPersonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 1; // Person Id
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonData(Convert.ToInt32(txtFilterValue.Text.Trim()));
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "ID Number")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This Feild Is Required!");
            }
            else
                errorProvider1.SetError(txtFilterValue, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("Please Enter Required Info To Search.", "Missing information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbFilterBy.SelectedIndex == 1)
            {
                ctrlPersonCard1.LoadPersonData(Convert.ToInt32(txtFilterValue.Text.Trim()));
            }
            else
                ctrlPersonCard1.LoadPersonData(txtFilterValue.Text.Trim());

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.DataBack += frm2DataBack;
            frm.ShowDialog();
        }

        private void frm2DataBack(object sender,int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            btnFind.PerformClick();
        }

        public void FilterFocus()
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
            cbFilterBy.SelectedIndex = 1;
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            FilterFocus();
        }
    }
}
