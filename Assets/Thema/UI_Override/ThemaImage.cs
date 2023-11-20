using UnityEngine;
using UnityEngine.UI;

namespace Almond
{
	[AddComponentMenu("Almond/UI/ThemaImage")]
	public class ThemaImage : Image, IThemaUI
	{
		private ThemaColors themaColors;
		public ThemaColorType bgColorType;
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
			color = themaColors.GetColor(bgColorType);
		}
	}
}