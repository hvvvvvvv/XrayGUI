using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Vanara.PInvoke;
using static System.Net.Mime.MediaTypeNames;
using static Vanara.PInvoke.User32;

namespace XrayGUI.Tools
{
    public static class EncodeHelper
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
        public static string DecodeBase64(string inputText)
        {
            inputText = inputText.Trim()
                  .Replace(Environment.NewLine, "")
                  .Replace("\n", "")
                  .Replace("\r", "")
                  .Replace('_', '/')
                  .Replace('-', '+')
                  .Replace(" ", "");
            if (inputText.Length % 4 > 0)
            {
                inputText = inputText.PadRight(inputText.Length + 4 - inputText.Length % 4, '=');
            }
            byte[] decodedBytes = Convert.FromBase64String(inputText);          
            string ret = Encoding.UTF8.GetString(decodedBytes);
            if(!inputText.Equals(Convert.ToBase64String(Encoding.UTF8.GetBytes(ret))))
            {
                throw new Exception();
            }
            return ret;
        }
        public static bool TryConvertFromBase64(string base64Text, out string convertOutput)
        {
            convertOutput = string.Empty;
            try
            {
                convertOutput = DecodeBase64(base64Text);
                return true;
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
        public static string GetClipboardText()
        {
            string ret = string.Empty;
            if (OpenClipboard(IntPtr.Zero))
            {
                var clipData = GetClipboardData(13);
                var pData = Kernel32.GlobalLock(clipData);
                var len = Kernel32.GlobalSize(pData);
                if (len > 2)
                {
                    len -= 2;
                    var buffer = new byte[len];
                    Marshal.Copy(pData, buffer, 0, len);
                    ret = Encoding.Unicode.GetString(buffer);
                }
                Kernel32.GlobalUnlock(clipData);
                CloseClipboard();
            }
            return ret;
        }
    }
}
