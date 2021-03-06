using System;
using System.IO;
using Cairo;
using Gtk;

//using Gdk;
namespace GtkControl.Control
{
	/// <summary>
	/// Ресайзер
	/// </summary>
	public class Resizer : Gtk.DrawingArea,IDragged
	{
		//Элемент к которому привязан ресайзер
		public BaseItem baseItem{ get; set;}
		/// <summary>
		/// Gets or sets the x.
		/// </summary>
		/// <value>The x.</value>
		public int X{ get; set;}
		/// <summary>
		/// Gets or sets the y.
		/// </summary>
		/// <value>The y.</value>
		public int Y{ get; set;}

		// <summary>
		/// Признак перемещения
		/// </summary>
		public virtual bool IsDragged { get; set; }

		static Gdk.Cursor hresizeCursor = new Gdk.Cursor (Gdk.CursorType.Sizing);
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
		public void Redraw ()
		{
			Console.WriteLine ("Redrawing");
			this.QueueDraw ();
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
		protected override bool OnExposeEvent (Gdk.EventExpose args)
		{	
			//перерисовка элемента 
			using (Context g = Gdk.CairoHelper.Create (args.Window)) {
				g.Antialias = Antialias.Subpixel;
				Paint (g);
			}
			return true;
		}
		
		/// <summary>
		/// Ресайзер
		/// </summary>
		public Resizer ()
		{
			SetSizeRequest (10, 10);
			this.Events |= Gdk.EventMask.EnterNotifyMask | Gdk.EventMask.LeaveNotifyMask;
			//mask
			this.Realized += delegate {				
				Gdk.Pixmap pm = new Gdk.Pixmap (this.GdkWindow, 10, 10, 1);
				using (Context crPix = Gdk.CairoHelper.Create(pm)) {
					crPix.Antialias = Antialias.None;
					crPix.Operator = Operator.Source;
					crPix.Source = new SolidPattern (new Color (0, 0, 0, 0));
					crPix.Rectangle (0, 0, Allocation.Width, Allocation.Height);
					crPix.Paint ();

					crPix.Operator = Operator.Over;
					crPix.NewPath ();
					PaintMask (crPix);
				}
				this.ParentWindow.InputShapeCombineMask (pm, 0, 0);
				this.ParentWindow.ShapeCombineMask (pm, 0, 0);
				pm.Dispose ();
			};
		}
		/// <summary>
		/// Изменение курсора при наведении указателя мыши
		/// </summary>
		/// <param name="evnt"></param>
		/// <returns></returns>
		protected override bool OnEnterNotifyEvent (Gdk.EventCrossing evnt)
		{
			
			GdkWindow.Cursor = hresizeCursor;
			return base.OnEnterNotifyEvent (evnt);
		}
		/// <summary>
		/// Восстановление курсора
		/// </summary>
		/// <param name="evnt"></param>
		/// <returns></returns>
		protected override bool OnLeaveNotifyEvent (Gdk.EventCrossing evnt)
		{
			GdkWindow.Cursor = null;
			return base.OnLeaveNotifyEvent (evnt);
		}
		
		/// <summary>
		/// Отрисовка графики элемента
		/// </summary>
		/// <param name="g"></param>
		public void Paint (Cairo.Context g)
		{
			g.Save ();
			g.MoveTo (8, 5);
			g.Arc (5, 5, 4, 0, 2 * Math.PI);
			g.Color = new Cairo.Color (0.98, 0.98, 0.98);
			g.FillPreserve ();
			g.Color = new Cairo.Color (0.01, 0.4, 0.6);
	 			
			g.LineWidth = 1;
			g.Stroke ();
			g.Restore ();
		}

		/// <summary>
		/// отрисовка маски
		/// </summary>
		/// <param name="g"></param>
		public  void PaintMask (Cairo.Context g)
		{
			g.Save ();
			g.MoveTo (8, 5);
			g.Arc (5, 5, 4, 0, 2 * Math.PI);
			g.Color = new Cairo.Color (1, 1, 1);
			g.FillPreserve ();
			g.Color = new Cairo.Color (1, 1, 1);
			g.LineWidth = 1;
			g.Stroke ();
			g.Restore ();
		}
	}
}