// MVPanel.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Gtk;
using GtkControl.Control;

namespace GtkControl
{
	[System.ComponentModel.Category("GtkControl")]
	[System.ComponentModel.ToolboxItem(true)]
	public partial class MVPanel : Gtk.Bin
	{
		#region Переменные
		
		private Widget currCtrl = null;
		private Widget currClone = null;
		private List<Widget> selectedClones = new List<Widget>();
		private int origX = 0;
		private int origY = 0;
		public int pointX = 0;
		public int pointY = 0;
		private static bool have_drag;
		private SelectionService m_selectionService;


	
		#endregion
		
		#region Свойства

		internal SelectionService SelectionService {
			get { return m_selectionService ?? (m_selectionService = new SelectionService (this.fixed1)); }
		}
		
		#endregion

		/// <summary>
		/// 
		/// </summary>
		public MVPanel ()
		{
			this.Build ();
			fixed1.MotionNotifyEvent += OnFixed1MotionNotifyEvent;
			this.DragDataReceived += new DragDataReceivedHandler (HandleLabelDragDataReceived);
			this.DragDrop += new DragDropHandler (HandleTargetDragDrop);
			this.DragMotion += HandleTargetDragMotion;
			Gtk.Drag.DestSet (this, DestDefaults.All, 
			                  new[] { new TargetEntry("text/plain", TargetFlags.OtherWidget, 1)} ,
								 Gdk.DragAction.Move);
		}
		void HandleLabelDragDataReceived (object o, DragDataReceivedArgs args)
		{

			Console.WriteLine ("FixedDragDataReceived");
		}
		private static void HandleTargetDragMotion (object sender, DragMotionArgs args)
		{
			if (! have_drag) {
				have_drag = true;
				// FIXME?  Kinda wonky binding.
				//(sender as Gtk.Image).FromPixbuf = trashcan_open_pixbuf;
				//fixed1.Add(
			}
			
			Widget source_widget = Gtk.Drag.GetSourceWidget (args.Context);
			Console.WriteLine ("motion, source {0}", source_widget == null ? "null" : source_widget.ToString ());
			
			Gdk.Atom [] targets = args.Context.Targets;
			foreach (Gdk.Atom a in targets)
				Console.WriteLine (a.Name); 
			
			Gdk.Drag.Status (args.Context, args.Context.SuggestedAction, args.Time);
			args.RetVal = true;
		}
		private static void HandleTargetDragDrop (object sender, DragDropArgs args)
		{
			Console.WriteLine ("drop");
			have_drag = false;
			//(sender as Gtk.Image).FromPixbuf = trashcan_closed_pixbuf;
			
			#if BROKEN			// Context.Targets is not defined in the bindings
			if (Context.Targets.Length != 0) {
				Drag.GetData (sender, context, Context.Targets.Data as Gdk.Atom, args.Time);
				args.RetVal = true;
			}
			#endif
			
			args.RetVal = false;
		}
		/// <summary>
		/// Настройка контрола на перерерисовку
		/// </summary>
		public void RefreshChildren ()
		{
			this.fixed1.QueueDraw ();
		}
		
		/// <summary>
		/// Добавление передвигаемого элемента управления на панель
		/// </summary>
		/// <param name="name"></param>
		/// <param name="caption"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="typeEl"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void AddMovingObject (string name, string caption, int x, int y, BPMNElementType typeEl, int width, int height)
		{
			//Prevent the object to be displayed outside the panel
			if (x < 0) {
				x = 0;
			}
			
			if (y < 0) {
				y = 0;
			}
			
			//Создание контейнера где ползователький объект отобразится
			EventBox ev = GetMovingBox (name, caption, typeEl, width, height);

			//Add the events to control the movement of the box
			ev.ButtonPressEvent += new ButtonPressEventHandler (OnButtonPressed);
			ev.ButtonReleaseEvent += new ButtonReleaseEventHandler (OnButtonReleased);
			
			//Добавление контрола на панель
			this.fixed1.Put (ev, x, y);
			this.ShowAll ();
		}
		
