using Bank_System.Clients;
using Bank_System.Global_Classes;
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
    public partial class frmTransfer : Form
    {
        public frmTransfer()
        {
            InitializeComponent();
        }

        private void txtFromAccNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFromAccNumber.Text.Trim()))
            {
                e.Cancel = true;
                btnFromDetails.Enabled = false;
                errorProvider1.SetError(txtFromAccNumber, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtFromAccNumber, null);
            };

            if (!clsClient.IsClientExists(txtFromAccNumber.Text.Trim()))
            {
                e.Cancel = true;
                btnFromDetails.Enabled = false;
                errorProvider1.SetError(txtFromAccNumber, "This Account Number is not exist!");
            }
            else
            {
                btnFromDetails.Enabled = true;
                errorProvider1.SetError(txtFromAccNumber, null);
            }
        }

        private void ToAccNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtToAccNumber.Text.Trim()))
            {
                e.Cancel = true;
                btnToDetails.Enabled = false;
                errorProvider1.SetError(txtToAccNumber, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtToAccNumber, null);
            };

            if (!clsClient.IsClientExists(txtToAccNumber.Text.Trim()))
            {
                e.Cancel = true;
                btnToDetails.Enabled = false;
                errorProvider1.SetError(txtToAccNumber, "This Account Number is not exist!");
            }
            else
            {
                btnToDetails.Enabled = true;
                errorProvider1.SetError(txtToAccNumber, null);
            }
        }

        private void txtTransferAmount_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTransferAmount.Text.Trim()))
            {
                e.Cancel = true;
                btnTransfer.Enabled = false;
                errorProvider1.SetError(txtTransferAmount, "This field is required!");
                return;
            }
            else
            {
                btnTransfer.Enabled = true;
                errorProvider1.SetError(txtTransferAmount, null);
            };
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if(clsClient.Transfer(txtFromAccNumber.Text.Trim(),txtToAccNumber.Text.Trim(), Convert.ToDecimal(txtTransferAmount.Text.Trim())))
            {

                clsTransfer.AddNewTransferRecord(DateTime.Now, Convert.ToDecimal(txtTransferAmount.Text.Trim()), clsClient.FindForAccountNumber(txtFromAccNumber.Text.Trim()).ClientID
                    , clsClient.FindForAccountNumber(txtToAccNumber.Text.Trim()).ClientID, clsGlobal.CurrentUser.UserID);

                MessageBox.Show("Transfered Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Transfer is not done Successfully!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnFromDetails_Click(object sender, EventArgs e)
        {
            frmClientInfo frm = new frmClientInfo(clsClient.FindForAccountNumber(txtFromAccNumber.Text.Trim()).ClientID);
            frm.ShowDialog();
        }

        private void btnToDetails_Click(object sender, EventArgs e)
        {
            frmClientInfo frm = new frmClientInfo(clsClient.FindForAccountNumber(txtToAccNumber.Text.Trim()).ClientID);
            frm.ShowDialog();
        }

        private void txtTransferAmount_KeyPress(object sender, KeyPressEventArgs e)
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
