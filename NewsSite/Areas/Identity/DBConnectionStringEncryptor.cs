using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace newsSite.Areas.Identity
{
    public class DBConnectionStringEncryptor
    {
        static string GetProcessorId()
        {
        //wmic
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();
            foreach (ManagementObject managObj in managCollec)
            {
                return managObj.Properties["processorID"].Value.ToString();
            }
            return null;
        }
        static byte[] pass1 = Encoding.UTF8.GetBytes(GetProcessorId()).Take(16).ToArray();
        static byte[] pass2 = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 10, 21, 22, 23, 24, 25, 26 };
        public static string Encrypt(string text)
        {
            var aes = Aes.Create();
            var key = aes.CreateEncryptor(pass1, pass2);
            MemoryStream mem = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(mem, key, CryptoStreamMode.Write);
            StreamWriter streamWriter = new StreamWriter(cryptoStream);
            streamWriter.Write(text);
            streamWriter.Close();
            return "Encrypted" + Convert.ToBase64String(mem.ToArray());
        }

        public static string Decrypt(string cyphertext)
        {
            cyphertext = cyphertext.Replace("Encrypted", "");
            var aes = Aes.Create();
            var key = aes.CreateDecryptor(pass1, pass2);
            MemoryStream mem = new MemoryStream(Convert.FromBase64String(cyphertext));
            CryptoStream cryptoStream = new CryptoStream(mem, key, CryptoStreamMode.Read);
            StreamReader streamReader = new StreamReader(cryptoStream);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            return text;
        }

    }
}
