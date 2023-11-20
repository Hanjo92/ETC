using UnityEngine;

namespace Almond
{
	[AddComponentMenu("Almond/UI/ThemaSlideToggle")]
	public class ThemaSlideToggle : SlideToggle, IThemaUI
	{
		public ThemaColorType bgColorType;
		public ThemaColorType handleTrueColorType;
		public ThemaColorType handleFalseColorType;
		[SerializeField] private ThemaColors themaColors;
		protected override Color SelectedColor => themaColors.GetColor(handleTrueColorType);
		protected override Color DisabledColor => themaColors.GetColor(handleFalseColorType);

		protected override void OnEnable()
		{
			ThemaManager.Inst.AddListener(ApplyThema);
			ApplyThema(ThemaManager.Inst.CurrentThema, true);
		}
		protected override void OnDisable()
		{
			ThemaManager.Inst.RemoveListener(ApplyThema);
		}

		public void ApplyThema(Thema newThema, bool Immediate)
		{
			if(Application.isPlaying == false)
				return;

			themaColors = ThemaManager.Inst.GetThemaColors();
			var time = Immediate ? 0 : ThemaManager.ChangeThemaTime;
			ChangeHandleColor(isOn, time);
			// Custom
			toggleBG.color = themaColors.GetColor(bgColorType);
		}
	}
}