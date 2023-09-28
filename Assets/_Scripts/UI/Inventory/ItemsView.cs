using Chafear.Inventory;
using Chafear.Utils;
using Chafear.Utils.InputSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chafear.UI.Inventory
{
	public sealed class ItemsView : MonoBehaviour
	{
		private const int PoolSize = 20;

		[SerializeField] private CanvasGroup canvasGroup;
		[SerializeField] private InventoryItemView prefab;
		[SerializeField] private RectTransform root;

		private IAssetsProvider assetsProvider;
		private IInputSystem inputSystem;
		private IDragService dragService;
		private IInventory inventory;

		private MiniPool<InventoryItemView> pool;

		private Dictionary<Item, InventoryItemView> viewByItem = new( );

		public Dictionary<Item, InventoryItemView> ViewByItem => viewByItem;

		private void Start( )
		{
			prefab.gameObject.SetActive( false );
			pool = new MiniPool<InventoryItemView>( CreatePooledItem, OnTakeFromPool, OnReturnedToPool, PoolSize );
		}

		public void Construct( IAssetsProvider assetsProvider, IInputSystem inputSystem )
		{
			this.assetsProvider = assetsProvider;
			this.inputSystem = inputSystem;
		}
		
		public void Construct( IDragService dragService)
		{
			this.dragService = dragService;
		}

		public void StartDrag( ) =>	canvasGroup.SetNotInteractible( );

		public void StopDrag( ) => canvasGroup.SetInteractible( );

		public void Refresh( IInventory inventory, InventoryDropContainer container, 
			InventoryCellsView cellsView )
		{
			cellsView.ClearRarity( );
			List<Item> cachedList = new( viewByItem.Keys );
			foreach ( var item in inventory.AllItems )
			{
				if ( !viewByItem.ContainsKey( item.Key ) )
				{
					InventoryItemView view = pool.Get( );
					view.Construct( dragService );
					view.Init( item.Key, assetsProvider, inputSystem );
					view.OnDrop = inventory.RemoveItem;
					viewByItem.Add(item.Key, view );
				}
				viewByItem[item.Key].transform.SetParent( root );
				viewByItem[item.Key].transform.localPosition = container.CalcPositionFor(item.Value);
				
				cellsView.SetRarity(item.Value, item.Key.Data.Rarity );
			}
			var dirtyList = cachedList.Except( inventory.AllItems.Keys ).ToList( );
			foreach ( var item in dirtyList )
			{
				
				viewByItem[item].OnDrop -= this.inventory.RemoveItem;
				pool.Return( viewByItem[item] );
				viewByItem.Remove( item );
			}
			this.inventory = inventory;

		}

		#region PoolActions
		private InventoryItemView CreatePooledItem( )
		{
			return Instantiate( prefab, transform );
		}

		private void OnReturnedToPool( InventoryItemView system )
		{
			system.gameObject.SetActive( false );
		}

		private void OnTakeFromPool( InventoryItemView system )
		{
			system.gameObject.SetActive( true );
		}
		#endregion
	}
}
