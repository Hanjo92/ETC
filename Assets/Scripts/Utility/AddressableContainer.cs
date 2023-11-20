using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using UnityEngine.Video;
using System.Linq;

namespace Almond
{
	[CreateAssetMenu(fileName = "AddressableContainer", menuName = "Almond/AddressableContainer")]
	public class AddressableContainer : ScriptableObject
	{
		[Serializable]
		private class Addressable
		{
			public string key;
			public AssetReference asset;
		}

		[SerializeField] private Addressable[] container;

		public async UniTask<GameObject> Instance(string key)
		{
			if(string.IsNullOrEmpty(key))
			{
				Debug.LogWarning($"Key is null or empty");
				return null;
			}

			var addressable = container.FirstOrDefault(x => x.key == key);
			if(addressable == null)
			{
				Debug.LogWarning($"AssetReference not found :: {key}");
				return null;
			}
			if(addressable.asset == null)
			{
				Debug.LogWarning($"AssetReference is null :: {key}");
				return null;
			}

			return await Addressables.InstantiateAsync(addressable.asset);
		}
		public async UniTask<T> InstanceComponent<T>(string key) where T : Component
		{
			var gameObject = await Instance(key);
			if(gameObject == null)
				return null;
			return gameObject.GetComponent<T>();
		}
	}
}