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
        /// 
        /// </summary>
        /// <param name="item"></param>
        internal void SelectItem(ISelectable item)
        {
            ClearSelection();
            AddToSelection(item);
        }

        /// <summary>
        /// 
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
			int origX, origY, pointX, pointY;
			var baseItem = (item as BaseItem);
			if (baseItem != null) {
				int index = 0;
				for (var j = 0; j < 3; j++) {
					for (var i = 0; i < 3; i++) {
						if ((i == 1) && (j == 1)) {
							continue;
						}
						baseItem.Resizers [index].Events = (Gdk.EventMask)1020; //252;
						baseItem.Resizers [index].ButtonPressEvent +=
						delegate(object o, ButtonPressEventArgs args) {
							//resizing = true;
							var eventBox = o as EventBox;
							if (eventBox != null)
							{
								eventBox.TranslateCoordinates (m_panel, 0, 0,
							                              out origX,
							                              out origY);
								(eventBox.Child as IDragged).IsDragged = true;
							}
							m_panel.GetPointer (out pointX, out pointY);
						};
						baseItem.Resizers [index].ButtonReleaseEvent +=
						delegate(object o, ButtonReleaseEventArgs args) {
							var eventBox = (o as EventBox);
							if ((eventBox != null)&&(eventBox.Child is IDragged))
							{
								(eventBox.Child as IDragged).IsDragged = false;
							}
						};
						m_panel.Add (baseItem.Resizers [index++]);
					}
				}
			}
			m_panel.ShowAll ();
			item.IsSelected = true;
			CurrentSelection.Add (item);
			//}
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
                CurrentSelection.Remove(item);
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