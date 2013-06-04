using System;

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
		//public 
		#endregion
		public Lane (string pName, string cap,float _width, float _height)
			:base ( pName,  cap, _width, _height)
		{

		}
	}
}

