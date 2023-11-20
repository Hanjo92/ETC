using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace Almond
{
	public static class EditorUtil
	{
		public static void ColorTintView(Selectable selectable)
		{
			selectable.targetGraphic = EditorGUILayout.ObjectField("\tTarget Graphic", selectable.targetGraphic, typeof(Graphic), true) as Graphic;
			var colorBlock = selectable.colors;
			colorBlock.normalColor = EditorGUILayout.ColorField("\tNormal Color", colorBlock.normalColor);
			colorBlock.highlightedColor = EditorGUILayout.ColorField("\tHighlighted Color", colorBlock.highlightedColor);
			colorBlock.pressedColor = EditorGUILayout.ColorField("\tPressed Color", colorBlock.pressedColor);
			colorBlock.selectedColor = EditorGUILayout.ColorField("\tSelected Color", colorBlock.selectedColor);
			colorBlock.disabledColor = EditorGUILayout.ColorField("\tDisabled Color", colorBlock.disabledColor);
			colorBlock.colorMultiplier = EditorGUILayout.Slider("\tColor Multiplier", colorBlock.colorMultiplier, 1, 5);
			colorBlock.fadeDuration = EditorGUILayout.FloatField("\tFade Duration", colorBlock.fadeDuration);
			selectable.colors = colorBlock;
		}
		public static void SpriteSwapView(Selectable selectable)
		{
			selectable.targetGraphic = EditorGUILayout.ObjectField("\tTarget Graphic", selectable.targetGraphic, typeof(Graphic), true) as Graphic;
			var sprites = selectable.spriteState;
			sprites.highlightedSprite = EditorGUILayout.ObjectField("\tHighlighted Sprite", sprites.highlightedSprite, typeof(Sprite), true) as Sprite;
			sprites.pressedSprite = EditorGUILayout.ObjectField("\tPressed Sprite", sprites.pressedSprite, typeof(Sprite), true) as Sprite;
			sprites.selectedSprite = EditorGUILayout.ObjectField("\tSelected Sprite", sprites.selectedSprite, typeof(Sprite), true) as Sprite;
			sprites.disabledSprite = EditorGUILayout.ObjectField("\tDisabled Sprite", sprites.disabledSprite, typeof(Sprite), true) as Sprite;
			selectable.spriteState = sprites;
		}
		public static void AnimationView(Selectable selectable)
		{
			var triggers = selectable.animationTriggers;
			triggers.normalTrigger = EditorGUILayout.TextField("\tHighlighted Trigger", triggers.normalTrigger);
			triggers.highlightedTrigger = EditorGUILayout.TextField("\tHighlighted Trigger", triggers.highlightedTrigger);
			triggers.pressedTrigger = EditorGUILayout.TextField("\tPressed Trigger", triggers.pressedTrigger);
			triggers.selectedTrigger = EditorGUILayout.TextField("\tSelected Trigger", triggers.selectedTrigger);
			triggers.disabledTrigger = EditorGUILayout.TextField("\tDisabled Trigger", triggers.disabledTrigger);
			selectable.animationTriggers = triggers;
		}
	}
}