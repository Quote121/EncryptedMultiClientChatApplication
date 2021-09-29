
namespace ServerForm
{
    partial class Server
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
            this.RichTxtBoxMain = new System.Windows.Forms.RichTextBox();
            this.RichTxtBoxServer = new System.Windows.Forms.RichTextBox();
            this.TxtBoxMain = new System.Windows.Forms.TextBox();
            this.BtnSend = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RichTxtBoxMain
            // 
            this.RichTxtBoxMain.Location = new System.Drawing.Point(14, 36);
            this.RichTxtBoxMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RichTxtBoxMain.Name = "RichTxtBoxMain";
            this.RichTxtBoxMain.ReadOnly = true;
            this.RichTxtBoxMain.Size = new System.Drawing.Size(589, 508);
            this.RichTxtBoxMain.TabIndex = 0;
            this.RichTxtBoxMain.Text = "";
            this.RichTxtBoxMain.TextChanged += new System.EventHandler(this.RichTxtBoxMain_TextChanged);
            // 
            // RichTxtBoxServer
            // 
            this.RichTxtBoxServer.Location = new System.Drawing.Point(610, 36);
            this.RichTxtBoxServer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RichTxtBoxServer.Name = "RichTxtBoxServer";
            this.RichTxtBoxServer.ReadOnly = true;
            this.RichTxtBoxServer.Size = new System.Drawing.Size(290, 508);
            this.RichTxtBoxServer.TabIndex = 1;
            this.RichTxtBoxServer.Text = "";
            this.RichTxtBoxServer.TextChanged += new System.EventHandler(this.RichTxtBoxServer_TextChanged);
            // 
            // TxtBoxMain
            // 
            this.TxtBoxMain.Location = new System.Drawing.Point(14, 553);
            this.TxtBoxMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtBoxMain.Name = "TxtBoxMain";
            this.TxtBoxMain.Size = new System.Drawing.Size(589, 27);
            this.TxtBoxMain.TabIndex = 2;
            this.TxtBoxMain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPressedMsg);
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(610, 553);
            this.BtnSend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(146, 31);
            this.BtnSend.TabIndex = 3;
            this.BtnSend.Text = "Send";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(763, 553);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(137, 31);
            this.BtnClose.TabIndex = 4;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Chat";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(610, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Server";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnSend);
            this.Controls.Add(this.TxtBoxMain);
            this.Controls.Add(this.RichTxtBoxServer);
            this.Controls.Add(this.RichTxtBoxMain);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Server";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox RichTxtBoxMain;
        private System.Windows.Forms.RichTextBox RichTxtBoxServer;
        private System.Windows.Forms.TextBox TxtBoxMain;
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

