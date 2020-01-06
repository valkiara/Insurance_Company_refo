using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IBMS.Web.MVC.Security
{
    public class UtilityTools
    {
        public static string Encryptdata(string inputValue)
        {
            string encryptValue = string.Empty;
            byte[] encode = new byte[inputValue.Length];
            encode = Encoding.UTF8.GetBytes(inputValue);
            encryptValue = Convert.ToBase64String(encode);
            return encryptValue;
        }

        public static string Decryptdata(string inputValue)
        {
            string decryptValue = string.Empty;
            UTF8Encoding encodeValue = new UTF8Encoding();
            Decoder decode = encodeValue.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(inputValue);
            int charCount = decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptValue = new String(decoded_char);
            return decryptValue;
        }
    }
}