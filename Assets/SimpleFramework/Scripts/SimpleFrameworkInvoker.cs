using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFramework
{
	public class SFInvoke
	{
		public readonly float actionTime;
		public readonly Action action;
		private float fTime;
		public bool IsInvoked => fTime <= 0;
		public SFInvoke(float _actionTime, Action invoke)
		{
			actionTime = _actionTime;
			action = invoke;
			fTime = actionTime;
		}
		public int Update(float deltaTime)
		{
			if(IsInvoked)
				return 0;

			fTime -= deltaTime;
			if(fTime < 0)
			{
				action?.Invoke();
				return 1;
			}
			return 0;
		}
	}


	public class SimpleFrameworkInvoker
	{
		private HashSet<SFInvoke> invokes = new HashSet<SFInvoke>();

		public void AddInvoke(SFInvoke invoke) => invokes.Add(invoke);

		public void Update(float fTime)
		{
			var activeCount = 0;
			foreach (var invoke in invokes)
			{
				activeCount += invoke.Update(fTime);
			}
			if(activeCount > 0)
				Arrange();
		}

		private void Arrange()
		{
			var arrangeInvokes = invokes.Where(a => a.IsInvoked == false);
			invokes = new HashSet<SFInvoke>(arrangeInvokes);
		}

		public void ClearInvokes() => invokes.Clear();
	}
}