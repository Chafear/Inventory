using Chafear.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chafear.Inventory
{
	public class Inventory : IInventory
	{
		private (int cols, int rows) size;

		private Dictionary<Item, IEnumerable<int>> slotsByItem = new( );
		private Item[] slots;

		public event Action OnChange;

		public IReadOnlyList<Item> Slots => slots;

		public IReadOnlyDictionary<Item, IEnumerable<int>> AllItems => slotsByItem;

		public void Initilize( int cols, int rows )
		{
			slots = new Item[cols * rows];
			size = new ( cols, rows );
		}

		public void AddItemAtSlots( List<int> slotsToPlace, IItemInfo itemInfo )
		{
			Item item = itemInfo as Item;
			foreach ( var slot in slotsToPlace ) slots[slot] = item;
			slotsByItem.Add( item, slotsToPlace );
			OnChange?.Invoke( );
		}

		public void RemoveItem( Item item )
		{
			foreach ( var slot in slotsByItem[item] )
			{
				if( slots[slot] == item ) slots[slot] = null;
			}
			slotsByItem.Remove( item );
			OnChange?.Invoke( );
		}

		public void TryToAddItemAtFreeSlots( Item item )
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

		private bool TryToFillAt( int col, int row, Item item )
		{
			List<int> fitSlots = new( );
			for ( int i = 0; i < item.CurrentHeight; i++ )
			{
				for ( int j = 0; j < item.CurrentWidth; j++ )
				{
					if ( !item.CurrentShape[j, i] ) continue;
					if ( (col + j) >= size.cols || (row + i) >= size.rows ) return false;
					if ( slots[col + j + (row + i) * size.cols] is not null ) return false;
					fitSlots.Add( col + j + (row + i) * size.cols );
				}
			}
			AddItemAtSlots( fitSlots, item );
			return true;
		}
	}
}
