using System;
using System.Security.Cryptography;
using System.Text;
namespace blog.Extension
{
    public static class HashPasswordMD5
    {
        public static string ToMD5(this string str)
        {
          MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bhash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sbHash = new StringBuilder();
            foreach (byte b in bhash)
            {
                sbHash.Append(String.Format("{0:x2}",b));
                
            }
            return sbHash.ToString();

        }
    }
}
