// MVObject.cs
// 
// Copyright (C) 2008 Olivier Lecointre - Cadexis
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Drawing;
using System.IO;
using Gtk;
using Cairo;
using Color = Cairo.Color;

namespace GtkControl.Control
{
	public enum ElementType
		{
			NONE,
			
			/// <summary>
			/// Начало процесса.
			/// </summary>
			/// 
			START_EVENT,
			/// <summary>
			/// Конец процесса.
			/// </summary>
			END_EVENT,
			
			/// <summary>
			/// Задача
			/// </summary>
			TASK,
			
			/// <summary>
			/// Безусловный поток операций
			/// </summary>
			SEQUENCE_FLOW_UNCONDITIONAL,
			
			/// <summary>
			/// Условный поток операций
			/// </summary>
			SEQUENCE_FLOW_CONDITIONAL,
			
			/// <summary>
			/// Поток операций по умолчанию.
			/// </summary>
			SEQUENCE_FLOW_DEFAULT,
			
			/// <summary>
			/// Ассоциация
			/// </summary>
			ASSOCIATION,
			
			/// <summary>
			/// Поток сообщений
			/// </summary>
			MESSAGE_FLOW,
			
			/// <summary>
			/// Шлюз
			/// </summary>
			GATEWAY
		};
	
	public class BaseItem : Gtk.DrawingArea 
	{
		string parentName = "";
		
		/// <summary>
        /// Идентификатор
        /// </summary>
        public Guid ID { get; set; }
		
		/// <summary>
        /// Линк
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Код элемента
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Признак выбора
        /// </summary>
        public virtual bool IsSelected { get; set; }
		
		protected enum ArrowStyle
		{
			ARROW_OPEN,
			ARROW_SOLID,
			ARROW_SOLID_FILLED,
			ARROW_DIAMOND,
			ARROW_DIAMOND_FILLED,
			ARROW_CIRCLE,
			ARROW_CIRCLE_FILLED
		};
		
		static Gdk.Cursor mCursor = new Gdk.Cursor (Gdk.CursorType.Fleur);
		
		
		private int x;
		private int y;
		public int X {
			get { return x;}
			set{ x = value;}
		}
		public int Y {
			get { return y;}
			set{ y = value;}
		}
		//string caption = "";
		protected Gtk.Menu popup = null;
		protected string body = "";
		private double width=0;
		private double height=0;
		public virtual double Width { get { return width; } set { width = value;}}
		public virtual double Height { get { return height; } set { height = value;}}
		public ElementType ELType=ElementType.NONE;
		public void mask ()
		{
			Width = Math.Abs (Width);
			Height = Math.Abs (Height);
			Gdk.Pixmap pm = new Gdk.Pixmap (this.GdkWindow, (int)Width, (int)Height, 1);
			/*using (ImageSurface draw = new ImageSurface (Format.Argb32,(int) Width,(int) Height)) {
				using (Context gr = new Context(draw)) {
					gr.Antialias = Antialias.None;
					Paint (gr);
				}*/
				using (Context crPix = Gdk.CairoHelper.Create(pm)) {
					crPix.Antialias = Antialias.None;
					crPix.Operator = Operator.Source;
					crPix.Source = new SolidPattern (new Color (0, 0, 0, 0));
					crPix.Rectangle (0, 0, (int)Width, (int)Height);
					crPix.Paint ();
                    crPix.SetSourceRGB(1.0, 1.0, 1.0);
					crPix.Operator = Operator.Over;
					crPix.NewPath ();
					Paint (crPix);
					crPix.ClosePath ();
					//Paint (crPix);
				}
				//save the image as a png image.
				//draw.WriteToPng (("mask_" + ELType.ToString () + ID.ToString () + ".png"));
			//}
			//var image = new Gtk.Image (pm, pm);
            /*
		    Bitmap bmp = null;
            //save bitmap to stream
            var stream = new System.IO.MemoryStream();
            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            //verry important: put stream on position 0
            stream.Position = 0;
            //get the pixmap mask
            //var buf = new Gdk.Pixbuf(stream, bmp.Width, bmp.Height);
            */
            

			//pm.Colormap = null;
			Gdk.Pixmap map1, map2;
			//var px = new Gdk.Pixbuf ("mask_" + ELType.ToString () + ID.ToString () + ".png");
			
           // px.RenderPixmapAndMask (out map1, out map2, 255);
			//File.Delete ("mask_" + ELType.ToString () + ID.ToString () + ".png");
				
			this.ParentWindow.InputShapeCombineMask (pm, 0, 0);
			this.ParentWindow.ShapeCombineMask (pm, 0, 0);
			//px.Dispose ();
			pm.Dispose ();
		//	map1.Dispose ();
		//	map2.Dispose ();
			/*	
			var pix = new Gdk.Pixmap(this.ParentWindow,Allocation.Width,Allocation.Height,1);
				using (Context crPix = Gdk.CairoHelper.Create(pix)){
					crPix.Operator= Operator.Source;
					crPix.Source = new SolidPattern(new Color(0,0,0,0));
					crPix.Rectangle(0,0,Allocation.Width,Allocation.Height);
					crPix.Paint();

					crPix.Operator = Operator.Over;
					crPix.NewPath();
					Paint(crPix);
					//crPix.Save();
                    ParentWindow.ShapeCombineMask(pix, 0, 0);
                    ParentWindow.InputShapeCombineMask(pix, 0, 0);
					((IDisposable)crPix.Target).Dispose();
				}
			*/	
		}
		public BaseItem (string pName, string cap, ElementType typeEl, double _width, double _height)
		{
			ID = Guid.NewGuid ();
			popup = new Gtk.Menu ();
			this.Events |= Gdk.EventMask.EnterNotifyMask | Gdk.EventMask.LeaveNotifyMask;

			//цвет фона draw area
			//this.ModifyBg(StateType.Normal, new Gdk.Color());

			Gtk.MenuItem text1 = new MenuItem ("Test1");
			text1.Activated += new EventHandler (Menu1Clicked);
			Gtk.MenuItem text2 = new MenuItem ("Test2");
			text2.Activated += new EventHandler (Menu2Clicked);
			ELType = typeEl;
			popup.Add (text1);			
			popup.Add (text2);
			
			parentName = pName;
			body = cap;
			this.Width = _width;
			this.Height = _height;
			this.Name = parentName + "MVObject";
			
			this.SetSizeRequest ((int)Width, (int)Height);
			//mask
			this.Realized += delegate {
				mask ();				
			};
		}

