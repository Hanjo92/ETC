using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ColorPanel : MonoBehaviour
{
	private const int PaletteSize = 1024;

    [SerializeField] private ColorSlider colorSlider;
	[SerializeField] private RawImage targetPalette;
	[SerializeField] private Texture2D paletteTexture;
	private Color[] paletteColors;

	/// <summary>
	/// For Demo
	/// </summary>
	[SerializeField] private Image picker;
	private RectTransform rectTransform;

	public Image view;

	private void Start()
	{
		if(colorSlider == null)
		{
			colorSlider = GetComponentInChildren<ColorSlider>();
		}
		if(colorSlider == null)
		{
			Debug.LogError("Set colorSlider Component");
			Destroy(gameObject);
			return;
		}
		if(targetPalette == null)
		{
			targetPalette = GetComponent<RawImage>();
		}
		if(targetPalette == null)
		{
			targetPalette = transform.AddComponent<RawImage>();
		}

		rectTransform = transform as RectTransform;

		paletteTexture = new Texture2D(PaletteSize, PaletteSize);
		CreatePalettePixels(Color.red, PaletteSize);
		paletteTexture.SetPixels(paletteColors);
		paletteTexture.Apply();
		targetPalette.texture = paletteTexture;
		colorSlider.OnChangedColor += OnChangeColor;

		/// demo
		if(view != null)
		{
			OnColorPick += c => view.color = c;
		}
	}


	public Action<Color> OnColorPick;
	private void Update()
	{
		if(Input.GetMouseButton(0) == false)
			return;

		var pointerData = new PointerEventData(EventSystem.current);
		pointerData.position = Input.mousePosition;
		var results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerData, results);

		bool isIgnore = true;
		foreach(var result in results)
		{
			if(result.gameObject == gameObject)
			{
				isIgnore = false;
				break;
			}
		}
		if(isIgnore)
			return;


		var mousePosition = Input.mousePosition;
		var realRectSizeDelta = rectTransform.sizeDelta;
		realRectSizeDelta.x *= transform.localScale.x;
		realRectSizeDelta.y *= transform.localScale.y;

		var startCoord = rectTransform.position;
		startCoord.x += (rectTransform.sizeDelta.x * (rectTransform.pivot.x - 1f)) * transform.localScale.x;
		startCoord.y += (rectTransform.sizeDelta.y * (rectTransform.pivot.y - 1f)) * transform.localScale.y;
		var endCoord = startCoord;
		endCoord.x += realRectSizeDelta.x;
		endCoord.y += realRectSizeDelta.y;

		mousePosition.x = Mathf.Clamp(mousePosition.x, startCoord.x, endCoord.x);
		mousePosition.y = Mathf.Clamp(mousePosition.y, startCoord.y, endCoord.y);
		picker.transform.position = mousePosition;

		var colorCoord = new Vector2(Mathf.InverseLerp(startCoord.x, endCoord.x, mousePosition.x), Mathf.InverseLerp(startCoord.y, endCoord.y, mousePosition.y));
		var color = paletteColors[(int)(colorCoord.x * (PaletteSize - 1))
			 + PaletteSize * (int)((PaletteSize - 1) * colorCoord.y)];
		OnColorPick?.Invoke(color);
	}


	private void OnChangeColor(Color color)
	{
		CreatePalettePixels(color, PaletteSize);
		paletteTexture.SetPixels(paletteColors);
		paletteTexture.Apply();
		OnColorPick?.Invoke(color);
	}

	private void CreatePalettePixels(Color color, int size)
	{
		if(paletteColors == null || paletteColors.Length != size * size)
			paletteColors = new Color[size * size];

		for(int i = 0; i < paletteColors.Length; i++)
		{
			var multiply = (i / size) / (float)(size - 1);
			var lerp = (i % size) / (float)(size-1);

			var newColor = Color.Lerp(Color.white, color, lerp) * multiply;
			newColor.a = 1;
			paletteColors[i] = newColor;
		}
	}
}
