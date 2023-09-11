using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class SmokeDefines
{
	public static int MapLayer = 1 << LayerMask.NameToLayer("Map");
	public static float ReflectDirection = 0.7f;

	public static Vector3[] SquareDimention =
	{
		new Vector3(1, 1, 1),
		new Vector3(1, 1, -1),
		new Vector3(-1, 1, -1),
		new Vector3(-1, 1, 1),
		new Vector3(1, -1, 1),
		new Vector3(1, -1, -1),
		new Vector3(-1, -1, -1),
		new Vector3(-1, -1, 1),
	};
}