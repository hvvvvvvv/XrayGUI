using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using static Vanara.PInvoke.User32;

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
        public static bool TryDecodeFromUrlCode(string urlCodeText, out string decodedText)
        {
            decodedText = string.Empty;
            string pattern = @"(%[0-9A-Fa-f]{2})+";
            MatchCollection matches = Regex.Matches(urlCodeText, pattern);
            var ret = matches.Count == urlCodeText.Length;
            try
            {
                if (ret)
                {
                    decodedText = HttpUtility.UrlDecode(urlCodeText);
                }
                return ret;
            }
            catch(Exception) { return false; }
        }
    }
}
