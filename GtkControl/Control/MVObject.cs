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
using Gtk;
using Cairo;

namespace GtkControl.Control
{
	
	public class MVObject : Gtk.DrawingArea 
	{
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
		public enum ElementType
		{
			NONE,
			START_EVENT,
			END_EVENT,
			TASK,
			SEQUENCE_FLOW_UNCONDITIONAL,
			SEQUENCE_FLOW_CONDITIONAL,
			SEQUENCE_FLOW_DEFAULT,
			ASSOCIATION,
			MESSAGE_FLOW,
			GATEWAY
		};

		string parentName = "";
		public Guid ID;
		//string caption = "";
		protected Gtk.Menu popup = null;
		protected string body = "";
		public double width = 0;
		public double height = 0;
		public ElementType ELType=ElementType.NONE;
		public MVObject (string pName, string cap, ElementType typeEl, double _width, double _height)
		{
			ID = Guid.NewGuid ();
			popup = new Gtk.Menu ();

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
			this.width = _width;
			this.height = _height;
			this.Name = parentName + "MVObject";
			
			this.SetSizeRequest ((int)width, (int)height);
			//mask
			this.Realized += delegate {
				
				Gdk.Pixmap pm = new Gdk.Pixmap (this.GdkWindow, (int)width, (int)height, 1);
				using (ImageSurface draw = new ImageSurface (Format.Argb32,(int) width,(int) height)) {
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
						//Paint (crPix);
					}
					//save the image as a png image.
					draw.WriteToPng (("mask_" + ELType.ToString () + ID.ToString () + ".png"));
				}
				//var image = new Gtk.Image (pm, pm);
				
				//pm.Colormap = null;
				Gdk.Pixmap map1, map2;
				var px = new Gdk.Pixbuf ("mask_" + ELType.ToString () + ID.ToString () + ".png");
				px.RenderPixmapAndMask (out map1, out map2, 255);

				this.ParentWindow.InputShapeCombineMask (map2, 0, 0);
				this.ParentWindow.ShapeCombineMask (map2, 0, 0);
				px.Dispose ();
				pm.Dispose ();
				map1.Dispose ();
				map2.Dispose ();
				 
				/*var pix = new Gdk.Pixmap(this.ParentWindow,Allocation.Width,Allocation.Height,1);
				using (Context crPix = Gdk.CairoHelper.Create(pix)){
					crPix.Operator= Operator.Source;
					crPix.Source = new SolidPattern(new Color(0,0,0,0));
					crPix.Rectangle(0,0,Allocation.Width,Allocation.Height);
					crPix.Paint();

					crPix.Operator = Operator.Over;
					crPix.NewPath();
					Paint(crPix);
					//crPix.Save();
					GdkWindow.ShapeCombineMask(pix,0,0);
					GdkWindow.InputShapeCombineMask(pix,0,0);
					((IDisposable)crPix.Target).Dispose();
				}
				*/
			
			};
		}

		protected override void OnScreenChanged (Gdk.Screen previous_screen)
		{
			base.OnScreenChanged (previous_screen);
			this.Screen.DefaultColormap = this.Screen.RgbaColormap;
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