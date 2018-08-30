//************************************************
//Brief: String holder
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2016/09/18 Created by Liuhaixia
//************************************************
using UnityEngine;

// This class holds a string array and can be saved
// as an asset. This way MonoBehaviours can reference
// it, or it can be added to an assetbundle, making this
// a convenient way of storing procedurally generated
// data on editor time and accessing it at runtime.
namespace Ben.Core.Common {
    public class StringHolder : ScriptableObject {
        public string[] content;
        public StringHolder(string[] content) {
            this.content = content;
        }

    }// end class
}// end namespace