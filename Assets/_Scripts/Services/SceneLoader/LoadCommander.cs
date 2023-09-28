namespace Chafear.Utils.Loader
{
	public sealed class LoadCommander
	{
		private readonly SceneLoader sceneLoader;

		public LoadCommander( SceneLoader sceneLoader ) 
		{
			this.sceneLoader = sceneLoader;
		}

		public void Load( ILoadCommand command ) 
		{
			command.Load( sceneLoader ).Forget();
		}
	}
}
