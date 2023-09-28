using Chafear.Inventory;
using Chafear.Utils;
using System;
using System.Collections.Generic;
using VContainer;

namespace Chafear.GameLoop
{
	public sealed class GameStateMachine
	{
		private Dictionary<Type, IState> states;
		private IState currentState;

		[Inject]
		public GameStateMachine( Player player, IAssetsProvider assetsProvider,
			InventoryFactory inventoryFactory   )
		{
			states = new Dictionary<Type, IState>( )
			{
				[typeof( SessionState )] = new SessionState( this ),
				[typeof( InitializeLevelState )] = new InitializeLevelState( this, assetsProvider,
					player, inventoryFactory  )
			};
		}

		public void EnterIn<TState>( ) where TState : IState
		{
			if ( states.TryGetValue( typeof( TState ), out IState state ) )
			{
				currentState?.Exit( );
				currentState = state;
				currentState.Enter( );
			}
			else
			{
				throw new ArgumentException( "Not Registered State Passed" );
			}
		}
	}
}
