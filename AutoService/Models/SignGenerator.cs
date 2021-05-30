using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AutoService.Models
{
    public static class SignGenerator
    {
        private static MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider(); //генерирем хеш данных при регистрации или авторизации

        public static string GenerateSign(string input)
        {
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input + "AutoService"));

            return BitConverter.ToString(hashBytes).ToLower().Replace("-", "");
        }
    }
}