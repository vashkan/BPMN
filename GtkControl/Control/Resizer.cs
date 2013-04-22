using System;
namespace GtkControl.Control
{
    public class Resizer : MVObject
    {
        public Resizer()
            : base("resize", "", MVObject.ElementType.NONE, 10, 10)
        {
           // popup.Destroy();
            //popup = null;
        }
        public override void Paint (Cairo.Context g)
		{
			g.Save ();
			DrawRoundedRectangle (g, 0, 0, 10, 10, 5);
			g.Color = new Cairo.Color (0.98, 0.98, 0.98);
			g.FillPreserve ();
			g.Color = new Cairo.Color (0.01, 0.4, 0.6);
	 			
			g.LineWidth = 4;
			g.Stroke ();
			g.Restore ();
        }
		public override void PaintMask (Cairo.Context g)
		{
			base.PaintMask (g);
			Paint (g);
		}
    }
}