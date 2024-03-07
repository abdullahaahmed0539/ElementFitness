using ElementFitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementFitness.DAL.Interfaces
{
    public interface ISocialRepo
    {
        IEnumerable<Social> GetAll();
        Task<bool> UpdateLink(Social updatedObj);
        Social? GetByCondition(Func<Social, bool> predicate);
    }
}
