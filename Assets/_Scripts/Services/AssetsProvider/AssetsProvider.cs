using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Chafear.Utils
{
	public class AssetsProvider : IAssetsProvider
	{
		public async UniTask<IList<T>> Load<T>( string label, Action<T> onLoadCallback )
		{
			var load = Addressables.LoadAssetsAsync( label, onLoadCallback);
			return await load.Task;
		}
		
		public void LoadSingle<T>( string label, Action<T> onLoadCallback )
		{
			var load = Addressables.LoadAssetAsync<T>( label );
			load.Completed +=
				( asyncCallback ) =>
				{
					onLoadCallback.Invoke( asyncCallback.Result );
				};
		}
	}
}
