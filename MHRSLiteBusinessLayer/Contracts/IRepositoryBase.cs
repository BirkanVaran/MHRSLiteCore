﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLiteBusinessLayer.Contracts
{
    public interface IRepositoryBase<T> where T : class, new()
    {
        IQueryable<T> GetAll(Expression/*Expressions = x => ile yazdığımız ifadeler */ <Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null);
        T GetById(int id);
        T GetFirstOrDefault(Expression/*Expressions = x => ile yazdığımız ifadeler */ <Func<T,bool>> filter=null, string includeProperties=null);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);


    }
}
