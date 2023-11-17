using ElementFitness.BL.Interfaces;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;

namespace ElementFitness.BL.Services
{
    public class MemberAndLeadService: IMemberAndLeadService
    {
        private readonly IMemberAndLeadRepo _mnlRepo;

        public MemberAndLeadService(IMemberAndLeadRepo mnlRepo)
        {
            _mnlRepo = mnlRepo;
        }

        public async Task<Contact>? AddAsync(Contact newInstance)
        {
            return await _mnlRepo.AddAsync(newInstance);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _mnlRepo.DeleteAsync(id);
        }

        public IEnumerable<Contact>? GetAll()
        {
            return _mnlRepo.GetAll();
        }

        public Contact? GetById(int id)
        {
            return _mnlRepo.GetByCondition(c => c.ContactID == id);
        }

        public async Task<bool> UpdateAsync(Contact updatedObj)
        {
            return await _mnlRepo.UpdateAsync(updatedObj);
        }
    }
}