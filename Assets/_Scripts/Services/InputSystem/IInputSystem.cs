namespace Chafear.Utils.InputSystem
{
	public interface IInputSystem
	{
		public void Process( );
		public void Subscribe( InputBind inputBind );
		public void UnSubscribe( InputBind inputBind );
	}
}
