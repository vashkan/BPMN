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
using Gtk;
using Cairo;

namespace GtkControl.Control
{
	/// <summary>
	/// Тип элемента
	/// </summary>
	public enum ElementType
		{
			/// <summary>
			/// Неопределеннный
			/// </summary>
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
			GATEWAY,
		
			/// <summary>
            /// Пул
            /// </summary>
	        POOL
		};
	
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
		/// Ресайзеры
		/// </summary>
		public List<EventBox> Resizers;
		
		static Gdk.Cursor mCursor = new Gdk.Cursor (Gdk.CursorType.Fleur);


	    /// <summary>
	    /// 
	    /// </summary>
	    public int X { get; set; }

	    /// <summary>
	    /// 
	    /// </summary>
	    public int Y { get; set; }

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
	    public virtual double Width { get; set; }

	    /// <summary>
	    /// Высота
	    /// </summary>
	    public virtual double Height { get; set; }

	    /// <summary>
		/// Тип элемента
		/// </summary>
		public ElementType ELType=ElementType.NONE;
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
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pName"></param>
		/// <param name="cap"></param>
		/// <param name="typeEl"></param>
		/// <param name="_width"></param>
		/// <param name="_height"></param>
		public BaseItem (string pName, string cap, ElementType typeEl, double _width, double _height)
		{
			ID = Guid.NewGuid ();
			Popup = new Gtk.Menu ();
			this.Events |= Gdk.EventMask.EnterNotifyMask | Gdk.EventMask.LeaveNotifyMask;

			Gtk.MenuItem text1 = new MenuItem ("Test1");
			text1.Activated += new EventHandler (Menu1Clicked);
			Gtk.MenuItem text2 = new MenuItem ("Test2");
			text2.Activated += new EventHandler (Menu2Clicked);
			ELType = typeEl;
			Popup.Add (text1);			
			Popup.Add (text2);
			
			parentName = pName;
			Body = cap;
			this.Width = _width;
			this.Height = _height;
			this.Name = parentName + "MVObject";
			
			Resizers = new List<EventBox> ();
			for (var i=0; i<8; i++) {
				var evn = new EventBox ();
				evn.Add (new Resizer ());
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
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
		protected double Min (params double[] arr)
	    {
			int minp = 0;
			for (int i = 1; i < arr.Length; i++)
			    if (arr[i] < arr[minp])
				minp = i;
		 
			return arr[minp];
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
			    radius = Min (height / 2, width / 2);
		 
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