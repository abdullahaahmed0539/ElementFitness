using ElementFitness.Models;

namespace ElementFitness.BL.Interfaces
{
    public interface IEnquiryService
    {
        void SendEnquiryEmail(Enquiry enquiry);
        public IEnumerable<Enquiry>? GetAll();
        public Enquiry? GetById(int id);
        public Task<Enquiry>? AddAsync(Enquiry newInstance);
        public Task<bool> UpdateAsync(Enquiry updatedObj);
        public Task<bool> DeleteAsync(int id);
    }
}