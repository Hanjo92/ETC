using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro.EditorUtilities;
using UnityEditor;

namespace Almond
{
	[CustomEditor(typeof(ThemaTMP), true)]
	[CanEditMultipleObjects]
	public class ThemaTMPEditor : TMP_EditorPanelUI
	{
		private SerializedProperty m_ColorType;
		protected override void OnEnable()
		{
			base.OnEnable();
			m_ColorType = serializedObject.FindProperty("themaColorType");
		}
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			var themaTMP = serializedObject.targetObject as ThemaTMP;

			EditorGUILayout.LabelField("Thema");
			EditorGUILayout.BeginVertical("Box");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(m_ColorType);
			if(EditorGUI.EndChangeCheck())
			{
				themaTMP.themaColorType = (ThemaColorType)m_ColorType.enumValueIndex;
			}
			EditorGUILayout.EndVertical();
			serializedObject.ApplyModifiedProperties();
		}
	}
}