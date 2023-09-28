using Chafear.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Chafear
{
	public sealed class ViewBounds
	{
		private readonly float cellsSize;
		private readonly float cellsSizeLocal;
		private readonly int columns;
		private readonly int rows;
		private readonly Vector3 center;
		private readonly Vector3 leftCorner;
		private readonly Vector3 leftCornerLocals;

		private IItemInfo validate;

		public ViewBounds( float scaleFactor, RectTransform rect, int columns, int rows )
		{
			this.columns = columns;
			this.rows = rows;
			center = rect.transform.position;
			var size = rect.sizeDelta;
			leftCorner = center - new Vector3( size.x / 2, size.y / 2, 0 ) * scaleFactor;
			leftCornerLocals = - new Vector3( size.x / 2, size.y / 2, 0 ) ;
			cellsSize = size.x * scaleFactor / columns;
			cellsSizeLocal = size.x  / columns;
		}

		public Vector3 CalcPositionFor( IEnumerable<int> coords )
		{
			List<(int col, int row)> coordsMatrix = new( );
			foreach ( var item in coords )
			{
				int row = item / columns;
				int col = item - row * columns;
				coordsMatrix.Add( (col, row) );
			}
			int leftX = coordsMatrix[0].col;
			int leftY = coordsMatrix[0].row;
			int rightX = coordsMatrix[0].col;
			int rightY = coordsMatrix[0].row;

			foreach ( var item in coordsMatrix )
			{
				if( item.col <= leftX ) leftX = item.col;
				if( item.row <= leftY ) leftY = item.row;
				if( item.col >= rightX ) rightX = item.col;
				if( item.row >= rightY ) rightY = item.row;
			}

			float centerX =  (( float ) rightX - ( float ) leftX) / 2;
			float centerY = ( ( float ) rightY - ( float ) leftY) / 2;
			float offsetX = (leftX + centerX) * cellsSizeLocal;
			float offsetY = (rows - ( leftY + centerY )) * cellsSizeLocal;	

			Vector3 cellOffset = new Vector3( cellsSizeLocal / 2 , -cellsSizeLocal / 2, 0);

			return leftCornerLocals
				+ new Vector3( offsetX, offsetY, 0 )
				+ cellOffset;
		}

		public void UpdateItemInfo( IItemInfo validate )
		{
			this.validate = validate;
		}

		public bool GetShapeBounds( Vector3 inputMouse, out List<int> cells )
		{
			cells = new( );
			var mousePos = inputMouse - leftCorner;
			var currentShape = validate.CurrentShape;

			bool isEven = validate.CurrentRotation % 2 == 0;

			int width = isEven ? validate.CurrentWidth : validate.CurrentHeight;
			int height = isEven ? validate.CurrentHeight : validate.CurrentWidth;

			var isEvenX = width % 2 == 0;
			var isEvenY = height % 2 == 0;
			for ( int i = 0; i < height; i++ )
			{
				for ( int j = 0; j < width; j++ )
				{
					if ( !currentShape[j, i] ) continue;
					float offsetX = cellsSize * (isEvenX ? 0.5f : 0);
					float offsetY = cellsSize * (isEvenY ? 0.5f : 1f);
					
					var shapePosX = mousePos.x - cellsSize * (width / 2 - j) + offsetX;
					var shapePosY = mousePos.y + cellsSize * (height / 2 - i) + offsetY;
					
					int col = ( int ) (shapePosX / cellsSize);
					int row = rows - ( int ) (shapePosY / cellsSize);

					if ( col < 0 || col > (columns - 1) ) continue;
					if ( row < 0 || row > (rows - 1) ) continue;
					cells.Add( col + row * columns );
				}
			}
			return true;
		}
	}
}
