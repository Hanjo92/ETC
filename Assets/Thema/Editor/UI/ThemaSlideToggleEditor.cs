using Almond;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Almond
{
	[CustomEditor(typeof(ThemaSlideToggle),true)]
	[CanEditMultipleObjects]
	public class ThemaSlideToggleEditor : ToggleSliderEditor
	{
		private SerializedProperty m_bgColorType;
		private SerializedProperty m_handleTrueColorType;
		private SerializedProperty m_handleFalseColorType;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_bgColorType = serializedObject.FindProperty("bgColorType");
			m_handleTrueColorType = serializedObject.FindProperty("handleTrueColorType");
			m_handleFalseColorType = serializedObject.FindProperty("handleFalseColorType");
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			EditorGUILayout.Space();
			serializedObject.Update();
			var themaToggle = serializedObject.targetObject as ThemaSlideToggle;
			
			EditorGUILayout.LabelField("Thema");
			EditorGUILayout.BeginVertical("Box");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(m_bgColorType);
			if(EditorGUI.EndChangeCheck())
			{
				themaToggle.bgColorType = (ThemaColorType)m_bgColorType.enumValueIndex;
			}
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(m_handleTrueColorType);
			if(EditorGUI.EndChangeCheck())
			{
				themaToggle.handleTrueColorType = (ThemaColorType)m_handleTrueColorType.enumValueIndex;
			}
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(m_handleFalseColorType);
			if(EditorGUI.EndChangeCheck())
			{
				themaToggle.handleFalseColorType = (ThemaColorType)m_handleFalseColorType.enumValueIndex;
			}
			EditorGUILayout.EndVertical();
			serializedObject.ApplyModifiedProperties();
		}
	}
}
