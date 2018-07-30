using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRSubscribeServer.Model
{
    public static class Base64Url
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Encode(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Decode(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
        private static string Encode(byte[] arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException("arg");
            }
            var s = Convert.ToBase64String(arg);
            return s
                .Replace("=", "")
                .Replace("/", "_")
                .Replace("+", "-");
        }

        private static string ToBase64(string arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException("arg");
            }
            var s = arg
                .PadRight(arg.Length + (4 - arg.Length % 4) % 4, '=')
                .Replace("_", "/")
                .Replace("-", "+");
            return s;
        }

        private static byte[] Decode(string arg)
        {
            var decrypted = ToBase64(arg);
            return Convert.FromBase64String(decrypted);
        }
    }
}
