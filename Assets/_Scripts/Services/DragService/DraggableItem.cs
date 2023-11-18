using Chafear.Data;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chafear.UI.Utils
{
	public class DraggableItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,
		IPointerExitHandler, IDraggable
	{
		protected IItemInfo itemInfo;
		protected bool isDragging = false;

		[SerializeField] private CanvasGroup cg;

		private IDragService dragService;

		public GameObject GameObject => gameObject;

		public Action<IItemInfo> OnDrop {  get; set; }

		public IItemInfo ItemInfo => itemInfo;

		public void Construct( IDragService dragService )
		{
			this.dragService = dragService;
		}

		public void OnPointerDown( PointerEventData eventData )
		{
			StartDrag( );
		}

		public void StartDrag( )
		{
			isDragging = true;
			cg.SetNotInteractible( );
			dragService.StartDragFor( this );
			OnStartDrag( );
		}

		public void StopDrag( )
		{
			isDragging = false;
			cg.SetInteractible( );
			OnStopDrag( );
		}

		public virtual void OnPointerEnter( PointerEventData eventData ) { }

		public virtual void OnPointerExit( PointerEventData eventData ) { }

		protected virtual void OnStartDrag( ) { }

		protected virtual void OnStopDrag( ) { }
	}
}
