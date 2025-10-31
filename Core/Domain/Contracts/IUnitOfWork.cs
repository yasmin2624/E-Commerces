﻿using Domain.Entities.ProductModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        //  public IGenericRepository<Product, int> ProductRepository { get;  }
        IGenericRepository<TEntity, Tkey>GetRepository<TEntity,Tkey>() where TEntity : BaseEntity<Tkey>;
        Task<int>  SaveChangesAsync();
    }
}
