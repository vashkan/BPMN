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
using Gtk;
using GtkControl.Control;

namespace GtkControl
{
	[System.ComponentModel.Category("GtkControl")]
	[System.ComponentModel.ToolboxItem(true)]
	public partial class MVPanel : Gtk.Bin
	{
		private Widget currCtrl = null;
		private Widget currClone = null;
		private int origX = 0;
		private int origY = 0;
		private int pointX = 0;
		private int pointY = 0;
		private bool isDragged = false;
		private Resizer resizer = null;
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
		public void AddMovingObject (string name, string caption, int x, int y, MVObject.ElementType typeEl, int width, int height)
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
		private EventBox GetMovingBox (string name, string caption, MVObject.ElementType typeEl, double width, double height)
		{ 
			MVObject ctrl;
			switch (typeEl) {
			case MVObject.ElementType.START_EVENT:
			case MVObject.ElementType.END_EVENT:
				{
					ctrl = new Event (name, caption, typeEl, height / 2);
					break;
				}
			case MVObject.ElementType.TASK:
				{
					ctrl = new Task (name, caption, width, height);
					break;
				}
			case MVObject.ElementType.SEQUENCE_FLOW_UNCONDITIONAL:
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
			case MVObject.ElementType.SEQUENCE_FLOW_CONDITIONAL:
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
			case MVObject.ElementType.MESSAGE_FLOW:
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
			case MVObject.ElementType.GATEWAY:
				{
					ctrl = new Gateway (name, caption, width, height);
					break;
				}
			default:
				{
					ctrl = new MVObject (name, caption, typeEl, width, height);
					break;
				}
			}
			EventBox rev = new EventBox ();
			rev.Name = name;
			Fixed frame = new Fixed ();
			frame.SetSizeRequest ((int)width, (int)height);
			frame.Put (ctrl, 0, 0);
			//var resizer = new Button();
			//resizer.MotionNotifyEvent += OnFixed1MotionNotifyEvent;
			//frame.Put (resizer, (int)width + 5, (int)height + 5);
			frame.ShowAll ();
			rev.Add (frame);
			Console.WriteLine ("Creating new moving object" + rev.Name);
			return rev;
		}
		
		//Create a clone of the selected object that will be shown until the destination of the control is reached
		private Widget CloneCurrCtrl ()
		{
			Widget re = null;
			
			if (this.currCtrl != null) {
				if (currCtrl is EventBox) {
					Widget fr = (currCtrl as EventBox).Child;
					if (fr is Fixed) {
						Widget mv = (fr as Fixed).Children [0];
						re = GetMovingBox (
							(currCtrl as EventBox).Name+"Clone",
							(mv as  MVObject).Caption,
							(mv as MVObject).ELType,
							(mv as MVObject).width,
							(mv as MVObject).height
						);
					}
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
					if (fr is Fixed) {
						((fr as Fixed).Children [0] as MVObject).ShowMenu ();
					}
				}	
			} else if (a.Event.Button == 1) {
				
				if (a.Event.Type == Gdk.EventType.TwoButtonPress) {
					if (sender is EventBox) {
						//Calling the edit method of the control
						((sender as EventBox).Child as MVObject).Edit ();
					}	
				} else {
					//Setup the origin of the move
					isDragged = true;
					currCtrl = sender as Widget;
					currCtrl.TranslateCoordinates (this.fixed1, 0, 0, out origX, out origY);
					fixed1.GetPointer (out pointX, out pointY);
					Console.WriteLine ("MovingBox KeyPressed on " + (((currCtrl as EventBox).Child as Fixed).Children [0] as MVObject).Caption);
					Console.WriteLine ("Pointer:" + pointX.ToString () + "-" + pointY.ToString ());
					Console.WriteLine ("Origin:" + origX.ToString () + "-" + origY.ToString ());
					if (butt == null) {
						butt = new EventBox ();
						var res = new Resizer ();
						butt.Add (res);
						//butt.SetSizeRequest (10, 10);
						butt.Events = (Gdk.EventMask)1020;//252;
						
						butt.ButtonPressEvent += delegate {
							resizing = true;
							isDragged = true;
							resizer = (Resizer)(butt as EventBox).Child;
						};
						butt.ButtonReleaseEvent += delegate(object o, ButtonReleaseEventArgs args) {
							resizing = false;
							isDragged = false;
							fixed1.Move (butt, (int)args.Event.X, (int)args.Event.Y);
							//resizer = null;
						};
						//butt.MotionNotifyEvent += OnFixed1MotionNotifyEvent;
						fixed1.Put (butt, origX, origY);
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
				currCtrl = null;
				if (currClone != null) {
					this.fixed1.Remove (currClone);
					Console.WriteLine ("Deleting moving object" + currClone.Name);
					currClone.Destroy ();
					currClone = null;
				}
				var dx = (origX + System.Convert.ToInt32 (a.Event.X) - pointX);
				var dy = (origY + System.Convert.ToInt32 (a.Event.Y) - pointY);          
				if ((origX + System.Convert.ToInt32 (a.Event.X) - pointX != 0) || (origY + System.Convert.ToInt32 (a.Event.Y) - pointY != 0)) {
					fixed1.Remove (butt);
					butt.Dispose ();
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
				if (currCtrl != null) {
					if (((currCtrl as EventBox).Child as Fixed).Children [0] is MVObject)
						MoveClone (ref currClone, args.Event.X, args.Event.Y);
					if ((resizer!=null)&&resizing) {
						MoveControl (butt, args.Event.X, args.Event.Y, true);
						var obj = (((currCtrl as EventBox).Child as Fixed).Children [0] as MVObject);
						obj.height = args.Event.Y;
						obj.width = args.Event.X;
					}
				}
			}
		}
	}	
}
