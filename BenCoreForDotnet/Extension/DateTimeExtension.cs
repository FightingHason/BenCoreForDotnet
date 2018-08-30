//************************************************
//Brief: DateTime Extension
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2017/05/04 Created by Liuhaixia
//************************************************
using System;

namespace Ben.Core.Extension {
    public static class DateTimeExtension {

        const String TIME_FORMAT_1_DEFAULT = "00:00:00";
        const String TIME_FORMAT_1 = "{0}:{1}:{2}";
        const String TIME_FORMAT_CN = "{0}天{1}时{2}分{3}秒";
        const String TIME_FORMAT_DEFAUT = "yyyy-MM-dd HH:mm:ss";
        static readonly DateTime START_TIME = new DateTime(1970, 1, 1, 8, 0, 0, DateTimeKind.Local);

        /// <summary>
        /// MillsSecond convert to DateTime
        /// </summary>
        public static DateTime ToDateTime(this Int64 ms) {
            return START_TIME.AddMilliseconds(ms);
        }

        /// <summary>
        /// Check date distance
        /// </summary>
        public static Int32 CheckDateDistance(DateTime after, DateTime before) {
            if (after.CompareTo(before) == 0) {
                return 0;
            }

            var timeDist = after - before;
            Int32 result = (Int32)Math.Floor(timeDist.TotalSeconds);

            DateTime tempAfter = new DateTime(2000, 1, 1, after.Hour, after.Minute, after.Second);
            DateTime tempBefore = new DateTime(2000, 1, 1, before.Hour, before.Minute, before.Second);

            if (tempBefore > tempAfter) {
                result++;
            }
            return result;
        }

        /// <summary>
        /// Check date valid
        /// </summary>
        public static Boolean CheckDateValid(DateTime currentDate, DateTime startDate, DateTime endDate) {
            return (currentDate.CompareTo(startDate) >= 0) && (currentDate.CompareTo(endDate) <= 0);
        }


        #region Time to string

        /// <summary>
        /// String time to string
        /// </summary>
        public static String TimeToString(this String msString) {

            Int64 ms = 0;
            try {
                ms = Convert.ToInt64(msString);
            } catch {
                ms = 0;
            }

            Int32 hour = (Int32)(ms / (3600 * 1000));
            Int32 min = (Int32)(ms % (3600 * 1000) / (60 * 1000));
            Int32 sec = (Int32)(ms % (3600 * 1000) % (60 * 1000));
            return String.Format(TIME_FORMAT_1, hour, min, sec);
        }

        /// <summary>
        /// Int time to string
        /// </summary>
        public static String TimeToString(this Int32 seconds) {
            Int32 hour = (Int32)(seconds / 3600);
            Int32 min = (Int32)(seconds % 3600 / 60);
            Int32 sec = (Int32)(seconds % 3600 % 60);
            return String.Format(TIME_FORMAT_1, hour.ToString("D2"), min.ToString("D2"), sec.ToString("D2"));
        }

        /// <summary>
        /// Long time to string
        /// </summary>
        public static String TimeToString(this Int64 seconds) {
            Int32 hour = (Int32)(seconds / 3600);
            Int32 min = (Int32)(seconds % 3600 / 60);
            Int32 sec = (Int32)(seconds % 3600 % 60);
            return String.Format(TIME_FORMAT_1, hour.ToString("D2"), min.ToString("D2"), sec.ToString("D2"));
        }

        /// <summary>
        /// Int time to string for CN format
        /// </summary>
        public static String TimeToStringCN(this Int32 seconds) {
            Int32 day = seconds / (24 * 60 * 60);
            Int32 hour = seconds % (24 * 60 * 60) / 3600;
            Int32 min = seconds % (24 * 60 * 60) % 3600 / 60;
            Int32 sec = seconds % (24 * 60 * 60) % 3600 % 60;
            return String.Format(TIME_FORMAT_CN, day, hour, min, sec);
        }

        /// <summary>
        /// Long time to string for CN format
        /// </summary>
        public static String TimeToStringCN(this Int64 seconds) {
            Int32 day = (Int32)(seconds / (24 * 60 * 60));
            Int32 hour = (Int32)(seconds % (24 * 60 * 60) / 3600);
            Int32 min = (Int32)(seconds % (24 * 60 * 60) % 3600 / 60);
            Int32 sec = (Int32)(seconds % (24 * 60 * 60) % 3600 % 60);
            return String.Format(TIME_FORMAT_CN, day, hour, min, sec);
        }

        /// <summary>
        /// Millisecond to string
        /// </summary>
        public static String TimeToStringDefault(this Int64 ms) {
            return ms.ToDateTime().ToString(TIME_FORMAT_DEFAUT);
        }

        #endregion


        /// <summary>
        /// Remian millisecond to now
        /// </summary>
        public static String RemainTimeString(this Int64 ms) {
            DateTime now = DateTime.Now, targetTime = START_TIME.AddMilliseconds(ms);
            String result = TIME_FORMAT_1_DEFAULT;
            if (targetTime > now) {
                TimeSpan timeSpan = targetTime - now;
                result = String.Format(TIME_FORMAT_1, timeSpan.Hours.ToString("D2"), timeSpan.Minutes.ToString("D2"), timeSpan.Seconds.ToString("D2"));
            }

            return result;
        }

        /// <summary>
        /// Remian millisecond to now
        /// </summary>
        public static Int64 RemainTimeLong(this Int64 ms) {
            DateTime now = DateTime.Now, targetTime = START_TIME.AddMilliseconds(ms);
            Int64 result = 0;
            if (targetTime > now) {
                result = (targetTime - now).Milliseconds;
            }
            return result;
        }

        /// <summary>
        /// Check time expire
        /// </summary>
        public static Boolean CheckTimeExpire(this Int64 ms) {
            DateTime now = DateTime.Now;
            DateTime targetTime = START_TIME.AddMilliseconds(ms);
            if (targetTime > now) {
                return false;
            } else {
                return true;
            }
        }

        /// <summary>
        /// Get current time second
        /// </summary>
        public static Int64 CurrentTimeSecond() {
            return DateTime.Now.Second + DateTime.Now.Minute * 60 + DateTime.Now.Hour * 3600;
        }

        /// <summary>
        /// Get current time millisecond
        /// </summary>
        public static Int64 CurrentTimeMillisecond() {
            return DateTime.Now.Hour * 3600000 + DateTime.Now.Minute * 60000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        }

    }// end class
}// end namespace