using System;

namespace Chafear
{
	public abstract class EventField
	{
		public EEvents Type { get; protected set; }
	}

	public class EventField<T> : EventField
	{
		public event Action<T> Action;

		public EventField( EEvents type, Action<T> action )
		{
			Type = type;
			Action = action;
		}

		public void RaiseAction(T TObj) 
		{
			Action.Invoke( TObj );
		}
	}
}
