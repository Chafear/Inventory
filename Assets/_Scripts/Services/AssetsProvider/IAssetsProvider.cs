using System.Collections.Generic;
using System;
using Cysharp.Threading.Tasks;

namespace Chafear.Utils
{
	public interface IAssetsProvider
	{
		UniTask<IList<T>> Load<T>( string label, Action<T> onLoadCallback );
		void LoadSingle<T>( string label, Action<T> onLoadCallback );
	}
}
