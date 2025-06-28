namespace Bank_System
{
    partial class frmBigUserPicture
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblClickToClose = new System.Windows.Forms.Label();
            this.clsCircularPictureBox1 = new Bank_System.clsCircularPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.clsCircularPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblClickToClose
            // 
            this.lblClickToClose.AutoSize = true;
            this.lblClickToClose.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClickToClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClickToClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblClickToClose.Location = new System.Drawing.Point(13, 476);
            this.lblClickToClose.Name = "lblClickToClose";
            this.lblClickToClose.Size = new System.Drawing.Size(156, 27);
            this.lblClickToClose.TabIndex = 1;
            this.lblClickToClose.Text = "Click To Close";
            this.lblClickToClose.Click += new System.EventHandler(this.lblClickToClose_Click);
            // 
            // clsCircularPictureBox1
            // 
            this.clsCircularPictureBox1.BorderCapStyle = System.Drawing.Drawing2D.DashCap.Flat;
            this.clsCircularPictureBox1.BorderColor = System.Drawing.Color.RoyalBlue;
            this.clsCircularPictureBox1.BorderColor2 = System.Drawing.Color.HotPink;
            this.clsCircularPictureBox1.BorderLineSyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.clsCircularPictureBox1.BorderSize = 2;
            this.clsCircularPictureBox1.GradientAngle = 50F;
            this.clsCircularPictureBox1.Location = new System.Drawing.Point(13, 4);
            this.clsCircularPictureBox1.Name = "clsCircularPictureBox1";
            this.clsCircularPictureBox1.Size = new System.Drawing.Size(510, 469);
            this.clsCircularPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.clsCircularPictureBox1.TabIndex = 2;
            this.clsCircularPictureBox1.TabStop = false;
            // 
            // frmBigUserPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(534, 507);
            this.Controls.Add(this.lblClickToClose);
            this.Controls.Add(this.clsCircularPictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBigUserPicture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Picture";
            this.Load += new System.EventHandler(this.frmBigUserPicture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clsCircularPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblClickToClose;
        private clsCircularPictureBox clsCircularPictureBox1;
    }
}