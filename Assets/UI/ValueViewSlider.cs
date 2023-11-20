using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Almond
{
	[RequireComponent(typeof(Slider))]
	public class ValueViewSlider : MonoBehaviour
	{
		private Slider slider;
		[SerializeField] private TextMeshProUGUI valueView;
		[Tooltip("�Ҽ� �κ� ǥ�� ����")][SerializeField][Range(0, 8)] private int decimalCount = 2;
		private void Awake()
		{
			slider = GetComponent<Slider>();
		}
		private void OnEnable()
		{
			slider.onValueChanged.AddListener(OnValueChanged);
		}
		private void OnDisable()
		{
			slider.onValueChanged.RemoveListener(OnValueChanged);
		}

		protected virtual void OnValueChanged(float value)
		{
			if(valueView != null)
			{
				valueView.text = (value * 100).ToString($"F{decimalCount}");
			}
		}
	}
}