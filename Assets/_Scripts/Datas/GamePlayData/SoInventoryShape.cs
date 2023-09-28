using Chafear.Utils;
using System;
using UnityEngine;

namespace Chafear.Data
{
	[CreateAssetMenu]
	public class SoInventoryShape : ScriptableObject
	{
		public int Width = Constants.Game.InventoryShapeMaxSize;
		public int Height = Constants.Game.InventoryShapeMaxSize;

		//public List<bool[]> Shapes = new List<bool[]>( ( int ) EItemRotation.NUN );
		public bool[] OriginalShape = new bool[0];
		public bool IsDirty = false;

		public bool[][,] RotatedShapes = new bool[( int ) EItemRotation.NUN][,];

		public int ShapeSize;
		private Texture2D[] textures = new Texture2D[( int ) EItemRotation.NUN];
		

#if UNITY_EDITOR
		private void OnValidate( )
		{
			if ( RotatedShapes[0] == null )
			{
				for ( int i = 0; i < RotatedShapes.Length; i++ )
				{
					RotatedShapes[i] = new bool[0,0];
				}
			}	
			if ( OriginalShape.Length != Width * Height ) 
			{
				Array.Resize( ref OriginalShape, Width * Height );
				RotatedShapes = new bool[( int ) EItemRotation.NUN][,];
			}
			if ( IsDirty )
			{
				RotatedShapes[0] = new bool[Width, Height];
				ShapeSize = 0;
				foreach ( var item in OriginalShape )
				{
					ShapeSize += item ? 1 : 0;
				}
				for ( int i = 0; i < Width; i++ )
				{
					for ( int j = 0; j < Height; j++ )
					{
						RotatedShapes[0][i, j] = OriginalShape[i * Height + j];
					}
				}
				for ( int i = 1; i < ( int ) EItemRotation.NUN; i++ )
				{
					RotatedShapes[i] = Utilities.ArrayUtility.RotateArrayClockwise( RotatedShapes[i - 1] );
				}
			}
		}
#endif

		public Texture2D GetTextureByRotation( EItemRotation rotation )
		{
			if ( textures[( int ) rotation] != null ) return textures[( int ) rotation];
			for ( int i = 0; i < RotatedShapes.Length; i++ )
			{
				var isEven = i % 2 == 0;
				textures[i] = Utilities.TextureUtility.GenerateTexture(
						isEven ? Width : Height,
						isEven ? Height : Width,
						RotatedShapes[i] );
			}
			return textures[( int ) rotation];
		}

		public (int width, int height, bool[,] shape) GetShapeByRotation( EItemRotation rotation ) 
		{
			var isEven = (int)rotation % 2 == 0;
			return (isEven ? Width : Height,
					isEven ? Height : Width,
					RotatedShapes[( int ) rotation]);
		}
	}
}
