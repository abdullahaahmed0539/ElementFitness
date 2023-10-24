using ElementFitness.Models;

namespace ElementFitness.BL.Interfaces
{
    public interface IOfferService
    {
        public IEnumerable<Offer>? GetAll();
        public Offer? GetById(int id);
        public Offer? GetByName(string programName);
        public Task<Offer>? AddAsync(Offer newInstance);
        public Task<bool> UpdateAsync(Offer updatedObj);
        public Task<bool> DeleteAsync(int id);
    }
}