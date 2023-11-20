using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Almond
{
	[CreateAssetMenu(fileName = "ThemaColorData", menuName = "Almond/ThemaColorData")]
	public class ThemaColorData : ScriptableObject
	{
		[SerializeField] private ThemaColors[] themaColors = new ThemaColors[typeof(Thema).Lenght()];
		public ThemaColors GetThemaColors(Thema thema) => themaColors[(int)thema];
	}
}