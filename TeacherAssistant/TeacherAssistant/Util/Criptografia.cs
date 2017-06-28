using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace TeacherAssistant.Util
{
    public class Criptografia
    {
        public static string CriptografarMd5(string value)
        {
            byte[] hash;
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

            byte[] buffer = enc.GetBytes(value);

            hash = md5.ComputeHash(buffer);

            string hashS = "";

            foreach (byte b in hash)
            {
                hashS += b.ToString("x2");
            }

            return hashS;
        }
    }
}