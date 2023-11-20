using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace Almond
{
	[CustomEditor(typeof(ThemaSlider), true)]
	[CanEditMultipleObjects]
	public class ThemaSliderEditor : SliderEditor
	{
		private SerializedProperty m_bgColorType;
		private SerializedProperty m_handleColorType;
		private SerializedProperty m_fillColorType;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_bgColorType = serializedObject.FindProperty("bgColorType");
			m_handleColorType = serializedObject.FindProperty("handleColorType");
			m_fillColorType = serializedObject.FindProperty("fillColorType");
		}
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			var themaSlider = serializedObject.targetObject as ThemaSlider;

			EditorGUILayout.LabelField("Thema");
			EditorGUILayout.BeginVertical("Box");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(m_bgColorType);
			if(EditorGUI.EndChangeCheck())
			{
				themaSlider.bgColorType = (ThemaColorType)m_bgColorType.enumValueIndex;
			}
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(m_handleColorType);
			if(EditorGUI.EndChangeCheck())
			{
				themaSlider.handleColorType = (ThemaColorType)m_handleColorType.enumValueIndex;
			}
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(m_fillColorType);
			if(EditorGUI.EndChangeCheck())
			{
				themaSlider.fillColorType = (ThemaColorType)m_fillColorType.enumValueIndex;
			}
			EditorGUILayout.EndVertical();
			serializedObject.ApplyModifiedProperties();
		}
	}
}