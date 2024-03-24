using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.IRepository
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAll();
        Task<Entity> GetById(int id);
        Task UpdateAsync(Entity entity, int id);
    }
}
