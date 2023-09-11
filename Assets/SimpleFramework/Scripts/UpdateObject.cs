using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class UpdateObject : MonoBehaviour
{
    public bool paused = false;

    protected virtual void Start()
    {
        if(UpdateManager.Instance == null)
        {
            Debug.LogError("No Update Manager");
            Destroy(gameObject);
            return;
        }

        UpdateManager.Instance.objects.Add(this);
	}

	protected virtual void OnDestroy()
    {
        if(UpdateManager.Instance)
		    UpdateManager.Instance.objects.Remove(this);
	}


    public virtual void ObjectUpdate(float deltaTime) {}
	[Button("Restart")]
	[SerializeField] private bool restart = false;
	public virtual void Restart() { }
}
