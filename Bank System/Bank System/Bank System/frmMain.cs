using Bank_System.Clients;
using Bank_System.Global_Classes;
using Bank_System.Login;
using Bank_System.Login_File;
using Bank_System.People;
using Bank_System.Transactions;
using Bank_System.Transfer_Log;
using Bank_System.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank_System
{
    public partial class frmMain : Form
    {
        frmLogin _LoginForm;
        public frmMain(frmLogin frm)
        {
            InitializeComponent();
            _LoginForm = frm;
        }
        

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsersList frm = new frmUsersList();
            frm.ShowDialog();
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientList frm = new frmClientList();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _LoginForm.Show();
            this.Close();
        }

        private void withdrawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWithdraw frm = new frmWithdraw();
            frm.ShowDialog();
        }

        private void depositToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeposite frm = new frmDeposite();
            frm.ShowDialog();
        }

        private void transferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTransfer frm = new frmTransfer();
            frm.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if(clsGlobal.CurrentUser.PersonInfo.ImagePath != "")
                clsCircularPictureBox1.ImageLocation = clsGlobal.CurrentUser.PersonInfo.ImagePath;
            else
                clsCircularPictureBox1.Visible = false;
        }

        private void clsCircularPictureBox1_Click(object sender, EventArgs e)
        {
            frmBigUserPicture frm = new frmBigUserPicture(clsGlobal.CurrentUser.PersonInfo.ImagePath);
            frm.ShowDialog();
        }

        private void transferFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTransferLog frm = new frmTransferLog();
            frm.ShowDialog();
        }

        private void logFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoginFile frm = new frmLoginFile();
            frm.ShowDialog();
        }
    }
}
