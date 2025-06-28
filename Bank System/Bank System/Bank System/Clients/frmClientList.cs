using Bank_System.Users;
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
    public partial class frmClientList : Form
    {
        private static DataTable _dtAllClients;
        public frmClientList()
        {
            InitializeComponent();
        }
       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");


            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Enabled = false;
            }
            else
                txtFilterValue.Enabled = true;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Client ID":
                    FilterColumn = "ClientID";
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

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllClients.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvClients.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "GenderCaption" && FilterColumn != "IDNumber")
                //in this case we deal with numbers not string.
                _dtAllClients.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllClients.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtAllClients.Rows.Count.ToString();
        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            frmAddUpdateClient frm = new frmAddUpdateClient();
            frm.ShowDialog();
            frmClientList_Load(null, null);
        }

        private void frmClientList_Load(object sender, EventArgs e)
        {
            _dtAllClients = clsClient.GetAllClients();
            dgvClients.DataSource = _dtAllClients;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvClients.Rows.Count.ToString();

            if (dgvClients.Rows.Count > 0)
            {
                dgvClients.Columns[0].HeaderText = "Client ID";
                dgvClients.Columns[0].Width = 110;

                dgvClients.Columns[1].HeaderText = "Person ID";
                dgvClients.Columns[1].Width = 120;
                
                dgvClients.Columns[2].HeaderText = "ID Number";
                dgvClients.Columns[2].Width = 120;

                dgvClients.Columns[3].HeaderText = "Full Name";
                dgvClients.Columns[3].Width = 250;

                dgvClients.Columns[4].HeaderText = "Gender";
                dgvClients.Columns[4].Width = 120;

                dgvClients.Columns[5].HeaderText = "Account Balance";
                dgvClients.Columns[5].Width = 120;

            }
        }

        private void dgvClients_DoubleClick(object sender, EventArgs e)
        {
            frmClientInfo Frm1 = new frmClientInfo((int)dgvClients.CurrentRow.Cells[0].Value);
            Frm1.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ClientID = (int)dgvClients.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure you want to delete this Client ?", "Delete Client", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (clsClient.DeleteClient(ClientID))
                {
                    MessageBox.Show("Client has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmClientList_Load(null, null);
                }

                else
                    MessageBox.Show("Client is not deleted due to data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateClient Frm1 = new frmAddUpdateClient((int)dgvClients.CurrentRow.Cells[0].Value);
            Frm1.ShowDialog();
            frmClientList_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientInfo Frm1 = new frmClientInfo((int)dgvClients.CurrentRow.Cells[0].Value);
            Frm1.ShowDialog();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateClient Frm1 = new frmAddUpdateClient();
            Frm1.ShowDialog();
            frmClientList_Load(null, null);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Client ID" || cbFilterBy.Text == "ID Number")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
