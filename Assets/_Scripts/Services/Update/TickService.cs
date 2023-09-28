using Cysharp.Threading.Tasks;
using System;

namespace Chafear.Utils.Update
{
	public sealed class TickService : IUpdateService
	{
		public event Action OnUpdate;

		public TickService( )
		{
			StartTicking().Forget();
		}

		private async UniTaskVoid StartTicking( )
		{
			while ( true )
			{
				OnUpdate?.Invoke( );
				await UniTask.Yield();
			}
		}
	}
}
