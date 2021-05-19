using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Mediatr.Queries
{

    /// <summary>
    /// Base class for Queries ;) --therealmkb
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>Typically a collection of entities of type  <typeparamref name="T"/></returns>
    /// <remarks><see cref="IQuery{T}"/></remarks>
    public abstract class QueryBase<T> : IQuery<T>, IEquatable<QueryBase<T>> where T: class
    {
        protected QueryBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public bool Equals(QueryBase<T> other)
        {
            return other is not null && other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType()) 
            {
                return false;
            }

            return obj is QueryBase<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(QueryBase<T> left, QueryBase<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(QueryBase<T> left, QueryBase<T> right)
        {
            return !Equals(left, right);
        }
    }
}
