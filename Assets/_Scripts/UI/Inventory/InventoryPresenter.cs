using Chafear.Inventory;
using Chafear.Utils;
using Chafear.Utils.InputSystem;
using System.Collections.Generic;
using UiAnimation;
using UnityEngine;
using VContainer;

namespace Chafear.UI.Inventory
{
	public sealed class InventoryPresenter : MonoBehaviour
	{
		[SerializeField] private EEvents onShowEvent;
		[SerializeField] private InventoryDropContainer container;
		[SerializeField] private ItemsView itemsView;
		[SerializeField] private InventoryCellsView cellsView;
		[SerializeField] private UiAnimator animator;

		private IInventory inventory;
		private IDraggable draggingItem;

		private List<int> hoveredCells = new( );
		private List<Item> hoveredOnItems = new( );

		private bool isShown = false;

		public InventoryDropContainer Container => container;

		private void OnDestroy( )
		{
			container.OnValidate -= ValidateCells;
			container.OnClear -= ClearCells;
		}

		[Inject]
		public void Construct( UIEvents events, IAssetsProvider assetsProvider, IInputSystem inputSystem )
		{
			events.Subscribe( new EventField<IInventory>( onShowEvent, OnShow ) );
			container.OnValidate += ValidateCells;
			container.OnClear += ClearCells;
			itemsView.Construct( assetsProvider, inputSystem );
		}
		
		public void Construct( IDragService context, float scaleFactor )
		{
			container.Construct( context, scaleFactor );
			itemsView.Construct( context );
		}

		public void OnDragStarted( IDraggable draggingItem )
		{
			this.draggingItem = draggingItem;
			container.StartDrag( draggingItem.ItemInfo );
			itemsView.StartDrag( );
		}
		
		public void OnDragStopped( )
		{
			container.OnEndDrag( );
			itemsView.StopDrag( );
		}

		public void OnShow( IInventory inventory )
		{
			if ( isShown )
			{
				OnHide( );
				return;
			}
			animator.In( );
			isShown = true;
			hoveredCells.Clear(); 
			hoveredOnItems.Clear( );
			this.inventory = inventory;
			this.inventory.OnChange += Refresh;
			Refresh();
		}

		public void OnHide( )
		{
			isShown = false;
			inventory.OnChange -= Refresh;
			animator.Out( );
		}

		public bool IsCanDrop( )
		{
			if ( !container.IsEntered ) return false;
			if ( hoveredCells.Count != draggingItem.ItemInfo.ShapeSize ) return false;
			if ( hoveredOnItems.Count > 1 ) return false;
			return true;
		}

		public bool TryToGetItemForSwap( out IDraggable itemToSwapOnDrag )
		{
			itemToSwapOnDrag = null; 
			if ( hoveredOnItems.Count != 1 ) return false;
			itemToSwapOnDrag = itemsView.ViewByItem[hoveredOnItems[0]];
			return true;
		}

		public void ApplyDrop( ) 
			=> inventory.AddItemAtSlots( hoveredCells, draggingItem.ItemInfo );

		private void ValidateCells( IEnumerable<int> ids )
		{
			hoveredCells = ( List<int> ) ids;
			GetAllHoveredItems( );
			cellsView.ValidateCells(ids, inventory, draggingItem.ItemInfo );
		}

		private void ClearCells( )
		{
			cellsView.ClearValidation(  );
		}

		private void Refresh( ) 
		{
			itemsView.Refresh(inventory, container, cellsView);
		}

		private void GetAllHoveredItems( )
		{
			hoveredOnItems.Clear( );
			foreach ( var item in hoveredCells )
			{
				var hoveredItem = inventory.Slots[item];
				if ( hoveredItem is null ) continue;
				if ( hoveredItem == draggingItem.ItemInfo ) continue;
				if ( hoveredOnItems.Contains( hoveredItem ) ) continue;
				hoveredOnItems.Add( hoveredItem );
			}
		}
	}
}
