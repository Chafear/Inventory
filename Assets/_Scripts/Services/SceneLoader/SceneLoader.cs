using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Chafear.Utils.Loader
{
	public sealed class SceneLoader
	{
		private readonly LifetimeScope parent;

		public SceneLoader(LifetimeScope parent )
		{
			this.parent = parent;
		}

		public UniTask LoadSceneSingle( string sceneId, Action onLoad = null )
		{
			return LoadSceneSingle_( sceneId, onLoad );
		}
		
		public UniTask LoadSceneSingleWithScope( string sceneId, Action onLoad = null )
		{
			return LoadSceneSingleWithScope_( sceneId, onLoad );
		}

		//not using it rn but probbly gonna need it later
		public UniTask LoadSceneAdditive( string sceneId, Action onLoad = null )
		{
			return LoadSceneAdditive_( sceneId, onLoad );
		}
		
		public UniTask LoadSceneAdditiveWithScope( string sceneId, Action onLoad = null )
		{
			return LoadSceneSingleWithScope_( sceneId, onLoad );
		}

		public UniTask UnloadScene( string sceneId, Action onUnLoad = null )
		{
			return UnLoadScene_( sceneId, onUnLoad );
		}

		private async UniTask LoadSceneSingle_( string sceneId, Action onLoad = null )
		{
			var async = SceneManager.LoadSceneAsync( sceneId, LoadSceneMode.Single );

			while ( !async.isDone )	await UniTask.Yield( );

			onLoad?.Invoke( );
		}
		
		private async UniTask LoadSceneSingleWithScope_( string sceneId, Action onLoad = null )
		{
			using ( LifetimeScope.EnqueueParent( parent ) )
			{
				var async = SceneManager.LoadSceneAsync( sceneId, LoadSceneMode.Single );

				while ( !async.isDone ) await UniTask.Yield( );

				onLoad?.Invoke( );
			}
		}
		
		private async UniTask LoadSceneAdditive_( string sceneId, Action onLoad = null )
		{
			var async = SceneManager.LoadSceneAsync( sceneId, LoadSceneMode.Additive );

			while ( !async.isDone )	await UniTask.Yield( );

			onLoad?.Invoke( );
		}
		
		private async UniTask LoadSceneAdditiveWithScope_( string sceneId, Action onLoad = null )
		{
			using ( LifetimeScope.EnqueueParent( parent ) )
			{
				var async = SceneManager.LoadSceneAsync( sceneId, LoadSceneMode.Additive );

				while ( !async.isDone ) await UniTask.Yield( );

				onLoad?.Invoke( );
			}
		}
		
		private async UniTask UnLoadScene_( string sceneId, Action onUnLoad = null )
		{
			var async = SceneManager.UnloadSceneAsync( sceneId );

			while ( !async.isDone )	await UniTask.Yield( );

			onUnLoad?.Invoke( );
		}
	}
}
