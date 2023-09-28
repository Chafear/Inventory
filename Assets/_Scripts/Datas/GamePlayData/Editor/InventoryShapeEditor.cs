using Chafear.Utils;
using UnityEditor;
using UnityEngine;

namespace Chafear.Data
{
#if UNITY_EDITOR
	[CustomEditor( typeof( SoInventoryShape ) )]
	public class InventoryShapeEditor : Editor
	{
		SoInventoryShape shape;

		private void OnEnable( )
		{
			shape = ( SoInventoryShape ) target;
		}

		public void OnInspectorUpdate( )
		{
			this.Repaint( );
		}

		public override void OnInspectorGUI( )
		{
			serializedObject.Update( );

			var widtch = serializedObject.FindProperty( "Width" );
			var height = serializedObject.FindProperty( "Height" );

			widtch.intValue = (int)EditorGUILayout.Slider(
							"Widtch",
							widtch.intValue,
							Constants.Game.InventoryShapeMinSize, 
							Constants.Game.InventoryShapeMaxSize );

			height.intValue = (int)EditorGUILayout.Slider(
							"Height",
							height.intValue,
							Constants.Game.InventoryShapeMinSize, 
							Constants.Game.InventoryShapeMaxSize );

			var array = serializedObject.FindProperty( "OriginalShape" );

			if ( array.arraySize < (widtch.intValue * height.intValue) )
			{
				serializedObject.ApplyModifiedProperties( );
				return;
			}
			Vector2 schemePosition = DrawScheme( widtch.intValue, height.intValue, array );
			if ( Event.current.type == EventType.MouseDown && Event.current.button == 0 )
			{
				if(ProcessClick( Event.current.mousePosition, schemePosition,
					widtch.intValue, height.intValue, array ) )
				{
					serializedObject.FindProperty( "IsDirty" ).boolValue = true;
				}
			}
			serializedObject.ApplyModifiedProperties( );
		}

		private Vector2 DrawScheme(int width, int height, SerializedProperty array )
		{
			Rect r;
			r = EditorGUILayout.GetControlRect( false, 25 * (10 + 2) );
			Vector2 origin = new Vector2( r.center.x, r.yMin );
			for ( int x = 0; x < width; x++ )
			{
				for ( int y = 0; y < height; y++ )
				{
					r = TileRect( x, y, width, height, origin, 0.9f );
					Color c = Color.green;
					c.a = 0.1f;

					EditorGUI.DrawRect( r, array.GetArrayElementAtIndex(x * height + y).boolValue ? Color.green : Color.grey );
				}
			}
			return origin;
		}

		private static Rect TileRect( int x, int y, int width, int height, Vector2 position, float scale )
		{
			Vector2 offset = new Vector2( -width * 0.5f, height ) + Vector2.one * (1 - scale) * 0.5f;
			//return new Rect( (new Vector2( x, -y ) + offset) * 25 + position, Vector2.one * 25 * scale );
			return new Rect( position + new Vector2(x,y)*25 + offset , Vector2.one * 25 * scale );
		}

		private bool ProcessClick( Vector2 mousePosition, Vector2 schemePosition, int width, int height, SerializedProperty array )
		{
			for ( int x = 0; x < width; x++ )
			{
				for ( int y = 0; y < height; y++ )
				{
					if ( TileRect( x, y, width, height, schemePosition, 1 ).Contains( mousePosition ) )
					{
						var item = array.GetArrayElementAtIndex( x * height + y );
						item.boolValue = !item.boolValue;
						return true;
					}
				}
			}
			return false;
		}
	}
#endif
}
