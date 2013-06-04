using System;
using Gtk;
using Cairo;
namespace GtkControl.Control
{
	/// <summary>
	/// События
	/// </summary>
	public abstract class Event:BaseItem
	{

		#region Переменные
	    
		protected Cairo.Gradient pat;
		double radius;
	    protected double line_width;
	    protected Cairo.Color fill_color;
		protected Cairo.Color line_color;

		#endregion

		#region Свойства

        /// <summary>
        /// Высота
        /// </summary>
		public override float Height {
			get {
				return (float)radius*2;
			}
			set {
				base.Height = value;
				radius = Math.Min (base.Height,base.Width)/2;
			}
		}
        /// <summary>
        /// Ширина
        /// </summary>
		public override float Width {
			get {
				return (float)radius*2;
			}
			set {
				base.Width = value;
				radius = Math.Min (base.Height,base.Width)/2;
			}
		}
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		/// <value>The type of the event.</value>
		public abstract EventType EventType {
			get;
		}

		#endregion
		/// <summary>
		/// События
		/// </summary>
		/// <param name="pName"></param>
		/// <param name="cap"></param>
		/// <param name="typeEl"></param>
		/// <param name="radius"></param>
		public Event (string pName, string cap, float radius)
		:base(pName,cap,2*radius,2*radius)
		{
			this.radius = radius;

		}

	    /// <summary>
	    /// перегружаемая функция отрисовки элемента
	    /// </summary>
	    /// <param name="g"></param>
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

	    /// <summary>
	    /// перегружаемая функция отрисовки маски для элемента
	    /// </summary>
	    /// <param name="g"></param>
	    public override void PaintMask (Context g)
		{
			g.Save ();
			g.Arc (radius, radius, radius - line_width / 2, 0, 2 * Math.PI);
			g.Color = new Color (1, 1, 1);
			g.FillPreserve ();
			g.Restore ();
			g.Color = new Color (1, 1, 1);
			g.LineWidth = line_width;
			g.Stroke ();
		}
	}
}

