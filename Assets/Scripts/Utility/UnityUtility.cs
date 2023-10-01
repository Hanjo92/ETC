using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class UnityUtility
{
	public static void SetEngineTimeScale(float val)
	{
		UnityEngine.Time.timeScale = val;
	}
}