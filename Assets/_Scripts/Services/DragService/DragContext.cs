using Chafear.UI.Inventory;
using Chafear.Utils.InputSystem;
using Chafear.Utils.Update;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Chafear.UI.Utils
{
	[RequireComponent(typeof(Canvas))]
	public class DragContext : MonoBehaviour, IDragService,ITickable
	{
		[SerializeField] private List<InventoryPresenter> containers;
		[SerializeField] private Canvas prefabDragCanvas;

		private Transform dragSpace;

		private IInputSystem inputSystem;
		//private IUpdateService updateService;
		private IDraggable current;

		private InputBind dropAction;

		private bool isDragging = false;

		private void Awake( )
		{
			Canvas canvas = Instantiate( prefabDragCanvas );
			dragSpace = canvas.transform;

			foreach ( var container in containers )
			{
				container.Construct( this, canvas.scaleFactor );
			}
			dropAction = new InputBind
			{
				Key = EInputKey.Submit,
				ActionType = InputActionType.OnDown,
				OnAction = TryToDrop
			};
		}

		private void OnDestroy( )
		{
			//updateService.OnUpdate -= ProcessDrag;
		}

		private void Update( )
		{
			ProcessDrag( );
		}

		[Inject]
		public void Construct( IInputSystem inputSystem )
		{
			this.inputSystem = inputSystem;
			//this.updateService = updateService;
			//updateService.OnUpdate += ProcessDrag;
		}

		public void StartDragFor( IDraggable dragObject )
		{
			inputSystem.Subscribe( dropAction );
			current = dragObject;
			foreach ( var container in containers )
			{
				container.OnDragStarted( dragObject );
			}
			StartDraggingAfterFrame( ).Forget( );
		}

		public void ProcessDrag( )
		{
			if ( !isDragging ) return;
			current.GameObject.transform.position = Input.mousePosition;
			foreach ( var container in containers )
			{
				container.Container.OnDragProcess( );
			}
		}

		public void TryToDrop( )
		{
			foreach( var container in containers )
			{
				if( container.IsCanDrop( ) )
				{
					current.StopDrag( );
					container.ApplyDrop( );
					StopDrag( );
					if ( container.TryToGetItemForSwap( out IDraggable itemToSwapOnDrag ) )
					{
						StartDragFor( itemToSwapOnDrag );
					}
					return;
				}
			}
		}

		//need this to not catch same event what triggered dragging
		private async UniTaskVoid StartDraggingAfterFrame( )
		{
#if UNITY_2023_1_OR_NEWER
			await UniTask.WaitForEndOfFrame();
#else
			await UniTask.WaitForEndOfFrame( this );
#endif			

			current.GameObject.transform.SetParent( dragSpace, false );

			isDragging = true;
		}

		private void StopDrag( )
		{
			foreach ( var container in containers )
			{
				container.OnDragStopped( );
			}
			current.StopDrag( );

			isDragging = false;
			current = null;

			inputSystem.UnSubscribe( dropAction );
		}

		public void Tick( )
		{
			throw new System.NotImplementedException( );
		}
	}
}
