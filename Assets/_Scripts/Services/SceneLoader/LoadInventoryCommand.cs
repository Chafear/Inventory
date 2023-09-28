using Cysharp.Threading.Tasks;

namespace Chafear.Utils.Loader
{
	public sealed class LoadInventoryCommand : ILoadCommand
	{
		public async UniTaskVoid Load( SceneLoader sceneLoader )
		{
			await sceneLoader.LoadSceneSingleWithScope( MetaConfig.Scenes.Inventory );
		}
	}
}
