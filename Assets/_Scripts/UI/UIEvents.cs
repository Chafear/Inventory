using System.Collections.Generic;

namespace Chafear
{
	public class UIEvents
	{
		private Dictionary<EEvents, EventField> fields = new ();

		public void Subscribe( EventField eventField )
		{
			fields.Add(eventField.Type, eventField);
		}

		public EventField Get( EEvents type )
		{
			return fields[type];
		}
	}
}
