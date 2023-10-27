using ElementFitness.BL.Interfaces;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;

namespace ElementFitness.BL.Services
{
    public class PartnerService: IPartnerService
    {
        private readonly IPartnerRepo _partnerRepo;

        public PartnerService(IPartnerRepo partnerRepo)
        {
            _partnerRepo = partnerRepo;
        }

        public async Task<Partner>? AddAsync(Partner newInstance)
        {
            return await _partnerRepo.AddAsync(newInstance);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _partnerRepo.DeleteAsync(id);
        }

        public IEnumerable<Partner>? GetAll()
        {
            return _partnerRepo.GetAll();
        }

        public Partner? GetById(int id)
        {
            return _partnerRepo.GetByCondition(p => p.PartnerID == id);
        }

        public Partner? GetByName(string partnerName)
        {
            return _partnerRepo.GetByCondition(p => p.Name == partnerName);
        }

        public async Task<bool> UpdateAsync(Partner updatedObj)
        {
            return await _partnerRepo.UpdateAsync(updatedObj);
        }
    }
}