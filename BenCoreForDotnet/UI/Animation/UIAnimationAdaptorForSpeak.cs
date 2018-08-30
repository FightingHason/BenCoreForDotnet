//************************************************
//Brief: UI Animation Speak Adaptor
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/08/13 Created by Liuhaixia
//************************************************
using Ben.Core.Const;
using UnityEngine;
using UnityEngine.UI;

namespace Ben.Core.UI.Animation {
	public class UIAnimationAdaptorForSpeak : MonoBehaviour {

		const int TEXT_WIDTH_MAX_COUNT = 11, TEXT_HEIGHT_MAX_COUNT = 15,
			TEXT_MAX_LENGTH = TEXT_WIDTH_MAX_COUNT * TEXT_HEIGHT_MAX_COUNT,
			TEXT_WIDTH_OFFSET = 18, TEXT_HEIGHT_OFFSET = 25,
			BACKGROUND_WIDTH_BASE = 30, BACKGROUND_HEIGHT_BASE = 20;
		bool _isInitFinish = false;
		Transform _trans;
		Image _background;
		Text _description;

		void Awake() {
			if (!_isInitFinish) {
				_trans = this.transform;
				_background = _trans.Find("Image").GetComponent<Image>();
				_description = _trans.Find("Text").GetComponent<Text>();
				_isInitFinish = true;
			}
		}

		public void Show(string detail) {
			if (!_isInitFinish) {
				Awake();
			}

			detail = detail.Replace("\n", "");
			int maxDetailLength = TEXT_WIDTH_MAX_COUNT * TEXT_HEIGHT_MAX_COUNT;
			int multiple = detail.Length / TEXT_WIDTH_MAX_COUNT + 1;
			multiple = multiple > TEXT_HEIGHT_MAX_COUNT ? TEXT_HEIGHT_MAX_COUNT : multiple;

			int remainder = multiple > 1 ? TEXT_WIDTH_MAX_COUNT : detail.Length % TEXT_WIDTH_MAX_COUNT;
			int descriptionWith = TEXT_WIDTH_OFFSET * remainder;
			int descriptionHeight = TEXT_HEIGHT_OFFSET * multiple;

			_background.rectTransform.sizeDelta = new Vector2(BACKGROUND_WIDTH_BASE + descriptionWith, BACKGROUND_HEIGHT_BASE + descriptionHeight);
			_description.rectTransform.sizeDelta = new Vector2(descriptionWith, descriptionHeight);
			_description.text = detail.Length > maxDetailLength ? detail.Substring(0, maxDetailLength - 3) + CharConst.STR_AND_SO_ON : detail;
		}

	}// end class
}// end namespace