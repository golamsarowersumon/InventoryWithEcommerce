using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
  
    
        public interface IRepository<TEntity> where TEntity : class
        {
            IQueryable<TEntity> Get(string includeProperties = "");
            TEntity GetById(object id);
            void Insert(TEntity entity);
            void InsertRange(IEnumerable<TEntity> entity);
            void Update(TEntity entity);
            void Delete(TEntity entity);
            void DeleteRange(IEnumerable<TEntity> entity);
        }
    
}
