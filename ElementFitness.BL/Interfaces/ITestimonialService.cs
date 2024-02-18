using ElementFitness.Models;

namespace ElementFitness.BL.Interfaces
{
    public interface ITestimonialService
    {
        IEnumerable<Testimonial>? GetAll();
        Testimonial? GetById(int id);
        Testimonial? GetByName(string offerName);
        Task<Testimonial>? AddAsync(Testimonial newInstance);
        Task<bool> UpdateAsync(Testimonial updatedObj);
        Task<bool> DeleteAsync(int id);
        IEnumerable<Testimonial>? GetNRecords(int numberOfRecords);
    }
}