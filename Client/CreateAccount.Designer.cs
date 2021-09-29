
namespace SimpleAsyncServerV2Security
{
    partial class CreateAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateAccount));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CheckBoxShowPass = new System.Windows.Forms.CheckBox();
            this.LinkLblHelp = new System.Windows.Forms.LinkLabel();
            this.TxtBoxPassword = new System.Windows.Forms.TextBox();
            this.TxtBoxUserName = new System.Windows.Forms.TextBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnCreateAccount = new System.Windows.Forms.Button();
            this.TxtBoxConfirmPass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 408);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 369);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "UserName";
            // 
            // CheckBoxShowPass
            // 
            this.CheckBoxShowPass.AutoSize = true;
            this.CheckBoxShowPass.Location = new System.Drawing.Point(270, 485);
            this.CheckBoxShowPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CheckBoxShowPass.Name = "CheckBoxShowPass";
            this.CheckBoxShowPass.Size = new System.Drawing.Size(132, 24);
            this.CheckBoxShowPass.TabIndex = 14;
            this.CheckBoxShowPass.Text = "Show Password";
            this.CheckBoxShowPass.UseVisualStyleBackColor = true;
            this.CheckBoxShowPass.CheckedChanged += new System.EventHandler(this.CheckBoxShowPass_CheckedChanged);
            // 
            // LinkLblHelp
            // 
            this.LinkLblHelp.AutoSize = true;
            this.LinkLblHelp.Location = new System.Drawing.Point(110, 491);
            this.LinkLblHelp.Name = "LinkLblHelp";
            this.LinkLblHelp.Size = new System.Drawing.Size(51, 20);
            this.LinkLblHelp.TabIndex = 13;
            this.LinkLblHelp.TabStop = true;
            this.LinkLblHelp.Text = "(Help)";
            this.LinkLblHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLblHelp_LinkClicked);
            // 
            // TxtBoxPassword
            // 
            this.TxtBoxPassword.Location = new System.Drawing.Point(110, 411);
            this.TxtBoxPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtBoxPassword.Name = "TxtBoxPassword";
            this.TxtBoxPassword.Size = new System.Drawing.Size(277, 27);
            this.TxtBoxPassword.TabIndex = 12;
            this.TxtBoxPassword.UseSystemPasswordChar = true;
            // 
            // TxtBoxUserName
            // 
            this.TxtBoxUserName.Location = new System.Drawing.Point(110, 372);
            this.TxtBoxUserName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtBoxUserName.Name = "TxtBoxUserName";
            this.TxtBoxUserName.Size = new System.Drawing.Size(277, 27);
            this.TxtBoxUserName.TabIndex = 11;
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(56, 624);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(331, 73);
            this.BtnClose.TabIndex = 10;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnCreateAccount
            // 
            this.BtnCreateAccount.Location = new System.Drawing.Point(56, 543);
            this.BtnCreateAccount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnCreateAccount.Name = "BtnCreateAccount";
            this.BtnCreateAccount.Size = new System.Drawing.Size(331, 73);
            this.BtnCreateAccount.TabIndex = 9;
            this.BtnCreateAccount.Text = "Create Account";
            this.BtnCreateAccount.UseVisualStyleBackColor = true;
            this.BtnCreateAccount.Click += new System.EventHandler(this.BtnCreateAccount_Click);
            // 
            // TxtBoxConfirmPass
            // 
            this.TxtBoxConfirmPass.Location = new System.Drawing.Point(110, 447);
            this.TxtBoxConfirmPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtBoxConfirmPass.Name = "TxtBoxConfirmPass";
            this.TxtBoxConfirmPass.Size = new System.Drawing.Size(277, 27);
            this.TxtBoxConfirmPass.TabIndex = 18;
            this.TxtBoxConfirmPass.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 447);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 40);
            this.label4.TabIndex = 19;
            this.label4.Text = "Confirm\r\nPassword";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(14, 16);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(429, 304);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // CreateAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 713);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TxtBoxConfirmPass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CheckBoxShowPass);
            this.Controls.Add(this.LinkLblHelp);
            this.Controls.Add(this.TxtBoxPassword);
            this.Controls.Add(this.TxtBoxUserName);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnCreateAccount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CreateAccount";
            this.Text = "CreateAccount";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CheckBoxShowPass;
        private System.Windows.Forms.LinkLabel LinkLblHelp;
        private System.Windows.Forms.TextBox TxtBoxPassword;
        private System.Windows.Forms.TextBox TxtBoxUserName;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnCreateAccount;
        private System.Windows.Forms.TextBox TxtBoxConfirmPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}