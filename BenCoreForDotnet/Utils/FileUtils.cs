//************************************************
//Brief: File Utils
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2017/04/17 Created by Liuhaixia
//************************************************
using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Ben.Core.Const;
using Ben.Core.Extension;
using Ben.Core.Logger;
using LitJson;
using UnityEngine;

namespace Ben.Core.Utils {
    public class FileUtils {
        static readonly Vector2 VECTOR2_CENTER = new Vector2(0.5F, 0.5F);
        static readonly Encoding ENCODING_FORMAT = Encoding.UTF8;

        #region Sprite & Texture2D

        /// <summary>
        /// Get Texture2D by IO.FileStream
        /// </summary>
        public static Texture2D GetTexture2DByIO(String dirPath, String name) {
            return GetTexture2DByIO(Path.Combine(dirPath, name));
        }

        /// <summary>
        /// Get Texture2D by IO.FileStream
        /// </summary>
        public static Texture2D GetTexture2DByIO(String path) {
            Texture2D texture = new Texture2D(100, 100, TextureFormat.ARGB32, false);
            Byte[] byteArray = _GetBytesByStream(path);
            if (byteArray != null) {
                texture.LoadImage(byteArray);
            }
            return texture;
        }

        /// <summary>
        /// Get Sprite by IO.FileStream
        /// </summary>
        public static Sprite GetSpriteByIO(String dirPath, String name) {
            return GetSpriteByIO(Path.Combine(dirPath, name));
        }

