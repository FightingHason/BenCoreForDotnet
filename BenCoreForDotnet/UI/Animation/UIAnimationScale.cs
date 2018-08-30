//************************************************
//Brief: UI Animation Scale
//
//Author: Liuhaixia
//E-Mail: 609043941@qq.com
//
//History: 2018/08/13 Created by Liuhaixia
//************************************************
using System;
using System.Collections;
using UnityEngine;

namespace Ben.Core.UI.Animation {
	public class UIAnimationScale : MonoBehaviour {

		const float INTERVAL_TIME = 0.05F, WAIT_TIME = 0.01F;
		readonly Vector3 STANDARD_INTERVAL_SCALE = new Vector3(0.1F, 0.1F, 0.1F);
		Vector3 _targetScale, _intervalScale;
		Transform _trans;
		Coroutine _animationCoroutine = null;
		Boolean _isPlayingReverse = false;

		void Awake() {
			_trans = this.transform;
			_trans.localScale = Vector3.zero;
			_targetScale = Vector3.one;
		}

		public void Play(bool isShow, float disappearTime) {
			if (isShow) {
				_PlaySequence(disappearTime < 0 ? 1F : disappearTime);
			} else {
				_PlayReverse();
			}
		}

		void _PlaySequence(float disappearTime) {
			_targetScale = Vector3.one;
			_trans.localScale = Vector3.zero;
			_intervalScale = STANDARD_INTERVAL_SCALE;

			_StartAnimation(disappearTime, _PlayReverse);
		}

		void _PlayReverse() {
			if (_trans.localScale != Vector3.zero && !_isPlayingReverse) {
				_targetScale = Vector3.zero;
				_trans.localScale = Vector3.one;
				_intervalScale = -1 * STANDARD_INTERVAL_SCALE;
				_isPlayingReverse = true;

				_StartAnimation(0, null);
			}
		}

		void _StartAnimation(float stayTime, Action callback) {
			if (_animationCoroutine != null) {
				StopCoroutine(_animationCoroutine);
			}
			_animationCoroutine = StartCoroutine(_WaitForStartAnimation(stayTime, callback));
		}

		IEnumerator _WaitForStartAnimation(float stayTime, Action callback) {
			while(true) {
				if (_trans.localScale == _targetScale) {
					break;
				}
				_trans.localScale += _intervalScale;
				yield return new WaitForSeconds(WAIT_TIME);
			}
			_isPlayingReverse = false;

			if (callback != null) {
				yield return new WaitForSeconds(stayTime);
				callback();
			}
		}

	}// end class
}// end namespace