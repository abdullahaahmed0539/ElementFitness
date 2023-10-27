using ElementFitness.DAL.Data;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;
using Microsoft.EntityFrameworkCore;

namespace ElementFitness.DAL.Repositories 
{
    public class PartnerRepo: IPartnerRepo, IDisposable
    {

        private readonly ApplicationDbContext _dbContext;

        public PartnerRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Partner>? GetAll()
        {
            return _dbContext.Partners?.OrderByDescending(p => p.CreatedOn).ToList();
        }

        public Partner? GetByCondition(Func<Partner, bool> predicate)
        {
            return _dbContext.Partners?.FirstOrDefault(predicate);;
        }

        public async Task<Partner>? AddAsync(Partner newInstance)
        {
            if(newInstance == null)
                throw new NullReferenceException("Could not add new Partner. Partner value cannot be null.");
            
            newInstance.CreatedOn = DateTime.UtcNow;
            
            await _dbContext.Partners.AddAsync(newInstance);
            await _dbContext.SaveChangesAsync();
            return newInstance;
        }

        public async Task<bool> UpdateAsync(Partner updatedObj)
        {
            if (updatedObj == null)
                throw new NullReferenceException("Could not update the Partner. Partner value cannot be null.");
            
            _dbContext.Entry(updatedObj).State = EntityState.Modified;
            return (await _dbContext.SaveChangesAsync()) == 1 ? true : false ;
        

        }

        public async Task<bool> DeleteAsync(int id)
        {
            Partner? partnerToBeDelete = await _dbContext.Partners.FindAsync(id);
            if (partnerToBeDelete == null)
                throw new NullReferenceException("Could not delete the Partner. Partner value cannot be null.");
            
            _dbContext.Partners.Remove(partnerToBeDelete);
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