		//Создание eventBox для пользовательского элемента упраления
		private EventBox GetMovingBox (string name, string caption, BPMNElementType typeEl, float width, float height)
		{ 
			BaseItem ctrl;
			switch (typeEl) {
			case BPMNElementType.START_NONE:
			{
				ctrl = new StartEvent (name, caption,Math.Min (height / 2,width/2));
				break;
			}
			case BPMNElementType.END_NONE:
				{
				ctrl = new EndEvent (name, caption, Math.Min (height / 2,width/2));
					break;
				}
			case BPMNElementType.INTERMEDIATE_NONE:
			{
				ctrl = new IntermediateEvent (name,caption, Math.Min (height / 2,width/2));
				break;
			}
			case BPMNElementType.TASK:
				{
					ctrl = new Task (name, caption, width, height);
					break;
				}
			case BPMNElementType.SEQUENCE_FLOW_UNCONDITIONAL:
				{
					ctrl = new SequenceFlow (
						name,
						caption,
						width,
						height,
						new Cairo.PointD (15, 200),
						new Cairo.PointD (300, 20)
					);
					break;
				}
			case BPMNElementType.SEQUENCE_FLOW_CONDITIONAL:
				{
				ctrl = new SequenceFlow (
						name,
						caption,
						width,
						height,
						new Cairo.PointD (15, 200),
						new Cairo.PointD (300, 20)
					);
				(ctrl as SequenceFlow).ConditionType = ConditionType.Expression;
					break;
				}
			case BPMNElementType.MESSAGE_FLOW:
				{
					ctrl = new MessageFlow (
						name,
						caption,
						width,
						height,
						new Cairo.PointD (20, 100),
						new Cairo.PointD (150, 20)
					);
					break;
				}
			case BPMNElementType.GATEWAY:
				{
					ctrl = new Gateway (name, caption, width, height);
					break;
				}
			case BPMNElementType.POOL:
				{
					ctrl = new Pool (name, caption, width, height, OrientationEnum.Horizontal);
					break;
				}
			default:
				{
				ctrl = null;//new BaseItem (name, caption, typeEl, width, height);

					break;
				}
			}
			if (ctrl != null) {
				EventBox rev = new EventBox ();
				rev.Name = name;
				rev.Add (ctrl);
				Console.WriteLine ("Creating new moving object" + rev.Name);
				return rev;
			}
			return null;
		}
		
		//Create a clone of the selected object that will be shown until the destination of the control is reached
		private Widget CloneCurrCtrl ()
		{
			Widget re = null;
			var mve = (currCtrl as EventBox);
			if (mve != null) {
				BaseItem mv;
				if ((mv = mve.Child as BaseItem) != null) 
					re = GetMovingBox (
							mve.Name + "Clone",
							mv.Caption,
							mv.ElementType,
							mv.Width,
							mv.Height
					);
			
	
			}
			if (re == null) {
				//This should not really happen but that would prevent an exception
				re = GetMovingBox ("Unknown", "Unknown", 0, 0, 0);
			}
			return re;
		}
		
		//Render the clone of the selected object at the intermediate position
		private void MoveClone (ref Widget wdg, object eventX, object eventY)
		{
			if (wdg == null) {
				wdg = CloneCurrCtrl ();
				if (selectedClones.Count<=0)
				foreach (var selectedItem in SelectionService.CurrentSelection) {

					Widget re = null;
					BaseItem mv = selectedItem as BaseItem;
					if ((mv) != null) 
						re = GetMovingBox (
								mv.Name + "Clone",
								mv.Caption,
								mv.ElementType,
								mv.Width,
								mv.Height
								);
					if (re == null) {
						//This should not really happen but that would prevent an exception
						re = GetMovingBox ("Unknown", "Unknown", 0, 0, 0);
					}
					
				selectedClones.Add(re);	
				this.fixed1.Add(re);	
				MoveControl (re, eventX, eventY, true);
			}
				

				this.ShowAll ();
			}
			MoveControl (wdg, eventX, eventY, true);
		}
		
		//Move a control to the specified event location
		private void MoveControl (Widget wdg, object eventX, object eventY, bool isClone)
		{
			int destX = origX + System.Convert.ToInt32 (eventX) + origX - pointX;
			int destY = origY + System.Convert.ToInt32 (eventY) + origY - pointY;
			if (destX < 0) {
				destX = 0;
			}
			if (destY < 0) {
				destY = 0;
			}			
			this.fixed1.Move (wdg, destX, destY);
			if (wdg is EventBox) 
			{
				var baseItem = (wdg as EventBox).Child as BaseItem;
				if (baseItem != null)
				{
					baseItem.Location = new PointF(destX,destY);
				}
			}

			if (!isClone) {
				//Перемещение ресайзеров вместе с элементами
				foreach (var selectedItem in SelectionService.CurrentSelection) {
					int index = 0;
					var baseItem = selectedItem  as BaseItem;
					for (var j = 0; j < 3; j++) {
						for (var i = 0; i < 3; i++) {
							if ((i == 1) && (j == 1)) {
								continue;
							}
							fixed1.Move (
								baseItem.Resizers [index++],
								(int)baseItem.X   + j * (int)baseItem.Width / 2 - 5,
								(int)baseItem.Y   + i * (int)baseItem.Height / 2 - 5
								
							);
						}
					}
				}
				Console.WriteLine ("MovingBox KeyReleased:" + destX.ToString () + "-" + destY.ToString ());
			}
			this.fixed1.QueueDraw ();	
		}
		
