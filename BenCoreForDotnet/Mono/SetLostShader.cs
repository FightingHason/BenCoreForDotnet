//************************************************
//Brief: Reset Lost Shader
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2016/11/23 Created by Liuhaixia
//************************************************
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ben.Core.Mono {
    public class SetLostShader : MonoBehaviour {

        Boolean _bInitFinish = false;

        static HashSet<String> _lostShaderList = new HashSet<String>();

        public static void Init(HashSet<String> lostShaderList) {
            if (lostShaderList != null) {
                _lostShaderList = lostShaderList;
            }
        }

        // Use this for initialization
        void Start() {
            if (_bInitFinish == false) {
                _SetShader(transform);
                _bInitFinish = true;
            }
        }

        /// <summary>
        /// Reset shader
        /// </summary>
        void _SetShader(Transform mTran) {
            for (Int32 i = 0; i < mTran.childCount; i++) {
                Renderer render = mTran.GetChild(i).GetComponent<Renderer>();

                if (render != null) {
                    Material[] matArray = render.materials;
                    for (Int32 k = 0; k < matArray.Length; ++k) {
                        if (_lostShaderList.Contains(matArray[k].shader.name)) {
                            Shader shader = Shader.Find(matArray[k].shader.name);
                            if (shader != null) {
                                matArray[k].shader = shader;
                            }
                        }
                    }
                }

                _SetShader(mTran.GetChild(i));
            }
        }

        /// <summary>
        /// Excute reset shader
        /// </summary>
        public void Excute() {
            if (_bInitFinish == false) {
                Start();
            } else {
                _SetShader(transform);
            }
        }

    }// end class
}// end namespace