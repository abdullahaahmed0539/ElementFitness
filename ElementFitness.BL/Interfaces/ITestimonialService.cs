using ElementFitness.Models;

namespace ElementFitness.BL.Interfaces
{
    public interface ITestimonialService
    {
        public IEnumerable<Testimonial>? GetAll();
        public Testimonial? GetById(int id);
        public IEnumerable<Testimonial>? GetNRecords(int n);
        public Testimonial? GetByName(string offerName);
        public Task<Testimonial>? AddAsync(Testimonial newInstance);
        public Task<bool> UpdateAsync(Testimonial updatedObj);
        public Task<bool> DeleteAsync(int id);
    }
}