		/// <summary>
		/// Mouse click on the controls of the panel  
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="a"></param>
		protected void OnButtonPressed (object sender, ButtonPressEventArgs a)
		{

			if (sender is EventBox) {
				var baseItem = (sender as EventBox).Child as BaseItem;
				if (baseItem != null) {
					if (a.Event.Button == 3) {

						baseItem.ShowMenu ();
					} else if (a.Event.Button == 1)
					if (a.Event.Type == Gdk.EventType.TwoButtonPress) {
						baseItem.Edit ();
					} else {
						//Setup the origin of the move
						baseItem.IsDragged = true;
						currCtrl = sender as Widget;
						if (currCtrl != null) {
							currCtrl.TranslateCoordinates (this.fixed1, 0, 0, out origX, out origY);
							baseItem.X = origX;
							baseItem.Y = origY;

							if ((a.Event.State == Gdk.ModifierType.ControlMask))
							{
								if (!baseItem.IsSelected)
									SelectionService.AddToSelection(baseItem);
								else 
									SelectionService.RemoveFromSelection(baseItem);
							}else
							{
								if (!baseItem.IsSelected)
								{
									SelectionService.SelectItem (baseItem);
								}
							}
							fixed1.GetPointer (out pointX, out pointY);
							Console.WriteLine ("MovingBox KeyPressed on " + baseItem.Caption);
							Console.WriteLine ("Pointer:" + pointX.ToString () + "-" + pointY.ToString ());
							Console.WriteLine ("Origin:" + origX.ToString () + "-" + origY.ToString ());
						}
					}


					//расстановка ресазеров выделенных элементов
					foreach (var selected_item in SelectionService.CurrentSelection) {
						int index = 0;

						var baseItem1 = selected_item  as BaseItem;
						if (baseItem1 != null) {
							for (var j = 0; j < 3; j++) {
								for (var i = 0; i < 3; i++) {
									if ((i == 1) && (j == 1)) {
										continue;
									}
									fixed1.Move (
                                            baseItem1.Resizers [index++],
                                            (int)baseItem1.X + j * (int)baseItem1.Width / 2 - 5,
                                            (int)baseItem1.Y + i * (int)baseItem1.Height / 2 - 5
									);
								}
							}
						}
					}
					fixed1.ShowAll ();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="a"></param>
		protected void OnButtonReleased (object sender, ButtonReleaseEventArgs a)
		{
			//Final destination of the control
			if (a.Event.Button == 1) {
				foreach (var selectedItem in SelectionService.CurrentSelection)
				{
					MoveControl (currCtrl, a.Event.X, a.Event.Y, false);
				}
				MoveControl (currCtrl, a.Event.X, a.Event.Y, false);
				
				IDragged baseItem;
				if ((currCtrl is EventBox)&&(baseItem = ((currCtrl as EventBox).Child) as IDragged)!= null )
					baseItem.IsDragged = false;
				if (currClone != null) {
					foreach(var selectedItem in selectedClones)
						this.fixed1.Remove(selectedItem);
					selectedClones.Clear();
					this.fixed1.Remove (currClone);

					Console.WriteLine ("Deleting moving object" + currClone.Name);
					currClone.Destroy ();
					currClone = null;
				}

			}
		}


		/// <summary>
		/// Вызывается когда элементы перемещают
		/// </summary>
		/// <param name="o"></param>
		/// <param name="args"></param>
		protected virtual void OnFixed1MotionNotifyEvent (object o, Gtk.MotionNotifyEventArgs args)
		{
			this.fixed1.GdkWindow.Background = new Gdk.Color (0, 0, 0);
			this.scrolledwindow1.GdkWindow.Background = new Gdk.Color (128, 0, 0); 
			this.fixed1.QueueDraw ();

			var eventBox = currCtrl as EventBox;
			if (eventBox != null) {
				var obj = (eventBox.Child  as BaseItem);
				if (obj.IsDragged) {
					if (eventBox != null && eventBox.Child  is BaseItem)
					{
							MoveClone (ref currClone, args.Event.X, args.Event.Y);
					}
				} else {
					//Есть ли активные ресайзеры
					var IsDragged = false;
					//Resizer actRes = null;
					foreach (var item in obj.Resizers) {
						if (item.Child is IDragged) {
							IsDragged = (item.Child as IDragged).IsDragged; 
						}
						if (IsDragged){
							//actRes = item.Child as Resizer;
							break;
						}
					}
					// изменение размера элемента
					if (IsDragged) {
					
						int p_x, p_y;
						float dx, dy;
						fixed1.GetPointer (out p_x, out p_y);
						dx = /*(float)args.Event.X;*/p_x - obj.X;
						dy = /*(float)args.Event.Y;*/p_y - obj.Y;

						obj.Height = (dy > obj.MinHeight) ? ((obj.MaxHeight!=0 && dy>obj.MaxHeight)?obj.MaxHeight: dy) : obj.MinHeight;
						obj.Width = (dx > obj.MinWidth) ? ((obj.MaxWidth!=0 && dx>obj.MaxWidth)?obj.MaxWidth: dx) : obj.MinWidth;;

						currCtrl.SetSizeRequest ((int)obj.Width, (int)obj.Height);
						obj.SetSizeRequest ((int)obj.Width, (int)obj.Height);
						origX += (int)dx;
						origY += (int)dy;
					}
				}
			}
		}
	}
}

