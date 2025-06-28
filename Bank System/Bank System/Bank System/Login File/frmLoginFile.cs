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

namespace Bank_System.Login_File
{
    public partial class frmLoginFile : Form
    {
        public frmLoginFile()
        {
            InitializeComponent();
        }

        private void frmLoginFile_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = clsLoginLog.GetAllLoginRecords();

            if(dataGridView1.Rows.Count > 0 )
            {
                dataGridView1.Columns[1].Width = 300;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dataGridView1.CurrentRow.Cells[2].Value);
            frm.ShowDialog();
        }
    }
}
