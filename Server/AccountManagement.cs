using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;


namespace ServerForm
{
    class AccountManagement
    {
        private readonly string AccountsFilePath = @"C:\Users\mattb\source\repos\Encryption\ServerClient\ServerForm\Accounts.csv";
        public bool CheckLogin(string username, string password)
        {
            using (var reader = new StreamReader(AccountsFilePath))
            {
                // Catogroise valies
                List<string> names = new();
                List<string> passwordHashes = new();
                List<string> salts = new();

                // Read whole file and put data into listts
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(",");

                    names.Add(values[0]);
                    passwordHashes.Add(values[1]);
                    salts.Add(values[2]);
                }
                // Chech for asked username
                foreach (string name in names)
                {
                    if (name == username)
                    {
                        int index = names.IndexOf(name);

                        // Create a password hash with supplied pass and salt
                        string passwordHashToCheck = StringToHash($"{password}{salts[index]}");
                        // Check if the created password hash matches the one in record
                        if (passwordHashes[index].ToLower() == passwordHashToCheck.ToLower())
                        {
                            // Username is goodToGo, can be assigned to user
                            return true;
                        }
                    }
                }
                // Username or password where incorrect
                return false;
            }
        }




        public bool AccountNameExists(string accountName)
        {
            foreach(String line in File.ReadAllLines(AccountsFilePath))
            {
                string[] values = line.Split(",");
                if (values[0] == accountName)
                    return true;

            }
            // Account name does not exist
            return false;
        }
        public bool AccountCreateUser(string userName, string password)
        {
            string salt = SaltCreate512();
            string hashedPassword = StringToHash($"{password}{salt}");
            string data = $"{userName},{hashedPassword},{salt}\n";

            try
            {
                File.AppendAllText(AccountsFilePath, data);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
                return false;
            }
        }

        private string StringToHash(string input)
        {
            StringBuilder SB = new();
            foreach (byte b in GetHash(input))
                SB.Append(b.ToString("x2"));
            return SB.ToString();
        }

        private static byte[] GetHash(string inputString) // Used by the GetHashString function
        {
            using HashAlgorithm algorithm = SHA512.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
        private string SaltCreate512()
        {
            Random rand = new();
            StringBuilder SB = new();
            string acceptableCharacters = ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890");
            for (int i=0; i < 64; i++)
            {
                SB.Append(acceptableCharacters[rand.Next(0, acceptableCharacters.Length)]);
            }
            return SB.ToString();
        }
     }
}