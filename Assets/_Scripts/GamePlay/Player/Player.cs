using Chafear.Inventory;

namespace Chafear
{
	public class Player
	{
		private IInventory inventory;

		public IInventory Inventory => inventory;

		public void SetInventory( IInventory inventory )
		{
			this.inventory = inventory;
		}
	}
}
