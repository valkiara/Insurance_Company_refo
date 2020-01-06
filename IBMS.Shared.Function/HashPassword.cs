using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.Function
{
    public static class HashPassword
    {
        public static string Genaratehash(string inputValue)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.Unicode.GetBytes(inputValue);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public static bool CompareHash(string inputValue, string hashValue)
        {
            string generatedValue = Genaratehash(inputValue);

            if (hashValue.Equals(generatedValue))
            {
                return true;
            }
            else
            {
                return false; ;
            }
        }
    }
}
