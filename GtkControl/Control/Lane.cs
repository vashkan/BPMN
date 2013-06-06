using System;
using System.Collections.Generic;
namespace GtkControl.Control
{
	[Serializable]
	public class Lane:Swimlane
	{
		#region Переменные

		#endregion
		#region Свойства
		public override BPMNElementType ElementType {
			get {
				return BPMNElementType.LANE;
			}
		}
		public override SwimlaneType SwimlaneType {
			get {
				return SwimlaneType.Lane;
			}
		}

		public  Pool ParentPool {
			get;
			set;
		}
		#endregion
		public Lane (string pName, string cap,float _width, float _height)
			:base ( pName,  cap, _width, _height)
		{

		}
		public List<BaseItem> GetElements()
		{
			return null;
		}
	}
}

