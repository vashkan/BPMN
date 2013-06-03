using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using GtkControl.Control;
namespace GtkControl
{
	public class SelectionService
	{
		#region переменные
		
		private readonly  Fixed m_panel;
		private List <ISelectable> m_selectedItems;
		
		#endregion
		
		#region Свойства
		
		internal List<ISelectable> CurrentSelection {
		get { 
			return m_selectedItems ?? (m_selectedItems = new List<ISelectable> ()); 
			}
    	} 
		
		#endregion
		public SelectionService (Fixed panel)
		{
			m_panel = panel;			
		}
		/// <summary>
        /// Выделить элемент
        /// </summary>
        /// <param name="item"></param>
        internal void SelectItem(ISelectable item)
        {
            ClearSelection();
            AddToSelection(item);
        }

        /// <summary>
        /// Добавить элемент к выделению
        /// </summary>
        /// <param name="item"></param>
        internal void AddToSelection (ISelectable item)
		{
			/*
            if (item is IGroupable)
            {
                List<IGroupable> groupItems = GetGroupMembers(item as IGroupable);

                foreach (ISelectable groupItem in groupItems)
                {
                    groupItem.IsSelected = true;
                    CurrentSelection.Add(groupItem);
                }
            }
            else
            {*/

			var baseItem = (item as BaseItem);
			if (baseItem != null) {
				foreach (var resizer in baseItem.Resizers) {
					resizer.ButtonPressEvent -= HandleButtonPressEvent;
					resizer.ButtonPressEvent += HandleButtonPressEvent;
					m_panel.Add (resizer);
				}
			}
			//m_panel.ShowAll ();
			item.IsSelected = true;
			CurrentSelection.Add (item);
		}

        void HandleButtonPressEvent (object o, ButtonPressEventArgs args)
        {
			int X, Y;
			var eventBox = o as EventBox;
			if ((eventBox != null) && (eventBox.Child is IDragged)) {
				var res = eventBox.Child as Resizer;
				if (res != null) {
					eventBox.TranslateCoordinates (m_panel, 0, 0,
					                               out X,
					                               out Y);
					res.X = X;
					res.Y = Y;
				}
				(eventBox.Child as IDragged).IsDragged = true;
			}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        internal void RemoveFromSelection(ISelectable item)
        {/*
            if (item is IGroupable)
            {
                List<IGroupable> groupItems = GetGroupMembers(item as IGroupable);

                foreach (ISelectable groupItem in groupItems)
                {
                    groupItem.IsSelected = false;
                    CurrentSelection.Remove(groupItem);
                }
            }
            else
            {*/

		item.IsSelected = false;
		CurrentSelection.Remove (item);
		foreach (var resizer in (item as BaseItem).Resizers) {
				m_panel.Remove(resizer);
		}
		//}
		}

        /// <summary>
        /// 
        /// </summary>
        internal void ClearSelection()
        {
            CurrentSelection.ForEach(item => {
				item.IsSelected = false;
				foreach (var resizer in (item as BaseItem).Resizers) {
					m_panel.Remove(resizer);
				}
			});
            CurrentSelection.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        internal void SelectAll()
        {
            ClearSelection();
            CurrentSelection.AddRange(m_panel.AllChildren.OfType<ISelectable>());
            CurrentSelection.ForEach(item => item.IsSelected = true);
        }
	}
}