


namespace ElementFitness.DAL.Interfaces
{
    public interface IBaseRepository<T>: IDisposable
    {
        public IEnumerable<T>? GetAll();
        public T? GetByCondition(Func<T, bool> predicate);
        public Task<T>? AddAsync(T newInstance);
        public Task<bool> UpdateAsync(T updatedObj);
        public Task<bool> DeleteAsync(int id);
    }
}