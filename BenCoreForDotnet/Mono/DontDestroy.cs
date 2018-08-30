//************************************************
//Brief: Dont Destroy on load scene
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2017/10/23 Created by Liuhaixia
//************************************************
using UnityEngine;

namespace Ben.Core.Mono {
    public class DontDestroy : MonoBehaviour {
        void Awake() {
            DontDestroyOnLoad(gameObject);
        }

    }// end class
}// end namespace