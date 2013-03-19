using System;
using Gtk;
using Cairo;

namespace GtkControl.Control
{
	public class Task:MVObject
	{
		public Task (string pName, string cap, double _width, double _height)
		:base(pName,cap,ElementType.TASK,_width,_height)
		{
		}
		public override void Paint (Context g)
		{
			g.Save();
			DrawRoundedRectangle (g,2, 2, width-4, height-4, 40);

			Cairo.Gradient pat = new Cairo.LinearGradient( 2,2, 2,2+height-4);
	        pat.AddColorStop (0, new Cairo.Color (0.98,0.98,1,1));
	        pat.AddColorStop (1, new Cairo.Color (0.90,0.9,1,1));
	        g.Pattern = pat;
	 
	        // Fill the path with pattern
	        g.FillPreserve ();
	 
	        // We "undo" the pattern setting here
	        g.Restore ();
	 
	        // Color for the stroke
	        g.Color = new Color (0.01, 0.4, 0.6);
	 
	        g.LineWidth = 4;
	        g.Stroke ();


			g.Color = new Color(0, 0, 0);
			g.SelectFontFace("Georgia", FontSlant.Normal, FontWeight.Bold);
			g.SetFontSize(36.0);
			TextExtents te = g.TextExtents(body);
			g.MoveTo(2 - te.Width/2+width/2,
			         2 + te.Height/2+height/2);
			g.ShowText(body);
		}
		public override void PaintMask (Context g)
		{
			Paint(g);
		}
	}
}

