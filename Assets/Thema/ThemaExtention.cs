using UnityEngine;
using UnityEngine.UI;

namespace Almond
{
	public static class ThemaExtentions
	{
		public static ColorBlock ApplyColor(this ColorBlock origin, Color themaColor)
		{
			var thema = new ColorBlock();

			thema.normalColor = origin.normalColor * themaColor;
			thema.highlightedColor = origin.highlightedColor * themaColor;
			thema.pressedColor = origin.pressedColor * themaColor;
			thema.selectedColor = origin.selectedColor * themaColor;
			thema.disabledColor = origin.disabledColor * themaColor;

			thema.colorMultiplier = origin.colorMultiplier;
			thema.fadeDuration = origin.fadeDuration;

			return thema;
		}

		public static string ToSentence(this Thema thema) =>
			thema switch
			{ 
				Thema.CityScape => "City Scape",
				Thema.PurplePalm => "Purple Palm",
				Thema.RadiumBloom => "Radium Bloom",
				Thema.Sunset80s => "80's Sunset",
				Thema.GlacierLake => "Glacier Lake",
				_ => thema.ToString(),
			};
	}
}