		protected override void OnScreenChanged (Gdk.Screen previous_screen)
		{
			base.OnScreenChanged (previous_screen);
			//this.Screen.DefaultColormap = this.Screen.RgbaColormap;
		}
		protected override bool OnEnterNotifyEvent (Gdk.EventCrossing evnt)
		{
			
			GdkWindow.Cursor = mCursor;
			return base.OnEnterNotifyEvent (evnt);
        }
		
		protected override bool OnLeaveNotifyEvent (Gdk.EventCrossing evnt)
		{
			GdkWindow.Cursor = null;
			return base.OnLeaveNotifyEvent (evnt);
            }

		public void ShowMenu()
		{
			if (popup!=null)
			{
				popup.Popup();
				popup.ShowAll();
			}
		}
		
		public void Edit()
		{
			body="Edit";
			this.QueueDraw();
		}
		
		public string Caption
		{
			get
			{
				return body;
			}
		}
		
		public void Redraw()
		{
			Console.WriteLine("Redrawing");
			this.QueueDraw();
		}
		
		private Pango.Layout GetLayout(string text)
		{
			Pango.Layout layout = new Pango.Layout(this.PangoContext);
			layout.FontDescription = Pango.FontDescription.FromString("monospace 8");
			layout.SetMarkup("<span color=\"black\">" + text + "</span>");
			return layout;
		}
		
		protected void Menu1Clicked(object sender, EventArgs args)
		{
			Console.WriteLine("Test");
			body = "Test1";
			this.QueueDraw();
		}
		
		protected void Menu2Clicked(object sender, EventArgs args)
		{
			Console.WriteLine("Test");
			body = "Test2";
			this.QueueDraw();
		}

		protected double min (params double[] arr)
	    {
			int minp = 0;
			for (int i = 1; i < arr.Length; i++)
			    if (arr[i] < arr[minp])
				minp = i;
		 
			return arr[minp];
	    }
		public void DrawRoundedRectangle (Cairo.Context gr, double x, double y, double width, double height, double radius)
	    {
			gr.Antialias=Antialias.Subpixel;
			gr.Save ();
		 
			if ((radius > height / 2) || (radius > width / 2))
			    radius = min (height / 2, width / 2);
		 
			gr.MoveTo (x, y + radius);
			gr.Arc (x + radius, y + radius, radius, Math.PI, -Math.PI / 2);
			gr.LineTo (x + width - radius, y);
			gr.Arc (x + width - radius, y + radius, radius, -Math.PI / 2, 0);
			gr.LineTo (x + width, y + height - radius);
			gr.Arc (x + width - radius, y + height - radius, radius, 0, Math.PI / 2);
			gr.LineTo (x + radius, y + height);
			gr.Arc (x + radius, y + height - radius, radius, Math.PI / 2, Math.PI);
			gr.ClosePath ();
			gr.Restore ();
	    }
		//перегружаемая функция отрисовки элемента
		virtual public void Paint (Context g)
		{
				
		}
		//перегружаемая функция отрисовки маски для элемента 
		virtual public void PaintMask (Context g)
		{

		}


		protected override bool OnExposeEvent (Gdk.EventExpose args)
		{	
					/*
						Gradient linpat = new LinearGradient(0, 0,width , height);
						linpat.AddColorStop(0, new Color(0, 0.3, 0.8));
						linpat.AddColorStop(1, new Color(1, 0.8, 0.3));



						Gradient radpat = new RadialGradient(width/2,height/2 , 0, width/2,height/2, height/2);
						radpat.AddColorStop(0, new Color(1, 1, 1, 1));
						radpat.AddColorStop(1, new Color(1, 1, 1, 0));
						gr.Source = linpat;
						gr.Mask(radpat);
					 */
					using (Context g = Gdk.CairoHelper.Create (args.Window)) {
						g.Antialias = Antialias.Subpixel;
						Paint(g);
					}
			return true;
		}
	}
}