        /// <summary>
        /// Get Sprite by IO.FileStream
        /// </summary>
        public static Sprite GetSpriteByIO(String path) {
            Texture2D texture = new Texture2D(100, 100, TextureFormat.ARGB32, false);
            Byte[] byteArray = _GetBytesByStream(path);
            if (byteArray != null) {
                texture.LoadImage(byteArray);
            }
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), VECTOR2_CENTER);
        }

        /// <summary>
        /// Get Sprite array by IO.FileStream
        /// </summary>
        public static Sprite[] GetSpritesByIO(String dirPath) {
            if (Directory.Exists(dirPath)) {
                String[] fileArray = Directory.GetFiles(dirPath);
                if (fileArray != null && fileArray.Length>0) {
                    Sprite[] spriteArray = new Sprite[fileArray.Length];
                    for (Int32 i = 0; i < fileArray.Length; ++i) {
                        Texture2D texture = new Texture2D(273, 114);
                        Byte[] byteArray = _GetBytesByStream(fileArray[i]);
                        if (byteArray != null) {
                            texture.LoadImage(byteArray);
                        }
                        spriteArray[i] = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), VECTOR2_CENTER);
                    }
                    return spriteArray;
                }
            }
            return null;
        }

        /// <summary>
        /// Get Sprite by Unity WWW
        /// </summary>
        public static IEnumerator GetSpriteByWWW(String srcPath, Sprite sprite) {
            WWW www = new WWW(srcPath);
            yield return www;
            if (www != null && www.error.IsNullOrEmpty()) {
                Texture2D texture = www.texture;
                sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), VECTOR2_CENTER);
            } else {
                BenLogger.Error("Load Error Path: " + srcPath);
            }
            www.Dispose();
            www = null;
        }

        #endregion

        #region Try Read Text

        /// <summary>
        /// Get string by read text
        /// </summary>
        public static Boolean TryReadText(String path, out String info) {
            if (File.Exists(path)) {
                info = ReadTextByStream(path);
                if (false == info.IsNullOrEmpty()) {
                    return true;
                } else {
                    DeleteFile(path);
                    BenLogger.Error("File exist, but info is null, delete file! Path: " + path);
                    return false;
                }
            } else {
                info = "";
                return false;
            }
        }

        /// <summary>
        /// Get json data by read text
        /// </summary>
        public static Boolean TryReadText(String path, out JsonData totalJson) {
            if (File.Exists(path)) {
                String jsonStr = ReadTextByStream(path);
                if (false == jsonStr.IsNullOrEmpty() && jsonStr != "\r" && jsonStr != "\n" && jsonStr != "\r\n") {
                    try {
                        totalJson = JsonMapper.ToObject(jsonStr);
                        return true;
                    } catch (Exception ex) {
                        DeleteFile(path);
                        BenLogger.Error("File convert to <LitJson.JsonData> error, delete file! Path: " + path + " || Exception: " + ex.Message);
                    }
                } else {
                    DeleteFile(path);
                    BenLogger.Error("File exist, but info is null, delete file! Path: " + path);
                }
            }
            totalJson = null;
            return false;
        }

        /// <summary>
        /// Get node by read text
        /// </summary>
        public static Boolean TryReadText(String path, out XmlNode rootNode) {
            rootNode = null;
            StreamReader sr;
            XmlDocument xmlDoc = new XmlDocument();
            if (!File.Exists(path)) {
                return false;
            } else if (CheckTextIsNull(path)) {
                DeleteFile(path);
                BenLogger.Error("File exist, but info is null, delete file! Path: " + path);
                return false;
            } else {
                sr = new StreamReader(path, ENCODING_FORMAT);
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                XmlReader reader = XmlReader.Create(sr, settings);
                xmlDoc.Load(reader);
            }

            rootNode = xmlDoc.SelectSingleNode("Data");
            if (rootNode == null) {
                sr.Close();
                xmlDoc = null;
                DeleteFile(path);
                BenLogger.Error("XML root node <Data> is not exist, delete file! Path: " + path);
                return false;
            }

            if (rootNode.ChildNodes.Count == 0) {
                sr.Close();
                rootNode = null;
                xmlDoc = null;
                DeleteFile(path);
                BenLogger.Error("XML root node <Data> info is null, delete file! Path: " + path);
                return false;
            }
            sr.Close();
            return true;
        }

        /// <summary>
        /// Get string by read Resources text
        /// </summary>
        public static Boolean TryReadTextByResource(String relativePath, out String info) {
            info = null;
            TextAsset textAsset = Resources.Load(relativePath) as TextAsset;
            if (textAsset != null) {
                info = textAsset.text;
            }

            if (false == info.IsNullOrEmpty()) {
                return true;
            } else {
                BenLogger.Error("Resources info is null！Path: " + relativePath);
                return false;
            }
        }

        /// <summary>
        /// Get json data by read Resources text
        /// </summary>
        public static Boolean TryReadTextByResource(String relativePath, out JsonData totalJson) {
            String jsonStr = null;
            TextAsset textAsset = Resources.Load(relativePath) as TextAsset;
            if (textAsset != null) {
                jsonStr = textAsset.text;

                if (false == jsonStr.IsNullOrEmpty()) {
                    try {
                        totalJson = JsonMapper.ToObject(jsonStr);
                        return true;
                    } catch (Exception ex) {
                        BenLogger.Error("Resources info convert to <LitJson.JsonData> error, delete file! Path: " + relativePath + " || Exception: " + ex.Message);
                    }
                } else {
                    BenLogger.Error("Resources info is null！Path: " + relativePath);
                }
            }
            totalJson = null;
            return false;
        }

        /// <summary>
        /// Get string by read text(First Cache, Then Resources)
        /// </summary>
        public static Boolean TryReadTextByAll(String path, out String info) {
            info = null;
            if (File.Exists(path)) {
                info = ReadTextByStream(path);
                if (false == info.IsNullOrEmpty()) {
                    return true;
                } else {
                    BenLogger.Error("File exist, but info is null, delete file! Path: " + path);
                    return false;
                }
            } else {
                return TryReadTextByResource(DirectoryConst.TABLE + path.LastSlashToPoint(), out info);
            }
        }

        /// <summary>
        /// Get json data by read text(First Cache, Second Resources)
        /// </summary>
        public static Boolean TryReadTextByAll(String path, out JsonData totalJson) {
            totalJson = null;
            if (File.Exists(path)) {
                Boolean isRet = false;
                String jsonStr = ReadTextByStream(path);
                if (false == jsonStr.IsNullOrEmpty() && jsonStr != "\r" && jsonStr != "\n" && jsonStr != "\r\n") {
                    try {
                        totalJson = JsonMapper.ToObject(jsonStr);
                        isRet = true;
                    } catch (Exception ex) {
                        BenLogger.Error("Resources info convert to <LitJson.JsonData> error, delete file! Path: " + path + " || Exception: " + ex.Message);
                        isRet = false;
                    }
                } else {
                    BenLogger.Error("File exist, but info is null, delete file! Path: " + path);
                    isRet = false;
                }

                if (totalJson == null) {
                    DeleteFile(path);
                    isRet = TryReadTextByResource(DirectoryConst.TABLE + path.LastSlashToPoint(), out totalJson);
                }
                return isRet;
            } else {
                return TryReadTextByResource(DirectoryConst.TABLE + path.LastSlashToPoint(), out totalJson);
            }
        }

        #endregion

        #region Save

        /// <summary>
        /// Save string to file
        /// </summary>
        public static void SaveTextByDelete(Boolean isSave, String path, String info) {
            if (isSave) {
                FileUtils.DeleteFile(path);
                FileUtils.WriteTextByStream(path, info);

                BenLogger.Debug("Save " + path + " Success!");
            }
        }

        #endregion

        #region Text

        /// <summary>
        /// Get string by read line text
        /// </summary>
        static String ReadTextByStreamLine(String path) {
            String result = "";
            if (File.Exists(path)) {
                try {
                    StreamReader sr = new StreamReader(path, ENCODING_FORMAT);
                    String line;
                    while ((line = sr.ReadLine()) != null) {
                        result += line;
                    }
                    sr.Dispose();
                    sr = null;
                } catch (Exception e) {
                    BenLogger.Error("The file could not be read! Path: " + path + " || Message: " + e.Message);
                }
            } else {
                BenLogger.Error("No exist file! Path: " + path);
            }
            return result;
        }

        /// <summary>
        /// Get string by IO.FileStream
        /// </summary>
        public static String ReadTextByIO(String path) {
            if (File.Exists(path)) {
                Byte[] byteArray = _GetBytesByStream(path);
                if (byteArray != null) {
                    return ENCODING_FORMAT.GetString(byteArray);
                } else {
                    BenLogger.Error("The file could not be read! Path: " + path);
                }
            } else {
                BenLogger.Error("No exist file! Path: " + path);
            }
            return "";
        }

        /// <summary>
        /// Write string by IO.FileStream
        /// </summary>
        public static void WriteTextByIO(String path, String info) {
            try {
                FileStream fs = new FileStream(path, FileMode.Create);
                Byte[] data = ENCODING_FORMAT.GetBytes(info);
                fs.Write(data, 0, data.Length);
                fs.Flush();
                fs.Dispose();
                fs = null;
            } catch (Exception e) {
                BenLogger.Error("The file could not be write! Path: " + path + " || Message: " + e.Message);
            }
        }

        /// <summary>
        /// Get string by StreamReader
        /// </summary>
        public static String ReadTextByStream(String path) {
            String result = "";
            if (File.Exists(path)) {
                try {
                    //using (StreamReader sr = new StreamReader(path, ENCODING_FORMAT)) {
                    //    result = sr.ReadToEnd();
                    //}
                    StreamReader sr = new StreamReader(path, ENCODING_FORMAT);
                    result = sr.ReadToEnd();
                    sr.Dispose();
                    sr = null;
                } catch (Exception e) {
                    BenLogger.Error("The file could not be read! Path: " + path + " || Message: " + e.Message);
                }
            } else {
                BenLogger.Error("No exist file! Path: " + path);
            }
            return result;
        }

        /// <summary>
        /// Write string by StreamWriter
        /// </summary>
        public static void WriteTextByStream(String path, String info) {
            try {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, ENCODING_FORMAT);
                sw.Write(info);
                sw.Flush();
                sw.Dispose();
                sw = null;

                fs = null;
            } catch (Exception e) {
                BenLogger.Error("The file could not be write! Path: " + path + " || Message: " + e.Message);
            }
        }

        #endregion

        #region File or Directory Operation(Delete, Create...)

        /// <summary>
        /// Check target file exist
        /// </summary>
        public static Boolean ExistFile(String path) {
            if (path.IsNullOrEmpty()) {
                BenLogger.Error("ExistFile Path is empty!");
                return false;
            }
            return File.Exists(path);
        }

        /// <summary>
        /// Delete target file
        /// </summary>
        public static void DeleteFile(String path) {
            if (path.IsNullOrEmpty()) {
                BenLogger.Error("DeleteFile Path is empty!");
                return;
            }

            if (File.Exists(path)) {
                File.Delete(path);
            }

            CheckDirectory(path.StartToLastSlash());
        }

        /// <summary>
        /// Check Directory
        /// </summary>
        public static void CheckDirectory(String path) {
            if (path.IsNullOrEmpty()) {
                BenLogger.Error("CheckDirectory Path is empty!");
                return;
            }

            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Clear all file and directory of the target directory
        /// </summary>
        public static void ClearDirectoryFiles(String path) {
            try {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileSystemInfo[] fileInfo = dir.GetFileSystemInfos();
                foreach (FileSystemInfo i in fileInfo) {
                    if (i is DirectoryInfo) {
                        DirectoryInfo subDir = new DirectoryInfo(i.FullName);
                        subDir.Delete(true);
                    } else {
                        File.Delete(i.FullName);
                    }
                }
                BenLogger.Error("Delete Path: " + path + " All Info Success!");
            } catch (Exception e) {
                BenLogger.Error("FileClearDir: " + path + " Exception: " + e.Message);
                throw;
            }
        }

        /// <summary>
        /// Check text is empty
        /// </summary>
        public static Boolean CheckTextIsNull(String path) {
            if (File.Exists(path)) {
                String info = ReadTextByStream(path);
                info = info.Replace(" ", "");
                if (false == info.IsNullOrEmpty()) {
                    return false;
                } else {
                    return true;
                }
            } else {
                return true;
            }
        }

        #endregion

        #region MD5

        const String ENCRYPT_KEY = "vksxkwlvkd@rlarudals%*&@lhx";

        /// <summary>
        /// Encrypt script
        /// </summary>
        public static String EnscriptionMD5(String normalString) {
            if (normalString == null) {
                return null;
            }
            if (normalString.Equals(String.Empty)) {
                return String.Empty;
            }

            Byte[] results;
            UTF8Encoding UTF8 = new UTF8Encoding();

            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            Byte[] TDESKey = hashProvider.ComputeHash(UTF8.GetBytes(ENCRYPT_KEY));

            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            try {
                Byte[] dataToEncrypt = UTF8.GetBytes(normalString);
                ICryptoTransform encryptor = TDESAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            } catch (Exception e) {
                return "Error : " + e.ToString() + " normalString : " + normalString;
            } finally {
                TDESAlgorithm.Clear();
                hashProvider.Clear();
                TDESAlgorithm.Clear();
                hashProvider.Clear();
            }
            return Convert.ToBase64String(results);
        }

        /// <summary>
        /// Decryption script
        /// </summary>
        public static String DescriptionMD5(String encryptedString) {
            if (encryptedString == null) {
                return null;
            }
            if (encryptedString.Equals(String.Empty)) {
                return String.Empty;
            }

            Byte[] results;

            UTF8Encoding UTF8 = new UTF8Encoding();

            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            Byte[] TDESKey = hashProvider.ComputeHash(UTF8.GetBytes(ENCRYPT_KEY));

            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            try {
                encryptedString = encryptedString.Replace(" ", "+");
                Byte[] dataToDecrypt = Convert.FromBase64String(encryptedString);
                ICryptoTransform decryptor = TDESAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            } catch (Exception e) {
                throw new Exception(e.ToString() + " encryptedString : " + encryptedString);
                //return "Error";
            } finally {
                TDESAlgorithm.Clear();
                hashProvider.Clear();
                TDESAlgorithm.Clear();
                hashProvider.Clear();
            }
            return UTF8.GetString(results);
        }

        /// <summary>
        /// Create File MD5
        /// </summary>
        public static Boolean TryParseFileMD5(String filePath, out String fileMD5) {
            fileMD5 = "";
            if (!File.Exists(filePath)) {
                return false;
            }
            MD5CryptoServiceProvider md5Generator = new MD5CryptoServiceProvider();
            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            Byte[] hash = md5Generator.ComputeHash(file);
            fileMD5 = BitConverter.ToString(hash).Replace("-", "");
            file.Close();
            return true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get byte array by IO.FileStream
        /// </summary>
        static Byte[] _GetBytesByStream(String path) {
            if (File.Exists(path)) {
                try {
                    FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    fileStream.Seek(0, SeekOrigin.Begin);
                    Byte[] bytes = new Byte[fileStream.Length];
                    fileStream.Read(bytes, 0, (Int32)fileStream.Length);
                    fileStream.Dispose();
                    fileStream = null;
                    return bytes;
                } catch (Exception e) {
                    BenLogger.Error("The file could not be read! Path: " + path + " || Message: " + e.Message);
                }
            } else {
                BenLogger.Error("No Exist File! Path: " + path);
            }
            return null;
        }

        #endregion

    }// end class
}// end namespace