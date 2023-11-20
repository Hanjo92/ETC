using TMPro;
using UnityEngine;

namespace Almond
{
	[AddComponentMenu("Almond/UI/ThemaTMP")]
	public class ThemaTMP : TextMeshProUGUI, IThemaUI
	{
		private ThemaColors themaColors;
		public ThemaColorType themaColorType;

		protected override void OnEnable()
		{
			base.OnEnable();
			ThemaManager.Inst.AddListener(ApplyThema);

			ApplyThema(ThemaManager.Inst.CurrentThema, true);
		}
		protected override void OnDisable()
		{
			base.OnDisable();
			ThemaManager.Inst.RemoveListener(ApplyThema);
		}

		public void ApplyThema(Thema newThema, bool Immediate)
		{
			if(Application.isPlaying == false)
				return;

			themaColors = ThemaManager.Inst.GetThemaColors();
			// Custom
			color = themaColors.GetColor(themaColorType);
		}
	}
}