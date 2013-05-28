using System;
using System.Collections.Generic;
using Cairo;
using Gtk;

namespace GtkControl.Control
{
    /// <summary>
    /// Стрелки
    /// </summary>
    public class Flow : BaseItem
    {
        #region Aggregations


        #endregion

        #region Compositions


        #endregion

        #region Attributes
        public string sourceRef;
        public string targetRef;

        public string conditionalExpression;

        private double arrow_degrees;
        private double arrow_lenght;
        protected double signX;
        protected double signY;
		protected Color fill_color;
        protected Color line_color;
        public List<Cairo.PointD> WayPoints;
        #endregion


        #region Public methods

        public Flow (string pName, string cap, BPMNElementType typeEl, double _width, double _height, PointD start, PointD end)
            : base(pName, cap, typeEl, _width, _height)
		{
			WayPoints = new List<PointD> ();
			arrow_degrees = 0.5;
			arrow_lenght = 15;
			WayPoints.Clear ();
			WayPoints.Add (start);
			
			WayPoints.Add (end);

            var dX = WayPoints[WayPoints.Count - 1].X - WayPoints[0].X;
            var dY = WayPoints[WayPoints.Count - 1].Y - WayPoints[0].Y;
			signX = dX / Math.Abs (dX);
			signY = dY / Math.Abs (dY);
			
			
			//todo path finder

            WayPoints.Insert(WayPoints.Count - 1, new PointD(WayPoints[0].X + dX / 2, WayPoints[0].Y));
            WayPoints.Insert(WayPoints.Count - 1, new PointD(WayPoints[0].X + dX / 2, WayPoints[WayPoints.Count-1].Y));
            WayPoints.Insert(WayPoints.Count - 1, new PointD(WayPoints[0].X + 3 * dX / 4, WayPoints[WayPoints.Count - 1].Y));
            WayPoints.Insert(WayPoints.Count - 1, new PointD(WayPoints[0].X + 3 * dX / 4, WayPoints[WayPoints.Count - 1].Y + 50));
            WayPoints.Insert(WayPoints.Count - 1, new PointD(WayPoints[0].X + 7 * dX / 8, WayPoints[WayPoints.Count - 1].Y + 50));
            WayPoints.Insert(WayPoints.Count - 1, new PointD(WayPoints[0].X + 7 * dX / 8, WayPoints[WayPoints.Count - 1].Y + 100));
            WayPoints.Insert(WayPoints.Count - 1, new PointD(WayPoints[0].X + 3 * dX / 4, WayPoints[WayPoints.Count - 1].Y + 100));
            WayPoints.Insert(WayPoints.Count - 1, new PointD(WayPoints[0].X + 3 * dX / 4, WayPoints[WayPoints.Count - 1].Y + 150));
            WayPoints.Insert(WayPoints.Count - 1, new PointD(WayPoints[0].X + 29 * dX / 32, WayPoints[WayPoints.Count - 1].Y + 150));
            WayPoints.Insert(WayPoints.Count - 1, new PointD(WayPoints[0].X + 29 * dX / 32, WayPoints[WayPoints.Count - 1].Y));
		}

        #endregion


        #region Protected methods

        #endregion


        #region Private methods
        /// <summary>
        /// Рассчет координат для направленной стрелки
        /// </summary>
        /// <param name='last_x'>
        /// Координата x для точки последнего изгиба.
        /// </param>
        /// <param name='last_y'>
        /// Start_y.
        /// </param>
        /// <param name='end_x'>
        /// End_x.
        /// </param>
        /// <param name='end_y'>
        /// End_y.
        /// </param>
        /// <param name='x1'>
        /// X1.
        /// </param>
        /// <param name='y1'>
        /// Y1.
        /// </param>
        /// <param name='x2'>
        /// X2.
        /// </param>
        /// <param name='y2'>
        /// Y2.
        /// </param>
        protected void calcVertexes(double last_x, double last_y, double end_x, double end_y, out double x1, out double y1, out double x2, out double y2)
        {
            double angle = Math.Atan2(end_y - last_y, end_x - last_x) + Math.PI;
            x1 = end_x + arrow_lenght * Math.Cos(angle - arrow_degrees);
            y1 = end_y + arrow_lenght * Math.Sin(angle - arrow_degrees);
            x2 = end_x + arrow_lenght * Math.Cos(angle + arrow_degrees);
            y2 = end_y + arrow_lenght * Math.Sin(angle + arrow_degrees);

        }
        #endregion


