using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Repository
{
    public class GenaricRepo<TEntity,TContext> : IRepository<TEntity>
        where TEntity:class
        where TContext: DbContext
    {
        private TContext context;
        private DbSet<TEntity> set;
        public GenaricRepo(TContext _context)
        {
            context = _context;
            set = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return set;
        }

        public TEntity GetById(params object[] Id)
        {
            return set.Find(Id);
        }

        public TEntity Insert(TEntity entity)
        {
            TEntity _entity =set.Add(entity);

            return context.SaveChanges()>0?_entity:null ;
        }

        public bool Remove(object id)
        {
            TEntity entity = set.Find(id);

            set.Remove(entity);

            return context.SaveChanges() > 0;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity entity)
        {
            set.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChanges()>0;
        }
    }
}
