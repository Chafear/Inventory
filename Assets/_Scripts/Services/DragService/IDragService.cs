namespace Chafear.UI
{
	public interface IDragService
	{
		public void StartDragFor( IDraggable dragObject );
		public void ProcessDrag( );
		public void TryToDrop( );
	}
}
