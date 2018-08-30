//************************************************
//Brief: Json Extension
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2017/04/17 Created by Liuhaixia
//************************************************
using System;
using System.Collections;
using LitJson;

namespace Ben.Core.Extension {
    public static class LitJsonExtension {

        /// <summary>
        /// Check json data by key
        /// </summary>
        public static Boolean ContainKey(this JsonData jsonData, String key) {
            if (jsonData != null) {
                if (((IDictionary)jsonData).Contains(key)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get string data by json
        /// </summary>
        public static String GetString(this JsonData jsonData, String key, String defaultValue = "") {
            if (jsonData.ContainKey(key)) {
                Object data = jsonData[key];
                if (data != null) {
                    try {
                        return Convert.ToString(data);
                    } catch (Exception) {
                        return defaultValue;
                    }
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Get float data by json
        /// </summary>
        public static Single GetFloat(this JsonData jsonData, String key, Single defaultValue = 0f) {
            if (jsonData.ContainKey(key)) {
                Object data = jsonData[key];
                if (data != null) {
                    try {
                        return Convert.ToSingle(data.ToString());
                    } catch (Exception) {
                        return defaultValue;
                    }
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Get double data by json
        /// </summary>
        public static Double GetDouble(this JsonData jsonData, String key, Double defaultValue = 0f) {
            if (jsonData.ContainKey(key)) {
                Object data = jsonData[key];
                if (data != null) {
                    try {
                        return Convert.ToDouble(data.ToString());
                    } catch (Exception) {
                        return defaultValue;
                    }
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Get int data by json
        /// </summary>
        public static Int32 GetInt(this JsonData jsonData, String key, Int32 defaultValue = 0) {
            if (jsonData.ContainKey(key)) {
                Object data = jsonData[key];
                if (data != null) {
                    try {
                        return Convert.ToInt32(data.ToString());
                    } catch (Exception) {
                        return defaultValue;
                    }
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Get long data by json
        /// </summary>
        public static Int64 GetLong(this JsonData jsonData, String key, Int64 defaultValue = 0) {
            if (jsonData.ContainKey(key)) {
                Object data = jsonData[key];
                if (data != null) {
                    try {
                        return Convert.ToInt64(data.ToString());
                    } catch (Exception) {
                        return defaultValue;
                    }
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Get bool data by json
        /// </summary>
        public static Boolean GetBool(this JsonData jsonData, String key, Boolean defaultValue = false) {
            if (jsonData.ContainKey(key)) {
                Object data = jsonData[key];
                if (data != null) {
                    try {
                        return Convert.ToBoolean(data.ToString());
                    } catch (Exception) {
                        return defaultValue;
                    }
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Get json string data by json
        /// </summary>
        public static String GetJsonArrayString(this JsonData jsonData, String key, String defaultValue = "") {
            if (jsonData.ContainKey(key)) {
                return jsonData[key].ToJson();
            } else {
                return defaultValue;
            }
        }

        /// <summary>
        /// String convert to LitJson
        /// </summary>
        public static Boolean TryGetJsonData(this String source, out JsonData outValue) {
            try {
                outValue = JsonMapper.ToObject(source);
                return true;
            } catch (Exception) {
                outValue = null;
                return false;
            }
        }

    }// end class
}//end namespace