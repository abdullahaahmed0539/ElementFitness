using ElementFitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementFitness.BL.Interfaces
{
    public interface ISocialService
    {
        Task<bool> UpdateLinkAsync(Social updatedObj);
        IEnumerable<Social>? GetAll();
        Social? GetByName(string platformName);
    }
}
