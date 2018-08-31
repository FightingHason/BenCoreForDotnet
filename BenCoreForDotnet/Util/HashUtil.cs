using System.Text;
using System.Security.Cryptography;

namespace Ben.Core.Util
{
    /// <summary>
    /// 计算hash值
    /// </summary>
    public class HashUtil
    {
        /// <summary>
        /// 计算SHA1
        /// </summary>
        /// <param name="data">字节数据</param>
        /// <returns>SHA1</returns>
        public static byte[] CalcSHA1(byte[] data)
        {
            SHA1 sha1 = SHA1.Create();
            return sha1.ComputeHash(data);
        }

        /// <summary>
        /// 计算MD5哈希(可能需要关闭FIPS)
        /// </summary>
        /// <param name="str">待计算的字符串</param>
        /// <returns>MD5结果</returns>
        public static string CalcMD5(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] data = Encoding.UTF8.GetBytes(str);
            byte[] hashData = md5.ComputeHash(data);
            StringBuilder sb = new StringBuilder(hashData.Length * 2);
            foreach (byte b in hashData)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 计算MD5哈希(第三方实现)
        /// </summary>
        /// <param name="str">待计算的字符串,避免FIPS-Exception</param>
        /// <returns>MD5结果</returns>
        public static string CalcMD5X(string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            LabMD5 md5 = new LabMD5();
            return md5.ComputeHash(data);
        }

    }
}
