using System;

namespace GtkControl
{
	/// <summary>
	/// Интерфейс перемещаемых элементов
	/// </summary>
	public interface IDragged
	{
		/// <summary>
		/// Признак перемещаемости элемента
		/// </summary>
		bool IsDragged { get; set; }
	}
}

