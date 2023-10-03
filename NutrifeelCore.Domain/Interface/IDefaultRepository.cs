using NutrifeelCore.Domain.Common;

namespace NutrifeelCore.Domain.Interface
{
    public interface IDefaultRepository<T> where T : EntityBase
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
