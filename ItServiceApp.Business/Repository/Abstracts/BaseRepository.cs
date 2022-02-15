﻿using ItServiceApp.Core.Entities.Abstracts;
using ItServiceApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ItServiceApp.Business.Repository.Abstracts
{
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected readonly MyContext Context;
        protected DbSet<TEntity> Table { get; }

        protected BaseRepository(MyContext context)
        {
            Context = context;
            Table = Context.Set<TEntity>();
        }
        public TEntity GetById(TKey id)
        {
            return Table.Find(id);
        }
        public TKey Insert (TEntity entity, bool isSaveLater = false)
        {
            Table.Add(entity);
            if (isSaveLater)
                Save();
            return entity.Id;
        }
        public int Delete(TKey id, bool isSaveLater = false)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(TKey id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
         return Context.SaveChanges();
        }

        public int Update(TEntity entity, bool isSaveLater = false)
        {
            throw new NotImplementedException();
        }
    }
}
