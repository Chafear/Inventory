using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Chafear.GameLoop
{
	public sealed class EntryPointLevel : IStartable
	{
		private readonly GameStateMachine gameStateMachine;

		[Inject]
		public EntryPointLevel(GameStateMachine gameStateMachine)
		{
			this.gameStateMachine = gameStateMachine;
		}

		public void Start( )
		{
			gameStateMachine.EnterIn<InitializeLevelState>( );
		}
	}
}
