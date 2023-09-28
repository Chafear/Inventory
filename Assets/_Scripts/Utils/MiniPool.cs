using System;
using System.Collections.Generic;

namespace Chafear.Utils
{
	// basically unity`s pool but with small tweaks and instatiate things on creation a prefer it this way
	public sealed class MiniPool<T> where T : class
	{
		private readonly Func<T> createFunc;
		private readonly Action<T> actionOnGet;
		private readonly Action<T> actionOnReturn;

		private Queue<T> queue = new Queue<T>( );

		public MiniPool( Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnReturn = null, int size = 10000 )
		{
			this.createFunc = createFunc;
			this.actionOnGet = actionOnGet;
			this.actionOnReturn = actionOnReturn;

			for ( int i = 0; i < size; i++ )
			{
				queue.Enqueue( createFunc.Invoke( ) );
			}
		}

		public T Get( )
		{
			if ( queue.Count == 0 ) queue.Enqueue( createFunc.Invoke( ) );
			var item = queue.Dequeue( );
			actionOnGet?.Invoke( item );
			return item;
		}

		public void Return( T t )
		{
			queue.Enqueue( t );
			actionOnReturn?.Invoke( t );
		}
	}
}
