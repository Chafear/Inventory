using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chafear
{
	public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T instance = default( T );

		public static T Instance
		{
			get { return instance; }
		}

		protected virtual void Awake( )
		{
			if ( instance != null )
			{
				Debug.LogError( "Singleton Copy Found" );
			}
			instance = ( T ) FindObjectOfType( typeof( T ) );
		}
	}

}
