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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="cap"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="orientation"></param>
        public Pool(string pName, string cap, double width, double height, OrientationEnum orientation)
            : base(pName, cap, ElementType.POOL, width, height)
        {
            Orientation = orientation;
        }

        private OrientationEnum Orientation { get; set; }

        public override void Paint(Context g)
        {
            g.Save();
            //DrawRoundedRectangle(g, 2, 2, Width - 4, Height - 4, 1);
            g.Rectangle(0, 0, Width - 2, Height - 2);
   
           

            // Color for the stroke
            g.Color = new Color(0.01, 0.4, 0.6);

            g.LineWidth = 3;
            g.Stroke();
            
           
            g.SelectFontFace("Georgia", FontSlant.Normal, FontWeight.Bold);
            g.SetFontSize(18.0);

            //g.Rotate(-Math.PI / 2);
            TextExtents te = g.TextExtents(body);
            
            g.Rectangle(0,0,te.Height+8,Height);
            g.Color = new Color(0.98, 0.98, 0.98);
            g.FillPreserve();
            g.Color = new Color(0.01, 0.4, 0.6);
            g.Stroke();
            g.Rotate(-Math.PI / 2);
            switch(Orientation)
            {
                case OrientationEnum.Horizontal: 
                    {

                        if (te.Width > Height)
                        {
                            MinHeight = (int) te.Width;
                        }
                        g.Rectangle(0, 0, te.Height + 6, Height);
                        g.MoveTo( -(te.Width/2 + Height/2), te.Height + 3);
                        break;
                    }
                case OrientationEnum.Vertical:
                    {
                        if (te.Width > Width)
                        {
                            MinWidth = (int) te.Width;
                        }
                        g.Rectangle(0, 0, Width, te.Height+6);
                        g.MoveTo(2 - te.Width/2 + Width/2, 3 + te.Height);
                        break;
                    }
            }

            g.Color = new Color(0, 0, 0);
            g.ShowText(body);
            g.IdentityMatrix();
            g.Restore();
        }

        /// <summary>
        /// Отрисовка маски
        /// </summary>
        /// <param name="g"></param>
        public override void PaintMask(Context g)
        {
            g.Antialias = Antialias.Subpixel;
            Paint(g);
        }
    }
}