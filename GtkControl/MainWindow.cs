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
			"start",
			92,
			222,
			BPMNElementType.START_EVENT,
			40,
			40
		);

		this.mvpanel1.AddMovingObject (name + (index++).ToString (), "Task", 10, 10, BPMNElementType.TASK, 140, 70);
		this.mvpanel1.AddMovingObject (name + (index++).ToString (), "end", 300, 222, BPMNElementType.END_EVENT, 40, 40);
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
		var ctrl = new Task (name,"", 10, 15);
		eventbox2.Add (ctrl);
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