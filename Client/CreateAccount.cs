using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace SimpleAsyncServerV2Security
{
    public partial class CreateAccount : Form
    {

        // If this is set to true there has been a socket exception thrown and we must assume socket is dead
        public bool ServerDead;
        // Pass back to main form that the account has been created
        public bool AccountCreated;
        // Encryption class
        EncryptionAES encryptionAES = new();
        // Client data class
        ClientData _Client;
        // Server socket info
        Socket _ServerSocket;
        private byte[] _buffer = new byte[1024];

        public CreateAccount(Socket ServerSocket, ClientData client)
        {
            InitializeComponent();
            _ServerSocket = ServerSocket;
            _Client = client;
            // Set timeouts for syncronous methods
            _ServerSocket.SendTimeout = 2000;
            _ServerSocket.ReceiveTimeout = 2000;

        }

        private void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            // These are the only allowed characters for a username
            string AllowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!?_";

            // If there has been data entered into all fields then
            if (TxtBoxUserName.Text != "" && TxtBoxPassword.Text != "" && TxtBoxConfirmPass.Text != "")
            {
                if (TxtBoxUserName.Text.Length <= 16)
                {
                    if (TxtBoxPassword.Text.Length <= 256 && TxtBoxPassword.Text.Length >=4)
                    {
                        // If the entered username only contains characters withinn the allowed characters string then we good
                        if (TxtBoxUserName.Text.All(c => AllowedCharacters.Contains(c)))
                        {
                            // If confirmed passwords match
                            if (TxtBoxPassword.Text == TxtBoxConfirmPass.Text)
                            {
                                if (AccountIsAllowed(TxtBoxUserName.Text, TxtBoxPassword.Text))
                                {
                                    // Account has been created // Close current form
                                    AccountCreated = true;
                                    MessageBox.Show("Account has been created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                                else
                                    //MessageBox.Show("Account username already exists.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearAllBoxes();
                            }
                            else
                                MessageBox.Show("Passwords don't match.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ClearPasswordBoxes();
                        }
                        else
                            MessageBox.Show("Only letters and numbers and !?_ are allowed in username", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ClearPasswordBoxes();
                    }
                    else
                        MessageBox.Show("Max password length is 256 characters. Min password length is 4 charaters", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearPasswordBoxes();
                }
                else
                    MessageBox.Show("Max username length is 16 characters.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ClearAllBoxes();
            }
            else
                MessageBox.Show("Please enter a username and password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        

        private bool AccountIsAllowed(string userName, string password)
        {
            try
            {
                // No message as we are client, server only has a message
                DataSerializer DD = new(userName, password, "");

                // Syncronous send and recieve
                byte[] EncryptedMessageOut = encryptionAES.EncryptDataBufferAES(DD.AccountPacketToByteArray(), _Client);
                _ServerSocket.Send(EncryptedMessageOut, 0, EncryptedMessageOut.Length, SocketFlags.None);

                // If there is no server responce the recieve funtion is blocking so we must add a timeout 

                int recievedAes = _ServerSocket.Receive(_buffer);
                Array.Resize(ref _buffer, recievedAes);
                byte[] decryptedBuffer = encryptionAES.DecryptDataBufferAES(_buffer, _Client);

                DataSerializer DD2 = new(decryptedBuffer);
                if (DD2.Message == "True")
                {
                    // Account creation sucessful // SetUserName
                    _Client.Name = TxtBoxUserName.Text;
                    return true;
                }
                else
                {
                    MessageBox.Show("Account username already exists.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (SocketException ex)
            {
                ServerDead = true;
                MessageBox.Show($"Socket exception, error. Target machine not responding. Timeout.\n\nTraceback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Need to disable login and create account and require the user to re connect
                this.Close();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login responce error.\n\nTraceback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


        }

        private void ClearAllBoxes()
        {
            TxtBoxUserName.Clear();
            TxtBoxPassword.Clear();
            TxtBoxConfirmPass.Clear();
        }
        private void ClearPasswordBoxes()
        {
            TxtBoxPassword.Clear();
            TxtBoxConfirmPass.Clear();
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            // Close current form
            this.Close();
        }
        
        private void LinkLblHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show($"Formatting help:\n\n-Max password length is 256 characters.\n-Min password length is 4 charaters.\n-Max username length is 16 characters.\n-Only letters and numbers and !?_ are allowed in username.\n-Please enter a username and password.", "Create account - help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CheckBoxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            //TxtBoxPassword.PasswordChar = '*';
            if (CheckBoxShowPass.Checked)
            {
                TxtBoxPassword.UseSystemPasswordChar = false;
                TxtBoxConfirmPass.UseSystemPasswordChar = false;
            }
            else if (!CheckBoxShowPass.Checked)
            {
                TxtBoxPassword.UseSystemPasswordChar = true;
                TxtBoxConfirmPass.UseSystemPasswordChar = true;
            }
        }
    }
}
