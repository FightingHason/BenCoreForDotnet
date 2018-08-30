//************************************************
//Brief: List Extension
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/02/01 Created by Liuhaixia
//************************************************
using System;
using System.Collections.Generic;

namespace Ben.Core.Extension {
    public static class ListExtension {

        /// <summary>
        /// Remove the null of list
        /// </summary>
        public static void RemoveNull<T>(this List<T> list) {
            Int32 count = list.Count;
            for (Int32 i = 0; i < count; ++i) {
                if (list[i] == null) {
                    // Current Position
                    Int32 newCount = i++;
                    // Move to current position for the non-empty elements
                    for (; i < count; ++i) {
                        if (list[i] != null) {
                            list[newCount++] = list[i];
                        }
                    }
                    // Remove
                    list.RemoveRange(newCount, count - newCount);
                    break;
                }
            }
        }

    }// end class
}// end namespace