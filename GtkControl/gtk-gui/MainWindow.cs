
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	private global::Gtk.HPaned hpaned1;
	private global::Gtk.VPaned vpaned2;
	private global::Gtk.HBox hbox2;
	private global::Gtk.Toolbar toolbar2;
	private global::Gtk.ColorButton colorbutton1;
	private global::Gtk.HScale hscale1;
	private global::GtkControl.MVPanel mvpanel1;
	private global::Gtk.VBox vbox1;
	private global::Gtk.Expander expander4;
	private global::Gtk.VBox vbox4;
	private global::Gtk.Button button4;
	private global::Gtk.Label GtkLabel2;
	private global::Gtk.Expander expander2;
	private global::Gtk.VBox vbox3;
	private global::Gtk.Button button3;
	private global::Gtk.Label GtkLabel4;
	private global::Gtk.Expander expander3;
	private global::Gtk.VBox vbox2;
	private global::Gtk.Button button1;
	private global::Gtk.Button button2;
	private global::Gtk.Label GtkLabel7;
	private global::Gtk.Expander expander1;
	private global::Gtk.VBox vbox5;
	private global::Gtk.Button button5;
	private global::Gtk.Button button6;
	private global::Gtk.Button button7;
	private global::Gtk.Label GtkLabel10;
	
	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.hpaned1 = new global::Gtk.HPaned ();
		this.hpaned1.CanFocus = true;
		this.hpaned1.Name = "hpaned1";
		this.hpaned1.Position = 820;
		// Container child hpaned1.Gtk.Paned+PanedChild
		this.vpaned2 = new global::Gtk.VPaned ();
		this.vpaned2.CanFocus = true;
		this.vpaned2.Name = "vpaned2";
		this.vpaned2.Position = 39;
		// Container child vpaned2.Gtk.Paned+PanedChild
		this.hbox2 = new global::Gtk.HBox ();
		this.hbox2.Name = "hbox2";
		this.hbox2.Spacing = 6;
		// Container child hbox2.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><toolbar name=\'toolbar2\'><toolitem/><toolitem/><toolitem/></toolbar></ui>");
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
		this.hscale1.Adjustment.Value = 101D;
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
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 2;
		this.vbox1.BorderWidth = ((uint)(1));
		// Container child vbox1.Gtk.Box+BoxChild
		this.expander4 = new global::Gtk.Expander (null);
		this.expander4.CanFocus = true;
		this.expander4.Name = "expander4";
		this.expander4.Expanded = true;
		this.expander4.BorderWidth = ((uint)(1));
		// Container child expander4.Gtk.Container+ContainerChild
		this.vbox4 = new global::Gtk.VBox ();
		this.vbox4.Name = "vbox4";
		this.vbox4.Spacing = 6;
		// Container child vbox4.Gtk.Box+BoxChild
		this.button4 = new global::Gtk.Button ();
		this.button4.TooltipMarkup = "Пул";
		this.button4.CanFocus = true;
		this.button4.Name = "button4";
		this.button4.UseUnderline = true;
		// Container child button4.Gtk.Container+ContainerChild
		global::Gtk.Alignment w8 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w9 = new global::Gtk.HBox ();
		w9.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w10 = new global::Gtk.Image ();
		w10.Pixbuf = new global::Gdk.Pixbuf (global::System.IO.Path.Combine (global::System.AppDomain.CurrentDomain.BaseDirectory, ".\\Pool_24.jpg"));
		w9.Add (w10);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w12 = new global::Gtk.Label ();
		w9.Add (w12);
		w8.Add (w9);
		this.button4.Add (w8);
		this.vbox4.Add (this.button4);
		global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.button4]));
		w16.Position = 0;
		w16.Expand = false;
		w16.Fill = false;
		this.expander4.Add (this.vbox4);
		this.GtkLabel2 = new global::Gtk.Label ();
		this.GtkLabel2.Name = "GtkLabel2";
		this.GtkLabel2.LabelProp = global::Mono.Unix.Catalog.GetString ("Роли");
		this.GtkLabel2.UseUnderline = true;
		this.expander4.LabelWidget = this.GtkLabel2;
		this.vbox1.Add (this.expander4);
		global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.expander4]));
		w18.Position = 0;
		w18.Expand = false;
		w18.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.expander2 = new global::Gtk.Expander (null);
		this.expander2.CanFocus = true;
		this.expander2.Name = "expander2";
		this.expander2.Expanded = true;
		this.expander2.BorderWidth = ((uint)(1));
		// Container child expander2.Gtk.Container+ContainerChild
		this.vbox3 = new global::Gtk.VBox ();
		this.vbox3.Name = "vbox3";
		this.vbox3.Spacing = 6;
		// Container child vbox3.Gtk.Box+BoxChild
		this.button3 = new global::Gtk.Button ();
		this.button3.CanFocus = true;
		this.button3.Name = "button3";
		this.button3.UseUnderline = true;
		// Container child button3.Gtk.Container+ContainerChild
		global::Gtk.Alignment w19 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w20 = new global::Gtk.HBox ();
		w20.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w21 = new global::Gtk.Image ();
		w20.Add (w21);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w23 = new global::Gtk.Label ();
		w23.LabelProp = global::Mono.Unix.Catalog.GetString ("Процесс");
		w23.UseUnderline = true;
		w20.Add (w23);
		w19.Add (w20);
		this.button3.Add (w19);
		this.vbox3.Add (this.button3);
		global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.button3]));
		w27.Position = 0;
		w27.Expand = false;
		w27.Fill = false;
		this.expander2.Add (this.vbox3);
		this.GtkLabel4 = new global::Gtk.Label ();
		this.GtkLabel4.Name = "GtkLabel4";
		this.GtkLabel4.LabelProp = global::Mono.Unix.Catalog.GetString ("Задачи");
		this.GtkLabel4.UseUnderline = true;
		this.expander2.LabelWidget = this.GtkLabel4;
		this.vbox1.Add (this.expander2);
		global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.expander2]));
		w29.Position = 1;
		w29.Expand = false;
		w29.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.expander3 = new global::Gtk.Expander (null);
		this.expander3.CanFocus = true;
		this.expander3.Name = "expander3";
		this.expander3.Expanded = true;
		this.expander3.BorderWidth = ((uint)(1));
		// Container child expander3.Gtk.Container+ContainerChild
		this.vbox2 = new global::Gtk.VBox ();
		this.vbox2.Name = "vbox2";
		this.vbox2.Spacing = 6;
		// Container child vbox2.Gtk.Box+BoxChild
		this.button1 = new global::Gtk.Button ();
		this.button1.TooltipMarkup = "Старт";
		this.button1.CanFocus = true;
		this.button1.Name = "button1";
		// Container child button1.Gtk.Container+ContainerChild
		global::Gtk.Alignment w30 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w31 = new global::Gtk.HBox ();
		w31.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w32 = new global::Gtk.Image ();
		w32.Pixbuf = new global::Gdk.Pixbuf (global::System.IO.Path.Combine (global::System.AppDomain.CurrentDomain.BaseDirectory, ".\\Start_24.jpg"));
		w31.Add (w32);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w34 = new global::Gtk.Label ();
		w31.Add (w34);
		w30.Add (w31);
		this.button1.Add (w30);
		this.vbox2.Add (this.button1);
		global::Gtk.Box.BoxChild w38 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.button1]));
		w38.Position = 0;
		w38.Expand = false;
		w38.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.button2 = new global::Gtk.Button ();
		this.button2.TooltipMarkup = "Завершение";
		this.button2.CanFocus = true;
		this.button2.Name = "button2";
		// Container child button2.Gtk.Container+ContainerChild
		global::Gtk.Alignment w39 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w40 = new global::Gtk.HBox ();
		w40.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w41 = new global::Gtk.Image ();
		w41.Pixbuf = new global::Gdk.Pixbuf (global::System.IO.Path.Combine (global::System.AppDomain.CurrentDomain.BaseDirectory, ".\\End_24.jpg"));
		w40.Add (w41);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w43 = new global::Gtk.Label ();
		w40.Add (w43);
		w39.Add (w40);
		this.button2.Add (w39);
		this.vbox2.Add (this.button2);
		global::Gtk.Box.BoxChild w47 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.button2]));
		w47.Position = 1;
		w47.Expand = false;
		w47.Fill = false;
		this.expander3.Add (this.vbox2);
		this.GtkLabel7 = new global::Gtk.Label ();
		this.GtkLabel7.Name = "GtkLabel7";
		this.GtkLabel7.LabelProp = global::Mono.Unix.Catalog.GetString ("События");
		this.GtkLabel7.UseUnderline = true;
		this.expander3.LabelWidget = this.GtkLabel7;
		this.vbox1.Add (this.expander3);
		global::Gtk.Box.BoxChild w49 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.expander3]));
		w49.Position = 2;
		w49.Expand = false;
		w49.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.expander1 = new global::Gtk.Expander (null);
		this.expander1.CanFocus = true;
		this.expander1.Name = "expander1";
		this.expander1.Expanded = true;
		// Container child expander1.Gtk.Container+ContainerChild
		this.vbox5 = new global::Gtk.VBox ();
		this.vbox5.Name = "vbox5";
		this.vbox5.Spacing = 6;
		// Container child vbox5.Gtk.Box+BoxChild
		this.button5 = new global::Gtk.Button ();
		this.button5.CanFocus = true;
		this.button5.Name = "button5";
		this.button5.UseUnderline = true;
		// Container child button5.Gtk.Container+ContainerChild
		global::Gtk.Alignment w50 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w51 = new global::Gtk.HBox ();
		w51.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w52 = new global::Gtk.Image ();
		w51.Add (w52);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w54 = new global::Gtk.Label ();
		w54.LabelProp = global::Mono.Unix.Catalog.GetString ("GtkButton");
		w54.UseUnderline = true;
		w51.Add (w54);
		w50.Add (w51);
		this.button5.Add (w50);
		this.vbox5.Add (this.button5);
		global::Gtk.Box.BoxChild w58 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.button5]));
		w58.Position = 0;
		w58.Expand = false;
		w58.Fill = false;
		// Container child vbox5.Gtk.Box+BoxChild
		this.button6 = new global::Gtk.Button ();
		this.button6.CanFocus = true;
		this.button6.Name = "button6";
		this.button6.UseUnderline = true;
		this.button6.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");
		this.vbox5.Add (this.button6);
		global::Gtk.Box.BoxChild w59 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.button6]));
		w59.Position = 1;
		w59.Expand = false;
		w59.Fill = false;
		// Container child vbox5.Gtk.Box+BoxChild
		this.button7 = new global::Gtk.Button ();
		this.button7.CanFocus = true;
		this.button7.Name = "button7";
		this.button7.UseUnderline = true;
		this.button7.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");
		this.vbox5.Add (this.button7);
		global::Gtk.Box.BoxChild w60 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.button7]));
		w60.Position = 2;
		w60.Expand = false;
		w60.Fill = false;
		this.expander1.Add (this.vbox5);
		this.GtkLabel10 = new global::Gtk.Label ();
		this.GtkLabel10.Name = "GtkLabel10";
		this.GtkLabel10.LabelProp = global::Mono.Unix.Catalog.GetString ("Коннекторы");
		this.GtkLabel10.UseUnderline = true;
		this.expander1.LabelWidget = this.GtkLabel10;
		this.vbox1.Add (this.expander1);
		global::Gtk.Box.BoxChild w62 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.expander1]));
		w62.Position = 3;
		w62.Expand = false;
		w62.Fill = false;
		this.hpaned1.Add (this.vbox1);
		this.Add (this.hpaned1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 933;
		this.DefaultHeight = 730;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.mvpanel1.KeyPressEvent += new global::Gtk.KeyPressEventHandler (this.OnMvpanel1KeyPressEvent);
		this.mvpanel1.KeyReleaseEvent += new global::Gtk.KeyReleaseEventHandler (this.OnMvpanel1KeyReleaseEvent);
		this.mvpanel1.ButtonPressEvent += new global::Gtk.ButtonPressEventHandler (this.OnMvpanel1ButtonPressEvent);
		this.mvpanel1.ButtonReleaseEvent += new global::Gtk.ButtonReleaseEventHandler (this.OnMvpanel1ButtonReleaseEvent);
	}
}
