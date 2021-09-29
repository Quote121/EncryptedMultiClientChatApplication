using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net.Sockets;

namespace SimpleAsyncServerV2Security
{
    public class ClientData
    {
        //public Socket ClientSocket { get; set; }
        // Set to internal sets as they will be
        public string Name { get; set; }
        public RSA ClientPrivateRSA { get; set; }
        // Server RSA details (might not need) maybe later for robustnous
        public byte[] AesKey { get; set; }
        public byte[] AesIV { get; set; }
        public ClientData()
        {
            // Allows me to set nothing therefore no nulls
        }
        public ClientData(string name, RSA ClientRsa, byte[] aesKey, byte[] aesIV)
        {
            Name = name;
            ClientPrivateRSA = ClientRsa;
            AesKey = aesKey;
            AesIV = aesIV;

        }
    }
    public class ClientDataModifyer
    {
        // Thses are used to update existing object since this data will be gatherd throughout boot
        public void SetClientName(ClientData client, string name)
        {
            client.Name = name;
        }
        public void SetClientRSA(ClientData client, RSA rsa)
        {
            client.ClientPrivateRSA = rsa;
        }
        //public void SetClientAes(ClientData client, Aes aes)
        //{
        //    client.ClientPrivateAes = aes;
        //}
        //public void SetClientAes(ClientData client, byte[] key, byte[] IV)
        //{
        //    client.ClientPrivateAes.Key = key;
        //    client.ClientPrivateAes.IV = IV;
        //}
    }
}
