using Microsoft.EntityFrameworkCore;
using NutrifeelCore.Domain.Common;
using NutrifeelCore.Domain.Context;
using NutrifeelCore.Domain.Interface;

namespace NutrifeelCore.Domain.Repository
{
    public class DefaultRepository<T> : IDefaultRepository<T> where T : EntityBase
    {
        #region Members
        private readonly NutrifeelContext _context;
        private readonly DbSet<T> table;
        #endregion

        #region Ctor
        public DefaultRepository(NutrifeelContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        #endregion

        #region Methods
        public IEnumerable<T> GetAll()
        {
            return table.AsQueryable();
        }

        public T GetById(Guid id)
        {
            return table.AsQueryable().FirstOrDefault(x => x.Id == id);
        }

        public bool Insert(T entity)
        {
            try
            {
                if (entity.Id == Guid.Empty)
                    entity.Id = Guid.NewGuid();

                entity.CreateTime = DateTime.Now;
                table.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                entity.UpdateTime = DateTime.Now;
                table.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                table.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
