using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Almond
{
	public static class PoppupManager
	{
		private static AddressableContainer AddressableContainer;// Need bind
		
		private static Dictionary<string, PopupBase> popups = new Dictionary<string, PopupBase>();

		private static bool IsActivedPoppup(string popupName, ref PopupBase popup)
		{
			if(popups.TryGetValue(popupName, out popup))
			{
				return popup.gameObject.activeSelf;
			}
			return false;
		}

		public static async UniTask<PopupBase> ShowPopup(string popupName, Action closeAction = null)
		{
			PopupBase popup = null;
			if(IsActivedPoppup(popupName, ref popup))
			{
				Debug.LogWarning($"Popup is already Opend :: {popupName}");
				return null;
			}

			if(popup == null)
			{
				Debug.Log($"Instancing Popup :: {popupName}");

				popup = await AddressableContainer.InstanceComponent<PopupBase>(popupName);
				
				popups.Add(popupName, popup);
			}

			popup.AddCloseAction(closeAction);
			popup.gameObject.SetActive(true);
			await popup.OpenAnimation();
			return popup;
		}

		public static void Close(PopupBase popup)
		{
			CloseAsync(popup).Forget();
		}
		public static void Close(string popupName)
		{
			PopupBase popup = null;
			if(IsActivedPoppup(popupName, ref popup) == false)
			{
				Debug.LogWarning($"Popup already closed or not instanced :: {popupName}");
				return;
			}

			CloseAsync(popup).Forget();
		}
		public static async UniTask CloseAsync(PopupBase popup)
		{
			if(popup == null)
			{
				return;
			}
			ScreenLock.Lock();
			await popup.CloseAnimation();
			popup.gameObject.SetActive(false);
			ScreenLock.Unlock();
		}

		public static PopupBase AddCloseAction(this PopupBase popup, Action action)
		{
			popup.AddCloseAction(action);
			return popup;
		}
	}
}