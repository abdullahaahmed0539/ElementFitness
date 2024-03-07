using ElementFitness.BL.Interfaces;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementFitness.BL.Services
{
    public class SocialService : ISocialService
    {
        private readonly ISocialRepo _socialRepo;
        public SocialService(ISocialRepo socialRepo)
        {
            _socialRepo = socialRepo;
        }
        public async Task<bool> UpdateLinkAsync(Social updatedObj)
        {
            return await _socialRepo.UpdateLink(updatedObj);
        }

        public IEnumerable<Social>? GetAll()
        {
            return _socialRepo.GetAll();
        }
        public Social? GetByName(string platformName)
        {
            return _socialRepo.GetByCondition(t => t.SocialPlatform == platformName);
        }
    }
}
