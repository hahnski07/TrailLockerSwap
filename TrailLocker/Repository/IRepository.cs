using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TrailLocker.Repository
{
    public interface IRepository<T> : IDisposable 
        where T : class
    {

        /// <summary>
        ///     Get a queryable of all the object in the repository of type T
        /// </summary>
        /// <returns>A Queryable to find all objects of type T</returns>
        IQueryable<T> FindAll();

        /// <summary>
        ///     Get a queryable of all the objects in the repository of type T, but limited by the predicate
        /// </summary>
        /// <param name="predicate">The where clause to limit by</param>
        /// <returns>A Queryable to find all objects of type T</returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Add an item of type T to the repository
        /// </summary>
        /// <param name="item">The object to add</param>
        void Add(T item);

        /// <summary>
        ///     Attach an existing item of type T to the repository
        /// </summary>
        /// <param name="item">The item to attach</param>
        /// <param name="setToChanged">(optional) flag to denote whether or not this object is to be marked as changed</param>
        void Attach(T item, bool setToChanged = true);

        /// <summary>
        ///     Remove an item of type T from the repository
        /// </summary>
        /// <param name="item">the item to remove</param>
        void Remove(T item);
        
        /// <summary>
        ///     Commit all of the work done prior to this call
        /// </summary>
        void Commit();

    }
}
