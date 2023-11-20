using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Collections;
using UnityEditor.UI;

namespace Almond
{
	public class SlideToggle : Toggle
	{
		protected const float SlideTime = 0.2f;

		public Image handle;
		[ReadOnly] public RectTransform handleTrue;
		[ReadOnly] public RectTransform handleFalse;
		[Space(10)]
		public Image toggleBG;
		public ParticleSystem effect; 

		protected virtual Color SelectedColor => colors.selectedColor;
		protected virtual Color DisabledColor => colors.disabledColor;

		protected override void Awake()
		{
			onValueChanged.AddListener(OnChangeValue);
		}
		protected virtual void OnChangeValue(bool value)
		{
			if(effect != null)
				effect.Play();

			ChangeHandleColor(value, SlideTime);
			ChangeHandlePosition(value,SlideTime);
		}

		public void SetValueImmediately(bool newValue)
		{
			ChangeHandleColor(newValue, 0);
			ChangeHandlePosition(newValue, 0);
		}

		protected virtual void ChangeHandleColor(bool value, float time)
		{
			if(handle == null)
				return;
			handle.DOColor(value ? SelectedColor : DisabledColor, time);
		}
		protected void ChangeHandlePosition(bool value, float time)
		{
			if(handle == null || handleTrue == null || handleFalse == null)
				return;
			handle.transform.DOMove(isOn ? handleTrue.position : handleFalse.position, time); 
		}
	}
}