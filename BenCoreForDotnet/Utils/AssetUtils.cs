//************************************************
//Brief: Asset Utils
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/08/22 Created by Liuhaixia
//************************************************
using UnityEngine;

namespace Ben.Core.Utils {
	public class AssetUtils {
		static int _unloadUnusedAssetsCount = 0;

		/// <summary>
		/// Unload unused assets
		/// </summary>
		public static void UnloadUnusedAssets(int addCount = 1, bool isUnload = true) {
			_unloadUnusedAssetsCount += addCount;
			if (isUnload && _unloadUnusedAssetsCount >= 18) {
				Resources.UnloadUnusedAssets();
				_unloadUnusedAssetsCount = 0;
			}
		}

	}// end class
}// end namespace