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
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using Gtk;
using Cairo;

namespace GtkControl.Control
{
		/// <summary>
	/// 
	/// </summary>
	public class BaseItem : Gtk.DrawingArea,ISelectable, IDragged
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

		// <summary>
		/// Признак перемещения
		/// </summary>
		public virtual bool IsDragged { get; set; }
		
		/// <summary>
		/// Минимальная ширина
		/// </summary>
		public int MinWidth { get; set; }
		
        /// <summary>
        /// Минимальная высота
        /// </summary>
        public int MinHeight { get; set; }

		/// <summary>
		/// Максимальная ширина
		/// </summary>
		public int MaxWidth { get; set; }
		
		/// <summary>
		/// Максимальная высота
		/// </summary>
		public int MaxHeight { get; set; }

		/// <summary>
		/// Ресайзеры
		/// </summary>
		public List<EventBox> Resizers;
		
		static Gdk.Cursor mCursor = new Gdk.Cursor (Gdk.CursorType.Fleur);


	    /// <summary>
	    /// 
	    /// </summary>
	    public float X { get; set; }

	    /// <summary>
	    /// 
	    /// </summary>
	    public float Y { get; set; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>The location.</value>
		public PointF Location 
		{
			get{
				return new PointF(this.X,this.Y);
			}
			set{
				this.X=value.X;
				this.Y=value.Y;
			}
		}
		public PointF Center
		{
			get
			{
				return new PointF(this.X+this.Width/2,this.Y+this.Height/2);
			}
		}

		public float Bottom
		{
			get
			{
				return this.Y + this.Height;
			}
		}
		public float Right
		{
			get{
				return this.X + this.Width;
			}

		}

	    /// <summary>
        /// 
        /// </summary>
		protected Gtk.Menu Popup = null;
        /// <summary>
        /// 
        /// </summary>
		protected string Body = "";

	    /// <summary>
	    /// Ширина
	    /// </summary>
	    public virtual float Width { get; set; }

	    /// <summary>
	    /// Высота
	    /// </summary>
	    public virtual float Height { get; set; }

	    /// <summary>
		/// Тип элемента
		/// </summary>
		public BPMNElementType ElementType=BPMNElementType.NONE;
		/// <summary>
		/// Наложение маска
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void Mask (int width, int height)
		{
			Width = Math.Abs (width);
			Height = Math.Abs (height);
			var pm = new Gdk.Pixmap (this.GdkWindow, (int)Width, (int)Height, 1);

				using (Context crPix = Gdk.CairoHelper.Create(pm)) {
					crPix.Antialias = Antialias.None;
					crPix.Operator = Operator.Source;
					crPix.Source = new SolidPattern (new Cairo.Color (0, 0, 0, 0));
					crPix.Rectangle (0, 0, Allocation.Width, Allocation.Height);
					crPix.Paint ();

					crPix.Operator = Operator.Over;
					crPix.NewPath ();
					PaintMask (crPix);
			}
			//this.ParentWindow.InputShapeCombineMask (pm, 0, 0);
			this.ParentWindow.ShapeCombineMask (pm, 0, 0);
			pm.Dispose ();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pName"></param>
		/// <param name="cap"></param>
		/// <param name="typeEl"></param>
		/// <param name="_width"></param>
		/// <param name="_height"></param>
		public BaseItem (string pName, string cap, BPMNElementType typeEl, float _width, float _height)
		{
			ID = Guid.NewGuid ();
			Popup = new Gtk.Menu ();

			this.Events |= Gdk.EventMask.EnterNotifyMask | Gdk.EventMask.LeaveNotifyMask;

			Gtk.MenuItem text1 = new MenuItem ("Test1");
			text1.Activated += new EventHandler (Menu1Clicked);
			Gtk.MenuItem text2 = new MenuItem ("Test2");
			text2.Activated += new EventHandler (Menu2Clicked);
			ElementType = typeEl;
			Popup.Add (text1);			
			Popup.Add (text2);
			
			parentName = pName;
			Body = cap;
			this.Width = _width;
			this.Height = _height;
			this.Name = parentName + "MVObject";
			SetMaxMin (this);
			Resizers = new List<EventBox> ();
			for (var i=0; i<8; i++) {
				var evn = new EventBox ();
				Resizer resizer = new Resizer();
				resizer.baseItem = this;
				evn.Add (resizer);
				evn.Events = (Gdk.EventMask)1020; 

				evn.ButtonReleaseEvent +=
				delegate(object o, ButtonReleaseEventArgs args) {
					var eventBox = (o as EventBox);
					if ((eventBox != null) && (eventBox.Child is IDragged)) {
						(eventBox.Child as IDragged).IsDragged = false;
						var fixed1 = (this.Parent.Parent as Fixed);
						if (fixed1 !=null)
						{
							var index = 0;
							for (var l = 0; l < 3; l++) {
								for (var k = 0; k < 3; k++) {
									if ((k == 1) && (l == 1)) {
										continue;
									}
									fixed1.Move (
										this.Resizers [index++],
										(int)this.X + l * (int)this.Width / 2 - 5,
										(int)this.Y + k * (int)this.Height / 2 - 5
										);
								}
							}
						}
					}
				};
				Resizers.Add (evn);

			}
			
			this.SetSizeRequest ((int)Width, (int)Height);
			//mask
			this.Realized += delegate {
				Mask ((int)Width,(int)Height);				
			};
		}

		/// <summary>
        /// 
        /// </summary>
        /// <param name="allocation"></param>
        protected override void OnSizeAllocated(Gdk.Rectangle allocation)
        {
            base.OnSizeAllocated(allocation);
            Mask(allocation.Width, allocation.Height);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous_screen"></param>
		protected override void OnScreenChanged (Gdk.Screen previous_screen)
		{
			base.OnScreenChanged (previous_screen);
			//this.Screen.DefaultColormap = this.Screen.RgbaColormap;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="evnt"></param>
        /// <returns></returns>
		protected override bool OnEnterNotifyEvent (Gdk.EventCrossing evnt)
		{
			
			GdkWindow.Cursor = mCursor;
			return base.OnEnterNotifyEvent (evnt);
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="evnt"></param>
		/// <returns></returns>
		protected override bool OnLeaveNotifyEvent (Gdk.EventCrossing evnt)
		{
			GdkWindow.Cursor = null;
			return base.OnLeaveNotifyEvent (evnt);
            }

		/// <summary>
		/// 
		/// </summary>
		public void ShowMenu()
		{
			if (Popup!=null)
			{
				Popup.Popup();
				Popup.ShowAll();
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public void Edit()
		{
			Body="Edit";
			this.QueueDraw();
		}
		
		/// <summary>
		/// Загаловок
		/// </summary>
		public string Caption
		{
			get
			{
				return Body;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
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
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		protected void Menu1Clicked(object sender, EventArgs args)
		{
			Console.WriteLine("Test");
			Body = "Test1";
			this.QueueDraw();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		protected void Menu2Clicked(object sender, EventArgs args)
		{
			Console.WriteLine("Test");
			Body = "Test2";
			this.QueueDraw();
		}
       	/// <summary>
		/// Установка минимально и максимального размера элемента
		/// </summary>
		/// <param name="item"></param>
		private void SetMaxMin(BaseItem item)
		{
			switch (item.ElementType)
			{
			case BPMNElementType.TASK:
				item.MinWidth = 60;
				item.MinHeight = 40;
				item.MaxWidth = 300;
				item.MaxHeight = 180;
				break;
			case BPMNElementType.GATEWAY:
				item.MinWidth = 40;
				item.MinHeight = 40;
				item.MaxWidth = 60;
				item.MaxHeight = 60;
				break;
			case BPMNElementType.START_EVENT:
			case BPMNElementType.END_EVENT:
				item.MinWidth = 35;
				item.MinHeight = 35;
				item.MaxWidth = 60;
				item.MaxHeight = 60;
				break;
			case BPMNElementType.POOL:
				item.MinWidth = 90;
				item.MinHeight = 60;
				break;
			case BPMNElementType.NONE:
				item.MinWidth = 20;
				item.MinHeight = 20;
				break;
			}
		}

		/// <summary>
		/// Скругленный прямоугольник
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="radius"></param>
		public void DrawRoundedRectangle (Cairo.Context gr, double x, double y, double width, double height, double radius)
	    {
			gr.Antialias=Antialias.Subpixel;
			gr.Save ();
		 
			if ((radius > height / 2) || (radius > width / 2))
			    radius = Math.Min(height / 2, width / 2);
		 
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
		/// <summary>
        /// перегружаемая функция отрисовки элемента
		/// </summary>
		/// <param name="g"></param>
		virtual public void Paint (Context g)
		{
				
		}
		/// <summary>
        /// перегружаемая функция отрисовки маски для элемента
		/// </summary>
		/// <param name="g"></param>
		virtual public void PaintMask (Context g)
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
		protected override bool OnExposeEvent (Gdk.EventExpose args)
		{	
					using (Context g = Gdk.CairoHelper.Create (args.Window)) {
						g.Antialias = Antialias.Subpixel;
						Paint(g);
					}
			return true;
		}
	}
}