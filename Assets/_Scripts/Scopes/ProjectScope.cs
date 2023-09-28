using Chafear.Utils;
using Chafear.Utils.Loader;
using VContainer;
using VContainer.Unity;

namespace Chafear
{
	public sealed class ProjectScope : LifetimeScope
	{
		protected override void Configure( IContainerBuilder builder )
		{
			DontDestroyOnLoad( this );
			builder.Register<SceneLoader>( Lifetime.Singleton );
			builder.Register<LoadCommander>( Lifetime.Singleton );
			builder.Register<AssetsProvider>( Lifetime.Singleton ).As<IAssetsProvider>();
			builder.RegisterEntryPoint<EntryPoint>( ).AsSelf();
		}
	}
}
