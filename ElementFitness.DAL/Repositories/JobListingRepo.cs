using System.Diagnostics;
using ElementFitness.DAL.Data;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;
using Microsoft.EntityFrameworkCore;

namespace ElementFitness.DAL.Repositories 
{
    public class JobListingRepo: IJobListingRepo, IDisposable
    {

        private readonly ApplicationDbContext _dbContext;

        public JobListingRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Job>? GetAll()
        {
            return _dbContext.Jobs?.OrderByDescending(p => p.CreatedOn).ToList();
        }

         public IEnumerable<Job>? GetAllActive()
        {
            return _dbContext.Jobs?.Where(j => j.Active==true).OrderByDescending(p => p.CreatedOn).ToList();
        }

        public Job? GetByCondition(Func<Job, bool> predicate)
        {
            return _dbContext.Jobs?.FirstOrDefault(predicate);;
        }

        public async Task<Job>? AddAsync(Job newInstance)
        {
            if(newInstance == null)
                throw new NullReferenceException("Could not add new Job. Job value cannot be null.");
            
            newInstance.CreatedOn = DateTime.UtcNow;
            newInstance.Active = true;
            
            await _dbContext.Jobs.AddAsync(newInstance);
            await _dbContext.SaveChangesAsync();
            return newInstance;
        }

        public async Task<bool> UpdateAsync(Job updatedObj)
        {
            if (updatedObj == null)
                throw new NullReferenceException("Could not update the Job. Job value cannot be null.");
            
            _dbContext.Entry(updatedObj).State = EntityState.Modified;
            return (await _dbContext.SaveChangesAsync()) == 1 ? true : false ;
        

        }

        public async Task<bool> DeleteAsync(int id)
        {
            Job? jobToBeDelete = await _dbContext.Jobs.FindAsync(id);
            if (jobToBeDelete == null)
                throw new NullReferenceException("Could not delete the Job. Job value cannot be null.");
            
            IEnumerable<JobApplicant> jobApplicants = _dbContext.JobApplicants.Where( ja => ja.JobID == id).ToList();
            foreach(JobApplicant applicant in jobApplicants)
            {
                applicant.JobID = 0;
            }



            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                _dbContext.JobApplicants.UpdateRange(jobApplicants);
                await _dbContext.SaveChangesAsync();

                _dbContext.Jobs.Remove(jobToBeDelete);
                await _dbContext.SaveChangesAsync();

                transaction.Commit();
                return true;
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                return false;
            }
            
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
