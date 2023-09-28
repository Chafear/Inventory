using Chafear.Data;
using System.Collections.Generic;

namespace Chafear.Inventory
{
	public interface IInventory : IRefreshable
	{
		public void Initilize(int cols, int rows);
		public void AddItemAtSlots( List<int> slots, IItemInfo itemInfo );
		public void TryToAddItemAtFreeSlots( Item item );
		public void RemoveItem( Item item );
		public IReadOnlyList<Item> Slots { get; }
		public IReadOnlyDictionary<Item, IEnumerable<int>> AllItems { get; }
	}
}
