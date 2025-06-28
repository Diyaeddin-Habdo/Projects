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

namespace Bank_System.Users
{
    public partial class ctrlUserCard : UserControl
    {
        private clsUser _User = new clsUser();
        private int _UserID = -1;

        public int UserID
        {
            get { return _UserID; }
        }

        public clsUser UserInfo
        {
            get
            {
                return _User;
            }
        }
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.FindByUserID(UserID);
            if (_User == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        private void _FillUserInfo()
        {

            ctrlPersonCard1.LoadPersonData(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();

        }

        private void _ResetPersonInfo()
        {

            ctrlPersonCard1.ResetDefaultValues();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
        }
    }
}
