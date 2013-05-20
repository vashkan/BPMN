using System;
namespace GtkControl
{
    /// <summary>
    /// Интерфейс элементов которые могут быть выбраны
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// Признак выбраности элемента
        /// </summary>
        bool IsSelected { get; set; }
    }
}