using Chafear.Data;
using System;
using System.Collections.Generic;

namespace Chafear.Inventory
{
	public class Inventory : IInventory
	{
		private (int cols, int rows) size;

		private Dictionary<IItemInfo, IEnumerable<int>> slotsByItem = new( );
		private IItemInfo[] itemsMap;

		public event Action OnChange;

		public IReadOnlyList<IItemInfo> ItemsMap => itemsMap;

		public IReadOnlyDictionary<IItemInfo, IEnumerable<int>> AllItems => slotsByItem;

		public void Initilize( int cols, int rows )
		{
			itemsMap = new Item[cols * rows];
			size = new ( cols, rows );
		}

		public void AddItemAtSlots( List<int> slotsToPlace, IItemInfo itemInfo )
		{
			foreach ( var slot in slotsToPlace ) itemsMap[slot] = itemInfo;
			slotsByItem.Add( itemInfo, slotsToPlace );
			OnChange?.Invoke( );
		}

		public void RemoveItem( IItemInfo item )
		{
			foreach ( var slot in slotsByItem[item] )
			{
				if( itemsMap[slot] == item ) itemsMap[slot] = null;
			}
			slotsByItem.Remove( item );
			OnChange?.Invoke( );
		}

		public void TryToAddItemAtFreeSlots( IItemInfo item )
		{
			for ( int i = 0; i < size.rows; i++ )
				for ( int j = 0; j < size.cols; j++ )
					if ( TryToFillAt( j, i, item ) ) return;
			 
			throw new InvalidOperationException( "Cant find free space to add item" );
        }

		public void ChangeItemPosition( List<int> slots, Item item )
		{
			RemoveItem( item );
			AddItemAtSlots(slots, item );
		}

		private bool TryToFillAt( int col, int row, IItemInfo item )
		{
			List<int> fitSlots = new( );
			for ( int i = 0; i < item.CurrentHeight; i++ )
			{
				for ( int j = 0; j < item.CurrentWidth; j++ )
				{
					if ( !item.CurrentShape[j, i] ) continue;
					if ( (col + j) >= size.cols || (row + i) >= size.rows ) return false;
					if ( itemsMap[col + j + (row + i) * size.cols] is not null ) return false;
					fitSlots.Add( col + j + (row + i) * size.cols );
				}
			}
			AddItemAtSlots( fitSlots, item );
			return true;
		}

	}
}
