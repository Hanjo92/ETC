using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Almond
{
	public abstract class PageBase : MonoBehaviour, IPage
	{
		public abstract string Key { get; }

		public abstract UniTask Active();
		public abstract UniTask Inactive();
	}
}