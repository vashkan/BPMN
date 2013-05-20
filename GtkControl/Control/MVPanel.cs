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
		private int origX = 0;
		private int origY = 0;
		private int pointX = 0;
		private int pointY = 0;
		private bool isDragged = false;
		private SelectionService m_selectionService; 
		private List <ISelectable> selectedItems;
		
		#endregion
		
		#region Свойства
		
		internal List<ISelectable> CurrentSelection {
			get { 
				return selectedItems ?? (selectedItems = new List<ISelectable> ()); 
			}
        } 
		internal SelectionService SelectionService
        {
            get { return m_selectionService ?? (m_selectionService = new SelectionService(this.fixed1)); }
        }
		
		#endregion
		public MVPanel ()
		{
			this.Build ();
		}
		
		//Set the controls to be redrawn
		public void RefreshChildren ()
		{
			this.fixed1.QueueDraw ();
		}
		
		//Add a movable control to the panel
		public void AddMovingObject (string name, string caption, int x, int y, ElementType typeEl, int width, int height)
		{
			//Prevent the object to be displayed outside the panel
			if (x < 0) {
				x = 0;
			}
			
			if (y < 0) {
				y = 0;
			}
			
			//Create the box where the custom object is rendered
			EventBox ev = GetMovingBox (name, caption, typeEl, width, height);

			//Add the events to control the movement of the box
			ev.ButtonPressEvent += new ButtonPressEventHandler (OnButtonPressed);
			ev.ButtonReleaseEvent += new ButtonReleaseEventHandler (OnButtonReleased);
			
			//Add the control to the panel
			this.fixed1.Put (ev, x, y);
			this.ShowAll ();
		}
		
		//Create the event box for the custom control
		private EventBox GetMovingBox (string name, string caption, ElementType typeEl, double width, double height)
		{ 
			BaseItem ctrl;
			switch (typeEl) {
			case ElementType.START_EVENT:
			case ElementType.END_EVENT:
				{
					ctrl = new Event (name, caption, typeEl, height / 2);
					break;
				}
			case ElementType.TASK:
				{
					ctrl = new Task (name, caption, width, height);
					break;
				}
			case ElementType.SEQUENCE_FLOW_UNCONDITIONAL:
				{
					ctrl = new UnCondSeqFlow (
						name,
						caption,
						typeEl,
						width,
						height,
						new Cairo.PointD (15, 200),
						new Cairo.PointD (300, 20)
					);
					break;
				}
			case ElementType.SEQUENCE_FLOW_CONDITIONAL:
				{
					ctrl = new CondSeqFlow (
						name,
						caption,
						typeEl,
						width,
						height,
						new Cairo.PointD (15, 200),
						new Cairo.PointD (300, 20)
					);
					break;
				}
			case ElementType.MESSAGE_FLOW:
				{
					ctrl = new MessageFlow (
						name,
						caption,
						typeEl,
						width,
						height,
						new Cairo.PointD (20, 100),
						new Cairo.PointD (150, 20)
					);
					break;
				}
			case ElementType.GATEWAY:
				{
					ctrl = new Gateway (name, caption, width, height);
					break;
				}
			case ElementType.POOL:
				{
					ctrl = new Pool (name, caption, width, height, OrientationEnum.Horizontal);
					break;
				}
			default:
				{
					ctrl = new BaseItem (name, caption, typeEl, width, height);
					break;
				}
			}
			EventBox rev = new EventBox ();
			rev.Name = name;
			rev.Add (ctrl);
			Console.WriteLine ("Creating new moving object" + rev.Name);
			return rev;
		}
		
		//Create a clone of the selected object that will be shown until the destination of the control is reached
		private Widget CloneCurrCtrl ()
		{
			Widget re = null;
			
			if (this.currCtrl != null) {
				if (currCtrl is EventBox) {
					Widget mv = (currCtrl as EventBox).Child;
							re = GetMovingBox (
							(currCtrl as EventBox).Name+"Clone",
							(mv as  BaseItem).Caption,
							(mv as BaseItem).ELType,
							(mv as BaseItem).Width,
							(mv as BaseItem).Height
						);
				}
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
				this.fixed1.Add (wdg);		
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
			if (!isClone) {
				Console.WriteLine ("MovingBox KeyReleased:" + destX.ToString () + "-" + destY.ToString ());
			}
			this.fixed1.QueueDraw ();	
		}
		
		EventBox butt;
		bool resizing;
		//Mouse click on the controls of the panel  
		protected void OnButtonPressed (object sender, ButtonPressEventArgs a)
		{		
			
			if (a.Event.Button == 3) {
				if (sender is EventBox) {
					Widget fr = (sender as EventBox).Child;
					(fr as BaseItem).ShowMenu (); 
				}	
			} else if (a.Event.Button == 1) {
				
				if (a.Event.Type == Gdk.EventType.TwoButtonPress) {
					if (sender is EventBox) {
						//Calling the edit method of the control
						((sender as EventBox).Child as BaseItem).Edit ();
					}	
				} else {
					//Setup the origin of the move
					isDragged = true;
					currCtrl = sender as Widget;
					currCtrl.TranslateCoordinates (this.fixed1, 0, 0, out origX, out origY);
					((currCtrl as EventBox).Child  as BaseItem).X = (int)origX;
					((currCtrl as EventBox).Child  as BaseItem).Y = (int)origY;
					CurrentSelection.Clear ();
					CurrentSelection.Add ((currCtrl as EventBox).Child  as BaseItem);
					fixed1.GetPointer (out pointX, out pointY);
					Console.WriteLine ("MovingBox KeyPressed on " + ((currCtrl as EventBox).Child as BaseItem).Caption);
					Console.WriteLine ("Pointer:" + pointX.ToString () + "-" + pointY.ToString ());
					Console.WriteLine ("Origin:" + origX.ToString () + "-" + origY.ToString ());
					
					if (butt == null) {
						//var res = new Resizer ();
						foreach (var selected_item in CurrentSelection) {
							int index = 0;
							for (var j = 0; j<3; j++) {
								for (var i = 0; i<3; i++) {
									if ((i == 1) && (j == 1)) {
										continue;
									}
									(selected_item as BaseItem).Resizers [index].Events = (Gdk.EventMask)1020;//252;
									(selected_item as BaseItem).Resizers [index].ButtonPressEvent += delegate (object o, ButtonPressEventArgs args) {
										resizing = true;
										isDragged = true;
										(o as EventBox).TranslateCoordinates (this.fixed1, 0, 0, out origX, out origY);
										fixed1.GetPointer (out pointX, out pointY);
									};
									(selected_item as BaseItem).Resizers[index].ButtonReleaseEvent += delegate(object o, ButtonReleaseEventArgs args) {
										resizing = false;
										isDragged = false;
										fixed1.Move (butt, origX, origY);
									};
								
									//evn.Add (selectedItems [0].Resizers [index++]);
									fixed1.Add ((selected_item as BaseItem).Resizers [index]);
									fixed1.Move (
										(selected_item as BaseItem).Resizers [index++],
										origX + j * currCtrl.Allocation.Width / 2-5,//evn.WidthRequest/2,
										origY + i * currCtrl.Allocation.Height / 2 -5//evn.HeightRequest/2
									);
								}
							}
						}
						//fixed1.Add (butt);
						//fixed1.Move (butt, origX + (int)(((currCtrl as EventBox).Child) as BaseItem).Width,
						//    (int)(((currCtrl as EventBox).Child)  as BaseItem).Height + origY);
						fixed1.ShowAll ();
					}
				}
			}
		}
	
		protected void OnButtonReleased (object sender, ButtonReleaseEventArgs a)
		{
			//Final destination of the control
			if (a.Event.Button == 1) {
				MoveControl (currCtrl, a.Event.X, a.Event.Y, false);
				isDragged = false;
				//currCtrl = null;
				if (currClone != null) {
					this.fixed1.Remove (currClone);
					Console.WriteLine ("Deleting moving object" + currClone.Name);
					currClone.Destroy ();
					currClone = null;
				}
				var dx = (origX + System.Convert.ToInt32 (a.Event.X) - pointX);
				var dy = (origY + System.Convert.ToInt32 (a.Event.Y) - pointY);
				if ((dx != 0) || (dy != 0)) {
					for (var i=0; i<8; i++) {
						foreach (var selected_item in selectedItems) {
							fixed1.Remove ((selected_item as BaseItem).Resizers [i]);
						}						
					}
					
					butt = null;
				}
			}
		}

		//Called whenever a control is moved
		protected virtual void OnFixed1MotionNotifyEvent (object o, Gtk.MotionNotifyEventArgs args)
		{
			this.fixed1.GdkWindow.Background = new Gdk.Color (0, 0, 0); //ModifyBg(StateType.Normal,new Gdk.Color(0,0,0));
			this.scrolledwindow1.GdkWindow.Background = new Gdk.Color (128, 0, 0); //ModifyBg(StateType.Normal,new Gdk.Color(0,0,0));
			this.fixed1.QueueDraw ();
			if (isDragged) {
				//Render of a clone at the desired location
				if (/*(resizer != null) &&*/ resizing && (currCtrl != null)) {
					//MoveControl (butt, args.Event.XRoot, args.Event.YRoot, true);
					var obj = ((/*(*/currCtrl as EventBox).Child  as BaseItem);
					int p_x, p_y, dx, dy;
					fixed1.GetPointer (out p_x, out p_y);
					dx = p_x - pointX;
					dy = p_y - pointY;
					var temp = obj.Height + dy;
					temp = (temp > 10) ? temp : 10;
					obj.Height = temp;
					temp = obj.Width + dx;
					temp = (temp > 10) ? temp : 10;
					obj.Width = temp;
					pointX = p_x;
					pointY = p_y;
					currCtrl.SetSizeRequest ((int)obj.Width, (int)obj.Height);
					obj.SetSizeRequest ((int)obj.Width, (int)obj.Height);
					Console.WriteLine ("Resizing: \n width: " + obj.Width.ToString () + "\n height: " + obj.Height.ToString ());
					Console.WriteLine ("dx: " + dx.ToString () + "dy: " + dy.ToString ()
						+ "\nPointer1(" + pointX.ToString () + ", " + pointY.ToString () + ")\n"
						+ "pointer current (" + p_x + ", " + p_y + ")"
					);
					Console.WriteLine ("x_root: " + args.Event.XRoot.ToString () + " y_root: " + args.Event.YRoot.ToString ());
					
					obj.X = Math.Min ((int)obj.X, (int)args.Event.X);
					obj.Y = Math.Min ((int)obj.Y, (int)args.Event.Y);
					//MoveControl (currCtrl,obj.X , obj.Y, true);
					origX += dx;
					origY += dy;
					Console.WriteLine ("Origin: (" + origX.ToString () + ", " + origY.ToString ()+")");
					//fixed1.Move(butt, origX,origY);
				}
				else
				if (currCtrl != null) {
					if ((currCtrl as EventBox).Child  is BaseItem)
						MoveClone (ref currClone, args.Event.X, args.Event.Y);
				}
			}
		}
	}	
}
