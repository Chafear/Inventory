using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Chafear.Utils
{
	public class Utilities
	{
		public class ArrayUtility
		{
			public static bool[,] RotateArrayClockwise( bool[,] src )
			{
				int width;
				int height;
				bool[,] dst;

				width = src.GetUpperBound( 0 ) + 1;
				height = src.GetUpperBound( 1 ) + 1;
				dst = new bool[height, width];

				for ( int row = 0; row < height; row++ )
				{
					for ( int col = 0; col < width; col++ )
					{
						int newRow;
						int newCol;

						newRow = col;
						newCol = height - (row + 1);

						dst[newCol, newRow] = src[col, row];
					}
				}

				return dst;
			}
		}
		

		public class TextureUtility
		{
			public static Texture2D GenerateTexture( int widtch, int height, bool[,] shape )
			{
				//Debug.Log(widtch +" " + height +" " + shape.Length);
				var texture = new Texture2D( widtch * Constants.UI.TextureScale,
							height * Constants.UI.TextureScale, TextureFormat.ARGB32, false );
				texture.filterMode = FilterMode.Point;
				texture.wrapMode = TextureWrapMode.Clamp;

				for ( int i = 0; i < widtch; i++ )
				{
					for ( int j = 0; j < height; j++ )
					{
						float setOpacity = shape[i, j] ? 1 : 0f;
						Color[] colors = new Color[Constants.UI.TextureScale * Constants.UI.TextureScale];
						Array.Fill( colors, new Color( 1, 1, 1, setOpacity ) );
						texture.SetPixels( i * Constants.UI.TextureScale, (height - j - 1) * Constants.UI.TextureScale,
							Constants.UI.TextureScale, Constants.UI.TextureScale, colors );
					}
				}
				texture.Apply( );
				return texture;
			}

			public static Sprite GenerateSpriteFromTexutre(Texture2D texture )
			{
				return Sprite.Create(
							texture,
							new Rect( 0, 0, texture.width, texture.height ),
							new Vector2( 0, 0 ),
							pixelsPerUnit: 1,
							extrude: 0,
							meshType: SpriteMeshType.FullRect );
			}
		}
	}
}
