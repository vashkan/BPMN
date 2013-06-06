using System;
using System.Collections.Generic;
using GtkControl.Control;
namespace GtkControl
{
	public interface IConnectingObject : /*IGraphicalElement, */IElement, ICloneable
	{
		ConnectingObjectType ConnectingObjectType
		{
			get;
		}
		BaseItem Source
		{
			get;
			set;
		}
		BaseItem Target
		{
			get;
			set;
		}
		string SourcePort
		{
			get;
			set;
		}
		string TargetPort
		{
			get;
			set;
		}
		List<Cairo.PointD> Points
		{
			get;
		}
	}
}
