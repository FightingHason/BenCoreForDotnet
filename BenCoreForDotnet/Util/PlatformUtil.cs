//************************************************
//Brief: File Utils
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2017/04/17 Created by Liuhaixia
//************************************************
using Ben.Core.Const;
using Ben.Core.Enum;

namespace Ben.Core.Util {
    public class PlatformUtil {

        /// <summary>
        /// Get Platform Relative Root Path
        /// </summary>
        public static string GetPlatformRelativeRootPath() {
            PlatformEnum currentPlatform = PlatformEnum.Unknown;
#if UNITY_ANDROID
            currentPlatform = PlatformEnum.Android;
#elif UNITY_IOS
            currentPlatform = PlatformEnum.IOS;
#elif UNITY_STANDALONE_WIN
            currentPlatform = PlatformEnum.Windows;
#elif UNITY_STANDALONE_LINUX
            currentPlatform = PlatformEnum.Linux;
#elif UNITY_STANDALONE_OSX
            currentPlatform = PlatformEnum.Mac;
#else
            currentPlatform = PlatformEnum.Unknown;
#endif
            return DirectoryConst.CACHE_ROOT + currentPlatform.ToString() + CharConst.STR_SLASH;
        }

    }// end class
}// end namespace