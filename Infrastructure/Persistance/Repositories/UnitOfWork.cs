using Domain.Contracts;
using Domain.Entities.ProductModules;
using Persistance.Data;


namespace Persistance.Repositories
{
    public class UnitOfWork(StoreContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> Repositories = [];
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            //1-Get type Name
            var TypeName = typeof(TEntity).Name; //=> EX: Product

            //2-Dic<String,object> string [Key] => [Name Of Type] __ object [Value] => Object From Generic Repository
            //if (Repositories.ContainsKey(TypeName))
            //{
            //    return (IGenericRepository<TEntity, Tkey>)Repositories[TypeName];
            //}
            if (Repositories.TryGetValue(TypeName,out object?value))
                return (IGenericRepository<TEntity, Tkey>)value;
            
            else
            {
                //Create Object
                var Repo = new GenericRepository<TEntity, Tkey>(dbContext);

                //Store Object In Dictionary

               // Repositories.Add(TypeName, Repo);  [or ]
               Repositories["TypeName"] = Repo;

                //Return Object
                return Repo;
            }

        }

        public async Task<int> SaveChangesAsync()
        => await dbContext.SaveChangesAsync();
        
    }
}
