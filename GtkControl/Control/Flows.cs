using System;
using System.Collections.Generic;
using Cairo;
using Gtk;

namespace GtkControl.Control
{
    /// <summary>
    /// Стрелки
    /// </summary>
    public class Flow : MVObject
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
        /// <summary>
        /// 
        /// </summary>
        public double StartX
        {
            get
            {
                return m_StartX;
            }
            set
            {
                m_StartX = value;
            }
        }
        private double m_StartX;


        /// <summary>
        /// 
        /// </summary>
        public double StartY
        {
            get
            {
                return m_StartY;
            }
            set
            {
                m_StartY = value;
            }
        }
        private double m_StartY;


        /// <summary>
        /// 
        /// </summary>
        public double EndX
        {
            get
            {
                return m_EndX;
            }
            set
            {
                m_EndX = value;
            }
        }
        private double m_EndX;


        /// <summary>
        /// 
        /// </summary>
        public double EndY
        {
            get
            {
                return m_EndY;
            }
            set
            {
                m_EndY = value;
            }
        }
        private double m_EndY;


        /// <summary>
        /// 
        /// </summary>
        public double LastX
        {
            get
            {
                return m_LastX;
            }
            set
            {
                m_LastX = value;
            }
        }
        private double m_LastX;
        /// <summary>
        /// 
        /// </summary>
        public double LastY
        {
            get
            {
                return m_LastY;
            }
            set
            {
                m_LastY = value;
            }
        }
        private double m_LastY;


        public List<Cairo.PointD> WayPoints;
        #endregion


        #region Public methods

        public Flow (string pName, string cap, ElementType typeEl, double _width, double _height, PointD start, PointD end)
            : base(pName, cap, typeEl, _width, _height)
		{
			WayPoints = new List<PointD> ();
			arrow_degrees = 0.5;
			arrow_lenght = 15;
			StartX = start.X;
			StartY = start.Y;
			WayPoints.Clear ();
			WayPoints.Add (start);
			
			WayPoints.Add (end);
			EndX = end.X;
			EndY = end.Y;
			
			var dX = EndX - StartX;
			var dY = EndY - StartY;
			signX = dX / Math.Abs (dX);
			signY = dY / Math.Abs (dY);
			
			LastX = EndX - dX / 2 + 15 * signX;
			LastY = EndY;
			//todo path finder
			
			WayPoints.Insert (WayPoints.Count - 1, new PointD (StartX + dX / 2, StartY));
			WayPoints.Insert (WayPoints.Count - 1, new PointD (StartX + dX / 2, EndY));
			WayPoints.Insert (WayPoints.Count - 1, new PointD (StartX + 3 * dX / 4, EndY));
			WayPoints.Insert (WayPoints.Count - 1, new PointD (StartX + 3 * dX / 4, EndY + 50));
			WayPoints.Insert (WayPoints.Count - 1, new PointD (StartX + 7 * dX / 8, EndY + 50));
			WayPoints.Insert (WayPoints.Count - 1, new PointD (StartX + 7 * dX / 8, EndY + 100));
			WayPoints.Insert (WayPoints.Count - 1, new PointD (StartX + 3 * dX / 4, EndY + 100));
			WayPoints.Insert (WayPoints.Count - 1, new PointD (StartX + 3 * dX / 4, EndY + 150));
			WayPoints.Insert (WayPoints.Count - 1, new PointD (StartX + 28 * dX / 32, EndY + 150));
			//WayPoints.Insert (WayPoints.Count - 1, new PointD (StartX + 28 * dX / 32, EndY));
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

			//throw new Exception("The method or operation is not implemented.");
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
					angleG = Math.Abs (angleG) == 3 * Math.PI / 2 ? (-angleG / 3) : angleG;
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
        public override void PaintMask(Context g)
        {
            g.LineWidth = 12;
            Paint(g);
        }
    }
        #endregion


    /// <summary>
    /// Unconditional sequence flow.																		
    /// </summary> 
    public class UnCondSeqFlow : Flow
    {
        private Color line_color;
        private Color fill_color;
        public UnCondSeqFlow(string pName, string cap, ElementType typeEl, double _width, double _height, PointD start, PointD end)
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
    }
    /// <summary>
    /// Conditonal sequence flow.
    /// </summary>
    public class CondSeqFlow : Flow
    {
        private Color line_color;
        private Color fill_color;
        public CondSeqFlow(string pName, string cap, ElementType typeEl, double _width, double _height, PointD start, PointD end)
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
            gr.LineTo(x1, y1);
            //gr.MoveTo(EndX,EndY);
            gr.LineTo(x2, y2);
            gr.ClosePath();

            gr.Color = line_color;
            gr.StrokePreserve();
            gr.Color = line_color;
            gr.Fill();

            gr.NewPath();
            double angle = Math.Atan2(WayPoints[0].Y - WayPoints[1].Y, WayPoints[0].X - WayPoints[1].X) + Math.PI;
            var arrow_lenght = 10;
            var arrow_degrees = 0.5;
            x1 = StartX + arrow_lenght * Math.Cos(angle - arrow_degrees);
            y1 = StartY + arrow_lenght * Math.Sin(angle - arrow_degrees);
            x2 = StartX + arrow_lenght * Math.Cos(angle + arrow_degrees);
            y2 = StartY + arrow_lenght * Math.Sin(angle + arrow_degrees);
            x3 = StartX + arrow_lenght * 2 * Math.Cos(angle);
            y3 = StartY + arrow_lenght * 2 * Math.Sin(angle);
            gr.MoveTo(StartX, StartY);
            gr.LineTo(x1, y1);
            gr.LineTo(x3, y3);
            gr.LineTo(x2, y2);
            gr.ClosePath();
            gr.Color = line_color;
            gr.StrokePreserve();
            gr.Color = fill_color;
            gr.Fill();
        }
    }
    /// <summary>
    /// Поток сообщений.
    /// </summary>
    public class MessageFlow : Flow
    {
        private Color line_color;
        private Color fill_color;
        public MessageFlow(string pName, string cap, ElementType typeEl, double _width, double _height, PointD start, PointD end)
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
    }

}//end namespace GtkControl.Control