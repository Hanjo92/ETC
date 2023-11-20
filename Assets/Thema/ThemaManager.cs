using Almond;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Almond
{
	public class ThemaManager : Singleton<ThemaManager>
	{
		public const string GameThema = "Saved_Game_Thema";
		public const float ChangeThemaTime = 0.2f;

		public ThemaManager()
		{
			currentThema = (Thema)Enum.Parse(typeof(Thema), PlayerPrefs.GetString(GameThema, Thema.CityScape.ToString()));
		}

		private ThemaColorData themaColorData;
		public ThemaColors GetThemaColors()	=> themaColorData == null ? new ThemaColors() : themaColorData.GetThemaColors(CurrentThema);

		private Thema currentThema;
		public Thema CurrentThema
		{
			get => currentThema;
			private set
			{
				currentThema = value;
				PlayerPrefs.SetString(GameThema, currentThema.ToString());
			}
		}

		private Action<Thema, bool> OnChangeThema;
		public void AddListener(Action<Thema, bool> action) => OnChangeThema += action;
		public void RemoveListener(Action<Thema, bool> action) => OnChangeThema -= action;
		public void SetThema(Thema newThema, bool Immediate)
		{
			CurrentThema = newThema;
			OnChangeThema?.Invoke(newThema, Immediate);
		}

		public async UniTask Initialize()
		{
			themaColorData = await Resources.LoadAsync<ThemaColorData>("ThemaColorData") as ThemaColorData;
		}
	}
}