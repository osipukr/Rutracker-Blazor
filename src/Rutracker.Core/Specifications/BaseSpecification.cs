﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Rutracker.Core.Entities;
using Rutracker.Core.Extensions;
using Rutracker.Core.Interfaces.Specifications;

namespace Rutracker.Core.Specifications
{
    public abstract class BaseSpecification<TEntity, TPrimaryKey> : ISpecification<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        public virtual Expression<Func<TEntity, bool>> Criteria { get; private set; }
        public virtual List<Expression<Func<TEntity, object>>> Includes { get; private set; }
        public virtual Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public virtual Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        public virtual int Take { get; private set; }
        public virtual int Skip { get; private set; }
        public virtual bool IsPagingEnabled { get; private set; }

        protected BaseSpecification(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;
            Includes = new List<Expression<Func<TEntity, object>>>();
        }

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) =>
            Includes.Add(includeExpression);

        protected void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression) => 
            OrderBy = orderByExpression;

        protected void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression) =>
            OrderByDescending = orderByDescendingExpression;

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected void ApplyAndCriteria(Expression<Func<TEntity, bool>> andExpression) =>
            Criteria = Criteria.And(andExpression);

        protected void ApplyOrCriteria(Expression<Func<TEntity, bool>> orExpression) =>
            Criteria = Criteria.Or(orExpression);
    }
}