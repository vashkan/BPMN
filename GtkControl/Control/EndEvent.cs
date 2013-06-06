using System;

namespace GtkControl.Control
{
	[Serializable]
	public class EndEvent:Event
	{
		#region Переменные
		#endregion
		#region Свойства

		/// <summary>
		/// Тип элемента
		/// </summary>
		/// <value>The type of the element.</value>
		public override BPMNElementType ElementType {
			get {
				return BPMNElementType.END_NONE;
			}
		}
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		/// <value>The type of the event.</value>
		public override EventType EventType {
			get {
				return EventType.End;
			}
		}

		#endregion
		public EndEvent (string pName, string cap, float radius)
			:base(pName,cap,radius)
		{
			line_width = 3;
			fill_color = new Cairo.Color (0.93,0.7,0.7,1);
			line_color = new Cairo.Color (0.6,0,0);
			var x = line_width/2;
			pat = new Cairo.LinearGradient(x,x, x+2*radius, x+2*radius);
			pat.AddColorStop (0, new Cairo.Color (1,1,1,1));
			pat.AddColorStop (1, fill_color);
		}

	}
}

