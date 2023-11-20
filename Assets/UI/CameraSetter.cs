using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Almond
{
	[RequireComponent(typeof(Canvas))]
	public class CameraSetter : MonoBehaviour
	{
		[SerializeField] string cameraTag = "MainCamera";
		private Canvas canvas;
		private void Awake()
		{
			canvas = GetComponent<Canvas>();
			canvas.worldCamera = GameObject.FindGameObjectWithTag(cameraTag).GetComponent<Camera>();
		}
	}
}