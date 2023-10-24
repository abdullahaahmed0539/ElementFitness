using ElementFitness.DAL.Data;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;
using Microsoft.EntityFrameworkCore;

namespace ElementFitness.DAL.Repositories 
{
    public class OfferRepo: IOfferRepo, IDisposable
    {

        private readonly ApplicationDbContext _dbContext;

        public OfferRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Offer>? GetAll()
        {
            return _dbContext.Offers?.OrderByDescending(p => p.CreatedOn).ToList();
        }

        public Offer? GetByCondition(Func<Offer, bool> predicate)
        {
            return _dbContext.Offers?.FirstOrDefault(predicate);;
        }

        public async Task<Offer>? AddAsync(Offer newInstance)
        {
            if(newInstance == null)
                throw new NullReferenceException("Could not add new Offer. Offer value cannot be null.");
            
            newInstance.CreatedOn = DateTime.UtcNow;
            newInstance.Active = true;
            
            await _dbContext.Offers.AddAsync(newInstance);
            await _dbContext.SaveChangesAsync();
            return newInstance;
        }

        public async Task<bool> UpdateAsync(Offer updatedObj)
        {
            if (updatedObj == null)
                throw new NullReferenceException("Could not update the Offer. Offer value cannot be null.");
            
            _dbContext.Entry(updatedObj).State = EntityState.Modified;
            return (await _dbContext.SaveChangesAsync()) == 1 ? true : false ;
        

        }

        public async Task<bool> DeleteAsync(int id)
        {
            Offer? programToBeDelete = await _dbContext.Offers.FindAsync(id);
            if (programToBeDelete == null)
                throw new NullReferenceException("Could not delete the Offer. Offer value cannot be null.");
            
            _dbContext.Offers.Remove(programToBeDelete);
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
