using ElementFitness.Models;

namespace ElementFitness.BL.Interfaces
{
    public interface IEnquiryService
    {
        public IEnumerable<Enquiry>? GetAll();
        public Enquiry? GetById(int id);
        public Task<Enquiry>? AddAsync(Enquiry newInstance);
        public Task<bool> UpdateAsync(Enquiry updatedObj);
        public Task<bool> DeleteAsync(int id);
    }
}