        #region MVObject members
        public override void Paint (Cairo.Context gr)
		{

			var radius = 15;
			//gr.NewPath ();
			gr.MoveTo (WayPoints [0]);
			
			//gr.Arc (StartX + dX / 2 + radius * signX, EndY - radius * signY, radius, Math.PI, 3*Math.PI / 2);
			//gr.MoveTo(EndX-dX/2+radius*signX,EndY);
			//gr.LineTo (EndX  10, EndY - 5);
			var count = WayPoints.Count;
			if (count > 2) {
				for (int i = 1; i< count-1; i++) {
					var length1 = Math.Sqrt(Math.Pow(WayPoints [i - 1].X - WayPoints [i].X,2) + Math.Pow(WayPoints [i - 1].Y - WayPoints [i].Y,2));
					var length2 = Math.Sqrt(Math.Pow(WayPoints [i + 1].X - WayPoints [i].X,2) + Math.Pow(WayPoints [i + 1].Y - WayPoints [i].Y,2));
					double angleA = Math.Atan2 (WayPoints [i - 1].Y - WayPoints [i].Y, WayPoints [i - 1].X - WayPoints [i].X);
					//приведение угла к (0;2*pi)
					angleA = (angleA >= 0) ? angleA : (angleA + 2 * Math.PI) % (2 * Math.PI);
					double angleB = Math.Atan2 (WayPoints [i + 1].Y - WayPoints [i].Y, WayPoints [i + 1].X - WayPoints [i].X);
					//приведение угла к (0;2*pi)
					angleB = (angleB >= 0) ? angleB : (angleB + 2 * Math.PI) % (2 * Math.PI);
					double angleG = angleB - angleA;
					angleG = Math.Abs (angleG) > Math.PI / 2 ?  Math.Sign(angleG)*((Math.Abs(angleG)-2*Math.PI)) : angleG;
					//3 точки находтся на одной прямой текущий узел должен быть удален
					if (angleG == Math.PI) 
						continue;	
					
					var krad = radius / Math.Tan (Math.Abs (angleG / 2));
					var kxc = WayPoints [i].X + krad * Math.Cos (angleA);
					var kyc = WayPoints [i].Y + krad * Math.Sin (angleA);

					var xc = WayPoints [i].X + radius / Math.Sin (Math.Abs (angleG / 2)) * Math.Cos (angleA + angleG / 2);
					var yc = WayPoints [i].Y + radius / Math.Sin (Math.Abs (angleG / 2)) * Math.Sin (angleA + angleG / 2);
					
					var rxc = WayPoints [i].X + krad * Math.Cos (angleB);
					var ryc = WayPoints [i].Y + krad * Math.Sin (angleB);
					
					
					if ((length1 < 2 * radius) || (length2 < 2 * radius)) {
						gr.LineTo (WayPoints [i]);
					} else {
						gr.LineTo (kxc, kyc);
						if (angleG > 0) {
							gr.ArcNegative (xc, yc, radius, Math.PI + angleB, Math.PI + angleA);					
						} else {
							gr.Arc (xc, yc, radius, Math.PI + angleB, Math.PI + angleA);
						}
						gr.MoveTo (rxc, ryc);
					}
				}
			}
			
			gr.LineTo (WayPoints [WayPoints.Count - 1]);
			gr.LineJoin = LineJoin.Round;
			//gr.ClosePath ();
            //gr.Antialias = Antialias.Subpixel;
            gr.Stroke();
        }
        public override void PaintMask (Context g)
		{
			var temp_col = line_color;
			line_color = new Color (1, 1, 1);
			Paint (g);
			line_color = (Cairo.Color)temp_col;
        }
    }
        #endregion


