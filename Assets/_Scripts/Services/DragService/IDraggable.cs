using Chafear.Data;
using Chafear.Inventory;
using System;
using UnityEngine;

namespace Chafear.UI
{
	public interface IDraggable
	{
		public Action<Item> OnDrop { get; set; }
		public GameObject GameObject { get; }
		public IItemInfo ItemInfo { get;}
		public void StopDrag( );
	}
}
