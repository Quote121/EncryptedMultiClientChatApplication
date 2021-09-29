using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ServerForm
{
    public class User
    {
        // Client socket object
        public Socket ClientSocket { get; set; }
        // Client chosen name
        public string Name { get; set; }


        // Rsa public key for 1st stage encryption
        public byte[] RsaPublicMod { get; set; }
        public byte[] RsaPublicExp { get; set; }
        
        // Private server key genereated for each client
        public RSA RsaPrivateServerKey { get; set; }
        
        // Aes private key for continous data 
        public byte[] AesPrivateKey { get; set; }

        public byte[] AesPrivateIV { get; set; }





        // Input shizzle
        public User(Socket clientSocket)
        {
            ClientSocket = clientSocket;
        }
        // RSA
        public User(RSA rsaPrivateServerKey, byte[] rsaPublicMod, byte[] rsaPublicExp)
        {
            RsaPrivateServerKey = rsaPrivateServerKey;
            RsaPublicMod = rsaPublicMod;
            RsaPublicExp = rsaPublicExp;
        }
        //AES
        public User(byte[] aesPrivateKey, byte[] aesPrivateIV)
        {
            AesPrivateKey = aesPrivateKey;
            AesPrivateIV = aesPrivateIV;
        }
        // Everything
        public User(Socket clientSocket, string name, byte[] rsaPublicMod, byte[] rsaPublicExp, RSA rsaPrivateServerKey, byte[] aesPrivateKey, byte[] aesPrivateIV)
        {
            ClientSocket = clientSocket;
            Name = name;
            RsaPublicMod = rsaPublicMod;
            RsaPublicExp = rsaPublicExp;
            RsaPrivateServerKey = rsaPrivateServerKey;
            AesPrivateKey = aesPrivateKey;
            AesPrivateIV = aesPrivateIV;
        }
    }
    public class Users
    {
        // = new() at end to enitilise the list
        public List<User> UserDataList { get; private set; } = new();

        public void AddUser(User user)
        {
            UserDataList.Add(user);
        }
        // Could change return types to bools/string to indicate success
        public void RemoveUserByName(string userName)
        {
            // Remove object from list if the name matches object data

            // Add try catch for invalid dat
            UserDataList.Remove(UserDataList.Single(a => a.Name == userName));
        }
        public void RemoveUserBySocket(Socket client)
        {
            UserDataList.Remove(UserDataList.Single(a => a.ClientSocket == client));
        }
        public void EditUserName(Socket Client, string userName)
        {
            foreach (User user in UserDataList)
            {
                if (user.ClientSocket == Client)
                    // Update name through socket id
                    user.Name = userName;
            }
        }
        public void RemoveAllUsers()
        {
            foreach (User u in UserDataList)
            {
                UserDataList.Remove(u);
            }
        }
        public bool UserLoggedIn(string name)
        {
            foreach (User user in UserDataList)
            {
                if (user.Name == name)
                {
                    return true;
                }
            }
            // There are no users logged in with that name
            return false;
        }


        #region RSA Setters
        /// <summary>
        /// RSA
        /// </summary>
        public void AddRSAPrivate(Socket Client, RSA rsaKey)
        {
            // For each user if the socket matches add the rsa key to the entry
            foreach (User user in UserDataList)
            {
                if (user.ClientSocket == Client)
                {
                    user.RsaPrivateServerKey = rsaKey;
                }
            }
        }
        public void AddRSAPublic(Socket Client, byte[] RsaPubMod, byte[] RsaPubExp)
        {
            foreach (User user in UserDataList)
            {
                if (user.ClientSocket == Client)
                {
                    // Add mod and exp to the entry
                    user.RsaPublicMod = RsaPubMod;
                    user.RsaPublicExp = RsaPubExp;
                }
            }
        }
        /// <summary>
        /// RSA END
        /// </summary>
        #endregion
        #region AES Setters
        public void AddAESPrivate(Socket Client, byte[] AesKey, byte[] AesIV)
        {
            foreach (User user in UserDataList)
            {
                if (user.ClientSocket == Client)
                {
                    // Set AES key and IV to user entry
                    user.AesPrivateKey = AesKey;
                    user.AesPrivateIV = AesIV;
                }
            }
        }

        #endregion

        public User GetUser(Socket socket)
        {
            foreach (User u in UserDataList)
            {
                if (u.ClientSocket == socket)
                {
                    return u;
                }
                // Handle this shit
                
            }
            return null;
        }

        // Get username of user with socket
        public string GetUserName(Socket socket)
        {
            foreach (User u in UserDataList)
            {
                if (u.ClientSocket == socket)
                {
                    return u.Name;
                }
                return null;
            }
            return null;
        }

        // Gets list of users that have encryption enabled
        public List<User> UsersWithEncryption()
        {
            List<User> UserListWithEncryption = new();
            foreach (User u in UserDataList)
            {
                // If user has a AES key and iv then add to list and return list at end
                if (u.AesPrivateKey != null && u.AesPrivateIV != null)
                {
                    UserListWithEncryption.Add(u);
                }
            }
            return UserListWithEncryption;
        }

        public List<Socket> GetSockets()
        {
            List<Socket> SocketList = new();

            // Go through users and append all sockets to list and return
            foreach (User u in UserDataList)
            {
                SocketList.Add(u.ClientSocket);
            }

            return SocketList;
        }

        /// <summary>
        /// Pass username into method and the method will return socket or null if not found
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Socket GetSocketWithUserName(string userName)
        {
            foreach (User user in UserDataList)
            {
                if (user.Name == userName)
                {
                    // return socket
                    return user.ClientSocket;
                }
            }
            // not found
            return null;
        }
    }
}