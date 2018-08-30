//************************************************
//Brief: Delay display
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2016/12/05 Created by Liuhaixia
//************************************************
using UnityEngine;

namespace Ben.Core.Mono {
    [System.Serializable]
    public class DelayDisplay : MonoBehaviour {

        [SerializeField]
        float _displayDelayTime = 1.0f;

        void Awake() {
            gameObject.SetActive(false);
        }

        // Use this for initialization
        void Start() {
            Invoke("_Display", _displayDelayTime);
        }

        void _Display() {
            gameObject.SetActive(true);
        }

    }// end class
}// end namespace