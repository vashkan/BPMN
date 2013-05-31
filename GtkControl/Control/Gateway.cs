using System;
using Cairo;
using Gtk;
namespace GtkControl.Control
{
	/// <summary>
	/// 
	/// </summary>
	public class Gateway:BaseItem
	{
	    private const double line_width = 4.5;

	    /// <summary>
		/// 
		/// </summary>
		/// <param name="pName"></param>
		/// <param name="cap"></param>
		/// <param name="_width"></param>
		/// <param name="_height"></param>
		public Gateway (string pName, string cap, float _width, float _height)
		:base(pName,cap,BPMNElementType.GATEWAY,_width,_height)
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
		public override void Paint (Context g)
		{
			g.Save ();
			var rad = Math.Min(Height,Width)/2-line_width/2;
			g.MoveTo (Width / 2,Height / 2);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
		public override void PaintMask (Context g)
		{
			g.Save ();
			var rad = Math.Min (Height,Width) / 2 - line_width / 2;
			g.MoveTo (Width / 2, Height / 2);
			g.RelMoveTo (-rad, 0);
			g.RelLineTo (+rad, -rad);
			g.RelLineTo (+rad, +rad);
			g.RelLineTo (-rad, +rad);
			g.RelLineTo (-rad, -rad);
			g.ClosePath ();	
			g.Color = new Color (1, 1, 1);
			g.LineWidth = line_width;
			g.StrokePreserve ();
			g.Color = new Color (1, 1, 1);
			g.Fill ();
			g.Restore ();
		}
	}
}

