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

namespace Bank_System.People
{
    public partial class frmListPeople : Form
    {
        public frmListPeople()
        {
            InitializeComponent();
        }

        private static DataTable dtAllPeople = clsPerson.GetAllPeople();
        private DataTable dtPeople = dtAllPeople.DefaultView.ToTable(false,"PersonID","IDNumber","FullName","DateOfBirth","GenderCaption", "Phone","CountryName");

        private void _RefreshPeopleList()
        {
            dtAllPeople = clsPerson.GetAllPeople();
            dtPeople = dtAllPeople.DefaultView.ToTable(false, "PersonID", "IDNumber", "FullName", "DateOfBirth", "GenderCaption", "Phone", "CountryName");

            dgvListPeople.DataSource = dtPeople;
            lblRecordCount.Text = dgvListPeople.RowCount.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dgvListPeople.DataSource = dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRecordCount.Text = dgvListPeople.Rows.Count.ToString();

            if(dgvListPeople.Rows.Count > 0)
            {
                dgvListPeople.Columns[0].HeaderText = "Person ID";
                dgvListPeople.Columns[0].Width = 60;

                dgvListPeople.Columns[1].HeaderText = "ID Number";
                dgvListPeople.Columns[1].Width = 120;

                dgvListPeople.Columns[2].HeaderText = "Full Name";
                dgvListPeople.Columns[2].Width = 200;

                dgvListPeople.Columns[3].HeaderText = "Date Of Birth";
                dgvListPeople.Columns[3].Width = 100;

                dgvListPeople.Columns[4].HeaderText = "Gender Caption";
                dgvListPeople.Columns[4].Width = 120;

                dgvListPeople.Columns[5].HeaderText = "Phone";
                dgvListPeople.Columns[5].Width = 100;

                dgvListPeople.Columns[6].HeaderText = "Country";
                dgvListPeople.Columns[6].Width = 100;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.SelectedIndex == 0)
            {
                txtFilterValue.Visible = false;
            }
            else
            {
                txtFilterValue.Visible= true;
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.SelectedItem)
            {
                case "None":
                    FilterColumn = "None";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "ID Number":
                    FilterColumn = "IDNumber";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;

            }
           
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                dtPeople.DefaultView.RowFilter = "";
                lblRecordCount.Text = dgvListPeople.Rows.Count.ToString();
                return;
            }

            if(FilterColumn == "PersonID")
                dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordCount.Text = dgvListPeople.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "ID Number" || cbFilterBy.Text == "Phone")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson((int)dgvListPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvListPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void dgvListPeople_DoubleClick(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvListPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this Person ?","Delete Person"
                ,MessageBoxButtons.OKCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if(clsPerson.DeletePerson((int)dgvListPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.","Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            _RefreshPeopleList();
        }
    }
}
