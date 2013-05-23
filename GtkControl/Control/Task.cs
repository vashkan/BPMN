using System;
using Gtk;
using Cairo;

namespace GtkControl.Control
{
	/// <summary>
	/// 
	/// </summary>
	public class Task:BaseItem
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pName"></param>
		/// <param name="cap"></param>
		/// <param name="_width"></param>
		/// <param name="_height"></param>
		public Task (string pName, string cap, double _width, double _height)
		:base(pName,cap,BPMNElementType.TASK,_width,_height)
		{
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="g"></param>
		public override void Paint (Context g)
		{
			g.Save();
			DrawRoundedRectangle (g,1, 1, Width-2, Height-2, 10);

			Cairo.Gradient pat = new Cairo.LinearGradient( 2,2, 2,2+Height-4);
	        pat.AddColorStop(0, new Cairo.Color(0.98,0.98,1,1));
	        pat.AddColorStop(1, new Cairo.Color(0.90,0.9,1,1));
	        g.Pattern = pat;
	 
	        // Fill the path with pattern
	        g.FillPreserve();
	 
	        // We "undo" the pattern setting here
	        g.Restore();
	 
	        // Color for the stroke
	        g.Color = new Color (0.01, 0.4, 0.6);
	 
	        g.LineWidth = 2;
	        g.Stroke();


			g.Color = new Color(0, 0, 0);
			g.SelectFontFace("Georgia", FontSlant.Normal, FontWeight.Bold);
			g.SetFontSize(14.0);
			TextExtents te = g.TextExtents(Body);
			g.MoveTo(1 - te.Width/2+Width/2,
			         1 + te.Height/2+Height/2);
			g.ShowText(Body);
		}

	    /// <summary>
	    /// перегружаемая функция отрисовки маски для элемента
	    /// </summary>
	    /// <param name="g"></param>
	    public override void PaintMask (Context g)
		{
			g.Save ();
			DrawRoundedRectangle (g, 1, 1, Width - 2, Height - 2, 10);

			
			g.Color = new Color (1, 1, 1);
	 
			// Fill the path with pattern
			g.FillPreserve ();
	 
			// We "undo" the pattern setting here
			g.Restore ();
	 
			// Color for the stroke
			g.Color = new Color (1, 1, 1);
	 
			g.LineWidth = 2;
			g.Stroke ();


			g.Color = new Color (1, 1, 1);
			g.SelectFontFace ("Georgia", FontSlant.Normal, FontWeight.Bold);
			g.SetFontSize (14.0);
			TextExtents te = g.TextExtents (Body);
			g.MoveTo (1 - te.Width / 2 + Width / 2,
			         1 + te.Height / 2 + Height / 2);
			g.ShowText (Body);
		}
	}
}

