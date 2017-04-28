﻿using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace DinePlan.DineConnect.EntityFramework.Repositories
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public abstract class DineConnectRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<DineConnectDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected DineConnectRepositoryBase(IDbContextProvider<DineConnectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add your common methods for all repositories
    }

    /// <summary>
    /// Base class for custom repositories of the application.
    /// This is a shortcut of <see cref="DineConnectRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class DineConnectRepositoryBase<TEntity> : DineConnectRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected DineConnectRepositoryBase(IDbContextProvider<DineConnectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)!!!
    }
}
