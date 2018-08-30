//************************************************
//Brief: String Extension
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/02/01 Created by Liuhaixia
//************************************************
using System;
using System.Collections.Generic;
using Ben.Core.Const;

namespace Ben.Core.Extension {
    public static class StringExtension {

        const String REPLACE_CLONE = "(Clone)";

        /// <summary>
        /// Check string is null, empty , ""
        /// </summary>
        public static Boolean IsNullOrEmpty(this String source) {
            return String.IsNullOrEmpty(source);
        }

        /// <summary>
        /// Check is number
        /// </summary>
        public static Boolean IsNumber(this String source) {
            if (source.IsNullOrEmpty()) {
                return false;
            }
            for (int i = 0; i < source.Length; ++i) {
                if (source[i] >= '0' && source[i] <= '9') {
                    continue;
                } else {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Delete String
        /// </summary>
        public static String Delete(this String source, String delete) {
            if (source.IsNullOrEmpty() || delete.IsNullOrEmpty()) {
                return source;
            }
            return source.Replace(delete, CharConst.STR_EMPTY);
        }

        /// <summary>
        /// Delete "(Clone)"
        /// </summary>
        public static String DeleteCloneName(this String source) {
            if (source.IsNullOrEmpty()) {
                return source;
            }
            return source.Replace(REPLACE_CLONE, CharConst.STR_EMPTY);
        }

        /// <summary>
        /// Standard path
        /// </summary>
        public static String StandardSplitForPath(this String path) {
            return path.Replace(CharConst.STR_BACK_SLASH_DOUBLE, CharConst.STR_SLASH);
        }

        /// <summary>
        /// Delete the data of deleteList from sourceList
        /// </summary>
        public static void DeleteList(this List<String> sourceList, List<String> deleteList) {
            for (int j = sourceList.Count - 1; j >= 0; j--) {
                if (deleteList.Contains(sourceList[j])) {
                    sourceList.RemoveAt(j);
                }
            }
        }


        #region Contains

        /// <summary>
        /// Contains String[] Value
        /// </summary>
        public static Boolean Contains(this String source, String[] targetArray) {
            if (targetArray == null) {
                return false;
            }

            for (int i = 0; i < targetArray.Length; i++) {
                if (source.Contains(targetArray[i])) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// String[] Contains Value
        /// </summary>
        public static Boolean Contains(this String[] source, String value) {
            for (int i = 0; i < source.Length; i++) {
                if (source[i].Equals(value)) {
                    return true;
                }
            }
            return false;
        }

        #endregion


        #region Split

        /// <summary>
        /// Get string array by split
        /// </summary>
        public static String[] GetArrayBySplit(this String source, Char splitChar=CharConst.CHAR_COMMA) {
            if (source.IsNullOrEmpty()) {
                return null;
            }
            return source.Split(splitChar);
        }

        /// <summary>
        /// Get string array by split
        /// </summary>
        public static String[] GetArrayBySplit(this String source, String splitStr) {
            if (source.IsNullOrEmpty()) {
                return null;
            }
            return source.Split(new String[] { splitStr }, StringSplitOptions.None);
        }

        /// <summary>
        /// Combine string array to string
        /// </summary>
        public static String GetStringByArray(this String[] sourceArray, String splitStr = CharConst.STR_COMMA) {
            if (sourceArray == null) {
                return null;
            }

            String retStr = CharConst.STR_EMPTY;
            for (int i = 0; i < sourceArray.Length; i++) {
                retStr += sourceArray[i];
                if (splitStr != null && i != (sourceArray.Length - 1)) {
                    retStr += splitStr.ToString();
                }
            }
            return retStr;
        }

        /// <summary>
        /// Remove the empty element of array
        /// </summary>
        public static String[] RemoveArrayEmpty(this String[] sourceArray) {
            if (sourceArray == null) {
                return null;
            }

            List<String> resultList = new List<string>();
            for (int i = 0; i < sourceArray.Length; i++) {
                if (false == sourceArray[i].IsNullOrEmpty()) {
                    resultList.Add(sourceArray[i]);
                }
            }
            return resultList.ToArray();
        }

#endregion


        #region Substring

        /// <summary>
        /// Get substring
        /// </summary>
        public static String Substring(this String source, String key, Boolean isContainKey = false) {
            Int32 startPosition = source.IndexOf(key);
            if (isContainKey) {
                return source.Substring(startPosition);
            } else {
                return source.Substring(startPosition + key.Length);
            }
        }

        /// <summary>
        /// Get substring from start to first "/" position
        /// </summary>
        public static String StartToFirstSlash(this String source) {
            Int32 length = source.IndexOf(CharConst.CHAR_SLASH);
            if (length == -1 || length == 0) {
                length = source.Length;
            }
            return source.Substring(0, length);
        }

        /// <summary>
        /// Get substring from start to last "/" position
        /// </summary>
        public static String StartToLastSlash(this String source) {
            Int32 length = source.LastIndexOf(CharConst.CHAR_SLASH);
            if (length == -1) {
                length = source.Length;
            }
            return source.Substring(0, length);
        }

        /// <summary>
        /// Get substring from start to first "." position
        /// </summary>
        public static String StartToFirstPoint(this String source) {
            Int32 length = source.IndexOf(CharConst.CHAR_DOT);
            if (length == -1 || length == 0) {
                length = source.Length;
            }
            return source.Substring(0, length);
        }

        /// <summary>
        /// Get substring from start to last "." position
        /// </summary>
        public static String StartToLastPoint(this String source) {
            Int32 length = source.LastIndexOf(CharConst.CHAR_DOT);
            if (length == -1) {
                length = source.Length;
            }
            return source.Substring(0, length);
        }

        /// <summary>
        /// Get substring from last "/" position to end
        /// </summary>
        public static String LastSlashToEnd(this String source) {
            return source.Substring(source.LastIndexOf(CharConst.CHAR_SLASH) + 1);
        }

        /// <summary>
        /// Get substring from last"/" position to first "." position
        /// </summary>
        public static String LastSlashToPoint(this String source) {
            String temp = source.LastSlashToEnd();
            temp = temp.StartToFirstPoint();
            return temp;
        }

        /// <summary>
        /// Get substring from first"/" position to end
        /// </summary>
        public static String StartWithSlash(this String source) {
            String temp = source;
            while (true) {
                if (temp.StartsWith(CharConst.STR_SLASH)) {
                    temp = temp.Substring(1, temp.Length - 1);
                } else {
                    break;
                }
            }
            return temp;
        }

        #endregion


        #region Convert To Base ValueType

        /// <summary>
        /// Convert to bool
        /// </summary>
        public static Boolean ToBoolean(this String source, Boolean defaultValue = false) {
            if (source.IsNullOrEmpty()) {
                return defaultValue;
            } else {
                try {
                    return Convert.ToBoolean(source);
                } catch (Exception) {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Convert to float
        /// </summary>
        public static Single ToSingle(this String source, Single defaultValue = 0) {
            if (source.IsNullOrEmpty()) {
                return defaultValue;
            } else {
                try {
                    return Convert.ToSingle(source);
                } catch (Exception) {
                    return defaultValue;
                } 
            }
        }

        /// <summary>
        /// Convert to double
        /// </summary>
        public static Double ToDouble(this String source, Double defaultValue = 0) {
            if (source.IsNullOrEmpty()) {
                return defaultValue;
            } else {
                try {
                    return Convert.ToDouble(source);
                } catch (Exception) {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Convert to int
        /// </summary>
        public static Int32 ToInt32(this String source, Int32 defaultValue = 0) {
            if (source.IsNullOrEmpty()) {
                return defaultValue;
            } else {
                try {
                    return Convert.ToInt32(source);
                } catch (Exception) {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Convert to long
        /// </summary>
        public static Int64 ToInt64(this String source, Int64 defaultValue = 0) {
            if (source.IsNullOrEmpty()) {
                return defaultValue;
            } else {
                try {
                    return Convert.ToInt64(source);
                } catch (Exception) {
                    return defaultValue;
                }
            }
        }

        #endregion

    }// end class
}// end namespace