using ElementFitness.DAL.Data;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;
using Microsoft.EntityFrameworkCore;

namespace ElementFitness.DAL.Repositories 
{
    public class EnquiryRepo: IEnquiryRepo, IDisposable
    {

        private readonly ApplicationDbContext _dbContext;

        public EnquiryRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Enquiry>? GetAll()
        {
            IEnumerable<Enquiry>? enquiries = _dbContext.Enquiries?.OrderByDescending(e => e.CreatedOn).ToList();
            foreach (Enquiry enquiry in enquiries)
            {
                enquiry.Contact =  _dbContext.Contacts.FirstOrDefault( c => c.ContactID == enquiry.ContactID);
            }
            return enquiries;

        }

        public Enquiry? GetByCondition(Func<Enquiry, bool> predicate)
        {
            return _dbContext.Enquiries?.FirstOrDefault(predicate);;
        }

        public async Task<Enquiry>? AddAsync(Enquiry newInstance)
        {
            if(newInstance == null)
                throw new NullReferenceException("Could not add new Enquiry. Enquiry value cannot be null.");
            
            newInstance.CreatedOn = DateTime.UtcNow;
            
            await _dbContext.Enquiries.AddAsync(newInstance);
            await _dbContext.SaveChangesAsync();
            return newInstance;
        }

        public async Task<bool> UpdateAsync(Enquiry updatedObj)
        {
            if (updatedObj == null)
                throw new NullReferenceException("Could not update the Enquiry. Enquiry value cannot be null.");
            
            _dbContext.Entry(updatedObj).State = EntityState.Modified;
            return (await _dbContext.SaveChangesAsync()) == 1 ? true : false ;
        

        }

        public async Task<bool> DeleteAsync(int id)
        {
            Enquiry? enquiryToBeDelete = await _dbContext.Enquiries.FindAsync(id);
            if (enquiryToBeDelete == null)
                throw new NullReferenceException("Could not delete the Enquiry. Enquiry value cannot be null.");
            
            _dbContext.Enquiries.Remove(enquiryToBeDelete);
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
