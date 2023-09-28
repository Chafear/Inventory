using System;

namespace Chafear.Utils.InputSystem
{
	public struct InputBind
	{
		public EInputKey Key;
		public Action OnAction;
		public InputActionType ActionType;
	}

	public enum InputActionType
	{
		OnUp,
		OnDown
	}
}
