using System;

namespace GtkControl.Control
{
	[Serializable]
	public class IntermediateEvent : Event//,/* ICopiable, ICloneable
	{
		#region Переменные
		#endregion
		#region Свойства
		public override BPMNElementType ElementType {
			get {
				return BPMNElementType.INTERMEDIATE_NONE;
			}
		}

		public override EventType EventType {
			get {
				return EventType.Intermediate;
			}
		}
		
		#endregion

		public IntermediateEvent (string pName, string cap, float radius)
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

