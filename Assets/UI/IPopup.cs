using Cysharp.Threading.Tasks;

namespace Almond
{
	public interface IPoppup
	{
		string Key { get; }
		bool Open { get; set; }
		UniTask OpenAnimation();
		UniTask CloseAnimation();
	}
}