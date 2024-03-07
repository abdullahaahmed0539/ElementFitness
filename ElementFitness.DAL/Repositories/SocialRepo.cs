using ElementFitness.DAL.Data;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementFitness.DAL.Repositories
{
    public class SocialRepo : ISocialRepo, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public SocialRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Social> GetAll()
        {
            return _dbContext.Socials?.ToList();
        }

        public Social? GetByCondition(Func<Social, bool> predicate)
        {
            return _dbContext.Socials?.FirstOrDefault(predicate);
        }

        public async Task<bool> UpdateLink(Social updatedObj)
        {
            if (updatedObj == null)
                throw new NullReferenceException("Could not update the link. Values provided cannot be null.");

            _dbContext.Entry(updatedObj).State = EntityState.Modified;
            return (await _dbContext.SaveChangesAsync()) == 1 ? true : false;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
