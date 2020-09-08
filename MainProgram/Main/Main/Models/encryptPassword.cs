using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Main.Models
{
    public static class encryptPassword
    {
        public static string textToEncrypt(string passWord)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(passWord)));
        }
    }
}