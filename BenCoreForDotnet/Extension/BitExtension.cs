//************************************************
//Brief: Bit Extension
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/04/23 Created by Liuhaixia
//************************************************
using System;
using System.Collections;

namespace Gowild.Core.Extension {
    public static class BitExtension {

        /// <summary>
        /// Sub BityArray
        /// </summary>
        public static BitArray SubBitArray(this BitArray sourceArray, int sourceStartIndex, int destinationLength) {
            var destinationIndex = 0;
            var destinationBitArray = new BitArray(destinationLength, false);
            for (var i = sourceStartIndex; i < sourceArray.Length; ++i) {
                if ( destinationIndex >= destinationLength ) {
                    break;
                }
                destinationBitArray[destinationIndex++] = sourceArray[i];
            }

            return destinationBitArray;
        }

        /// <summary>
        /// Copy BityArray
        /// </summary>
        public static BitArray Copy(this BitArray sourceArray, int sourceStartIndex, BitArray updateArray, int updateLength) {
            for (var i = sourceStartIndex; i < sourceArray.Length; ++i) {
                if ( (i-sourceStartIndex) >= updateLength )  {
                    break;
                }
                sourceArray[i] = updateArray[i-sourceStartIndex];
            }
            return sourceArray;
        }

        /// <summary>
        /// Get BitArray Value(Only: Int, Byte, bool)
        /// </summary>
        public static T GetValue<T>(this BitArray sourceArray) {
            var destinationArray = new T[1];
            sourceArray.CopyTo(destinationArray, 0);
            return destinationArray[0];
        }

        /// <summary>
        /// Get BitArray Long Value
        /// </summary>
        public static long GetValueLong(this BitArray sourceArray) {
            var destinationByteArray = new byte[sizeof(long)];
            sourceArray.CopyTo(destinationByteArray, 0);
            return BitConverter.ToInt64(destinationByteArray,0);
        }

        /// <summary>
        /// Get BitArray String(eg: 0001)
        /// </summary>
        public static string GetString(this BitArray sourceArray) {
            var resultString = "";
            for (var i = 0; i < sourceArray.Length; i++) {
                resultString = System.Convert.ToInt32(sourceArray[i]) + resultString;
            }
            return resultString;
        }

    }// end class
}// end namespace