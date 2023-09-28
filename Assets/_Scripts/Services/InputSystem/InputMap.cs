using System.Collections.Generic;
using UnityEngine;

namespace Chafear.Utils.InputSystem
{
	public sealed class InputMap
	{
		public Dictionary<EInputKey, KeyCode> DefaultMap = new( ) 
		{
			{ EInputKey.Submit, KeyCode.Mouse0 },
			{ EInputKey.Rotate, KeyCode.R }
		};
	}
}
