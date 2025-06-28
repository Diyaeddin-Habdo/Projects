using Bank_System.Clients;
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

namespace Bank_System.Transactions
{
    public partial class frmDeposite : Form
    {
        public frmDeposite()
        {
            InitializeComponent();
        }

        private void txtAccNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccNumber.Text.Trim()))
            {
                e.Cancel = true;
                btnDetails.Enabled = false;
                errorProvider1.SetError(txtAccNumber, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtAccNumber, null);
            };

            if (!clsClient.IsClientExists(txtAccNumber.Text.Trim()))
            {
                e.Cancel = true;
                btnDetails.Enabled = false;
                errorProvider1.SetError(txtAccNumber, "This Account Number is not exist!");
            }
            else
            {
                btnDetails.Enabled = true;
                errorProvider1.SetError(txtAccNumber, null);
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            frmClientInfo frm = new frmClientInfo(clsClient.FindForAccountNumber(txtAccNumber.Text.Trim()).ClientID);
            frm.ShowDialog();
        }

        private void btnDeposite_Click(object sender, EventArgs e)
        {
            if (clsClient.Deposit(txtAccNumber.Text.Trim(), Convert.ToDecimal(txtDepositeAmount.Text.Trim())))
            {
                MessageBox.Show("Deposit Operation Done Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Deposit Operation Is not Done Successfully!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }

        private void txtDepositeAmount_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDepositeAmount.Text.Trim()))
            {
                e.Cancel = true;
                btnDeposite.Enabled = false;
                errorProvider1.SetError(txtDepositeAmount, "This field is required!");
                return;
            }
            else
            {
                btnDeposite.Enabled = true;
                errorProvider1.SetError(txtDepositeAmount, null);
            };
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDepositeAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Eğer basılan tuş sayı, virgül veya kontrol tuşlarından biri değilse, olayı iptal et
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            // Virgül zaten varsa ve tekrar virgül girişi yapılmışsa olayı iptal et
            if (e.KeyChar == ',' && (sender as TextBox).Text.Contains(","))
            {
                e.Handled = true;
            }
            // Eğer TextBox boşsa ve basılan tuş virgül değilse, ilk karakter virgül olmasın
            if (string.IsNullOrEmpty((sender as TextBox).Text) && e.KeyChar == ',')
            {
                e.Handled = true;
            }
        }
    }
}
