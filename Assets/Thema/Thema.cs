using System;
using UnityEngine;

namespace Almond
{
	public enum Thema
	{
		CityScape,
		Futurism,
		PurplePalm,
		RadiumBloom,
		Blacklight,
		Sunset80s,
		GlacierLake,
		//Desert
		//Snow
	}
	public enum ThemaColorType
	{
		Main1,
		Main2,
		Main3,
		Main4,
		Main5,

		Light,
		Dark,

		SelectedUI,
		DisabledUI,
	}

	[Serializable]
	public struct ThemaColors
	{
		public Thema thema;
		public Color main1;
		public Color main2;
		public Color main3;
		public Color main4;
		public Color main5;

		public Color dark;
		public Color light;

		public Color selectedUI;
		public Color disabledUI;

		public Color GetColor(ThemaColorType colorType) =>
			colorType switch
			{
				ThemaColorType.Main1 => main1,
				ThemaColorType.Main2 => main2,
				ThemaColorType.Main3 => main3,
				ThemaColorType.Main4 => main4,
				ThemaColorType.Main5 => main5,
				ThemaColorType.Dark => dark,
				ThemaColorType.Light => light,
				ThemaColorType.SelectedUI => selectedUI,
				ThemaColorType.DisabledUI => disabledUI,
				_ => Color.white,
			};
	}
}