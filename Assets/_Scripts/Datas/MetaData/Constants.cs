using System.Collections.Generic;
using UnityEngine;

namespace Chafear.Utils
{
	public sealed class Constants
	{
		public class Game
		{
			public const int InventoryShapeMinSize = 1;
			public const int InventoryShapeMaxSize = 5;
		}

		public class Test 
		{
			public const int AmountOfTestItemsToAdd = 3;
			public const int InventoryColumns = 6;
			public const int InventoryRows = 6;
			public const int LootColumns = 5;
			public const int LootRows = 2;
		}
		
		public class UI 
		{
			public const float CellSize = 50;
			public const float CellOffset = 2;
			public const float TriggerTreshlohd = .8f;
			public const int TextureScale = 32; // weird but 1x1 pixels not working properly with triggering system, probably something with smoothering dont know at this point
			public static readonly Dictionary<ERarity, Color> RarityColors = new( )
			{
				{ ERarity.Common, new Color(1,1,1,0) },
				{ ERarity.Rare, new Color(0,.8f,1,1) },
				{ ERarity.Epic, new Color(1,0,.8f,1) }
			};
		}
	}
}