    /// <summary>
    /// Unconditional sequence flow.																		
    /// </summary> 
    public class UnCondSeqFlow : Flow
    {
        public UnCondSeqFlow(string pName, string cap, BPMNElementType typeEl, double _width, double _height, PointD start, PointD end)
            : base(pName, cap, typeEl, _width, _height, start, end)
        {
            line_color = new Color(0, 0, 0);
        }
        public override void Paint (Context gr)
		{
			gr.LineWidth = 2;
			base.Paint (gr);
			double x1;
			double y1;
			double x2;
			double y2;
			calcVertexes (
				WayPoints [WayPoints.Count - 2].X,
				WayPoints [WayPoints.Count - 2].Y,
				WayPoints [WayPoints.Count - 1].X,
				WayPoints [WayPoints.Count - 1].Y, 
				out x1, out y1, out x2, out y2
			);
			gr.MoveTo (WayPoints [WayPoints.Count - 1]);
            gr.LineTo(x1, y1);
            gr.LineTo(x2, y2);
            gr.ClosePath();
            gr.Color = line_color;
            gr.StrokePreserve();
            gr.Color = line_color;
            gr.Fill();
        }
		/// <summary>
        /// Отрисовка маски
        /// </summary>
        /// <param name="g"></param>
        public override void PaintMask (Context g)
		{
			base.PaintMask (g);
			var temp_col = line_color;
			line_color = new Color (1, 1, 1);
			Paint (g);
			line_color = (Cairo.Color)temp_col;
        }
    }
    /// <summary>
    /// Conditonal sequence flow.
    /// </summary>
    public class CondSeqFlow : Flow
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="cap"></param>
        /// <param name="typeEl"></param>
        /// <param name="_width"></param>
        /// <param name="_height"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public CondSeqFlow(string pName, string cap, BPMNElementType typeEl, double _width, double _height, PointD start, PointD end)
            : base(pName, cap, typeEl, _width, _height, start, end)
        {
            line_color = new Color(0, 0, 0);
            fill_color = new Color(1, 1, 1);
        }
        public override void Paint (Context gr)
		{
			gr.LineWidth = 2;
			base.Paint (gr);
			double x1;
			double y1;
			double x2;
			double y2;
			double x3;
			double y3;

			calcVertexes (
				WayPoints [WayPoints.Count - 2].X,
				WayPoints [WayPoints.Count - 2].Y,
				WayPoints [WayPoints.Count - 1].X,
				WayPoints [WayPoints.Count - 1].Y, 
				out x1, out y1, out x2, out y2
			);
			gr.MoveTo (WayPoints [WayPoints.Count - 1]);
			gr.LineTo (x1, y1);
			//gr.MoveTo(WayPoints [WayPoints.Count - 1]);
			gr.LineTo (x2, y2);
			gr.ClosePath ();

			gr.Color = line_color;
			gr.StrokePreserve ();
			gr.Color = line_color;
			gr.Fill ();

			gr.NewPath ();
			double angle = Math.Atan2 (WayPoints [0].Y - WayPoints [1].Y, WayPoints [0].X - WayPoints [1].X) + Math.PI;
			var arrow_lenght = 10;
			var arrow_degrees = 0.5;
			x1 = WayPoints [0].X + arrow_lenght * Math.Cos (angle - arrow_degrees);
			y1 = WayPoints [0].Y + arrow_lenght * Math.Sin (angle - arrow_degrees);
			x2 = WayPoints [0].X + arrow_lenght * Math.Cos (angle + arrow_degrees);
			y2 = WayPoints [0].Y + arrow_lenght * Math.Sin (angle + arrow_degrees);
			x3 = WayPoints [0].X + arrow_lenght * 2 * Math.Cos (angle);
			y3 = WayPoints [0].Y + arrow_lenght * 2 * Math.Sin (angle);
			gr.MoveTo (WayPoints [0]);
            gr.LineTo(x1, y1);
            gr.LineTo(x3, y3);
            gr.LineTo(x2, y2);
            gr.ClosePath();
            gr.Color = line_color;
            gr.StrokePreserve();
            gr.Color = fill_color;
            gr.Fill();
        }

        /// <summary>
        /// Отрисовка маски
        /// </summary>
        public override void PaintMask (Context g)
		{
			base.PaintMask (g);
			var temp_col = line_color;
			line_color = new Color (1, 1, 1);
			Paint (g);
			line_color = (Cairo.Color)temp_col;
		
        }
    }
    /// <summary>
    /// Поток сообщений.
    /// </summary>
    public class MessageFlow : Flow
    {
        public MessageFlow(string pName, string cap, BPMNElementType typeEl, double _width, double _height, PointD start, PointD end)
            : base(pName, cap, typeEl, _width, _height, start, end)
        {
            line_color = new Color(0, 0, 0);
            fill_color = new Color(1, 1, 1);
        }
        public override void Paint (Context gr)
		{

			gr.LineWidth = 2;
			double[] dash = { 10, 2 };
			double[] none_dash = { 10, 0 };
			gr.SetDash (dash, -10);
			base.Paint (gr);
			gr.SetDash (none_dash, 0);
			double x1;
			double y1;
			double x2;
			double y2;
			calcVertexes (
				WayPoints [WayPoints.Count - 2].X,
				WayPoints [WayPoints.Count - 2].Y,
				WayPoints [WayPoints.Count - 1].X,
				WayPoints [WayPoints.Count - 1].Y, 
				out x1, out y1, out x2, out y2
			);
			gr.MoveTo (WayPoints [WayPoints.Count - 1]);
            gr.LineTo(x1, y1);
            gr.LineTo(x2, y2);
            gr.ClosePath();
            gr.Color = line_color;
            gr.StrokePreserve();
            gr.Color = fill_color;
            gr.Fill();
            gr.NewPath();
            gr.MoveTo(WayPoints[0]);
            gr.Arc(WayPoints[0].X, WayPoints[0].Y, 5, 0, 2 * Math.PI);
            gr.ClosePath();
            gr.Color = line_color;
            gr.StrokePreserve();
            gr.Color = fill_color;
            gr.Fill();
        }

        /// <summary>
        /// Отрисовка маски
        /// </summary>
        public override void PaintMask (Context g)
		{
			base.PaintMask (g);
			var temp_col = line_color;
			line_color = new Color (1, 1, 1);
			Paint (g);
			line_color = (Cairo.Color)temp_col;
        }
    }

}//end namespace GtkControl.Control