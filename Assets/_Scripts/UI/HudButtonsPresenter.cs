using Chafear.Inventory;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Chafear.UI
{
	public class HudButtonsPresenter : MonoBehaviour
	{
		[SerializeField] private Button inventoryButton;
		[SerializeField] private Button lootButton;

		private UIEvents uiEvents;
		
		[Inject]
		public void Construct( UIEvents uiEvents, Player player,
			InventoryFactory inventoryFactory )
		{
			this.uiEvents = uiEvents;
			inventoryButton.onClick.AddListener( ()=> 
					{ 
						ShowInventoryButton( player.Inventory );
						ShowLoot( inventoryFactory.CreateInventory( EInventoryTypes.Loot ) );
					} );
		}

		private void ShowInventoryButton( IInventory inventory )
		{
			var field = uiEvents.Get( EEvents.OpenInventory ) as EventField<IInventory>;
			field.RaiseAction( inventory );
		}

		private void ShowLoot( IInventory inventory )
		{
			var field = uiEvents.Get( EEvents.OpenLootInventory ) as EventField<IInventory>;
			field.RaiseAction( inventory );
		}
	}
}
