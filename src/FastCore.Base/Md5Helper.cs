using System.Text;

namespace FastCore.Base
{
    public class Md5Helper
    {
        public static string Encrypt(string pwd)
        {
            using System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            var arr = Encoding.Default.GetBytes(pwd + "fastcore");
            var output = md5.ComputeHash(arr);

            StringBuilder sb = new();
            for (int i = 0; i < output.Length; i++)
            {
                sb.Append(output[i].ToString("x"));
            }

            return sb.ToString().ToLower();
        }
    }
}
