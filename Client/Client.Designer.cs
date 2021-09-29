
namespace SimpleAsyncServerV2Security
{
    partial class Client
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnConnect = new System.Windows.Forms.Button();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.BtnCreateAccount = new System.Windows.Forms.Button();
            this.BtnSend = new System.Windows.Forms.Button();
            this.RichTxtBoxMain = new System.Windows.Forms.RichTextBox();
            this.RichTxtBoxDebug = new System.Windows.Forms.RichTextBox();
            this.RichTxtBoxConnInfo = new System.Windows.Forms.RichTextBox();
            this.MessageTxtBox = new System.Windows.Forms.TextBox();
            this.IPDropDown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(15, 75);
            this.BtnConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(239, 72);
            this.BtnConnect.TabIndex = 0;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // BtnLogin
            // 
            this.BtnLogin.Enabled = false;
            this.BtnLogin.Location = new System.Drawing.Point(14, 155);
            this.BtnLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(239, 72);
            this.BtnLogin.TabIndex = 1;
            this.BtnLogin.Text = "Login";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // BtnCreateAccount
            // 
            this.BtnCreateAccount.Enabled = false;
            this.BtnCreateAccount.Location = new System.Drawing.Point(14, 235);
            this.BtnCreateAccount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnCreateAccount.Name = "BtnCreateAccount";
            this.BtnCreateAccount.Size = new System.Drawing.Size(239, 72);
            this.BtnCreateAccount.TabIndex = 2;
            this.BtnCreateAccount.Text = "Create account";
            this.BtnCreateAccount.UseVisualStyleBackColor = true;
            this.BtnCreateAccount.Click += new System.EventHandler(this.BtnCreateAccount_Click);
            // 
            // BtnSend
            // 
            this.BtnSend.Enabled = false;
            this.BtnSend.Location = new System.Drawing.Point(14, 512);
            this.BtnSend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(239, 72);
            this.BtnSend.TabIndex = 3;
            this.BtnSend.Text = "Send";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // RichTxtBoxMain
            // 
            this.RichTxtBoxMain.Location = new System.Drawing.Point(259, 36);
            this.RichTxtBoxMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RichTxtBoxMain.Name = "RichTxtBoxMain";
            this.RichTxtBoxMain.ReadOnly = true;
            this.RichTxtBoxMain.Size = new System.Drawing.Size(716, 508);
            this.RichTxtBoxMain.TabIndex = 4;
            this.RichTxtBoxMain.Text = "";
            this.RichTxtBoxMain.TextChanged += new System.EventHandler(this.RichTxtBoxMain_TextChanged);
            // 
            // RichTxtBoxDebug
            // 
            this.RichTxtBoxDebug.Location = new System.Drawing.Point(983, 36);
            this.RichTxtBoxDebug.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RichTxtBoxDebug.Name = "RichTxtBoxDebug";
            this.RichTxtBoxDebug.ReadOnly = true;
            this.RichTxtBoxDebug.Size = new System.Drawing.Size(315, 547);
            this.RichTxtBoxDebug.TabIndex = 5;
            this.RichTxtBoxDebug.Text = "";
            // 
            // RichTxtBoxConnInfo
            // 
            this.RichTxtBoxConnInfo.Location = new System.Drawing.Point(14, 335);
            this.RichTxtBoxConnInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RichTxtBoxConnInfo.Name = "RichTxtBoxConnInfo";
            this.RichTxtBoxConnInfo.ReadOnly = true;
            this.RichTxtBoxConnInfo.Size = new System.Drawing.Size(238, 168);
            this.RichTxtBoxConnInfo.TabIndex = 6;
            this.RichTxtBoxConnInfo.Text = "";
            this.RichTxtBoxConnInfo.TextChanged += new System.EventHandler(this.RichTxtBoxConnInfo_TextChanged);
            // 
            // MessageTxtBox
            // 
            this.MessageTxtBox.Enabled = false;
            this.MessageTxtBox.Location = new System.Drawing.Point(259, 553);
            this.MessageTxtBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MessageTxtBox.Name = "MessageTxtBox";
            this.MessageTxtBox.Size = new System.Drawing.Size(716, 27);
            this.MessageTxtBox.TabIndex = 7;
            this.MessageTxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPressedMsg);
            // 
            // IPDropDown
            // 
            this.IPDropDown.FormattingEnabled = true;
            this.IPDropDown.Location = new System.Drawing.Point(14, 36);
            this.IPDropDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.IPDropDown.Name = "IPDropDown";
            this.IPDropDown.Size = new System.Drawing.Size(238, 28);
            this.IPDropDown.TabIndex = 9;
            this.IPDropDown.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Recently Connected Hosts";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Chat window";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(983, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Diagnostics/Debug Window";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 311);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Connection Info";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1312, 600);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IPDropDown);
            this.Controls.Add(this.MessageTxtBox);
            this.Controls.Add(this.RichTxtBoxConnInfo);
            this.Controls.Add(this.RichTxtBoxDebug);
            this.Controls.Add(this.RichTxtBoxMain);
            this.Controls.Add(this.BtnSend);
            this.Controls.Add(this.BtnCreateAccount);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.BtnConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.Button BtnCreateAccount;
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.RichTextBox RichTxtBoxMain;
        private System.Windows.Forms.RichTextBox RichTxtBoxDebug;
        private System.Windows.Forms.RichTextBox RichTxtBoxConnInfo;
        private System.Windows.Forms.TextBox MessageTxtBox;
        private System.Windows.Forms.ComboBox IPDropDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

