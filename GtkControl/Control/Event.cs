using System;
using Gtk;
using Cairo;
namespace GtkControl.Control
{
	public class Event:BaseItem
	{
		Cairo.Gradient pat;
		double radius;
		double line_width;
		Cairo.Color fill_color;
		Cairo.Color line_color;

		public Event (string pName, string cap, ElementType typeEl, double radius)
		:base(pName,cap,typeEl,2*radius,2*radius)
		{
			this.radius = radius;
			switch ((ElementType)ELType) {
			case ElementType.START_EVENT:{
				line_width = 4;
				fill_color = new Cairo.Color (0.8,1,0.5,1);
				line_color = new Color (0.35,0.65,0.08);
				break;
			}
			case ElementType.END_EVENT:{
				line_width = 5;
				fill_color = new Cairo.Color (0.93,0.7,0.7,1);
				line_color = new Color (0.6,0,0);
				break;
			}
			}
			var x = line_width/2;
			pat = new Cairo.LinearGradient(x,x, x+2*radius, x+2*radius);
	        pat.AddColorStop (0, new Cairo.Color (1,1,1,1));
	        pat.AddColorStop (1, fill_color);
		}
		public override void Paint (Context g)
		{

			g.Save ();
			g.Arc(radius,radius,radius-line_width/2,0,2*Math.PI);
	        g.Pattern = pat;
	        g.FillPreserve ();
	 
	        g.Restore ();
			g.Color = line_color;
	        g.LineWidth = line_width;
	        g.Stroke ();
		
		}
		public override void PaintMask (Context g)
		{
			Paint(g);
		}
	}
}

