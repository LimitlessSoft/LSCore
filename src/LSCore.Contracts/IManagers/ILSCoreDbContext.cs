using System.Linq.Expressions;

namespace LSCore.Contracts.IManagers
{
    public interface ILSCoreDbContext
    {
        IQueryable<T> AsQueryable<T>() where T : class;
        List<T> SqlQuery<T>(string query) where T : class, new();
        void Insert<T>(T entity) where T : class;
        void InsertMultiple<T>(IEnumerable<T> entities) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Delete<T>(IEnumerable<T> entities) where T : class;
        void DeleteNonEntity<T>(Expression<Func<T, bool>> expression) where T : class;
        T Get<T>(Expression<Func<T, bool>> expression) where T : class;
        void UpdateMultiple<T>(IEnumerable<T> entities) where T : class;
    }
}
