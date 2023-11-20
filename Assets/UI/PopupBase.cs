using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System;
using Unity.Collections;
using UnityEngine.UI;

namespace Almond
{
	public class PopupBase : MonoBehaviour, IPoppup
	{
		[SerializeField] private string popupName; 

		[Serializable]
		public struct PopupAnimationParam
		{
			public enum AnimationType
			{
				Scale,
				worldPosition,
				localPosition,
			}

			public AnimationType animationType;
			public Vector3 startValue;
			public Vector3 endValue;
			public Ease animationEase;
			public float animationTime;
		}

		[Header("Animation")]
		[SerializeField] private PopupAnimationParam openAnimation;
		[SerializeField] private PopupAnimationParam closeAnimation;
		[SerializeField] private Transform root;
		[SerializeField] protected Button closeButton;

		private Action closeAction;
		public void AddCloseAction(Action action) => closeAction += action;
		protected virtual void Awake()
		{
			popupName = string.IsNullOrEmpty(popupName) ? name : popupName;
			root ??= transform;
			if(closeButton)
			{
				closeButton.onClick.AddListener(Close);
			}
		}
		protected virtual void OnDisable()
		{
			closeAction?.Invoke();
			closeAction = null;
		}

		public virtual void Close()
		{
			PoppupManager.Close(this);
		}

		public bool Open { get; set; }
		public string Key => popupName;
		public async UniTask OpenAnimation()
		{
			var seq = DOTween.Sequence();
			seq.Append(
			openAnimation.animationType switch
			{
				PopupAnimationParam.AnimationType.Scale => root.DOScale(openAnimation.startValue, 0),
				PopupAnimationParam.AnimationType.worldPosition => root.DOMove(openAnimation.startValue, 0),
				PopupAnimationParam.AnimationType.localPosition => root.DOLocalMove(openAnimation.startValue, 0),
				_ => throw new NotImplementedException(),
			});
			seq.Append(
			openAnimation.animationType switch
			{
				PopupAnimationParam.AnimationType.Scale => root.DOScale(openAnimation.endValue, openAnimation.animationTime),
				PopupAnimationParam.AnimationType.worldPosition => root.DOMove(openAnimation.endValue, openAnimation.animationTime),
				PopupAnimationParam.AnimationType.localPosition => root.DOLocalMove(openAnimation.endValue, openAnimation.animationTime),
				_ => throw new NotImplementedException(),
			});
			await seq.AsyncWaitForCompletion();
		}
		public async UniTask CloseAnimation()
		{
			var seq = DOTween.Sequence();
			seq.Append(
			closeAnimation.animationType switch
			{
				PopupAnimationParam.AnimationType.Scale => root.DOScale(closeAnimation.startValue, 0),
				PopupAnimationParam.AnimationType.worldPosition => root.DOMove(closeAnimation.startValue, 0),
				PopupAnimationParam.AnimationType.localPosition => root.DOLocalMove(closeAnimation.startValue, 0),
				_ => throw new NotImplementedException(),
			});
			seq.Append(
			closeAnimation.animationType switch
			{
				PopupAnimationParam.AnimationType.Scale => root.DOScale(closeAnimation.endValue, closeAnimation.animationTime),
				PopupAnimationParam.AnimationType.worldPosition => root.DOMove(closeAnimation.endValue, closeAnimation.animationTime),
				PopupAnimationParam.AnimationType.localPosition => root.DOLocalMove(closeAnimation.endValue, closeAnimation.animationTime),
				_ => throw new NotImplementedException(),
			});
			await seq.AsyncWaitForCompletion();
		}
	}
}