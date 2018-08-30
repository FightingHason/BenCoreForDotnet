//************************************************
//Brief: Directory const
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/01/18 Created by Liuhaixia
//************************************************
using System;

namespace Ben.Core.Const {
    public class DirectoryConst {

        public const String CACHE_ROOT = "robot/";

        public const String UI = "UI/";
        public const String CLIP = "Clip/";
        public const String OTHER = "Other/";
        public const String TABLE = "Table/";
        public const String EFFECT = "Effect/";
        public const String PREFABS = "Prefabs/";
        public const String TEXTURES = "Textures/";
        public const String MESH_BONE = "MeshBone/";
        public const String ANIMATION = "Animation/";
        public const String CHARACTER = "Character/";
        public const String ASSETBUNDLES = "AssetBundles/";

        // Combine
        public const String ANIMATION_CLIP = ANIMATION + CLIP;
        public const String ANIMATION_MESH_BONE = ANIMATION + MESH_BONE;
        public const String PREFABS_UI = PREFABS + UI;

    }// end class
}// end namespace