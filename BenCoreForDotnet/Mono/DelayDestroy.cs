//************************************************
//Brief: Delay Delete MonoBehaviour
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2016/12/05 Created by Liuhaixia
//************************************************
using System;
using UnityEngine;

namespace Ben.Core.Mono {
    [Serializable]
    public class DelayDestroy : MonoBehaviour {

        [SerializeField]
        float _destroyDelayTime = 2.5F;
        Action _deleteCallback = null;
        Action<string> _deleteCallbackByName = null;

        // Use this for initialization
        void Start() {
            Invoke("_Delete", _destroyDelayTime);
        }

        void _Delete() {
            if (_deleteCallback != null) {
                _deleteCallback();
            }
            if (_deleteCallbackByName != null && this.gameObject != null) {
                _deleteCallbackByName(this.gameObject.name);
            }

            UnityEngine.Object.Destroy(this.gameObject);
        }

        /// <summary>
        /// Add callback
        /// </summary>
        public void AddCallback(Action callback) {
            _deleteCallback += callback;
        }

        /// <summary>
        /// Add callback
        /// </summary>
        public void AddCallback(Action<string> callback) {
            _deleteCallbackByName += callback;
        }

    }// end class
}// end namespace