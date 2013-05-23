using System;
using Gtk;
using Cairo;

namespace GtkControl.Control
{
    /// <summary>
    /// 
    /// </summary>
    public enum OrientationEnum
    {
        /// <summary>
        /// 
        /// </summary>
        Horizontal,

        /// <summary>
        /// 
        /// </summary>
        Vertical
    }

    /// <summary>
    /// Пул
    /// </summary>
    public class Pool : BaseItem
    {
		Color line_color;
		Color fill_color;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="cap"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="orientation"></param>
        public Pool (string pName, string cap, double width, double height, OrientationEnum orientation)
            : base(pName, cap, BPMNElementType.POOL, width, height)
		{
			fill_color = new Color (0.94, 0.94, 0.94);
			line_color = new Color (0.0, 0.0, 0.0);
            Orientation = orientation;
        }

        private OrientationEnum Orientation { get; set; }

        /// <summary>
        /// перегружаемая функция отрисовки элемента
        /// </summary>
        /// <param name="g"></param>
        public override void Paint (Context g)
		{
			g.Save ();
			//DrawRoundedRectangle(g, 2, 2, Width - 4, Height - 4, 1);
			
			g.Rectangle (1, 1, Width - 2, Height - 2);
   
           

			// Color for the stroke
			g.Color = line_color;

			g.LineWidth = 2;
			g.Stroke ();
            
           
			g.SelectFontFace ("Georgia", FontSlant.Normal, FontWeight.Bold);
			g.SetFontSize (18.0);

			TextExtents te = g.TextExtents (Body);
			PointD titlePoint = new PointD();
			switch (Orientation) {
			case OrientationEnum.Horizontal: 
				{

					if (te.Width > Height) {
						MinHeight = (int)te.Width;
					}
				g.LineCap = LineCap.Round;
					g.Rectangle (1, 1, te.Height+8, Height-2);
					titlePoint = new PointD (-(te.Width / 2 + Height / 2), te.Height + 2);
					break;
				}
			case OrientationEnum.Vertical:
				{
					if (te.Width > Width) {
						MinWidth = (int)te.Width;
					}
					g.Rectangle (0, 0, Width-2, te.Height + 8);
					titlePoint = new PointD (2 - te.Width / 2 + Width / 2, te.Height + 2);
					break;
				}
			}

			g.Color = fill_color;
			g.FillPreserve ();
			g.Color = line_color;
			g.Stroke ();
			if (Orientation  == OrientationEnum.Horizontal)
				g.Rotate (-Math.PI / 2);
			g.MoveTo (titlePoint);
			
			g.Antialias = Antialias.Subpixel;
            g.Color = line_color;
            g.ShowText(Body);
            g.IdentityMatrix();
            g.Restore();
        }

        /// <summary>
        /// Отрисовка маски
        /// </summary>
        /// <param name="g"></param>
        public override void PaintMask (Context g)
		{
			var temp_line_col = line_color;
			var temp_fill_col = fill_color;
			line_color = new Color (1, 1, 1);
			fill_color = new Color (1, 1, 1);
			Paint (g);
			line_color = (Cairo.Color)temp_line_col;
			fill_color = (Cairo.Color)temp_fill_col;
        }
    }
}