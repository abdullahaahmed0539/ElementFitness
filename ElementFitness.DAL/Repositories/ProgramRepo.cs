


using ElementFitness.DAL.Data;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;
using Microsoft.EntityFrameworkCore;

namespace ElementFitness.DAL.Repositories 
{
    public class ProgramRepo: IProgramRepo, IDisposable
    {

        private readonly ApplicationDbContext _dbContext;

        public ProgramRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Program>? GetAll()
        {
            return _dbContext.Programs?.OrderByDescending(p => p.CreatedOn).ToList();
        }

        public Program? GetByCondition(Func<Program, bool> predicate)
        {
            return _dbContext.Programs?.FirstOrDefault(predicate);;
        }

        public async Task<Program>? AddAsync(Program newInstance)
        {
            if(newInstance == null)
                throw new NullReferenceException("Could not add new Program. Program value cannot be null.");
            
            newInstance.CreatedOn = DateTime.UtcNow;
            newInstance.Active = true;
            
            await _dbContext.Programs.AddAsync(newInstance);
            await _dbContext.SaveChangesAsync();
            return newInstance;
        }

        public async Task<bool> UpdateAsync(Program updatedObj)
        {
            if (updatedObj == null)
                throw new NullReferenceException("Could not update the Program. Program value cannot be null.");
            
            _dbContext.Entry(updatedObj).State = EntityState.Modified;
            return (await _dbContext.SaveChangesAsync()) == 1 ? true : false ;
        

        }

        public async Task<bool> DeleteAsync(int id)
        {
            Program? programToBeDelete = await _dbContext.Programs.FindAsync(id);
            if (programToBeDelete == null)
                throw new NullReferenceException("Could not delete the Program. Program value cannot be null.");
            
            _dbContext.Programs.Remove(programToBeDelete);
            return (await _dbContext.SaveChangesAsync()) == 1 ? true : false;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


}
