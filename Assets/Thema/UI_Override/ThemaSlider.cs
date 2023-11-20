using UnityEngine;
using UnityEngine.UI;

namespace Almond
{
	[AddComponentMenu("Almond/UI/ThemaSlider")]
	public class ThemaSlider : Slider, IThemaUI
	{
		private ThemaColors themaColors;
		private Graphic frame;
		private Image fill;
		private Image handle;

		public ThemaColorType bgColorType;
		public ThemaColorType handleColorType;
		public ThemaColorType fillColorType;

		protected override void Awake()
		{
			frame = targetGraphic;
			fill = (fillRect == null) ? null : fillRect.GetComponent<Image>();
			handle = (handleRect == null) ? null : handleRect.GetComponent<Image>();
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
			// Custom
			frame.color = themaColors.GetColor(bgColorType);
			fill.color = themaColors.GetColor(fillColorType);
			handle.color = themaColors.GetColor(handleColorType);
		}
	}
}