using ElementFitness.BL.Interfaces;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;

namespace ElementFitness.BL.Services
{
    public class TestimonialService: ITestimonialService
    {
        private readonly ITestimonialRepo _testimonialRepo;

        public TestimonialService(ITestimonialRepo testimonialRepo)
        {
            _testimonialRepo = testimonialRepo;
        }

        public async Task<Testimonial>? AddAsync(Testimonial newInstance)
        {
            return await _testimonialRepo.AddAsync(newInstance);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _testimonialRepo.DeleteAsync(id);
        }

        public IEnumerable<Testimonial>? GetAll()
        {
            return _testimonialRepo.GetAll();
        }

        public IEnumerable<Testimonial>? GetNRecords(int numberOfRecords)
        {
            return _testimonialRepo.GetAll().Take(numberOfRecords);
        }

        public Testimonial? GetById(int id)
        {
            return _testimonialRepo.GetByCondition(t => t.TestimonialID == id);
        }

        public Testimonial? GetByName(string testimonialName)
        {
            return _testimonialRepo.GetByCondition(t => t.Name == testimonialName);
        }

        public async Task<bool> UpdateAsync(Testimonial updatedObj)
        {
            return await _testimonialRepo.UpdateAsync(updatedObj);
        }
    }
}