using System;
using Cairo;
using Gtk;
namespace GtkControl.Control
{
	public class Gateway:MVObject
	{
		double line_width = 6;
		public Gateway (string pName, string cap, double _width, double _height)
		:base(pName,cap,ElementType.GATEWAY,_width,_height)
		{
		}
		public override void Paint (Context g)
		{
			g.Save ();
			var rad = min(new double[]{height,width})/2-line_width/2;
			g.MoveTo (width / 2, height / 2);
			g.RelMoveTo (-rad, 0);
			g.RelLineTo (+rad, -rad);
			g.RelLineTo (+rad, +rad);
			g.RelLineTo (-rad, +rad);
			g.RelLineTo (-rad, -rad);
			g.ClosePath ();	
			g.Color = new Color(166f/255f,166f/255f,29f/255f);
			g.LineWidth= line_width;
			g.StrokePreserve();
			g.Color = new Color(1,1,219f/255f);
			g.Fill();
			g.Restore();
		}
		public override void PaintMask (Context g)
		{
			Paint(g);
		}
	}
}

