using Bank_System.Clients;
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

namespace Bank_System.Transfer_Log
{
    public partial class frmTransferLog : Form
    {
        public frmTransferLog()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTransferLog_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = clsTransfer.GetAllTransferRecords();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            if (rowIndex >= 0 && colIndex >= 0) // Kontrol, başlık hücresine tıklanıldığında -1 değerini döndürebilir
            {
                object cellValue = dataGridView1.Rows[rowIndex].Cells[colIndex].Value;

                if(colIndex == 5)
                {
                    frmUserInfo frm = new frmUserInfo((int)cellValue);
                    frm.ShowDialog();
                }

                if(colIndex == 3 || colIndex == 4)
                {
                    frmClientInfo frm = new frmClientInfo((int)cellValue);
                    frm.ShowDialog();
                }
            }
        }
    }
}
