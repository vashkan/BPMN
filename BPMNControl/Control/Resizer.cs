using System;
using System.IO;
using Cairo;
using Gdk;
using Gtk;
using Color = Gdk.Color;
using Point = Cairo.Point;

//using Gdk;
namespace GtkControl.Control
{
    /// <summary>
    /// 
    /// </summary>
    public class Resizer : Gtk.DrawingArea
    {
		static Gdk.Cursor hresizeCursor = new Gdk.Cursor(Gdk.CursorType.Sizing);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous_screen"></param>
		protected override void OnScreenChanged (Gdk.Screen previous_screen)
		{
			base.OnScreenChanged (previous_screen);
		}
		
		/// <summary>
		/// 
		/// </summary>
		public void Redraw()
		{
			Console.WriteLine("Redrawing");
			this.QueueDraw();
		}
		
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
		protected override bool OnExposeEvent (Gdk.EventExpose args)
        {
            //цвет границы заголовка
            var gc = new Gdk.GC(args.Window)
            {
                RgbFgColor = new Gdk.Color((int) (0.01*255), (int) (255*0.4), (int) (255*0.6))
            };

            gc.SetLineAttributes(1, LineStyle.Solid, CapStyle.Butt, JoinStyle.Round);
            
            gc.Background = new Gdk.Color(250, 250, 250);
            //args.Window.DrawRectangle(gc,true, 0, 0,9,9);
            args.Window.DrawLines(gc,new[]
                                          {
                                              new Gdk.Point(0,0), 
                                              new Gdk.Point(0,9),
                                              new Gdk.Point(9,9),
                                              new Gdk.Point(9,0),
                                              new Gdk.Point(0,0)
                                          }
                                          );
            args.Window.Fullscreen();
            //QueueDraw();
            return true;
		/*			using (Context g = Gdk.CairoHelper.Create (args.Window)) {
						g.Antialias = Antialias.Subpixel;
						Paint(g);
					}*/
            
            return true;
		}
		
        /// <summary>
        /// 
        /// </summary>
        public Resizer ()
        {
			this.SetSizeRequest ((int)10, (int)10);
			this.Events |= Gdk.EventMask.EnterNotifyMask | Gdk.EventMask.LeaveNotifyMask;
			//mask
			this.Realized += delegate {

                var black = new Gdk.Color(0, 0, 0) { Pixel = 1 };
                var white = new Color(255, 255, 255) { Pixel = 0 };

                var pm = new Pixmap(GdkWindow, 10, 10, 1);
                var gc = new Gdk.GC(pm)
                {
                    Background = white,
                    Foreground = black
                };

                pm.DrawRectangle(gc, true,0,0,10,10);

                ShapeCombineMask(pm, 0, 0);

                QueueDraw();
                /*
				Gdk.Pixmap pm = new Gdk.Pixmap (this.GdkWindow, 10, 10, 1);
				using (ImageSurface draw = new ImageSurface (Format.Argb32,10,10)) {
					using (Context gr = new Context(draw)) {
						gr.Antialias = Antialias.None;
						Paint (gr);
					}
					using (Context crPix = Gdk.CairoHelper.Create(pm)) {
						crPix.Antialias = Antialias.None;
						crPix.Operator = Operator.Source;
						crPix.Source = new SolidPattern (new Color (0, 0, 0, 0));
						crPix.Rectangle (0, 0, Allocation.Width, Allocation.Height);
						crPix.Paint ();

						crPix.Operator = Operator.Over;
						crPix.NewPath ();
						Paint (crPix);
						crPix.ClosePath ();
					}
					//save the image as a png image.
					draw.WriteToPng (("mask_" + "resizer" + ".png"));
				}
				Gdk.Pixmap map1, map2;
				var px = new Gdk.Pixbuf ("mask_" + "resizer" + ".png");
				File.Delete ("mask_" + "resizer" + ".png");
				px.RenderPixmapAndMask (out map1, out map2, 255);

				this.ParentWindow.InputShapeCombineMask (map2, 0, 0);
				this.ParentWindow.ShapeCombineMask (map2, 0, 0);
				px.Dispose ();
				pm.Dispose ();
				map1.Dispose ();
				map2.Dispose ();*/
			};
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="evnt"></param>
		/// <returns></returns>
	
		protected override bool OnEnterNotifyEvent(Gdk.EventCrossing evnt)
       {
			
                GdkWindow.Cursor = hresizeCursor ;
                return base.OnEnterNotifyEvent(evnt);
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="evnt"></param>
		/// <returns></returns>
		protected override bool OnLeaveNotifyEvent(Gdk.EventCrossing evnt)
            {
                GdkWindow.Cursor = null;
                return base.OnLeaveNotifyEvent(evnt);
            }
		
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public void Paint (Cairo.Context g)
		{
			g.Save ();
			g.MoveTo (8, 5);
			g.Arc (5, 5, 4, 0, 2 * Math.PI);
			g.Color = new Cairo.Color (0.98, 0.98, 0.98);
			//g.ClosePath ();
			g.FillPreserve ();
			g.Color = new Cairo.Color (0.01, 0.4, 0.6);
	 			
			g.LineWidth = 2;
			g.Stroke ();
			g.Restore ();
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="g"></param>
		public  void PaintMask (Cairo.Context g)
		{
			Paint (g);
		}
    }
}