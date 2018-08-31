namespace Ben.Core.Data
{
    /// <summary>
    /// 账户访问控制(密钥)
    /// </summary>
    public class MacData
    {
        /// <summary>
        /// 密钥-AccessKey
        /// </summary>
        public string AccessKey { set; get; }

        /// <summary>
        /// 密钥-SecretKey
        /// </summary>
        public string SecretKey { set; get; }

        /// <summary>
        /// 初始化密钥AK/SK
        /// </summary>
        /// <param name="accessKey">AccessKey</param>
        /// <param name="secretKey">SecretKey</param>
        public MacData(string accessKey, string secretKey)
        {
            this.AccessKey = accessKey;
            this.SecretKey = secretKey;
        }
    }
}