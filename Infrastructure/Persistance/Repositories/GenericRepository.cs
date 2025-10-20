using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Specifications;


namespace Persistance.Repositories
{
    public class GenericRepository<TEntity, Tkey>(StoreContext dbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity)
         =>  await dbContext.Set<TEntity>().AddAsync(entity);


        public async Task<IEnumerable<TEntity>> GetAllAsync()
          =>  await dbContext.Set<TEntity>().ToListAsync();
        

        public async Task<TEntity?> GetByIdAsync(Tkey id)
        =>  await dbContext.Set<TEntity>().FindAsync(id).AsTask();


        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);

        #region With Specifications

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specification)
        {
            return await SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), specification).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specification)
        {
            return await SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), specification).FirstOrDefaultAsync();

               
        } 
        #endregion
    }
}
