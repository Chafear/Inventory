using Chafear.Utils.Loader;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Chafear
{
	public sealed class EntryPoint : IStartable
	{
		private LoadCommander loadCommander;

		[Inject]
		public void Construct( LoadCommander loadCommander )
		{
			this.loadCommander = loadCommander;
		}

		void IStartable.Start( )
		{
			loadCommander.Load( new LoadInventoryCommand( ) );
		}
	}
}
