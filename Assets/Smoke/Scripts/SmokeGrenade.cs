using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class SmokeGrenade : UpdateObject
{
	public enum EruptionShape
	{
		Sphere,
		HalfSphere,
		Disc
	}
	[Tooltip("Eruption count per direction")]
	[SerializeField][Range(1, 100)] private int eruptionCount = 1;
	public int EntityTotalCount =>
			eruptionShape switch
			{ 
				EruptionShape.Disc => eruptionCount * directionCount,
				EruptionShape.Sphere => eruptionCount * directionCount * directionCount,
				EruptionShape.HalfSphere => eruptionCount * directionCount * directionCount / 2,
				_ =>eruptionCount
			};
	private int EruptionEntityCount =>
			eruptionShape switch
			{
				EruptionShape.Disc => directionCount,
				EruptionShape.Sphere => directionCount * directionCount,
				EruptionShape.HalfSphere => directionCount * directionCount / 2,
				_ => eruptionCount
			};

	[Tooltip("Eruption time per entity")]
	[SerializeField][Range(0, 1f)] private float eruptionTime = 0.5f;

	[SerializeField][Range(0.05f, 1f)] private float entitySize = 0.1f;
	[SerializeField] private EruptionShape eruptionShape = EruptionShape.Sphere;
	[SerializeField][Range(2, 64)] int directionCount = 6;
	public float EntitySize => entitySize;
	[SerializeField][Range(0.1f, 10f)] private float eruptionPower = 2f;
	public float EruptionPower => eruptionPower;
	[SerializeField] private AnimationCurve eruptionPowerCurve = new AnimationCurve();
	[SerializeField][Range(3f, 30f)] private float holdingTime = 10f;
	public float HoldingTime => holdingTime;

	[SerializeField] private float airResistance = -1f;
	public float AirResistance => airResistance;
	[SerializeField][Range(0.1f, 10f)] private float apearTime = 3f;
	public float ApearTime => apearTime;

	[SerializeField]private SmokeEntity entityPrefab;
	public SmokeEntity EntityPrefab => entityPrefab;

	[SerializeField] private List<SmokeEntity> smokeEntities = new List<SmokeEntity>();
	private List<SmokeEntity> releaseEntities = new List<SmokeEntity>();

	[SerializeField] private bool cascadeEntity = false;

	[Button("Explosion")] public bool explosion = true;
	public void Explosion()
	{
		SetCreateSmoke();
	}

	private bool create = false;
	private float fTime = 0f;
	private int eruptedCount = 0;
	private void SetCreateSmoke()
	{
		Clear();
		fTime = 0f;
		eruptedCount = 0;
		create = true;
	}

	private void CreateSmokes()
	{
		if(fTime > eruptedCount * eruptionTime)
		{
			for(int i = 0; i < EruptionEntityCount; i++)
			{
				smokeEntities.Add(SmokeEntity.CreateEntity(this, CalculateFragmentsDirection(i), CalculateFragmentsPower(eruptedCount)));
			}
			eruptedCount++;
		}
		if(eruptedCount >= eruptionCount)
			create = false;

		releaseEntities = smokeEntities.ToList();
	}

	public void ReleaseSmoke(SmokeEntity smokeEntity)
	{
		releaseEntities.Remove(smokeEntity);
	}
	private void CheckEntityAndSwap()
	{
		if(releaseEntities.Count != smokeEntities.Count)
			smokeEntities = releaseEntities.ToList();
	}
	[Button("Clear")] public bool clear = false;
	public void Clear()
	{
		if(smokeEntities.Count > 0)
		{
			for(int i = 0; i < smokeEntities.Count; i++)
			{
				smokeEntities[i].Release();
			}
		}
		smokeEntities.Clear();
	}

	public override void ObjectUpdate(float deltaTime)
	{
		if(create)
		{
			CreateSmokes();
			fTime += deltaTime;
		}

		if(smokeEntities != null && smokeEntities.Count > 0)
		{
			foreach(var entity in smokeEntities)
			{
				entity.SmokeUpdate(deltaTime);
			}
			CheckEntityAndSwap();
		}
	}

	private Vector3 CalculateFragmentsDirection(int index)
	{
		var angleSlice = 360f / directionCount;
		var angleIndex = index % directionCount;
		switch(eruptionShape)
		{
			case EruptionShape.Disc:
			{
				return Quaternion.Euler(0f, angleSlice * angleIndex, 0f) * transform.forward;
			}
			case EruptionShape.Sphere:
			{
				var angleIndex2 = index / directionCount;
				return transform.rotation * Quaternion.Euler(0, angleSlice * angleIndex, 0) * Quaternion.Euler(angleSlice * angleIndex2, 0, 0) * Vector3.forward;
			}
			case EruptionShape.HalfSphere:
			{
				var angleIndex2 = index / directionCount;

			}
			break;
		}
		return Vector3.forward;
	}
	private float CalculateFragmentsPower(int index)
	{
		var ratio = index / (float)eruptionCount;
		var powerCurve = (eruptionPowerCurve.length > 0) ? eruptionPowerCurve.Evaluate(ratio) : Mathf.Cos(Mathf.PI * 0.5f * ratio);
		return powerCurve * EruptionPower;
	}

#if UNITY_EDITOR
	[Range(0, 3f)] public float gizmoRadius = 1;
	public Color gizmoColor = Color.white;
#endif
	private void OnDrawGizmos()
	{
		if(eruptionShape == EruptionShape.Disc)
		{
			Handles.color = gizmoColor;
			Handles.DrawSolidDisc(transform.position, transform.up, gizmoRadius);
		}
		else if(eruptionShape == EruptionShape.Sphere)
		{
			Gizmos.color = gizmoColor;
			Gizmos.DrawSphere(transform.position, gizmoRadius);
		}

		if(smokeEntities != null && smokeEntities.Count > 0)
		{
			foreach(var entity in smokeEntities)
			{
				Gizmos.DrawCube(entity.Position, Vector3.one * EntitySize);
			}
		}
	}
}
