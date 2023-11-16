using ElementFitness.DAL.Data;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;
using Microsoft.EntityFrameworkCore;

namespace ElementFitness.DAL.Repositories 
{
    public class JobApplicantRepo: IJobApplicantRepo, IDisposable
    {

        private readonly ApplicationDbContext _dbContext;

        public JobApplicantRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<JobApplicant>? GetAll()
        {
            IEnumerable<JobApplicant>? jobApplicants = _dbContext.JobApplicants?.OrderByDescending(e => e.CreatedOn).ToList();
            foreach (JobApplicant jobApplicant in jobApplicants)
            {
                    jobApplicant.Job = _dbContext.Jobs.FirstOrDefault( j => j.JobID == jobApplicant.JobID);
            }
            return jobApplicants;
        }

        public JobApplicant? GetByCondition(Func<JobApplicant, bool> predicate)
        {
            JobApplicant jobApplicant = _dbContext.JobApplicants?.FirstOrDefault(predicate);
            jobApplicant.Job = _dbContext.Jobs.FirstOrDefault(ja => ja.JobID == jobApplicant.JobID);
            return jobApplicant;
        }

        public async Task<JobApplicant>? AddAsync(JobApplicant newInstance)
        {
            if(newInstance == null)
                throw new NullReferenceException("Could not add new Job Applicant. Job Applicant value cannot be null.");
            
            newInstance.CreatedOn = DateTime.UtcNow;
            
            await _dbContext.JobApplicants.AddAsync(newInstance);
            await _dbContext.SaveChangesAsync();
            return newInstance;
        }

        public async Task<bool> UpdateAsync(JobApplicant updatedObj)
        {
            if (updatedObj == null)
                throw new NullReferenceException("Could not update the Job Applicant. Enquiry value cannot be null.");
            
            _dbContext.Entry(updatedObj).State = EntityState.Modified;
            return (await _dbContext.SaveChangesAsync()) == 1 ? true : false ;
        

        }

        public async Task<bool> DeleteAsync(int id)
        {
            JobApplicant? jobApplicantToBeDelete = await _dbContext.JobApplicants.FindAsync(id);
            if (jobApplicantToBeDelete == null)
                throw new NullReferenceException("Could not delete the Enquiry. Enquiry value cannot be null.");
            
            _dbContext.JobApplicants.Remove(jobApplicantToBeDelete);
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
