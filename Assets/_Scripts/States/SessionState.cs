using UnityEngine;

namespace Chafear.GameLoop
{
	public sealed class SessionState : IState
	{
		private readonly GameStateMachine stateMachine;

		public SessionState( GameStateMachine stateMachine )
		{
			this.stateMachine = stateMachine;
		}

		public void Enter( )
		{
			//Game Loop Starting Here
			Debug.Log( "Starting Game!" );
		}

		public void Exit( )
		{
			
		}
	}
}
