using Chafear.Inventory;
using Chafear.UI.Utils;
using Chafear.Utils;
using Chafear.Utils.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chafear.UI
{
	public sealed class InventoryItemView : DraggableItem
	{
		[SerializeField] private Image triggerImage;
		[SerializeField] private Image pic;
		[SerializeField] private RectTransform rect;

		private Item item;
		private InputBind rotateBind;

		private IInputSystem inputSystem;

		private void Awake( )
		{
			rotateBind = new InputBind
			{
				ActionType = InputActionType.OnDown,
				Key = EInputKey.Rotate,
				OnAction = Rotate
			};
		}

		private void OnDestroy( )
		{
			if ( item != null ) item.OnChange -= SetVisual;
			if ( isDragging ) inputSystem.UnSubscribe( rotateBind );
		}

		public void Init( Item item, IAssetsProvider assetsProvider, IInputSystem inputSystem )
		{
			this.item = item;
			this.itemInfo = item;
			this.inputSystem = inputSystem;
			SetVisual( );
			item.OnChange += SetVisual;
			pic.color = new Color( 1, 1, 1, 0 );
			assetsProvider.LoadSingle<Sprite>( item.Data.IconId, 
				( sprite )	=> { pic.sprite = sprite; pic.color = new Color( 1, 1, 1, 1 ); } );
		}
		
		public override void OnPointerEnter( PointerEventData eventData )
		{
			pic.color = Color.red;
		}

		public override void OnPointerExit( PointerEventData eventData )
		{
			pic.color = Color.white;
		}

		protected override void OnStartDrag( )
		{
			inputSystem.Subscribe( rotateBind );
		}

		protected override void OnStopDrag( )
		{
			OnDrop?.Invoke( item );
			inputSystem.UnSubscribe( rotateBind );
		}

		private void Rotate( )
		{
			item.Rotate( );
		}

		private void SetVisual( )
		{
			var shapeTexture = item.Data.ShapeData.GetTextureByRotation( 0 );
			var shapeSprite = Utilities.TextureUtility.GenerateSpriteFromTexutre( shapeTexture );
			rect.transform.eulerAngles = new Vector3( 0, 0, -90 * item.CurrentRotation );
			triggerImage.sprite = shapeSprite;
			triggerImage.SetTreshold( );
			SetSize( );
		}

		private void SetSize( )
		{
			float widtch = item.CurrentWidth * Constants.UI.CellSize
							+ Constants.UI.CellOffset * (item.CurrentWidth - 1);
			float height = item.CurrentHeight * Constants.UI.CellSize
							+ Constants.UI.CellOffset * (item.CurrentHeight - 1);
			rect.sizeDelta = new Vector2( widtch, height );
		}
	}
}
