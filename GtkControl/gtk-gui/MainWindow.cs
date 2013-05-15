
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	private global::Gtk.Action zoomOutAction;
	private global::Gtk.Action zoom100Action;
	private global::Gtk.Action zoomInAction;
	private global::Gtk.HPaned hpaned1;
	private global::Gtk.VPaned vpaned2;
	private global::Gtk.HBox hbox2;
	private global::Gtk.Toolbar toolbar2;
	private global::Gtk.ColorButton colorbutton1;
	private global::Gtk.HScale hscale1;
	private global::GtkControl.MVPanel mvpanel1;
	private global::Gtk.VPaned vpaned1;
	private global::Gtk.Label label2;
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	private global::Gtk.TextView textview1;
	
	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.zoomOutAction = new global::Gtk.Action (
			"zoomOutAction",
			null,
			null,
			"gtk-zoom-out"
		);
		w1.Add (this.zoomOutAction, null);
		this.zoom100Action = new global::Gtk.Action (
			"zoom100Action",
			null,
			null,
			"gtk-zoom-100"
		);
		w1.Add (this.zoom100Action, null);
		this.zoomInAction = new global::Gtk.Action (
			"zoomInAction",
			null,
			null,
			"gtk-zoom-in"
		);
		w1.Add (this.zoomInAction, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.hpaned1 = new global::Gtk.HPaned ();
		this.hpaned1.CanFocus = true;
		this.hpaned1.Name = "hpaned1";
		this.hpaned1.Position = 955;
		// Container child hpaned1.Gtk.Paned+PanedChild
		this.vpaned2 = new global::Gtk.VPaned ();
		this.vpaned2.CanFocus = true;
		this.vpaned2.Name = "vpaned2";
		this.vpaned2.Position = 43;
		// Container child vpaned2.Gtk.Paned+PanedChild
		this.hbox2 = new global::Gtk.HBox ();
		this.hbox2.Name = "hbox2";
		this.hbox2.Spacing = 6;
		// Container child hbox2.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><toolbar name=\'toolbar2\'><toolitem name=\'zoomOutAction\' action=\'zoomOutAction" +
			"\'/><toolitem name=\'zoom100Action\' action=\'zoom100Action\'/><toolitem name=\'zoomIn" +
			"Action\' action=\'zoomInAction\'/></toolbar></ui>"
		);
		this.toolbar2 = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/toolbar2")));
		this.toolbar2.Name = "toolbar2";
		this.toolbar2.ShowArrow = false;
		this.hbox2.Add (this.toolbar2);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.toolbar2]));
		w2.Position = 0;
		// Container child hbox2.Gtk.Box+BoxChild
		this.colorbutton1 = new global::Gtk.ColorButton ();
		this.colorbutton1.CanFocus = true;
		this.colorbutton1.Events = ((global::Gdk.EventMask)(784));
		this.colorbutton1.Name = "colorbutton1";
		this.hbox2.Add (this.colorbutton1);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.colorbutton1]));
		w3.Position = 1;
		w3.Expand = false;
		w3.Fill = false;
		// Container child hbox2.Gtk.Box+BoxChild
		this.hscale1 = new global::Gtk.HScale (null);
		this.hscale1.CanFocus = true;
		this.hscale1.Name = "hscale1";
		this.hscale1.Adjustment.Upper = 500D;
		this.hscale1.Adjustment.PageIncrement = 10D;
		this.hscale1.Adjustment.StepIncrement = 1D;
		this.hscale1.Adjustment.Value = 100D;
		this.hscale1.DrawValue = true;
		this.hscale1.Digits = 0;
		this.hscale1.ValuePos = ((global::Gtk.PositionType)(2));
		this.hbox2.Add (this.hscale1);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.hscale1]));
		w4.Position = 2;
		this.vpaned2.Add (this.hbox2);
		global::Gtk.Paned.PanedChild w5 = ((global::Gtk.Paned.PanedChild)(this.vpaned2 [this.hbox2]));
		w5.Resize = false;
		// Container child vpaned2.Gtk.Paned+PanedChild
		this.mvpanel1 = new global::GtkControl.MVPanel ();
		this.mvpanel1.Events = ((global::Gdk.EventMask)(768));
		this.mvpanel1.Name = "mvpanel1";
		this.vpaned2.Add (this.mvpanel1);
		this.hpaned1.Add (this.vpaned2);
		global::Gtk.Paned.PanedChild w7 = ((global::Gtk.Paned.PanedChild)(this.hpaned1 [this.vpaned2]));
		w7.Resize = false;
		// Container child hpaned1.Gtk.Paned+PanedChild
		this.vpaned1 = new global::Gtk.VPaned ();
		this.vpaned1.CanFocus = true;
		this.vpaned1.Name = "vpaned1";
		this.vpaned1.Position = 49;
		// Container child vpaned1.Gtk.Paned+PanedChild
		this.label2 = new global::Gtk.Label ();
		this.label2.Name = "label2";
		this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("label2");
		this.vpaned1.Add (this.label2);
		global::Gtk.Paned.PanedChild w8 = ((global::Gtk.Paned.PanedChild)(this.vpaned1 [this.label2]));
		w8.Resize = false;
		// Container child vpaned1.Gtk.Paned+PanedChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.textview1 = new global::Gtk.TextView ();
		this.textview1.CanFocus = true;
		this.textview1.Name = "textview1";
		this.GtkScrolledWindow.Add (this.textview1);
		this.vpaned1.Add (this.GtkScrolledWindow);
		this.hpaned1.Add (this.vpaned1);
		this.Add (this.hpaned1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 1239;
		this.DefaultHeight = 681;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.mvpanel1.KeyPressEvent += new global::Gtk.KeyPressEventHandler (this.OnMvpanel1KeyPressEvent);
		this.mvpanel1.KeyReleaseEvent += new global::Gtk.KeyReleaseEventHandler (this.OnMvpanel1KeyReleaseEvent);
		this.mvpanel1.ButtonPressEvent += new global::Gtk.ButtonPressEventHandler (this.OnMvpanel1ButtonPressEvent);
		this.mvpanel1.ButtonReleaseEvent += new global::Gtk.ButtonReleaseEventHandler (this.OnMvpanel1ButtonReleaseEvent);
	}
}
