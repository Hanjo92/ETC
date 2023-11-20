using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor.UI;

namespace Almond
{
	[CustomEditor(typeof(SlideToggle), true)]
	[CanEditMultipleObjects]
	public class ToggleSliderEditor : ToggleEditor
	{
		private SerializedProperty m_handle;
		private SerializedProperty m_handleTrue; 
		private SerializedProperty m_handleFalse;
		private SerializedProperty m_toggleBG;
		private SerializedProperty m_effect;

		protected override void OnEnable()
		{
			base.OnEnable();

			m_handle = serializedObject.FindProperty("handle");
			m_handleTrue = serializedObject.FindProperty("handleTrue");
			m_handleFalse = serializedObject.FindProperty("handleFalse");
			m_toggleBG = serializedObject.FindProperty("toggleBG");
			m_effect = serializedObject.FindProperty("effect");
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			var slideToggle = serializedObject.targetObject as SlideToggle;
			EditorGUILayout.LabelField("Slide Toggle");
			EditorGUILayout.BeginVertical("Box");

			EditorGUILayout.BeginVertical("Box");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(m_handle);
			if(EditorGUI.EndChangeCheck())
			{
				slideToggle.handle = m_handle.objectReferenceValue as Image;
			}
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(m_handleTrue);
			if(EditorGUI.EndChangeCheck())
			{
				slideToggle.handleTrue = m_handleTrue.objectReferenceValue as RectTransform;
			}
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(m_handleFalse);
			if(EditorGUI.EndChangeCheck())
			{
				slideToggle.handleFalse = m_handleFalse.objectReferenceValue as RectTransform;
			}
			EditorGUILayout.EndVertical();

			EditorGUILayout.Space(10);
			EditorGUILayout.BeginVertical("Box");
			
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(m_toggleBG);
			if(EditorGUI.EndChangeCheck())
			{
				slideToggle.toggleBG = m_toggleBG.objectReferenceValue as Image;
			}
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(m_effect);
			if(EditorGUI.EndChangeCheck())
			{
				slideToggle.effect = m_effect.objectReferenceValue as ParticleSystem;
			}
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
			serializedObject.ApplyModifiedProperties();
		}
	}
}