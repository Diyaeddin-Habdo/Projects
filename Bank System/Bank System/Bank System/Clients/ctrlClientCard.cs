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
    public partial class ctrlClientCard : UserControl
    {
        public ctrlClientCard()
        {
            InitializeComponent();
        }

        private clsClient _Client = new clsClient();
        private int _ClientID = -1;

        public int ClientID
        {
            get { return _ClientID; }
        }

        public clsClient ClientInfo
        {
            get
            {
                return _Client;
            }
        }

        public void LoadClientInfo(int ClientID)
        {
            _Client = clsClient.FindByClientID(ClientID);
            if (_Client == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No Client with ClientID = " + ClientID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        private void _FillUserInfo()
        {

            ctrlPersonCard1.LoadPersonData(_Client.PersonID);
            lblClientID.Text = _Client.ClientID.ToString();
            lblAccNumber.Text = _Client.AccountNumber.ToString();
            lblAccBalance.Text = _Client.AccountBalance.ToString();

        }

        private void _ResetPersonInfo()
        {

            ctrlPersonCard1.ResetDefaultValues();
            lblClientID.Text = "????";
            lblAccNumber.Text = "????";
            lblAccBalance.Text = "????";
        }
    }
}
