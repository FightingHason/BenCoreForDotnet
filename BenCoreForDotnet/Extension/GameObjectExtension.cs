//************************************************
//Brief: Transform Extension
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/08/16 Created by Liuhaixia
//************************************************
using UnityEngine;

namespace Ben.Core.Extension {
    public static class GameObjectExtension {

        /// <summary>
        /// Get Or Add GameObject Component
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject target) where T : MonoBehaviour {
            T script = target.GetComponent<T>();
            if (script == null) {
                script = target.AddComponent<T>();
            }
            return script;
        }

    }// end class
}// end namespace