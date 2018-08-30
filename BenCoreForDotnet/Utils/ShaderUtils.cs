//************************************************
//Brief: Shader Utils
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/08/22 Created by Liuhaixia
//************************************************
using System.Collections.Generic;
using UnityEngine;

namespace Ben.Core.Utils {
	public class ShaderUtils {
		static readonly Dictionary<string, Shader> _existShaderDict = new Dictionary<string, Shader>();

		/// <summary>
		/// Reset Transform Shader
		/// </summary>
		public static void ResetByTransform(Transform transform) {
			for (int i = 0; i < transform.childCount; ++i) {
				ResetByRender(transform.GetChild(i).GetComponent<Renderer>());
				ResetByTransform(transform.GetChild(i));
			}
		}

		/// <summary>
		/// Reset Renderer Shader
		/// </summary>
		public static void ResetByRender(Renderer renderer) {
			if (renderer != null) {
				Material[] materials = renderer.materials;
				if (materials != null) {
					for (int i = 0; i < materials.Length; ++i) {
						materials[i].shader = GetShader(materials[i].shader.name);
					}
				}
			}
		}

		/// <summary>
		/// Get Shader
		/// </summary>
		public static Shader GetShader(string shaderName) {
			Shader tempShader;
			if (!_existShaderDict.TryGetValue(shaderName, out tempShader)) {
				tempShader = Shader.Find(shaderName);
				_existShaderDict.Add(shaderName, tempShader);
			}
			return tempShader;
		}

	}// end class
}// end namespace