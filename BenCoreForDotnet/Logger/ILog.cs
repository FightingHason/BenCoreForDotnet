//************************************************
//Brief: Log Interface
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/08/30 Created by Liuhaixia
//************************************************

namespace Ben.Core.Logger {
    public interface ILog {
        /// <summary>
        /// Debug
        /// </summary>
        void Debug(string format);

        /// <summary>
        /// Info
        /// </summary>
        void Info(string format);

        /// <summary>
        /// Warn
        /// </summary>
        void Warn(string format);

        /// <summary>
        /// Error
        /// </summary>
        void Error(string format);

    }
}// end namespace