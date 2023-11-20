using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Almond
{
	public interface IPage
	{
		string Key { get; }
		UniTask Active();
		UniTask Inactive();
	}
}
