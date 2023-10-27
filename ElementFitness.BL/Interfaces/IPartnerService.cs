using ElementFitness.Models;

namespace ElementFitness.BL.Interfaces
{
    public interface IPartnerService
    {
        public IEnumerable<Partner>? GetAll();
        public Partner? GetById(int id);
        public Partner? GetByName(string partnerName);
        public Task<Partner>? AddAsync(Partner newInstance);
        public Task<bool> UpdateAsync(Partner updatedObj);
        public Task<bool> DeleteAsync(int id);
    }
}