using System;
using Gtk;
using Cairo;

namespace GtkControl.Control
{
	/// <summary>
	/// Задача
	/// </summary>
	public class Task:BaseItem
	{	
		#region Переменные
		#endregion
		#region Свойства
			public override  BPMNElementType ElementType
		{
			get 
			{
				return BPMNElementType.TASK;
			}
		}
		#endregion
		/// <summary>
		/// Задача
		/// </summary>
		/// <param name="pName">Имя</param>
		/// <param name="cap">Заголовок</param>
		/// <param name="_width">ширина</param>
		/// <param name="_height">высота</param>
		public Task (string pName, string cap, float _width, float _height)
		:base(pName,cap,_width,_height)
		{

		}


		/// <summary>
		/// Отрисовка элемента задача
		/// </summary>
		/// <param name="g"></param>
		public override void Paint (Context g)
		{
			g.Save();
			//установка траектории прямоугольника со скругленными углами
			DrawRoundedRectangle (g,1, 1, Width-2, Height-2, 10);
			//настройка градиета 
			Cairo.Gradient pat = new Cairo.LinearGradient( 2,2, 2,2+Height-4);
	        pat.AddColorStop(0, new Cairo.Color(0.98,0.98,1,1));
	        pat.AddColorStop(1, new Cairo.Color(0.90,0.9,1,1));
	        g.Pattern = pat;
	 
	        // Заливка градиентом
	        g.FillPreserve();
	 
	        // We "undo" the pattern setting here
	        g.Restore();
	 
	        // Установка цвета отрисовки границ
	        g.Color = new Color (0.01, 0.4, 0.6);
	        g.LineWidth = 2;
	        g.Stroke();
			//Отрисовка текста при сборке в VS возможны проблемы с отображением кириллицы
			g.Color = new Color(0, 0, 0);
			g.SelectFontFace("Georgia", FontSlant.Normal, FontWeight.Bold);
			g.SetFontSize(14.0);
			TextExtents te = g.TextExtents(Body);
			g.MoveTo(1 - te.Width/2+Width/2,
			         1 + te.Height/2+Height/2);
			g.ShowText(Body);
		}

	    /// <summary>
	    /// Перегружаемая функция отрисовки маски для элемента
	    /// </summary>
	    /// <param name="g"></param>
	    public override void PaintMask (Context g)
		{
			g.Save ();
			DrawRoundedRectangle (g, 1, 1, Width - 2, Height - 2, 10);

			
			g.Color = new Color (1, 1, 1);
			//заливка
			g.FillPreserve ();
	 
			// Востановление настроек цвета
			g.Restore ();
			// Цвет линии
			g.Color = new Color (1, 1, 1);
	 		//Толщина линии
			g.LineWidth = 2;
			//Отрисовка линии
			g.Stroke ();
			//Задание цвета текста
			g.Color = new Color (1, 1, 1);
			g.SelectFontFace ("Georgia", FontSlant.Normal, FontWeight.Bold);
			g.SetFontSize (14.0);
			TextExtents te = g.TextExtents (Body);
			g.MoveTo (1 - te.Width / 2 + Width / 2,
			         1 + te.Height / 2 + Height / 2);
			//Отрисовка текста
			g.ShowText (Body);
		}
	}
}

