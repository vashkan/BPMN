// MainWindow.cs
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
using GtkControl.Control;
public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		ItemPanelInit ();

		string name = "MovingBox";
		int index = 0;
		this.hpaned1.Position = 700;
		this.mvpanel1.AddMovingObject (
			name + (index++).ToString (),
			"Процесс 1",
			30,
			300,
			BPMNElementType.POOL,
			600,
			200
		);
		this.mvpanel1.AddMovingObject (
			name + (index++).ToString (),
			"оаро",
			92,
			222,
			BPMNElementType.START_NONE,
			40,
			40
		);
		this.mvpanel1.AddMovingObject (
			name + (index++).ToString (),
			"Indeterm",
			384,
			289,
			BPMNElementType.INTERMEDIATE_NONE,
			40,
			40
			);

		this.mvpanel1.AddMovingObject (name + (index++).ToString (), "Task", 10, 10, BPMNElementType.TASK, 140, 70);
		this.mvpanel1.AddMovingObject (name + (index++).ToString (), "end", 300, 222, BPMNElementType.END_NONE, 40, 40);
		this.mvpanel1.AddMovingObject (
			name + (index++).ToString (),
			"flow",
			246,
			115,
			BPMNElementType.SEQUENCE_FLOW_UNCONDITIONAL,
			303,
			250
		);
		this.mvpanel1.AddMovingObject (
			name + (index++).ToString (),
			"flowArrowOpen",
			276,
			169,
			BPMNElementType.SEQUENCE_FLOW_CONDITIONAL,
			303,
			225
		);
		this.mvpanel1.AddMovingObject (
			name + (index++).ToString (),
			"gateway",
			187,
			212,
			BPMNElementType.GATEWAY,
			50,
			50
		);
		this.mvpanel1.AddMovingObject (
			name + (index++).ToString (),
			"message_flow",
			384,
			289,
			BPMNElementType.MESSAGE_FLOW,
			303,
			225
		);

		/*//Прозрачность окна  
		this.Screen.DefaultColormap = this.Screen.RgbaColormap;
		this.Colormap = this.Screen.RgbaColormap;
		this.Opacity = 0.5;
		*/
		//this.mvpanel1.AddMovingObject(name+(index++).ToString(),"Gtk#",10,145);
		//this.mvpanel1.AddMovingObject(name+(index++).ToString(),"MonoDevelop",10,190);
		//this.mvpanel1.AddMovingObject(name+(index++).ToString(),"Pango",10,235);
		//this.mvpanel1.AddMovingObject(name+(index++).ToString(),"Test",10,280);
	}

	private static void HandleSourceDragDataGet (object sender, DragDataGetArgs args)

	{
		if (args.Info == (uint) TargetType.RootWindow)
			Console.WriteLine ("I was dropped on the rootwin");
		else
			args.SelectionData.Text = "I'm data!";
	}
	enum TargetType {
		String,
		RootWindow
	};
