using ElementFitness.DAL.Data;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;
using Microsoft.EntityFrameworkCore;

namespace ElementFitness.DAL.Repositories 
{
    public class TestimonialRepo: ITestimonialRepo, IDisposable
    {

        private readonly ApplicationDbContext _dbContext;

        public TestimonialRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Testimonial>? GetAll()
        {
            return _dbContext.Testimonials?.OrderByDescending(p => p.CreatedOn).ToList();
        }

        public Testimonial? GetByCondition(Func<Testimonial, bool> predicate)
        {
            return _dbContext.Testimonials?.FirstOrDefault(predicate);;
        }

        public IEnumerable<Testimonial>? GetNRecords(int n)
        {
            return _dbContext.Testimonials?.OrderByDescending(p => p.CreatedOn).Take(n).ToList();
        }

        public async Task<Testimonial>? AddAsync(Testimonial newInstance)
        {
            if(newInstance == null)
                throw new NullReferenceException("Could not add new Testimonial. Testimonial value cannot be null.");
            
            newInstance.CreatedOn = DateTime.UtcNow;
            
            await _dbContext.Testimonials.AddAsync(newInstance);
            await _dbContext.SaveChangesAsync();
            return newInstance;
        }

        public async Task<bool> UpdateAsync(Testimonial updatedObj)
        {
            if (updatedObj == null)
                throw new NullReferenceException("Could not update the Testimonial. Testimonial value cannot be null.");
            
            _dbContext.Entry(updatedObj).State = EntityState.Modified;
            return (await _dbContext.SaveChangesAsync()) == 1 ? true : false ;
        

        }

        public async Task<bool> DeleteAsync(int id)
        {
            Testimonial? testimonialToBeDelete = await _dbContext.Testimonials.FindAsync(id);
            if (testimonialToBeDelete == null)
                throw new NullReferenceException("Could not delete the Offer. Offer value cannot be null.");
            
            _dbContext.Testimonials.Remove(testimonialToBeDelete);
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
