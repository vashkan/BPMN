
// This file has been generated by the GUI designer. Do not modify.
namespace GtkControl
{
	public partial class MVPanel
	{
		private global::Gtk.ScrolledWindow scrolledwindow1;
		private global::Gtk.Fixed fixed1;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget GtkControl.MVPanel
			global::Stetic.BinContainer.Attach (this);
			this.Name = "GtkControl.MVPanel";
			// Container child GtkControl.MVPanel.Gtk.Container+ContainerChild
			this.scrolledwindow1 = new global::Gtk.ScrolledWindow ();
			this.scrolledwindow1.CanFocus = true;
			this.scrolledwindow1.Name = "scrolledwindow1";
			this.scrolledwindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child scrolledwindow1.Gtk.Container+ContainerChild
			global::Gtk.Viewport w1 = new global::Gtk.Viewport ();
			w1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child GtkViewport.Gtk.Container+ContainerChild
			this.fixed1 = new global::Gtk.Fixed ();
			this.fixed1.Events = ((global::Gdk.EventMask)(252));
			this.fixed1.Name = "fixed1";
			this.fixed1.HasWindow = false;
			w1.Add (this.fixed1);
			this.scrolledwindow1.Add (w1);
			this.Add (this.scrolledwindow1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Show ();
			this.fixed1.MotionNotifyEvent += new global::Gtk.MotionNotifyEventHandler (this.OnFixed1MotionNotifyEvent);
		}
	}
}