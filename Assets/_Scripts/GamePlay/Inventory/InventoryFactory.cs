using Chafear.Utils;

namespace Chafear.Inventory
{
	public sealed class InventoryFactory
	{
		private ItemFactory itemFactory;

		public void Initialize( ItemFactory itemFactory )
		{
			this.itemFactory = itemFactory;
		}

		public IInventory CreateInventory( EInventoryTypes type )
		{
			IInventory inventory = new Inventory( );
			switch ( type )
			{
				case EInventoryTypes.Player: return new Inventory( );
				case EInventoryTypes.PlayerTest:
					inventory.Initilize(Constants.Test.InventoryColumns, 
						Constants.Test.InventoryColumns);	
					FillInventory(inventory, Constants.Test.AmountOfTestItemsToAdd );
					return inventory;
				case EInventoryTypes.Loot:
					inventory = new Inventory( );
					inventory.Initilize( Constants.Test.LootColumns,
						Constants.Test.LootRows );
					FillInventory( inventory, 1 );
					return inventory;
			}
			return inventory;
		}

		private void FillInventory( IInventory inventory, int count )
		{	
			for ( int i = 0; i < count; i++ )
			{
				var item = itemFactory.GetRandomItem( );
				inventory.TryToAddItemAtFreeSlots( item );
			}
		}
	}
}
