using System;

namespace Bloghost.Model
{
    public abstract class Entity : IEntity<Guid>, IEquatable<Entity>
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModificatedDate { get; set; }

        protected Entity()
        {
            CreatedDate = LastModificatedDate = DateTime.UtcNow;
        }

        public override bool Equals(object obj)
        {
            if (obj is Entity)
                return Equals(obj as Entity);
            return false;
        }

        public bool Equals(Entity other)
        {
            if (other == null)
                return false;
            if (other.Id == this.Id)
                return true;
            if (ReferenceEquals(this, other))
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
