using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SimpleAsyncServerV2Security
{
    class DataSerializer
    {
        /// <summary>
        /// This class takes the client data and deserialises it
        /// First few bytes of the data indicate wether the message is to pass public keys private keys,
        /// set names or for general messages
        /// The data after that will be treated based on the setting bytes at the begginging of the message
        /// </summary>

        // BASIC
        public bool RSAPublicKeyBool { get; set; }
        public bool AESPrivateKeyBool { get; set; }
        // Send login info to create a new account, only checking is username conflictions
        public bool AccountSetBool { get; set; }
        // Send login info, server returns with bool of success or fail
        public bool LoginGetBool { get; set; }

        //RSA
        // Passed in once encoded
        public RSAParameters RSAParameter { get; set; }

        // These are retrieved once decoded // Not technically private but it doesnt matter right now
        public byte[] RSAPrivateMod { get; set; }
        public byte[] RSAPrivateExp { get; set; }


        // AES
        public byte[] AESPrivateKey { get; set; }
        public byte[] AESPrivateIV { get; set; }


        // General data
        public string Name { get; set; }
        public string Password { get; set; }
        // message data will be used for username confliction responces or general chat
        public string Message { get; set; }

        // RSAToByteArray
        public DataSerializer(RSAParameters rsaParameters)
        {
            RSAParameter = rsaParameters;
        }
        // RSAToByteArray
        //public DataDeserializer(byte[] rsaPrivateMod, byte[] rsaPrivateExp)
        //{
        //    RSAPrivateMod = rsaPrivateExp;
        //    RSAPrivateExp = rsaPrivateExp;
        //}

        // AESToBytesArray
        public DataSerializer(byte[] aesPrivateKey, byte[] aesPrivateIV)
        {
            AESPrivateKey = aesPrivateKey;
            AESPrivateIV = aesPrivateIV;
        }
        // Login or create account


        /// <summary>
        /// Packets for loging in or creating accounts, message is for server responce true or false
        /// Login Set bool or account set bool should be used
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="message"></param>
        public DataSerializer(string name, string password, string message)
        {
            Name = name;
            Password = password;
            Message = message;
        }




        // Normal messages
        public DataSerializer(string name, string message)
        {
            Name = name;
            Message = message;
        }

        public DataSerializer(byte[] data)
        {
            RSAPublicKeyBool = BitConverter.ToBoolean(data, 0);
            AESPrivateKeyBool = BitConverter.ToBoolean(data, 1);
            AccountSetBool = BitConverter.ToBoolean(data, 2);
            LoginGetBool = BitConverter.ToBoolean(data, 3);

            // If the byte is to exchange public keys then
            // Messages are not encrypted yet
            if (RSAPublicKeyBool)
            {
                int modLength = BitConverter.ToInt32(data, 4); //4 bytes
                RSAPrivateMod = data[8..(8 + modLength)];

                int expLength = BitConverter.ToInt32(data, 8 + modLength); // 4bytes
                RSAPrivateExp = data[(12 + modLength)..(12 + modLength + expLength)]; // Change this its takiung wrong bytes from data
            }
            else if (AESPrivateKeyBool)
            {
                int keyLength = BitConverter.ToInt32(data, 4);
                AESPrivateKey = data[8..(8 + keyLength)];

                int IVLength = BitConverter.ToInt32(data, 8 + keyLength);
                AESPrivateIV = data[(12 + keyLength)..(12 + keyLength + IVLength)];
            }
            // Name data, password data, message data, (Message for server to client accept or deny)
            else if (AccountSetBool || LoginGetBool) // ID in code by using state set bool
            {
                //Length of string //Int32 are 4 bytes
                int nameLength = BitConverter.ToInt32(data, 4);
                Name = Encoding.ASCII.GetString(data, 8, nameLength);

                int passwordLength = BitConverter.ToInt32(data, nameLength + 8);
                Password = Encoding.ASCII.GetString(data, 12 + nameLength, passwordLength);


                int messageLength = BitConverter.ToInt32(data, nameLength + passwordLength + 12);
                Message = Encoding.ASCII.GetString(data, 16 + nameLength + passwordLength, messageLength);
            }
            // message data (Name stored on server) (ID WITH SOCKET)
            else
            {
                int nameLength = BitConverter.ToInt32(data, 4);
                Name = Encoding.ASCII.GetString(data, 8, nameLength);

                int messageLength = BitConverter.ToInt32(data, 8 + nameLength);
                Message = Encoding.ASCII.GetString(data, nameLength + 12, messageLength);
            }
        }
        // To get byte array of data (RSAPUB KEY)
        public byte[] RSAToByteArray()
        {
            List<byte> byteList = new();

            byte[] mod = RSAParameter.Modulus;
            byte[] exp = RSAParameter.Exponent;

            // RSA Public Key Set bool
            byte[] set = { 0x01, 0x00, 0x00, 0x00 };
            byteList.AddRange(set);


            // Modulus
            byteList.AddRange(BitConverter.GetBytes(mod.Length));
            byteList.AddRange(mod); //Already byte[]

            // Exponent
            byteList.AddRange(BitConverter.GetBytes(exp.Length));
            byteList.AddRange(exp);

            // Return bytearray
            return byteList.ToArray();
        }
        public byte[] AESToBytesArray()
        {
            List<byte> byteList = new();
            // AES PRIVATE KEY set bool
            byte[] set = { 0x00, 0x01, 0x00, 0x00 };
            byteList.AddRange(set);

            byteList.AddRange(BitConverter.GetBytes(AESPrivateKey.Length));
            byteList.AddRange(AESPrivateKey);

            byteList.AddRange(BitConverter.GetBytes(AESPrivateIV.Length));
            byteList.AddRange(AESPrivateIV);

            return byteList.ToArray();
        }
        public byte[] AccountPacketToByteArray()
        {
            List<byte> byteList = new();

            // Name set bools
            byte[] set = { 0x00, 0x00, 0x01, 0x00 };
            byteList.AddRange(set);

            // Send username and password, they will be hashed at server end
            byteList.AddRange(BitConverter.GetBytes(Name.Length)); //Add length of name data
            byteList.AddRange(Encoding.ASCII.GetBytes(Name)); //Add name data

            byteList.AddRange(BitConverter.GetBytes(Password.Length)); //Add length of name data
            byteList.AddRange(Encoding.ASCII.GetBytes(Password)); //Add name data

            byteList.AddRange(BitConverter.GetBytes(Message.Length));
            byteList.AddRange(Encoding.ASCII.GetBytes(Message));

            return byteList.ToArray();
        }

        public byte[] LoginPacketToByteArray()
        {
            List<byte> byteList = new();

            // Name set bools
            byte[] set = { 0x00, 0x00, 0x00, 0x01 };
            byteList.AddRange(set);

            // Send username and password, they will be hashed at server end
            byteList.AddRange(BitConverter.GetBytes(Name.Length)); //Add length of name data
            byteList.AddRange(Encoding.ASCII.GetBytes(Name)); //Add name data

            byteList.AddRange(BitConverter.GetBytes(Password.Length)); //Add length of name data
            byteList.AddRange(Encoding.ASCII.GetBytes(Password)); //Add name data

            byteList.AddRange(BitConverter.GetBytes(Message.Length));
            byteList.AddRange(Encoding.ASCII.GetBytes(Message));
            return byteList.ToArray();
        }

        // For sending standard messages
        public byte[] MessageToByteArray()
        {
            List<byte> byteList = new();

            // All state set bools are 0 beacause standard message
            byte[] set = { 0x00, 0x00, 0x00, 0x00 };
            byteList.AddRange(set);

            // Name append to list
            byteList.AddRange(BitConverter.GetBytes(Name.Length));
            byteList.AddRange(Encoding.ASCII.GetBytes(Name));

            // Message append to list
            byteList.AddRange(BitConverter.GetBytes(Message.Length));
            byteList.AddRange(Encoding.ASCII.GetBytes(Message));

            return byteList.ToArray();
        }
    }
}