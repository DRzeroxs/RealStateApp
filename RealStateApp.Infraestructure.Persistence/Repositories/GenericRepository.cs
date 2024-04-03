using RealStateApp.Infraestructure.Persistence.Context;
using RealStateApp.Core.Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace RealStateApp.Infraestructure.Persistence.Repositories
{
    public class GenericRepository <Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _context.Set<Entity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(Entity entity, int id)
        {
            Entity entry = await _context.Set<Entity>().FindAsync(id);
            _context.Entry(entry).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            _context.Set<Entity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAll()
        {
               var result = await _context.Set<Entity>().ToListAsync();

            return result;
        }
        public virtual async Task<Entity> GetById(int id)
        {
            var entity = await _context.Set<Entity>().FindAsync(id);
            return entity;
        }
    }
}
