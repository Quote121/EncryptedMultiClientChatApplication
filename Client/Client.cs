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
using System.Security.Cryptography;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;

namespace SimpleAsyncServerV2Security
{
    public partial class Client : Form
    {
        private Socket _ClientSocket;

        public ClientData CD = new();
        public ClientDataModifyer CDM = new();
        private const int BUFFERSIZE = 1024;
        private byte[] buffer = new byte[BUFFERSIZE];
        private const int PORT = 3215;
        private string RecentlyVisitedTextFilePath = $"{Application.StartupPath}\\RecentlyVisited.txt";

        public Client()
        {
            InitializeComponent();
            GenerateRSA();
            GetComboBoxAddresses();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (_ClientSocket != null && _ClientSocket.Connected)
            {
                _ClientSocket.Shutdown(SocketShutdown.Both);////Not good maybe
                _ClientSocket.Close(); //Disconnect socket
            }
        }

        private void GenerateRSA()
        {
            RSA ClientPrivateRSA = RSA.Create();
            //ClientData CD = new(null, ClientPrivateRSA, null);
            CDM.SetClientRSA(CD, ClientPrivateRSA);
        }


        private void BtnConnect_Click(object sender, EventArgs e)
        {

            // Temporeraly diable the button (To avoid errors)
            AppendToTextBoxConnectionInfo($"Attempting connection...");
            UpdateButtonState("Connecting");
            AddIPToRecents(IPDropDown.Text);
            // Connect to server (To establish link and encryption)
            _ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveBufferSize = BUFFERSIZE,
                SendBufferSize = BUFFERSIZE,
                // Setting timeouts for syncronous methods 1000ms
                ReceiveTimeout = 1000,
                SendTimeout = 1000
            };
            // Begin connection
            try
            {
                _ClientSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(IPDropDown.Text), PORT), new AsyncCallback(ConnectCallBack), null);
            }
            catch (FormatException ex)
            {
                // Invalid IP address format
                MessageBox.Show($"Invalid IP address.\nAn ip address should look like 192.168.1.1.\n\nTraceback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IPDropDown.Text = "";
                UpdateButtonState("Disconnected");

            }
            catch (Exception ex)
            {
                // Other exception with connection (SERVER MAY BE OFF)
                MessageBox.Show($"ComboBox: Error beginning connection.\nServer may be offline or not responding.\n\nTraceback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateButtonState("Disconnected");
            }

        }


        private void ConnectCallBack(IAsyncResult AR)
        {
            try
            {
                EncryptionAES encryptionAES = new();
                _ClientSocket.EndConnect(AR);

                AppendToTextBoxConnectionInfo($"SERVER: Online\r PROTOCOL: TCP\n");
                // THis could be a good time to send RSA public key
                SendRSAPublicKey();
                // Then listen for the encrypted AES KEY in response
                // Key setting should be syncronous

                int recievedAesKey = _ClientSocket.Receive(buffer);
                Array.Resize(ref buffer, recievedAesKey);
                byte[] decryptedAesKeyBuffer = CD.ClientPrivateRSA.Decrypt(buffer, RSAEncryptionPadding.Pkcs1);
                DataSerializer DDAES = new(decryptedAesKeyBuffer);

                // Set AES keys
                CD.AesKey = DDAES.AESPrivateKey;
                CD.AesIV = DDAES.AESPrivateIV;

                // User should be setup with AES
                UpdateButtonState("Connected");
                // Once encryption is enabled we shall allow data to be sent via client and server
                AppendToTextBoxConnectionInfo($"RSA: USED\r AES: ENABLED\n");
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"Connection cannot be made, target machine actively refused it.\nInvalid IP may have been given or target machine is offline.\nContact target machine admin.\n\nTraceback: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateButtonState("Disconnected");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connect CallBack error.\n\nTraceback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateButtonState("Disconnected");
            }
        }



        // Used for general messages, and AES key setting
        private void RecieveCallBack(IAsyncResult AR)
        {
            try
            {
                // If encryption is enabled
                if (CD.AesKey != null || CD.AesIV != null)
                {
                    // This will only be dealing with mesages from server

                    // Client has AES and so handle message with AES encryption/decryption
                    int recivedAES = _ClientSocket.EndReceive(AR);
                    byte[] dataBufAESENC = new byte[recivedAES];
                    // Resize buffer
                    Array.Copy(buffer, dataBufAESENC, recivedAES);

                    EncryptionAES encryptionAes = new();
                    // Pass encrypted AES databuf and client data
                    byte[] DecryptedDataBuffer = encryptionAes.DecryptDataBufferAES(dataBufAESENC, CD);
                    AppendToTextBoxDebug($"Recieved {DecryptedDataBuffer.Length}bytes");
                    DataSerializer DD = new(DecryptedDataBuffer);


                    // If all false then this is a standard message
                    if (DD.AccountSetBool == DD.AESPrivateKeyBool == DD.LoginGetBool == DD.RSAPublicKeyBool)
                    {
                        // Write message to maintext box
                        AppendToTextBoxMain($"{DD.Name}: {DD.Message}");

                    }
                    // After all that begin to listen for another incoming packet
                    _ClientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(RecieveCallBack), null);
                }
                else
                {
                    AppendToTextBoxDebug("Encryption (AES) not enabled, reeee!");
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"Recieve CallBack error. Connection Died?\nSever might have shut down or rejected client.\n\nTraceback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateButtonState("Disconnected");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Recieve callback client error.\n\nTraceback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SendRSAPublicKey()
        {
            ClientData CDTemp = CD;
            // Get RSA Parameters and sterilize
            DataSerializer DD = new(CDTemp.ClientPrivateRSA.ExportParameters(false));
            byte[] data = DD.RSAToByteArray();
            _ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), null);
        }

        private void SendCallBack(IAsyncResult AR)
        {
            try
            {
                _ClientSocket.EndSend(AR);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send CallBack:" + ex.Message, "Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AppendToTextBoxMain(string message)
        {
            MethodInvoker invoker = new(delegate
            {
                RichTxtBoxMain.Text += $"\r\n{message}";
            });
            this.Invoke(invoker);
        }
        private void AppendToTextBoxDebug(string message)
        {
            MethodInvoker invoker = new(delegate
            {
                RichTxtBoxDebug.Text += $" {message}\n ";
            });
            this.Invoke(invoker);
        }
        private void AppendToTextBoxConnectionInfo(string message)
        {
            MethodInvoker invoker = new(delegate
            {
                RichTxtBoxConnInfo.Text += $" {message}\n";
            });
            this.Invoke(invoker);
        }




        // Form starts in "disconnected" state
        /// <summary>
        /// Connected, , Connecting, LoggedIn, Disconnected are the 4 states of this method
        /// </summary>
        /// <param name="State"></param>
        public void UpdateButtonState(string State)
        {
            // Thread safe way to updata buttons
            MethodInvoker invoker = new(delegate
            {
                if (State == "Connected")
                {
                    BtnConnect.Enabled = BtnSend.Enabled = MessageTxtBox.Enabled = IPDropDown.Enabled = false;
                    BtnCreateAccount.Enabled = BtnLogin.Enabled = true;
                    AppendToTextBoxConnectionInfo("Connected to server\n");
                }
                else if (State == "Connecting")
                {
                    BtnLogin.Enabled = BtnCreateAccount.Enabled = BtnSend.Enabled = MessageTxtBox.Enabled = BtnConnect.Enabled = IPDropDown.Enabled = false;
                }
                else if (State == "LoggedIn")
                {
                    BtnConnect.Enabled = BtnLogin.Enabled = BtnCreateAccount.Enabled = IPDropDown.Enabled = false;
                    BtnSend.Enabled = MessageTxtBox.Enabled = true;
                    // After the user has sucessfully logged in, encryption is now enabled and we can recieve messages and so we start the main recieve
                    _ClientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(RecieveCallBack), null);
                }

                else if (State == "Disconnected")
                {
                    BtnLogin.Enabled = BtnCreateAccount.Enabled = BtnSend.Enabled = MessageTxtBox.Enabled = false;
                    BtnConnect.Enabled = IPDropDown.Enabled = true;
                    AppendToTextBoxConnectionInfo("Lost connection to server...\n");
                    try
                    {
                        _ClientSocket.Disconnect(false);
                        // might not need this
                        _ClientSocket.Dispose();
                    }
                    catch { }
                }
            });
            this.Invoke(invoker);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            using (Login login = new(_ClientSocket, CD))
            {
                login.ShowDialog();
                if (login.LoggedIn)
                {
                    // Update button state
                    UpdateButtonState("LoggedIn");
                }
                else if (login.ServerDead)
                {
                    // Socket error thrown and we must reconnect
                    UpdateButtonState("Disconnected");
                }
            }
        }
        private void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            using (CreateAccount CA = new(_ClientSocket, CD))
            {
                CA.ShowDialog();
                if (CA.AccountCreated)
                {
                    // Update button state
                    UpdateButtonState("LoggedIn");
                }
                else if (CA.ServerDead)
                {
                    // Server is presumed dead we must revert to button state of disconnected
                    UpdateButtonState("Disconnected");
                }
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            EncryptionAES encryptionAES = new();
            try
            {
                // If the length of the message in text box is lessthan or equal to 170 and not empty then we can send their message off
                if (MessageTxtBox.Text.Length <= 170 && MessageTxtBox.Text != "")
                {
                    // Name and message should be sent as we want to broadcast the username of the message to the rest of the clients
                    DataSerializer DS = new(CD.Name, MessageTxtBox.Text);

                    byte[] messageToSend = DS.MessageToByteArray();
                    AppendToTextBoxDebug($"Sending {messageToSend.Length}bytes");
                    byte[] encryptedMessage = encryptionAES.EncryptDataBufferAES(messageToSend, CD);

                    // Send message from client, message in text box
                    _ClientSocket.BeginSend(encryptedMessage, 0, encryptedMessage.Length, SocketFlags.None, new AsyncCallback(SendCallBack), null);
                    MessageTxtBox.Clear();
                }
                else if (MessageTxtBox.Text == "")
                {}
                
                else
                {
                    MessageBox.Show($"Message too long ({MessageTxtBox.Text.Length}) charaters. Max is 170 Characters.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"BtnSend_Click error. Socket Exception.\n\nTraceback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateButtonState("Disconnected");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"BtnSend_Click error.\n\nTraceback: {ex.Message}","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckKeyPressedMsg(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Enter ASCII Key Char
            {
                e.Handled = true; //Disable "dong" warning noise
                BtnSend_Click(sender, e); //Call send method
            }
        }

        private void RichTxtBoxMain_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            RichTxtBoxMain.SelectionStart = RichTxtBoxMain.Text.Length;
            // scroll it automatically
            RichTxtBoxMain.ScrollToCaret();
        }

        private void RichTxtBoxConnInfo_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            RichTxtBoxConnInfo.SelectionStart = RichTxtBoxConnInfo.Text.Length;
            // scroll it automatically
            RichTxtBoxConnInfo.ScrollToCaret();




        }

        private void AddIPToRecents(string address)
        {
            try
            {
                string[] lines = File.ReadAllLines(RecentlyVisitedTextFilePath);
                foreach (var line in lines)
                {
                    if (!lines.Contains(address))
                    {
                        File.AppendAllText(RecentlyVisitedTextFilePath, $"\n{address}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"OpenFile ComboBox error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetComboBoxAddresses()
        {
            try
            {
                IPDropDown.BeginUpdate();
                string[] lineOfContents = File.ReadAllLines(RecentlyVisitedTextFilePath);
                foreach (var line in lineOfContents)
                {
                    if (line != "")
                    {
                        IPDropDown.Items.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"OpenFile ComboBox error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
