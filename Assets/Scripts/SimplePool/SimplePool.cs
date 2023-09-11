using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IPoolObj
{
	string TemplateKey {  get; }
	void Init();
    void Release();
}

public class SimplePool
{
    public static Dictionary<string, Queue<MonoBehaviour>> poolDictionary = new Dictionary<string, Queue<MonoBehaviour>>();

    public static T Instantiate<T>(T template) where T : MonoBehaviour, IPoolObj
	{
		var key = template.gameObject.name.ToString();
		var pooledObj = GetPooledObject<T>(key);
		if(pooledObj != null) return pooledObj;

		return GameObject.Instantiate(template);
	}

	public static T InstantiatePrimitive<T>(PrimitiveType primitiveType, string key) where T : MonoBehaviour, IPoolObj
	{
		var primitiveName = primitiveType.ToString() + key;
		var pooledObj = GetPooledObject<T>(primitiveName);
		if(pooledObj != null)
			return pooledObj;

		var gameObject = GameObject.CreatePrimitive(primitiveType);
		gameObject.name = primitiveName;
		return gameObject.AddComponent<T>();
	}

	private static T GetPooledObject<T>(string key) where T : MonoBehaviour, IPoolObj
	{
		var hasPool = poolDictionary.TryGetValue(key, out var pool);
		if(hasPool)
		{
			if(pool.Count > 0)
			{
				var obj = (T)pool.Dequeue();
				obj.gameObject.SetActive(true);
				obj.Init();
				return obj;
			}
		}
		if(hasPool == false)
		{
			poolDictionary.Add(key, new Queue<MonoBehaviour>());
		}
		return null;
	}

	public static void Release<T>(T obj, bool destroy = false) where T : MonoBehaviour, IPoolObj
	{
		var key = obj.TemplateKey;
		if(destroy)
		{
			GameObject.Destroy(obj.gameObject);
			return;
		}

		if(poolDictionary.TryGetValue(key, out var pool))
		{
			pool.Enqueue(obj);
			obj.gameObject.SetActive(false);
		}
		else
		{
			Debug.LogWarning($"Is not pooled object :: {key}");
			GameObject.Destroy(obj.gameObject);
		}
	}

	public static void Clear(string key)
	{
		if( poolDictionary.TryGetValue(key, out var pool))
		{
			while(pool.Count > 0)
			{
				var obj = pool.Dequeue();
				GameObject.Destroy(obj.gameObject);
			}
			poolDictionary.Remove(key);
		}
	}
}
