using Chafear.Data;
using System;

namespace Chafear.Inventory
{
	public sealed class Item : IItemInfo
	{
		private readonly SoItem data;

		private EItemRotation rotation;

		public event Action OnChange;

		public SoItem Data => data;

		public int CurrentWidth => data.ShapeData.Width;

		public int CurrentHeight => data.ShapeData.Height;

		public int CurrentRotation => (int)rotation;

		public bool[,] CurrentShape => data.ShapeData.RotatedShapes[(int)rotation];

		public int ShapeSize => data.ShapeData.ShapeSize;

		public Item( SoItem data ) 
		{
			this.data = data;
		}

		public void Rotate( )
		{
			rotation++;
			if ( rotation == EItemRotation.NUN ) rotation = 0;
			OnChange?.Invoke( );
		}
	}
}
