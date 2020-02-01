using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        TEntity GetById(params object[] Id);
        TEntity Insert(TEntity entity);
        bool Remove(object id);
        bool Update(TEntity entity);
        void Save();
    }
}
