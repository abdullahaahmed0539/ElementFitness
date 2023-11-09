using ElementFitness.Models;

namespace ElementFitness.DAL.Interfaces
{
    public interface IJobListingRepo: IBaseRepository<Job>
    {
        public IEnumerable<Job>? GetAllActive();
        
    }
}