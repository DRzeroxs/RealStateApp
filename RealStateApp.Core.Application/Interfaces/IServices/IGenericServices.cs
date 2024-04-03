using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.IServices
{
    public interface IGenericServices<ViewModel, SaveViewModel, Entity>
    {
        Task<SaveViewModel> AddAsync(SaveViewModel vm);
        Task<List<ViewModel>> GetAllAsync();
        Task<ViewModel> GetByIdAsync(int Id);
        Task RemoveAsync(int Id);
        Task UpdateAsync(SaveViewModel vm, int ID);
    }
}
