using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
namespace GtkControl
{
	/// <summary>
	/// 
	/// </summary>
	public class SelectionService
	{
		#region переменные
		
		private readonly  Fixed m_panel;
		private List <ISelectable> m_selectedItems;
		
		#endregion
		
		#region Свойства
		/// <summary>
		/// Текущее выделение
		/// </summary>
		internal List<ISelectable> CurrentSelection {
		get { 
			return m_selectedItems ?? (m_selectedItems = new List<ISelectable> ()); 
			}
    	} 
		
		#endregion
		/// <summary>
		/// 
		/// </summary>
		/// <param name="panel"></param>
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
        internal void AddToSelection(ISelectable item)
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
                item.IsSelected = true;
                CurrentSelection.Add(item);
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
            CurrentSelection.ForEach(item => item.IsSelected = false);
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