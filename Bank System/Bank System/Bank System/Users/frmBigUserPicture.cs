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
    public partial class frmBigUserPicture : Form
    {
        private string _ImagePath;
        public frmBigUserPicture(string imagePath)
        {
            InitializeComponent();
            _ImagePath = imagePath;
        }

        private void frmBigUserPicture_Load(object sender, EventArgs e)
        {
            clsCircularPictureBox1.ImageLocation = _ImagePath;
        }

        private void lblClickToClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
