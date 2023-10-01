using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class AnimatorUtility
{
	public static void AnimatorSetInt(Animator animator, string parameter, int val)
	{
		if(animator == null)
			return;
		animator.SetInteger(parameter, val);
	}
	public static void AnimatorSetFloat(Animator animator, string parameter, float val)
	{
		if(animator == null)
			return;
		animator.SetFloat(parameter, val);
	}
	public static void AnimatorSetBool(Animator animator, string parameter, bool val)
	{
		if(animator == null)
			return;
		animator.SetBool(parameter, val);
	}
	public static void AnimatorSetTrigger(Animator animator, string parameter)
	{
		if(animator == null)
			return;
		animator.SetTrigger(parameter);
	}
	public static void AnimatorClearTriggers(Animator animator)
	{
		if(animator == null)
			return;
		var animatorParams = animator.parameters;
		foreach(var param in animatorParams)
		{
			if(param.type == AnimatorControllerParameterType.Trigger)
			{
				animator.SetBool(param.name, false);
			}
		}
	}
}
