using Ben.Core.Net;

namespace Ben.Core.Net.Http
{
    /// <summary>
    /// HTTP 内容类型(Content-Type)
    /// </summary>
    public class ContentType
    {
        /// <summary>
        /// 资源类型：普通文本
        /// </summary>
        public const string TEXT_PLAIN = NetConst.TEXT_PLAIN;

        /// <summary>
        /// 资源类型：JSON字符串
        /// </summary>
        public const string APPLICATION_JSON = NetConst.APPLICATION_JSON;

        /// <summary>
        /// 资源类型：未知类型(数据流)
        /// </summary>
        public const string APPLICATION_OCTET_STREAM = NetConst.APPLICATION_OCTET_STREAM;

        /// <summary>
        /// 资源类型：表单数据(键值对)
        /// </summary>
        public const string WWW_FORM_URLENC = NetConst.WWW_FORM_URLENC;

        /// <summary>
        /// 资源类型：多分部数据
        /// </summary>
        public const string MULTIPART_FORM_DATA = NetConst.MULTIPART_FORM_DATA;
    }
}
