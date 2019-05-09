using System;

namespace Docunatr.Core
{
    public abstract class ElementBase : IEntity, IEquatable<ElementBase>
    {
        protected ElementBase(Title title) 
            : this(Guid.NewGuid(), title)
        {

        }

        private ElementBase(Guid id, Title title)
        {
            Id = id;
            Title = title;
        }

        public Title Title { get; }

        public Guid Id { get; }

        public bool Equals(ElementBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Title.Equals(other.Title);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ElementBase) obj);
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }

        public static bool operator ==(ElementBase left, ElementBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ElementBase left, ElementBase right)
        {
            return !Equals(left, right);
        }

        public bool IsSameAs<T>(T other) where T : ElementBase
        {
            return other.GetType() == typeof(T)
                && Equals(this, other) && Id == other.Id;
        }

        public override string ToString()
        {
            return $"Subsection {{ Title: \"{Title}\" }}";
        }
    }
}