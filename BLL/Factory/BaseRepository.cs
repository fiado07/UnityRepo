using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Factory
{
    public class BaseRepository<T> : UnityRepoContracts.Contracts.IRepository<T> where T : class
    {

        private UnityRepo.UnityRepo.Repository<T> baseRepo { set; get; }

        public BaseRepository(object context)
        {

            baseRepo = new UnityRepo.UnityRepo.Repository<T>(context);

        }


        public void AddEntity(T entity)
        {
            baseRepo.AddEntity(entity);
        }

        public void AddEntityRange(List<T> entityList)
        {
            baseRepo.AddEntityRange(entityList);
        }

        public bool Any()
        {
            return baseRepo.Any();
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return baseRepo.Any(predicate);
        }

        public int Count()
        {
            return baseRepo.Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return baseRepo.Count(predicate);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return baseRepo.Find(predicate);
        }

        public IEnumerable<T> FindAll()
        {
            return baseRepo.FindAll();
        }

        public T GetEntity(object id)
        {
            return baseRepo.GetEntity(id);
        }

        public IEnumerable<T> Pagination(int pageIndex, int pageSize = 10)
        {
            return baseRepo.Pagination(pageIndex, pageSize);
        }

        public void RemoveEntity(Expression<Func<T, bool>> predicate)
        {
            baseRepo.RemoveEntity(predicate);
        }

        public void RemoveEntity(T entity)
        {
            baseRepo.RemoveEntity(entity);
        }

        public void RemoveEntityRange(List<T> entityList)
        {
            baseRepo.RemoveEntityRange(entityList);
        }
    }
}
