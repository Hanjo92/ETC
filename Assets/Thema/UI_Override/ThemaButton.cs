using UnityEngine;
using UnityEngine.UI;

namespace Almond
{
	public class ThemaButton : Button
	{
		private ThemaColors themaColors;
		public ThemaColorType bgColorType;

		private ColorBlock originColorBlock;
		protected override void Awake()
		{
			originColorBlock = colors;	
		}

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
			var targetColor = themaColors.GetColor(bgColorType);
			var themaColorBlock = originColorBlock.ApplyColor(targetColor);
			colors = themaColorBlock;
		}
	}
}