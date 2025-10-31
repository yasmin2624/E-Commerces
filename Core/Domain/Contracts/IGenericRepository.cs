using Domain.Entities.ProductModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<IEntity,Tkey> where IEntity: BaseEntity<Tkey>
    {
        //Get All
        Task <IEnumerable<IEntity>> GetAllAsync();

        //Get By Id
        Task<IEntity?> GetByIdAsync(Tkey id);

        //Add
        Task AddAsync(IEntity entity);

        //Update
        void Update(IEntity entity);

        //Delete
        void Delete(IEntity entity);

        #region With Specifications
        Task<IEnumerable<IEntity>> GetAllAsync(ISpecifications <IEntity,Tkey> specification);
        Task<IEntity?> GetByIdAsync(ISpecifications<IEntity, Tkey> specification);
        #endregion



    }
}
