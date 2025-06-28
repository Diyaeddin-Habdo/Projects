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

namespace Bank_System.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindByUsernameAndPassword(txtUsername.Text.Trim(), txtPassword.Text.Trim());

            if (User != null)
            {
                
                clsGlobal.CurrentUser = User;
                this.Hide();
                clsLoginLog.AddNewLoginRecord(DateTime.Now, clsGlobal.CurrentUser.UserID);
                txtPassword.Text = "";
                txtUsername.Text = "";
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
            }
            else
            {
                txtUsername.Focus();
                MessageBox.Show("Invalid UserName / Password.", "Wrong Credintionals", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Red;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(45, 85, 95);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
