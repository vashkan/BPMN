using System;
using Cairo;
using Gtk;
namespace GtkControl.Control
{
	/// <summary>
	/// Шлюз
	/// </summary>
	public class Gateway:BaseItem
	{
		#region переменные
	    private const double line_width = 4.5;
		/*private float height;
		private float width;*/
		#endregion

		#region Свойства

		public override BPMNElementType ElementType {
			get {
				return BPMNElementType.GATEWAY;
			}
		}

		public override float Width {
		get {
			return base.Width;
		}
		set {
			base.Width = value;
			}
		}
		public override float Height {
			get {
				return base.Height;
			}
			set {
				base.Height = value;
			}
		}
		
		#endregion

	    /// <summary>
		/// Шлюз
		/// </summary>
		/// <param name="pName"></param>
		/// <param name="cap"></param>
		/// <param name="_width"></param>
		/// <param name="_height"></param>
		public Gateway (string pName, string cap, float _width, float _height)
		:base(pName,cap,_width,_height)
		{
		}
        /// <summary>
        /// Отрисовка элемента Шлюз
        /// </summary>
        /// <param name="g"></param>
		public override void Paint (Context g)
		{
			g.Save ();
			var wid = Width / 2 - line_width / 2;
			var hei = Height / 2 - line_width / 2;
			
			g.MoveTo (Width / 2,Height / 2);
			g.RelMoveTo (-wid, 0);
			g.RelLineTo (+wid, -hei);
			g.RelLineTo (+wid, +hei);
			g.RelLineTo (-wid, +hei);
			g.RelLineTo (-wid, -hei);
			g.ClosePath ();	
			g.Color = new Color(166f/255f,166f/255f,29f/255f);
			g.LineWidth= line_width;
			g.StrokePreserve();
			g.Color = new Color(1,1,219f/255f);
			g.Fill();
			g.Restore();
		}
        /// <summary>
        /// Перегрузавемая функция отрисовки маски
        /// </summary>
        /// <param name="g"></param>
		public override void PaintMask (Context g)
		{
			g.Save ();
			var wid = Width / 2 - line_width / 2;
			var hei = Height / 2 - line_width / 2;

			g.MoveTo (Width / 2,Height / 2);
			g.RelMoveTo (-wid, 0);
			g.RelLineTo (+wid, -hei);
			g.RelLineTo (+wid, +hei);
			g.RelLineTo (-wid, +hei);
			g.RelLineTo (-wid, -hei);
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

