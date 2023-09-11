using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class SmokeEntity : MonoBehaviour, IPoolObj
{
	private SmokeGrenade smokeGrenade;
	private static int MapLayer => SmokeDefines.MapLayer;
	public Vector3 Position
	{
		get => transform.position;
		private set => transform.position = value;
	}
	private float AirResistance => smokeGrenade?.AirResistance ?? -1f;
	private float holdingTime; // for Fade
	private float eruptionSpeed;
	private Vector3 moveDirection;
	private float ApearTime => smokeGrenade?.ApearTime ?? 0;
	private float fTime;
	public bool IsDisapear => fTime >= ApearTime;
	public float Size => smokeGrenade.EntitySize;

	[SerializeField]private string mTemplateKey;
	public string TemplateKey => mTemplateKey;

	public static SmokeEntity CreateEntity(SmokeGrenade sg, Vector3 direction, float startSpeed, bool isCascade = false)
	{
		SmokeEntity entity;
		#region pool
		if(sg.EntityPrefab)
		{
			entity = SimplePool.Instantiate(sg.EntityPrefab);
			entity.mTemplateKey = sg.EntityPrefab.name;
		}
		else
		{
			entity = SimplePool.InstantiatePrimitive<SmokeEntity>(PrimitiveType.Sphere, "_smoke");
			entity.mTemplateKey = PrimitiveType.Cube.ToString() + "_smoke";
		}
		#endregion
		entity.smokeGrenade = sg;
		entity.holdingTime = sg.HoldingTime;
		entity.eruptionSpeed = startSpeed;

		if(isCascade)
		{
			entity.transform.SetParent(sg.transform);
		}

		entity.Position = sg.transform.position;
		entity.transform.localScale = Vector3.one * entity.Size;

		entity.transform.rotation = isCascade ? Quaternion.identity : sg.transform.rotation;
		entity.moveDirection = direction;

		return entity;
	}

	[Button("Release")] public bool test = false;

	public void Release()
	{
		if(smokeGrenade)
			smokeGrenade.ReleaseSmoke(this);

		SimplePool.Release(this);
	}

	public void SmokeUpdate(float deltaTime)
	{
		if(fTime > ApearTime)
		{
			Release();
			return;
		}
		var moveDistance = CalculateDistance(fTime, deltaTime);
		fTime += deltaTime;
		if(moveDistance <= 0)
			return;

		while(Physics.Raycast(Position, moveDirection, out var info, moveDistance, MapLayer))
		{
			Position = info.point;
			moveDistance -= info.distance;
			var reflect = Vector3.Reflect(moveDirection, info.normal);
			var lerpCenter = Vector3.Lerp(moveDirection, reflect, 0.5f);
			moveDirection = Vector3.Lerp(reflect, lerpCenter, SmokeDefines.ReflectDirection);
		}
		Position += moveDirection * moveDistance;
	}

	private float CalculateDistance(float fTime, float deltaTime)
	{
		var startSpeed = eruptionSpeed + AirResistance * fTime;
		return (startSpeed + 0.5f * AirResistance * deltaTime) * deltaTime;
	}

	public void Init()
	{
		fTime = 0;
	}
}