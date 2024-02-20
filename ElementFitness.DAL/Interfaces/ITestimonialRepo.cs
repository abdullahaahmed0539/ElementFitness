using ElementFitness.Models;

namespace ElementFitness.DAL.Interfaces
{
    public interface ITestimonialRepo: IBaseRepository<Testimonial>
    {
        public IEnumerable<Testimonial> GetNRecords(int n);
    }
}