using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.IO;

namespace SimpleAsyncServerV2Security
{
    public class EncryptionAES
    {
        // Takes the client data over to this class

        // I know this is really messy, im sorry
        




        #region AES
        // Pass bytestream (SERILIZED DATA)
        public byte[] EncryptDataBufferAES(byte[] UnencryptedMessage, ClientData client)
        {
            byte[] key = client.AesKey;
            byte[] IV = client.AesIV;
            //Argument checker
            if (UnencryptedMessage == null || UnencryptedMessage.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            using (Aes aesEnc = Aes.Create())
            {
                // Import values to aes decryptor
                aesEnc.Key = key;
                aesEnc.IV = IV;
                aesEnc.Padding = PaddingMode.Zeros;
                // Create decryptor to perform the stream transformation;
                //ICryptoTransform decryptor = aesEnc.CreateDecryptor(aesEnc.Key, aesEnc.IV);

                // Create streams for decryption
                // LOOK INTO THIS
                using (var encryptor = aesEnc.CreateEncryptor(aesEnc.Key, aesEnc.IV))
                {
                    return PerformCryptogrphy(UnencryptedMessage, encryptor);
                }
            }
        }

        // Returned bytestream (DERILIZED DATA)
        public byte[] DecryptDataBufferAES(byte[] EncryptedBuffer, ClientData client)
        {
            
            
            byte[] key = client.AesKey;
            byte[] IV = client.AesIV;

            //Argument checker
            if (EncryptedBuffer == null || EncryptedBuffer.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Create a new AES object and import key and IV
            // AES DECRYPTOR

            using (Aes aesDec = Aes.Create())
            {
                // Import values to aes decryptor
                aesDec.Key = key;
                aesDec.IV = IV;
                aesDec.Padding = PaddingMode.Zeros;

                // Create decryptor to perform the stream transformation;
                // Create streams for decryption
                // LOOK INTO THIS
                using (var decryptor = aesDec.CreateDecryptor(aesDec.Key, aesDec.IV))
                {
                    return PerformCryptogrphy(EncryptedBuffer, decryptor);
                }
            }
        }
        // Can return encryped and decrypted (LOOKINTO THIS WHEN YOU CAN) THIS WAS KINDA A CTRLC CTRLV (NOT PROUD)
        public byte[] PerformCryptogrphy(byte[] data, ICryptoTransform cryptoTransform)
        {
            using (var ms = new MemoryStream())
            using (var cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                return ms.ToArray();
            }
        }
        #endregion AES
    }
}
