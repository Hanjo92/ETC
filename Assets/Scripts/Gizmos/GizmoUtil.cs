using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmoUtil
{

	public static Vector3[] CubeDirection =
{
			new Vector3(0,0,1),
			new Vector3(1,0,1),
			new Vector3(1,0,0),
			new Vector3(1,0,-1),
			new Vector3(0,0,-1),
			new Vector3(-1,0,-1),
			new Vector3(-1,0,0),
			new Vector3(-1,0,1),
		};
	public static void DrawSolidCube(Vector3 pos, Vector3 size, Color color)
	{
		Vector3 half = size * 0.5f;
		Vector3 pos1 = pos;
		{
			pos1 += half;
		}
		Vector3 pos2 = pos;
		{
			pos2 += half;
			pos2.z -= size.z;
		}
		Vector3 pos3 = pos;
		{
			pos3 -= half;
			pos3.y += size.y;
		}
		Vector3 pos4 = pos;
		{
			pos4 -= half;
			pos4.y += size.y;
			pos4.z += size.z;
		}
		Vector3 pos5 = pos;
		{
			pos5 += half;
			pos5.y -= size.y;
		}
		Vector3 pos6 = pos;
		{
			pos6 += half;
			pos6.z -= size.z;
			pos6.y -= size.y;
		}
		Vector3 pos7 = pos;
		{
			pos7 -= half;
		}
		Vector3 pos8 = pos;
		{
			pos8 -= half;
			pos8.z += size.z;
		}
		//draw top
		Vector3[] vertsT = { pos1, pos2, pos3, pos4 };
		UnityEditor.Handles.DrawSolidRectangleWithOutline(vertsT, color, color * 0.9f);
		//draw bottom
		Vector3[] vertsB = { pos5, pos6, pos7, pos8 };
		UnityEditor.Handles.DrawSolidRectangleWithOutline(vertsB, color, color * 0.9f);
		//draw left
		Vector3[] vertsL = { pos1, pos5, pos6, pos2 };
		UnityEditor.Handles.DrawSolidRectangleWithOutline(vertsL, color, color * 0.9f);
		//draw right
		Vector3[] vertsR = { pos4, pos3, pos7, pos8 };
		UnityEditor.Handles.DrawSolidRectangleWithOutline(vertsR, color, color * 0.9f);
		//draw front
		Vector3[] vertsF = { pos4, pos1, pos8, pos5 };
		UnityEditor.Handles.DrawSolidRectangleWithOutline(vertsF, color, color * 0.9f);
		//draw back
		Vector3[] vertsBack = { pos3, pos2, pos6, pos7 };
		UnityEditor.Handles.DrawSolidRectangleWithOutline(vertsBack, color, color * 0.9f);
	}
}
