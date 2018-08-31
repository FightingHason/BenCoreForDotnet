//************************************************
//Brief: System Utils
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/04/27 Created by Liuhaixia
//************************************************
using System;
using System.Text;
using Ben.Core.Logger;
using UnityEngine;

namespace Ben.Core.Util {
    public class SystemUtil {

        public static string GetDeviceInfo() {
            StringBuilder stringBuilder = new StringBuilder();
            BenLogger.Debug("设备模型deviceModel: " + SystemInfo.deviceModel);
            BenLogger.Debug("设备名称deviceName: " + SystemInfo.deviceName);
            BenLogger.Debug("设备类型（PC电脑，掌上型）deviceType: " + SystemInfo.deviceType);
            BenLogger.Debug("设备唯一标识符 deviceUniqueIdentifier: " + SystemInfo.deviceUniqueIdentifier);
            BenLogger.Debug("操作系统operatingSystem: " + SystemInfo.operatingSystem);
            BenLogger.Debug("系统内存大小MBsystemMemorySize: " + SystemInfo.systemMemorySize);
            return stringBuilder.ToString();
        }

        public static string GetGraphicsInfo() {
            StringBuilder stringBuilder = new StringBuilder();
            BenLogger.Debug("显卡IDgraphicsDeviceID: " + SystemInfo.graphicsDeviceID);
            BenLogger.Debug("显卡名称graphicsDeviceName: " + SystemInfo.graphicsDeviceName);
            BenLogger.Debug("显卡类型graphicsDeviceType: " + SystemInfo.graphicsDeviceType);
            BenLogger.Debug("显卡供应商graphicsDeviceVendor: " + SystemInfo.graphicsDeviceVendor);
            BenLogger.Debug("显卡供应唯一IDgraphicsDeviceVendorID: " + SystemInfo.graphicsDeviceVendorID);
            BenLogger.Debug("显卡版本号graphicsDeviceVersion: " + SystemInfo.graphicsDeviceVersion);
            BenLogger.Debug("显存大小MBgraphicsMemorySize: " + SystemInfo.graphicsMemorySize);
            BenLogger.Debug("显卡是否支持多线程渲染graphicsMultiThreaded: " + SystemInfo.graphicsMultiThreaded);
            BenLogger.Debug("支持的渲染目标数量graphicsShaderLevel: " + SystemInfo.graphicsShaderLevel);
            return stringBuilder.ToString();
        }

        public static string GetProcessorInfo() {
            StringBuilder stringBuilder = new StringBuilder();
            BenLogger.Debug("处理器的数量processorCount: " + SystemInfo.processorCount);
            BenLogger.Debug("处理器的名称processorType: " + SystemInfo.processorType);
            return stringBuilder.ToString();
        }

        public static string GetRenderInfo() {
            StringBuilder stringBuilder = new StringBuilder();
            BenLogger.Debug("支持渲染多少目标纹理supportedRenderTargetCount: " + SystemInfo.supportedRenderTargetCount);
            BenLogger.Debug("是否支持3D（体积）纹理supports3DTextures: " + SystemInfo.supports3DTextures);
            BenLogger.Debug("是否支持获取加速度计supportsAccelerometer: " + SystemInfo.supportsAccelerometer);
            BenLogger.Debug("是否支持计算着色器supportsComputeShaders: " + SystemInfo.supportsComputeShaders);
            BenLogger.Debug("是否支持获取陀螺仪supportsGyroscope: " + SystemInfo.supportsGyroscope);
            BenLogger.Debug("是否支持图形特效supportsImageEffects: " + SystemInfo.supportsImageEffects);
            BenLogger.Debug("是否支持实例化GPU的Draw CallsupportsInstancing: " + SystemInfo.supportsInstancing);
            BenLogger.Debug("是否支持定位功能supportsLocationService: " + SystemInfo.supportsLocationService);
#if !UNITY_5_5
            BenLogger.Debug("是否支持渲染纹理supportsRenderTextures: " + SystemInfo.supportsRenderTextures);
#endif
            BenLogger.Debug("是否支持立方体纹理supportsRenderToCubemap: " + SystemInfo.supportsRenderToCubemap);
            BenLogger.Debug("是否支持内置阴影supportsShadows: " + SystemInfo.supportsShadows);
            BenLogger.Debug("是否支持稀疏纹理supportsSparseTextures: " + SystemInfo.supportsSparseTextures);
#if !UNITY_5_5
            BenLogger.Debug("是否支持模版缓存supportsStencil: " + SystemInfo.supportsStencil);
#endif
            BenLogger.Debug("是否支持用户触摸震动反馈supportsVibration: " + SystemInfo.supportsVibration);

            foreach (RenderTextureFormat type in System.Enum.GetValues(typeof(RenderTextureFormat))) {
                try {
                    BenLogger.Debug("是否支持渲染纹理格式RenderTextureFormat: " + type.ToString() + " | SupportsRenderTextureFormat: " + SystemInfo.SupportsRenderTextureFormat(type));
                } catch (Exception ex) {
                    BenLogger.Debug(ex.Message);
                }
            }

            foreach (TextureFormat type in System.Enum.GetValues(typeof(TextureFormat))) {
                try {
                    BenLogger.Debug("是否支持纹理格式TextureFormat: " + type + " | SupportsTextureFormat: " + SystemInfo.SupportsTextureFormat(type));
                } catch (Exception ex) {
                    BenLogger.Debug(ex.Message);
                }
            }
            return stringBuilder.ToString();
        }

        public static void GetSystemInfo() {
            try {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(GetDeviceInfo());
                stringBuilder.AppendLine(GetGraphicsInfo());
                stringBuilder.AppendLine(GetProcessorInfo());
                stringBuilder.AppendLine(GetRenderInfo());

                BenLogger.Debug(stringBuilder.ToString());
            } catch (Exception) {
                
                throw;
            }
        }

#if UNITY_STANDALONE_WIN

        [StructLayout(LayoutKind.Sequential)]
        public struct MemoryInfo {
            public uint dwLength;
            public uint dwMemoryLoad;
            //系统内存总量
            public ulong dwTotalPhys;
            //系统可用内存
            public ulong dwAvailPhys;
            public ulong dwTotalPageFile;
            public ulong dwAvailPageFile;
            public ulong dwTotalVirtual;
            public ulong dwAvailVirtual;
        }

        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref MemoryInfo info);

        public static void GetSystemMemoryStatus() {
            MemoryInfo memoryInfo = new MemoryInfo();
            GlobalMemoryStatus(ref memoryInfo);

            long avaliableMb = Convert.ToInt64(memoryInfo.dwAvailPhys.ToString())/1024/1024;
            BenLogger.Debug("FreeMemory: " + Convert.ToString(avaliableMb) + " MB");
            if (avaliableMb<200) {
                BenLogger.Debug("内存不足");
            } else {
                BenLogger.Debug("可以使用");
            }
        }

#endif

    }// end class
}// end namespace