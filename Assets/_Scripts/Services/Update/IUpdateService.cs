using System;

namespace Chafear.Utils.Update
{
	public interface IUpdateService
	{
		event Action OnUpdate;
	}
}
