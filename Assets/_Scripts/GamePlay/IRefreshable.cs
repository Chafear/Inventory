using System;

namespace Chafear
{
	public interface IRefreshable
	{
		public event Action OnChange;
	}
}
