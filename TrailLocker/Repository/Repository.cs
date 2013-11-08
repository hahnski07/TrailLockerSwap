using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TrailLocker.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IUnitOfWork UnitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
     
        public IQueryable<T> FindAll()
        {
            return UnitOfWork.Get<T>();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return UnitOfWork.Get<T>().Where(predicate);
        }      

        public void Add(T item)
        {
            UnitOfWork.Add(item);
        }

        public void Attach(T item, bool setToChanged = true)
        {
            UnitOfWork.Attach((object)item, setToChanged);
        }

        public void Remove(T item)
        {
            UnitOfWork.Remove(item);
        }
       
        public void Commit()
        {
            UnitOfWork.Commit();
        }

        #region IDisposable Members

        /// <summary>
        /// Cleanup any resources being used.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (UnitOfWork != null)
                UnitOfWork.Dispose();
        }

        #endregion
    }
}
