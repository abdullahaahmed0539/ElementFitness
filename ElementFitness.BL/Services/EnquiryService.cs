using ElementFitness.BL.Interfaces;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;

namespace ElementFitness.BL.Services
{
    public class EnquiryService: IEnquiryService
    {
        private readonly IEnquiryRepo _enquiryRepo;

        public EnquiryService(IEnquiryRepo enquiryRepo)
        {
            _enquiryRepo = enquiryRepo;
        }

        public async Task<Enquiry>? AddAsync(Enquiry newInstance)
        {
            return await _enquiryRepo.AddAsync(newInstance);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _enquiryRepo.DeleteAsync(id);
        }

        public IEnumerable<Enquiry>? GetAll()
        {
            return _enquiryRepo.GetAll();
        }

        public Enquiry? GetById(int id)
        {
            return _enquiryRepo.GetByCondition(e => e.EnquiryID == id);
        }

        public async Task<bool> UpdateAsync(Enquiry updatedObj)
        {
            return await _enquiryRepo.UpdateAsync(updatedObj);
        }
    }
}