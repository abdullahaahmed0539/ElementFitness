using ElementFitness.DAL.Data;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;
using Microsoft.EntityFrameworkCore;

namespace ElementFitness.DAL.Repositories 
{
    public class TrainerRepo: ITrainerRepo, IDisposable
    {

        private readonly ApplicationDbContext _dbContext;

        public TrainerRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Trainer>? GetAll()
        {
            return _dbContext.Trainers?.OrderByDescending(p => p.CreatedOn).ToList();
        }

        public Trainer? GetByCondition(Func<Trainer, bool> predicate)
        {
            return _dbContext.Trainers?.FirstOrDefault(predicate);;
        }

        public async Task<Trainer>? AddAsync(Trainer newInstance)
        {
            if(newInstance == null)
                throw new NullReferenceException("Could not add new Trainer. Program value cannot be null.");
            
            newInstance.CreatedOn = DateTime.UtcNow;
            
            await _dbContext.Trainers.AddAsync(newInstance);
            await _dbContext.SaveChangesAsync();
            return newInstance;
        }

        public async Task<bool> UpdateAsync(Trainer updatedObj)
        {
            if (updatedObj == null)
                throw new NullReferenceException("Could not update the Trainer. Trainer value cannot be null.");
            
            _dbContext.Entry(updatedObj).State = EntityState.Modified;
            return (await _dbContext.SaveChangesAsync()) == 1 ? true : false ;
        

        }

        public async Task<bool> DeleteAsync(int id)
        {
            Trainer? trainerToBeDelete = await _dbContext.Trainers.FindAsync(id);
            if (trainerToBeDelete == null)
                throw new NullReferenceException("Could not delete the Trainer. Trainer value cannot be null.");
            
            _dbContext.Trainers.Remove(trainerToBeDelete);
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
