using ElementFitness.DAL.Data;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;
using Microsoft.EntityFrameworkCore;

namespace ElementFitness.DAL.Repositories 
{
    public class MemberAndLeadRepo: IMemberAndLeadRepo, IDisposable
    {

        private readonly ApplicationDbContext _dbContext;

        public MemberAndLeadRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Contact>? GetAll()
        {
            return _dbContext.Contacts?.OrderByDescending(p => p.CreatedOn).ToList();
        }

        public Contact? GetByCondition(Func<Contact, bool> predicate)
        {
            return _dbContext.Contacts?.FirstOrDefault(predicate);;
        }

        public async Task<Contact>? AddAsync(Contact newInstance)
        {
            if(newInstance == null)
                throw new NullReferenceException("Could not add new Contact. Contact value cannot be null.");
            
            newInstance.CreatedOn = DateTime.UtcNow;
            
            await _dbContext.Contacts.AddAsync(newInstance);
            await _dbContext.SaveChangesAsync();
            return newInstance;
        }

        public async Task<bool> UpdateAsync(Contact updatedObj)
        {
            if (updatedObj == null)
                throw new NullReferenceException("Could not update the Contact. Contact value cannot be null.");
            
            _dbContext.Entry(updatedObj).State = EntityState.Modified;
            return (await _dbContext.SaveChangesAsync()) == 1 ? true : false ;
        

        }

        public async Task<bool> DeleteAsync(int id)
        {
            Contact? contactToBeDelete = await _dbContext.Contacts.FindAsync(id);
            if (contactToBeDelete == null)
                throw new NullReferenceException("Could not delete the Contact. Contact value cannot be null.");
            
            _dbContext.Contacts.Remove(contactToBeDelete);
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
