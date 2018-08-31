//************************************************
//Brief: Net const
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/02/01 Created by Liuhaixia
//************************************************

namespace Ben.Core.Net {
    class NetConst {

        #region Http ContentType

        /// <summary>
        /// 资源类型：普通文本
        /// </summary>
        public const string TEXT_PLAIN = "text/plain";

        /// <summary>
        /// 资源类型：JSON字符串
        /// </summary>
        public const string APPLICATION_JSON = "application/json";

        /// <summary>
        /// 资源类型：未知类型(数据流)
        /// </summary>
        public const string APPLICATION_OCTET_STREAM = "application/octet-stream";

        /// <summary>
        /// 资源类型：表单数据(键值对)
        /// </summary>
        public const string WWW_FORM_URLENC = "application/x-www-form-urlencoded";

        /// <summary>
        /// 资源类型：多分部数据
        /// </summary>
        public const string MULTIPART_FORM_DATA = "multipart/form-data";

        #endregion

        #region Header

        /// <summary>
        /// Header - Authorization
        /// </summary>
        public const string HEADER_AUTHORIZATION = "Authorization";

        #endregion

        #region Request Method

        /// <summary>
        /// Request Method - GET
        /// </summary>
        public const string METHOD_GET = "GET";

        /// <summary>
        /// Request Method - POST
        /// </summary>
        public const string METHOD_POST = "POST";

        #endregion

        #region Data Type

        /// <summary>
        /// Type - Bin
        /// </summary>
        public const string DATA_TYPE_BIN = "BIN";

        /// <summary>
        /// Type - JSON
        /// </summary>
        public const string DATA_TYPE_JSON = "JSON";

        /// <summary>
        /// Type - TEXT
        /// </summary>
        public const string DATA_TYPE_TEXT = "TEXT";

        /// <summary>
        /// Type - FORM
        /// </summary>
        public const string DATA_TYPE_FORM = "FORM";

        /// <summary>
        /// Type - Multipart
        /// </summary>
        public const string DATA_TYPE_MULTIPART = "MPART";

        #endregion

    }// end class
}// end namespace
