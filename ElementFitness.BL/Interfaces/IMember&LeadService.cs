using ElementFitness.Models;

namespace ElementFitness.BL.Interfaces
{
    public interface IMemberAndLeadService
    {
        public IEnumerable<Contact>? GetAll();
        public Contact? GetById(int id);
        public Task<Contact>? AddAsync(Contact newInstance);
        public Task<bool> UpdateAsync(Contact updatedObj);
        public Task<bool> DeleteAsync(int id);
    }
}