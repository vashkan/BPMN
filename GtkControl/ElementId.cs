using System;
namespace GtkControl
{
	[Serializable]
	public class ElementId : IComparable, ICloneable, IEquatable<ElementId>
	{
		public static bool CloneId = true;
		private Guid _id;
		public Guid Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}
		public ElementId()
		{
			this._id = Guid.NewGuid();
		}
		public ElementId(Guid id)
		{
			this._id = id;
		}
		public ElementId(string id)
		{
			try
			{
				this._id = new Guid(id);
			}
			catch
			{
			}
		}
		public ElementId(ElementId prototype)
		{
			if (ElementId.CloneId)
			{
				this._id = prototype._id;
				return;
			}
			this._id = Guid.NewGuid();
		}
		public int CompareTo(object obj)
		{
			ElementId elementId = obj as ElementId;
			if (elementId != null)
			{
				return this._id.CompareTo(elementId._id);
			}
			return -1;
		}
		public override string ToString()
		{
			return this.Id.ToString();
		}
		object ICloneable.Clone()
		{
			return new ElementId(this);
		}
		public virtual ElementId Clone()
		{
			return new ElementId(this);
		}
		public bool Equals(ElementId other)
		{
			return other != null && this.EqualsInternal(other);
		}
		public override bool Equals(object other)
		{
			return other != null && other is ElementId && this.EqualsInternal(other as ElementId);
		}
		private bool EqualsInternal(ElementId other)
		{
			return object.ReferenceEquals(this, other) || this._id == other._id;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
