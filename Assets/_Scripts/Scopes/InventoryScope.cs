using Chafear.GameLoop;
using Chafear.Inventory;
using Chafear.UI;
using Chafear.UI.Utils;
using Chafear.Utils;
using Chafear.Utils.InputSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Chafear
{
	public sealed class InventoryScope : LifetimeScope
	{
		[SerializeField] private DragContext dragContext;
		[SerializeField] private HudButtonsPresenter hud;

		protected override void Configure( IContainerBuilder builder )
		{
			builder.RegisterEntryPoint<InputSystem>( Lifetime.Singleton ).As<IInputSystem>( );
			builder.RegisterComponent( dragContext );
			builder.Register<UIEvents>( Lifetime.Singleton ).AsSelf( );
			builder.Register<Player>( Lifetime.Singleton ).AsSelf( );
			builder.Register<InventoryFactory>( Lifetime.Singleton ).AsSelf( );
			builder.RegisterComponent( hud );
			builder.Register<GameStateMachine>( Lifetime.Singleton );
			builder.RegisterEntryPoint<EntryPointLevel>( Lifetime.Singleton );
		}
	}
}
