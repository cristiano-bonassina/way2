using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Way2.Domain.Entities.Abstractions;

namespace Way2.Domain.Entities
{
    /// <summary>
    /// Base class for entity definitions
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class Entity<TKey> : IEntity<TKey>, INotifyPropertyChanged, IEquatable<Entity<TKey>> where TKey : IEquatable<TKey>
    {

        public static bool operator ==(Entity<TKey> l, Entity<TKey> r)
        {

            if (l is null && r is null)
            {
                return true;
            }

            if (l is null || r is null)
            {
                return false;
            }

            return l.Equals(r);

        }

        public static bool operator !=(Entity<TKey> l, Entity<TKey> r)
        {
            return !(l == r);
        }

        #region Fields

        private DateTimeOffset _createdAt;
        private readonly int _hashcode = Guid.NewGuid().GetHashCode();
        private DateTimeOffset? _modifiedAt;
        private TKey _id;
        private long _version;

        #endregion

        #region Properties        

        public DateTimeOffset CreatedAt
        {
            get => _createdAt;
            set => SetWithNotify(value, ref _createdAt);
        }

        public TKey Id
        {
            get => _id;
            set => SetWithNotify(value, ref _id);
        }

        public DateTimeOffset? ModifiedAt
        {
            get => _modifiedAt;
            set => SetWithNotify(value, ref _modifiedAt);
        }

        public long Version
        {
            get => _version;
            set => SetWithNotify(value, ref _version);
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void SetWithNotify<T>(T value, ref T field, [CallerMemberName] string propertyName = "")
        {
            if (Equals(field, value))
            {
                return;
            }
            field = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override bool Equals(object obj)
        {

            if (!(obj is Entity<TKey> entity))
            {
                return false;
            }

            return this.Equals(entity);

        }

        public bool Equals(Entity<TKey> obj)
        {

            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (Equals(obj.Id, default(TKey)))
            {
                return false;
            }

            if (Equals(this.Id, default(TKey)))
            {
                return false;
            }

            return Equals(obj.Id, this.Id);

        }

        public override int GetHashCode()
        {
            return this.IsNew() ? _hashcode : this.Id.GetHashCode();
        }

        public object GetId()
        {
            return this.Id;
        }

        public bool IsNew()
        {
            return this.Id == null || this.Id.Equals(default);
        }

    }
}
