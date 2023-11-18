using Chafear.Data;
using System;
using UnityEngine;

namespace Chafear.UI
{
	public interface IDraggable
	{
		public Action<IItemInfo> OnDrop { get; set; }
		public GameObject GameObject { get; }
		public IItemInfo ItemInfo { get;}
		public void StopDrag( );
	}
}
