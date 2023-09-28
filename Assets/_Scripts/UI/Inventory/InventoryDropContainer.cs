using Chafear.Data;
using Chafear.UI.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chafear.UI
{
	public sealed class InventoryDropContainer : DropContainer
	{
		[SerializeField] private RectTransform rect;
		[SerializeField] private int columns = 6;
		[SerializeField] private int rows = 6;

		private ViewBounds validator;

		public event Action<IEnumerable<int>> OnValidate;
		public event Action OnClear;

		public override void OnConstruct( )
		{
			validator = new ViewBounds( scaleFactor, rect, columns, rows );
		}

		public Vector3 CalcPositionFor( IEnumerable<int> coords )
			=> validator.CalcPositionFor( coords );


		public override void StartDrag( IItemInfo validatable )
		{
			validator.UpdateItemInfo( validatable );
		}

		protected override void OnDrag( )
		{
			if ( !validator.GetShapeBounds( Input.mousePosition, out List<int> cells ) ) return;
			OnValidate?.Invoke( cells );
		}

		protected override void OnExit( )
		{
			OnClear?.Invoke( );
		}
	}
}
