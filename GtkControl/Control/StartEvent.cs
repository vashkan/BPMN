using System;

namespace GtkControl.Control
{
	public class StartEvent:Event
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
				return BPMNElementType.START_NONE;
			}
		}
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		/// <value>The type of the event.</value>
		public override EventType EventType {
			get {
				return EventType.Start;
			}
		}

		#endregion
		public StartEvent (string pName, string cap, float radius)
		:base(pName,cap,radius)
		{
			line_width = 2;
			fill_color = new Cairo.Color (0.8,1,0.5,1);
			line_color = new Cairo.Color (0.35,0.65,0.08);
			var x = line_width/2;
			pat = new Cairo.LinearGradient(x,x, x+2*radius, x+2*radius);
			pat.AddColorStop (0, new Cairo.Color (1,1,1,1));
			pat.AddColorStop (1, fill_color);
		}
	}
}

