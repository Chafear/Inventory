using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Chafear.Utils.InputSystem
{
	public sealed class InputSystem : IInputSystem, ITickable
	{
		private List<InputBind> binds = new( );
		private InputMap map = new ();

		public void Tick( )
		{
			Process( );
		}

		public void Process( )
		{
			for ( int i = binds.Count - 1; i >=0; i-- )
			{
				switch ( binds[i].ActionType )
				{
					case InputActionType.OnUp: CheckUpTrigger( binds[i] ); break;
					case InputActionType.OnDown: CheckDownTrigger( binds[i] ); break;
				}
			}
		}

		public void Subscribe( InputBind inputBind )
		{
			binds.Add( inputBind );
		}

		public void UnSubscribe( InputBind inputBind )
		{
			binds.Remove( inputBind );
		}

		private void CheckUpTrigger( InputBind bind )
		{
			if ( Input.GetKeyUp( map.DefaultMap[bind.Key] ) ) bind.OnAction.Invoke( );
		}

		private void CheckDownTrigger( InputBind bind )
		{
			if ( Input.GetKeyDown( map.DefaultMap[bind.Key] ) ) bind.OnAction.Invoke( );
		}
	}
}
