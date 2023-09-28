using Chafear.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chafear.UI.Utils
{
	public class DropContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		protected IDragService dragService;
		protected float scaleFactor;
		protected bool isEntered = false;

		public bool IsEntered => isEntered;

		public void Construct( IDragService context, float scaleFactor )
		{
			this.dragService = context;
			this.scaleFactor = scaleFactor;
			OnConstruct( );
		}

		public void OnDragProcess( ) 
		{
			if ( !isEntered ) return;
			OnDrag( );
		}

		public virtual void OnPointerEnter( PointerEventData eventData )
		{
			isEntered = true;
			OnEnter( );
		}

		public virtual void OnPointerExit( PointerEventData eventData )
		{
			isEntered = false;
			OnExit( );
		}

		public void OnEndDrag( ) { }

		public virtual void OnConstruct( ) { }

		public virtual void StartDrag( IItemInfo validatable ) { }

		protected virtual void OnDrag( ) { }
		
		protected virtual void OnExit( ) { }

		protected virtual void OnEnter( ) { }
	}
}
