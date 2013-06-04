using System;
namespace GtkControl.Control
{
	[Serializable]
	public abstract class Swimlane:BaseItem
	{
		public abstract SwimlaneType SwimlaneType 
		{
			get;
		}
		public Swimlane(string pName, string cap,float _width, float _height)
			:base( pName, cap, _width, _height)
		{
		}
	}
}

