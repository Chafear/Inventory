using Chafear.Data;
using Chafear.Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chafear.UI.Inventory
{
	public sealed class InventoryCellsView : MonoBehaviour
	{
		[SerializeField] private List<InventoryCell> cells;

		private List<int> highligtedCells = new( );

		public void ValidateCells( IEnumerable<int> ids, IInventory inventory,
				IItemInfo draggingItem )
		{
			ClearValidation( ids );

			foreach ( var item in ids.Except( highligtedCells ).ToList( ) )
			{
				var hoveredItem = inventory.ItemsMap[item];
				bool isOccupied = hoveredItem is not null;
				cells[item].SetValidation( !isOccupied || (hoveredItem == draggingItem) );
			}
			highligtedCells = ( List<int> ) ids;
		}

		public void ClearRarity( )
		{
			foreach ( var item in cells ) item.ClearRarity( );
		}

		public void ClearValidation( IEnumerable<int> ids )
		{
			foreach ( var item in highligtedCells.Except( ids ).ToList( ) )
				cells[item].ClearValidation( );
		}
		
		public void ClearValidation( )
		{
			foreach ( var item in highligtedCells )	cells[item].ClearValidation( );
			highligtedCells.Clear();
		}

		public void SetRarity( IEnumerable<int> ids, ERarity rarity )
		{
			foreach ( var item in ids )	cells[item].SetRarity( rarity );
		}
	}
}
