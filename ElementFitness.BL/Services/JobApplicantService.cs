using ElementFitness.BL.Interfaces;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;

namespace ElementFitness.BL.Services
{
    public class JobApplicantService: IJobApplicantService
    {
        private readonly IJobApplicantRepo _jobApplicantRepo;

        public JobApplicantService(IJobApplicantRepo jobApplicantRepo)
        {
            _jobApplicantRepo = jobApplicantRepo;
        }

        public async Task<JobApplicant>? AddAsync(JobApplicant newInstance)
        {
            return await _jobApplicantRepo.AddAsync(newInstance);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _jobApplicantRepo.DeleteAsync(id);
        }

        public IEnumerable<JobApplicant>? GetAll()
        {
            return _jobApplicantRepo.GetAll();
        }

        public JobApplicant? GetById(int id)
        {
            return _jobApplicantRepo.GetByCondition(j => j.JobApplicantID == id);
        }

        public async Task<bool> UpdateAsync(JobApplicant updatedObj)
        {
            return await _jobApplicantRepo.UpdateAsync(updatedObj);
        }
    }
}