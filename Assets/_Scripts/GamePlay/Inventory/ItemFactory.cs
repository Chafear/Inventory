using Chafear.Data;
using Chafear.Utils;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Chafear.Inventory
{
	public sealed class ItemFactory
	{
		private readonly IList<SoItem> items = new List<SoItem>( );

		private ItemFactory( IList<SoItem> items )
		{
			this.items = items;
		}

		async public static UniTask<ItemFactory> BuildItemFactoryAsync( IAssetsProvider assetsProvider )
		{
			var task = await assetsProvider.
				Load<SoItem>( MetaConfig.Addressables.ItemsLabel, null );
			return new ItemFactory( task );
		}

		public Item GetRandomItem( )
		{
			int randomIdx = Random.Range(0, items.Count );
			return new Item( items[randomIdx] );
		}
	}
}
