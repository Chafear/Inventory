using Chafear.Inventory;
using Chafear.Utils;
using Cysharp.Threading.Tasks;

namespace Chafear.GameLoop
{
	public sealed class InitializeLevelState : IState
	{
		private readonly GameStateMachine stateMachine;
		private readonly IAssetsProvider assetsProvider;
		private readonly Player player;
		private readonly InventoryFactory inventoryFactory;

		public InitializeLevelState( GameStateMachine stateMachine, IAssetsProvider assetsProvider,
			Player player, InventoryFactory inventoryFactory)
		{
			this.stateMachine = stateMachine;
			this.assetsProvider = assetsProvider;
			this.player = player;
			this.inventoryFactory = inventoryFactory;
		}


		public void Enter( )
		{
			Init( ).Forget( );
		}

		public void Exit( )
		{
			
		}

		private async UniTaskVoid Init( )
		{
			var itemFactory = await ItemFactory.BuildItemFactoryAsync( assetsProvider );
			inventoryFactory.Initialize( itemFactory );
			player.SetInventory( inventoryFactory.CreateInventory( EInventoryTypes.PlayerTest ) );
			stateMachine.EnterIn<SessionState>(  );
		}
	}
}
