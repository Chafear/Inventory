using Chafear.Data;
using System.Collections.Generic;

namespace Chafear.Inventory
{
	public interface IInventory : IRefreshable
	{
		public void Initilize(int cols, int rows);
		public void AddItemAtSlots( List<int> slots, IItemInfo itemInfo );
		public void TryToAddItemAtFreeSlots( IItemInfo item );
		public void RemoveItem( IItemInfo item );
		public IReadOnlyList<IItemInfo> ItemsMap { get; }
		public IReadOnlyDictionary<IItemInfo, IEnumerable<int>> AllItems { get; }
	}
}
