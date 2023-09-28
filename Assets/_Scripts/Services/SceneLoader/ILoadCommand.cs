using Cysharp.Threading.Tasks;

namespace Chafear.Utils.Loader
{
	public interface ILoadCommand
	{
		UniTaskVoid Load( SceneLoader sceneLoader );
	}
}
