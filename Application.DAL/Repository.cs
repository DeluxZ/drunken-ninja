using Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Specification.Contract;
using System.Linq.Expressions;
using Application.Common.Logging;
using Application.DAL.Resources;
using System.Data.Entity;
using Application.DAL.Contract;

namespace Application.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        IQueryableUnitOfWork _unitOfWork;

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        public void Add(T item)
        {
            if (item != (T)null)
            {
                // add new item in this set
                GetSet().Add(item);
            }
            else
            {
                LoggerFactory.CreateLog().LogInfo(Message.info_CannotAddNullEntity, typeof(T).ToString());
            }
        }

        public void Remove(T item)
        {
            if (item != (T)null)
            {
                // attach item if not exists
                _unitOfWork.Attach(item);

                // set as "removed"
                GetSet().Remove(item);
            }
            else
            {
                LoggerFactory.CreateLog().LogInfo(Message.info_CannotRemoveNullEntity, typeof(T).ToString());
            }
        }

        public void TrackItem(T item)
        {
            if (item != (T)null)
            {
                _unitOfWork.Attach<T>(item);
            }
            else
            {
                LoggerFactory.CreateLog().LogInfo(Message.info_CannotTrackNullEntity, typeof(T).ToString());
            }
        }

        public void Modify(T item)
        {
            if (item != (T)null)
            {
                _unitOfWork.SetModified<T>(item);
            }
            else
            {
                LoggerFactory.CreateLog().LogInfo(Message.info_CannotModifyNullEntity, typeof(T).ToString());
            }
        }

        public T Get(int id)
        {
            if (id != 0)
            {
                return GetSet().Find(id);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return GetSet();
        }

        public IEnumerable<T> AllMatching(ISpecification<T> specification)
        {
            return GetSet().Where(specification.SatisfiedBy());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Property"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public IEnumerable<T> GetPaged<Property>(int pageIndex, int pageCount, Expression<Func<T, Property>> orderByExpression, bool ascending)
        {
            if (ascending)
            {
                return GetSet().OrderBy(orderByExpression)
                    .Skip(pageCount * pageIndex)
                    .Take(pageCount);
            }
            else
            {
                return GetSet().OrderByDescending(orderByExpression)
                    .Skip(pageCount * pageIndex)
                    .Take(pageCount);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<T> GetFiltered(Expression<Func<T, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        public void Merge(T persisted, T current)
        {
            _unitOfWork.ApplyCurrentValues(persisted, current);
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

        private IDbSet<T> GetSet()
        {
            return _unitOfWork.CreateSet<T>();
        }
    }
}
