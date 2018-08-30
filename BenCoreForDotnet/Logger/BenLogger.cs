//************************************************
//Brief: Log Method
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/08/30 Created by Liuhaixia
//************************************************

namespace Ben.Core.Logger {
    public static class BenLogger {
        private static ILog _log = null;
        /// <summary>
        /// Set Log Object
        /// </summary>
        public static void SetLog(ILog iLog) {
            _log = iLog;
        }

        /// <summary>
        /// Debug
        /// </summary>
        public static void Debug(string format) {
            if (_log == null) return;
            _log.Debug(format);
        }

        /// <summary>
        /// Info
        /// </summary>
        public static void Info(string format) {
            if (_log == null) return;
            _log.Info(format);
        }

        /// <summary>
        /// Warn
        /// </summary>
        public static void Warn(string format) {
            if (_log == null) return;
            _log.Warn(format);
        }

        /// <summary>
        /// Error
        /// </summary>
        public static void Error(string format) {
            if (_log == null) return;
            _log.Error(format);
        }

    }// end class
}// end namespace