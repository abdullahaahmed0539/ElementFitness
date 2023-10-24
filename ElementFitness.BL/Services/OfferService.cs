using ElementFitness.BL.Interfaces;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;

namespace ElementFitness.BL.Services
{
    public class OfferService: IOfferService
    {
        private readonly IOfferRepo _offerRepo;

        public OfferService(IOfferRepo offerRepo)
        {
            _offerRepo = offerRepo;
        }

        public async Task<Offer>? AddAsync(Offer newInstance)
        {
            return await _offerRepo.AddAsync(newInstance);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _offerRepo.DeleteAsync(id);
        }

        public IEnumerable<Offer>? GetAll()
        {
            return _offerRepo.GetAll();
        }

        public Offer? GetById(int id)
        {
            return _offerRepo.GetByCondition(o => o.OfferID == id);
        }

        public Offer? GetByName(string offerName)
        {
            return _offerRepo.GetByCondition(o => o.Name == offerName);
        }

        public async Task<bool> UpdateAsync(Offer updatedObj)
        {
            return await _offerRepo.UpdateAsync(updatedObj);
        }
    }
}