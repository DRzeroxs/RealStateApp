using AutoMapper;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class GenericServices <ViewModel, SaveViewModel, Entity> : IGenericServices<ViewModel, SaveViewModel, Entity>
        where ViewModel : class where SaveViewModel : class where Entity : class
    {
        private IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;
        public GenericServices(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaveViewModel> AddAsync(SaveViewModel vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);

            entity = await _repository.AddAsync(entity);

            SaveViewModel postVm = _mapper.Map<SaveViewModel>(entity);

            return postVm;
        }

        public virtual async Task UpdateAsync(SaveViewModel vm, int ID)
        {
            Entity entity = _mapper.Map<Entity>(vm);

            await _repository.UpdateAsync(entity, ID);
        }

        public async Task RemoveAsync(int Id)
        {
            Entity entity = await _repository.GetById(Id);

            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<List<ViewModel>> GetAllAsync()
        {
            var Lista = await _repository.GetAll();

            return _mapper.Map<List<ViewModel>>(Lista);
        }

        public async Task<ViewModel> GetByIdAsync(int Id)
        {
            Entity entity = await _repository.GetById(Id);

            ViewModel postVm = _mapper.Map<ViewModel>(entity);

            return postVm;
        }
    }
}
