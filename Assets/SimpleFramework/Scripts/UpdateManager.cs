using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private static UpdateManager instance;
    public static UpdateManager Instance => instance;

	[Range(0f, 10f)] public float timeScale = 1f;
	public List<UpdateObject> objects = new List<UpdateObject>();

	[ToggleButton("Paused")][SerializeField] private bool mPaused = false;
	public bool Paused
	{
		get => mPaused;
		set
		{
			Debug.Log($"Change :: Current {mPaused}");
			mPaused = value;
		}
	}

	[Button("Restart")][SerializeField] private bool restart = false;
	public void Restart()
	{
		foreach(var obj in objects)
		{
			obj.Restart();
		}
	}

	private SimplePool SimplePool { get; set; }

	private void Awake()
	{
		if(Instance)
			Destroy(this.gameObject);
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

	}
	void Update()
    {
        if(Paused) return;

		var deltaTime = Time.deltaTime * timeScale;
		foreach(var obj in objects)
		{
			obj.ObjectUpdate(deltaTime);
		}
    }
}
