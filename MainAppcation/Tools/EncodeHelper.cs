using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NetProxyController.Tools
{
    internal static class EncodeHelper
    {
        public static string GetMD5(string str)
        {
            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            byte[] byteNew = MD5.HashData(byteOld);
            StringBuilder sb = new(32);
            foreach (byte b in byteNew)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        public static bool TryConvertFromBase64(string base64Text, out string convertOutput)
        {
            convertOutput = string.Empty;
            try
            {
                byte[] decodedBytes = Convert.FromBase64String(base64Text);
                convertOutput = Encoding.UTF8.GetString(decodedBytes);
                return base64Text.Equals(Convert.ToBase64String(Encoding.UTF8.GetBytes(convertOutput)));
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
