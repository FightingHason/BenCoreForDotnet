using System;

namespace Ben.Core.Net.Http
{
    class HttpException :Exception
    {
        public string message;
        public HttpResult HttpResult;
        public HttpException(HttpResult httpResult, string message)
        {
            this.HttpResult = httpResult == null ? new HttpResult() : httpResult;
            this.message = message;
        }

        public override string Message
        {
            get
            {
                return this.message;
            }
        }
    }
}
