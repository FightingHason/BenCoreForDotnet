//************************************************
//Brief: Transform Extension
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/03/07 Created by Liuhaixia
//************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Ben.Core.Logger;
using UnityEngine;

namespace Ben.Core.Extension {
    public static class TransformExtension {

        /// <summary>
        /// Find child by name
        /// </summary>
        public static Transform FindChildByName(this Transform trans, String childName) {
            int count = trans.childCount;
            while (count > 0) {
                count--;
                if (trans.GetChild(count).name.Equals(childName)) {
                    return trans.GetChild(count);
                } else {
                    Transform temp = FindChildByName(trans.GetChild(count), childName);
                    if (temp != null) {
                        return temp;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Reset for standard
        /// </summary>
        public static void ResetStandard(this Transform trans) {
            if (trans) {
                if (trans.localPosition != Vector3.zero) {
                    trans.localPosition = Vector3.zero;
                }
                if (trans.localRotation != Quaternion.identity) {
                    trans.localRotation = Quaternion.identity;
                }
                if (trans.localScale != Vector3.one) {
                    trans.localScale = Vector3.one;
                }
            }
        }

        /// <summary>
        /// Reset for standard by parent transform
        /// </summary>
        public static void ResetStandard(this Transform trans, Transform parentTrans) {
            if (trans) {
                trans.SetParent(parentTrans);
                trans.ResetStandard();
            }
        }

        /// <summary>
        /// Delete all childrens
        /// </summary>
        public static void DestroyChildrens(this Transform trans) {
            if (trans != null) {
                int childCount = trans.childCount;
                for (int i = childCount - 1; i >= 0; i--) {
                    GameObject.DestroyImmediate(trans.GetChild(i).gameObject);
                }
            }
        }

        /// <summary>
        /// Get Selection GameObject MeshRenderer Bones
        /// </summary>
        public static Transform[] GetMeshRendererBones(this Transform trans, Transform skinRootTrans) {
            List<Transform> resultBones = new List<Transform>();
            if (trans != null) {
                List<Transform> smrBones = new List<Transform>(), aloneBones = new List<Transform>(), bodyBones = new List<Transform>();
                // All SkinnedMeshRenderer
                foreach (SkinnedMeshRenderer smr in trans.GetComponentsInChildren<SkinnedMeshRenderer>(true)) {
                    smrBones.AddRange(smr.bones);
                }
                smrBones.RemoveNull();
                smrBones = smrBones.Distinct().ToList();
                // Child index
                List<int> childIndexList = new List<int>();
                for (int i = 0; i < smrBones.Count; ++i) {
                    for (int j = 0; j < smrBones.Count; ++j) {
                        if (i != j) {
                            if (smrBones[i] == smrBones[j].parent) {
                                childIndexList.Add(j);
                            }
                        }
                    }
                }
                // Remove the child of the parent in the bonelist
                for (int i = childIndexList.Count - 1; i >= 0; --i) {
                    smrBones.RemoveAt(i);
                }
                // Find the node of bip
                foreach (Transform bone in smrBones) {
                    bool bodyBip = false, aloneBone = false;
                    Transform temp = bone;

                    while (true) {
                        if (temp.name.Equals("Bip001")) {
                            bodyBip = true;
                            break;
                        }
                        // Alone bone root transform
                        if (temp.parent == skinRootTrans) {
                            aloneBone = true;
                            break;
                        }
                        if (temp.parent == null) {
                            BenLogger.Error("Parent is null!");
                            break;
                        }

                        temp = temp.parent;
                    }
                    if (bodyBip) {
                        bodyBones.Add(bone);
                    } else {
                        if (aloneBone) {
                            aloneBones.Add(temp);
                        } else {
                            BenLogger.Debug("Bone < " + bone.name + " > Top Parent is null!");
                        }
                    }
                }
                bodyBones = bodyBones.Distinct().ToList();
                aloneBones = aloneBones.Distinct().ToList();
                resultBones.AddRange(bodyBones);
                resultBones.AddRange(aloneBones);
            }
            return resultBones.ToArray();
        }

    }// end class
}// end namespace