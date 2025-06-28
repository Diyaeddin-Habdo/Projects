namespace Bank_System.Transactions
{
    partial class frmTransfer
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
            this.components = new System.ComponentModel.Container();
            this.btnFromDetails = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.txtTransferAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFromAccNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnToDetails = new System.Windows.Forms.Button();
            this.txtToAccNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFromDetails
            // 
            this.btnFromDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFromDetails.Enabled = false;
            this.btnFromDetails.FlatAppearance.BorderSize = 0;
            this.btnFromDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFromDetails.Image = global::Bank_System.Properties.Resources.PersonDetails_32;
            this.btnFromDetails.Location = new System.Drawing.Point(425, 33);
            this.btnFromDetails.Name = "btnFromDetails";
            this.btnFromDetails.Size = new System.Drawing.Size(32, 32);
            this.btnFromDetails.TabIndex = 11;
            this.btnFromDetails.UseVisualStyleBackColor = true;
            this.btnFromDetails.Click += new System.EventHandler(this.btnFromDetails_Click);
            // 
            // btnTransfer
            // 
            this.btnTransfer.Enabled = false;
            this.btnTransfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTransfer.Location = new System.Drawing.Point(236, 281);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(171, 39);
            this.btnTransfer.TabIndex = 3;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // txtTransferAmount
            // 
            this.txtTransferAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTransferAmount.Location = new System.Drawing.Point(236, 196);
            this.txtTransferAmount.Name = "txtTransferAmount";
            this.txtTransferAmount.Size = new System.Drawing.Size(171, 26);
            this.txtTransferAmount.TabIndex = 2;
            this.txtTransferAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTransferAmount_KeyPress);
            this.txtTransferAmount.Validating += new System.ComponentModel.CancelEventHandler(this.txtTransferAmount_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.label2.Location = new System.Drawing.Point(25, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Transfer Amount:";
            // 
            // txtFromAccNumber
            // 
            this.txtFromAccNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtFromAccNumber.Location = new System.Drawing.Point(236, 39);
            this.txtFromAccNumber.MaxLength = 20;
            this.txtFromAccNumber.Name = "txtFromAccNumber";
            this.txtFromAccNumber.Size = new System.Drawing.Size(171, 26);
            this.txtFromAccNumber.TabIndex = 0;
            this.txtFromAccNumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtFromAccNumber_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.label1.Location = new System.Drawing.Point(25, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "From Acc Number:";
            // 
            // btnToDetails
            // 
            this.btnToDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToDetails.Enabled = false;
            this.btnToDetails.FlatAppearance.BorderSize = 0;
            this.btnToDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToDetails.Image = global::Bank_System.Properties.Resources.PersonDetails_32;
            this.btnToDetails.Location = new System.Drawing.Point(425, 107);
            this.btnToDetails.Name = "btnToDetails";
            this.btnToDetails.Size = new System.Drawing.Size(32, 32);
            this.btnToDetails.TabIndex = 17;
            this.btnToDetails.UseVisualStyleBackColor = true;
            this.btnToDetails.Click += new System.EventHandler(this.btnToDetails_Click);
            // 
            // txtToAccNumber
            // 
            this.txtToAccNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtToAccNumber.Location = new System.Drawing.Point(236, 113);
            this.txtToAccNumber.MaxLength = 20;
            this.txtToAccNumber.Name = "txtToAccNumber";
            this.txtToAccNumber.Size = new System.Drawing.Size(171, 26);
            this.txtToAccNumber.TabIndex = 1;
            this.txtToAccNumber.Validating += new System.ComponentModel.CancelEventHandler(this.ToAccNumber_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.label4.Location = new System.Drawing.Point(25, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 25);
            this.label4.TabIndex = 12;
            this.label4.Text = "To Acc Number:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTransfer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtToAccNumber);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnToDetails);
            this.groupBox1.Controls.Add(this.txtFromAccNumber);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTransferAmount);
            this.groupBox1.Controls.Add(this.btnFromDetails);
            this.groupBox1.Location = new System.Drawing.Point(41, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(558, 341);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transfer Info";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::Bank_System.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(501, 392);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(98, 39);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(620, 448);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Transfer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFromDetails;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.TextBox txtTransferAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFromAccNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnToDetails;
        private System.Windows.Forms.TextBox txtToAccNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}