using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<IEntity,TKey> where IEntity: BaseEntity<TKey>
    {
        //Get All
        Task <IEnumerable<IEntity>> GetAllAsync();

        //Get By Id
        Task<IEntity?> GetByIdAsync(TKey id);

        //Add
        Task AddAsync(IEntity entity);

        //Update
        void Update(IEntity entity);

        //Delete
        void Delete(IEntity entity);
    }
}
