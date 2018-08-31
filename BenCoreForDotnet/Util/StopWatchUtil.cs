//************************************************
//Brief: StopWatch Utils
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2016/08/23 Created by Liuhaixia
//************************************************
using System.Diagnostics;
using Ben.Core.Logger;

namespace Ben.Core.Util {
    public class StopWatchUtil {

        #region Singleton

        static StopWatchUtil _instance = null;
        public static StopWatchUtil Inst {
            get {
                if (_instance == null) {
                    _instance = new StopWatchUtil();
                }
                return _instance;
            }
        }

        #endregion

        Stopwatch _stopwatch = null;

        StopWatchUtil() {
            _stopwatch = new Stopwatch();
        }

        public void Start() {
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        /// <summary>
        /// Stop By Tag
        /// </summary>
        public void Stop(string tag) {
            _stopwatch.Stop();
            if (string.IsNullOrEmpty(tag)) {
                BenLogger.Debug("StopWatch Print ElapsedMillseconds: " + _stopwatch.ElapsedMilliseconds);
            } else {
                BenLogger.Debug("Tag: " + tag + " | StopWatch Print ElapsedMillseconds: " + _stopwatch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// First Stop, Second Start
        /// </summary>
        public void StopAndStart(string tag) {
            Stop(tag);
            Start();
        }

        //public long ElapseTicks()
        //{
        //    return _stopwatch.ElapsedTicks;
        //}

        /// <summary>
        /// Destroy Instance
        /// </summary>
        public void Destroy() {
            _stopwatch = null;
            _instance = null;
        }

    }// end class
}// end namespace