/*	
	private static TargetEntry [] target_table = new TargetEntry [] {
		new TargetEntry ("STRING", 0, (uint) TargetType.String ),
		new TargetEntry ("text/plain", 0, (uint) TargetType.String),
		new TargetEntry ("application/x-rootwindow-drop", 0, (uint) TargetType.RootWindow)
	};*/
	//Инизиализация панели инструментов
	private void ItemPanelInit()
	{
		#region Роли

		Expander Roles = new Expander (null)
		{
			CanFocus = true,
			LabelWidget = new Label ("Роли"),
			BorderWidth = (int)1,
			Expanded = true,

		};

		EventBox pool = new EventBox()
		{
			TooltipMarkup = "<b>Пул</b>\nПул представляет \nучастника процесса.",
			WidthRequest = 30,
			CanFocus = true,
			Name = "pool"
		};

		Gtk.Drag.SourceSet (pool, Gdk.ModifierType.Button1Mask ,
		                    new[] { new TargetEntry("text/plain", TargetFlags.Widget, 1)} ,
							Gdk.DragAction.Copy | Gdk.DragAction.Move);
		//var temp  = new Pool ("", "", 40, 30,OrientationEnum.Horizontal);
		Gdk.Pixbuf icon = new Gdk.Pixbuf("Pool_24.jpg");

		Gdk.Pixmap pool_icon, pool_mask;
		icon.RenderPixmapAndMask(out pool_icon,out pool_mask,255);
		Gtk.Drag.SourceSetIcon(pool,this.Colormap,pool_icon, pool_mask);

		pool.DragDataGet += new DragDataGetHandler (HandleSourceDragDataGet);
		pool.DragDataDelete += HandleSourceDragDataDelete;
		pool.DragBegin += HandleDragBegin;
		pool.DragMotion += HandleDragMotion;
		pool.DragDrop += HandleDragDrop;
		//button.DragDataGet += new DragDataGetHandler (HandleSourceDragDataGet);


		using (Alignment a = new Alignment (0.5f, 0.5f, 0f, 0f))
		{
			a.Add (new EventBox{new Pool ("", "", 40, 30,OrientationEnum.Horizontal)});
			pool.Add(a);
		}
		VBox vb1 = new VBox {pool};
		Roles.Add (vb1);
		ItemPanel.Add (Roles);
		Gtk.Box.BoxChild w1= ((Gtk.Box.BoxChild)(ItemPanel [Roles]));
			w1.Position = 4;
			w1.Expand = false;
			w1.Fill = false;

		#endregion

		#region Задача

		Expander Tasks = new Expander (null)
		{
			CanFocus = true,
			LabelWidget = new Label ("Задача"),
			BorderWidth = (int)1,
			Expanded = true
		};

		Button task = new Button()
		{
			TooltipMarkup = "<b>Задача</b>\nЗадача представляет собой  \nэлементарное дествие \n в рамках процесса.",
			WidthRequest = 30,
			CanFocus = true,
			Name = "task"
		};
		using ( Alignment a = new Alignment (0.5f, 0.5f, 0f, 0f))
		{
			a.Add (new EventBox{new Task("","",40,30)});
			task.Add (a);
		}
		VBox vb2 = new VBox {task};
		Tasks.Add (vb2);
		ItemPanel.Add (Tasks);
		Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(ItemPanel [Tasks]));

			w2.Position = 4;
			w2.Expand = false;
			w2.Fill = false;

		#endregion
		
		
		#region События

		Expander Events = new Expander (null)
		{
			CanFocus = true,
			LabelWidget = new Label ("События"),
			BorderWidth = (int)1,
			Expanded = true
		};

		//старт
		Button startEvent = new Button()
		{
			TooltipMarkup = "<b>Старт процесса </b>\nПоказывает с чего начинается \nконкретный процесс." 
							+ "Старт \n не может иметь входящего\n потока управления.",
			WidthRequest = 30,
			CanFocus = true,
			Name = "startEvent"
		};
		using (Alignment a = new Alignment (0.5f, 0.5f, 0f, 0f))
		{
			a.Add (new EventBox{new StartEvent("","",15)});
			startEvent.Add (a);
		}
		//завершение
		Button endEvent = new Button()
		{
			TooltipMarkup = "<b>Завершение процесса </b>\nОбозначает завершение \nпотока управления  в рамках\n процесса." 
			+ "При этом другие \n потоки могут продолжать\n исполнение. Не может\n соединятся с исходящим \n"
			+"потоком управления.",
			WidthRequest = 30,
			CanFocus = true,
			Name = "endEvent"
		};
		using (Alignment a = new Alignment (0.5f, 0.5f, 0f, 0f))
		{
			a.Add (new EventBox{new EndEvent("","",15)});
			endEvent.Add (a);
		}
		VBox vb3 = new VBox {startEvent,endEvent};
		Events.Add (vb3);
		ItemPanel.Add (Events);
		Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(ItemPanel [Events]));

			w3.Position = 4;
			w3.Expand = false;
			w3.Fill = false;

		#endregion

		#region Шлюзы

		Expander Gateways = new Expander (null)
		{
			CanFocus = true,
			LabelWidget = new Label ("Шлюзы"),
			BorderWidth = (int)1,
			Expanded = true
		};

		Button gatway = new Button()
		{
			TooltipMarkup = "<b>Шлюз</b>\nШлюз изображает собой \nточку принятия решений.",
			WidthRequest = 30,
			CanFocus = true,
			Name = "gateway"
		};

		using (Alignment a = new Alignment (0.5f, 0.5f, 0f, 0f))
		{
			a.Add (new EventBox{new Gateway ("", "", 30, 30)});
			gatway.Add (a);
		}
		VBox vb4 = new VBox {gatway};
		Gateways.Add (vb4);
		ItemPanel.Add (Gateways);
		Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(ItemPanel [Gateways]));

			w4.Position = 4;
			w4.Expand = false;
			w4.Fill = false;

		#endregion 
				
		ItemPanel.ShowAll ();
	}

	void HandleSourceDragDataDelete (object o, DragDataDeleteArgs args)
	{
		Console.WriteLine ("Delete the data!");
	}

	void HandleDragDrop (object o, DragDropArgs args)
	{
		Console.WriteLine("DragDrop");
	}

	void HandleDragMotion (object o, DragMotionArgs args)
	{
		Gtk.Drag.SetIconWidget(args.Context ,new Pool ("", "", 40, 30,OrientationEnum.Horizontal),0,0);
	}
	/// <summary>
	/// Подготовка аргументов для переноса элемента на панель 
	/// </summary>
	/// <param name="o">источник</param>
	/// <param name="args">аргументы</param>
	[GLib.ConnectBefore]
	void HandleDragBegin (object o, DragBeginArgs args)
	{
		Console.WriteLine("DragBegin");
	}

	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected virtual void OnMvpanel1KeyPressEvent (object o, Gtk.KeyPressEventArgs args)
	{
	}

	protected virtual void OnMvpanel1KeyReleaseEvent (object o, Gtk.KeyReleaseEventArgs args)
	{
	}

	protected virtual void OnMvpanel1ButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
	{
	}

	protected virtual void OnMvpanel1ButtonReleaseEvent (object o, Gtk.ButtonReleaseEventArgs args)
	{
	}
}