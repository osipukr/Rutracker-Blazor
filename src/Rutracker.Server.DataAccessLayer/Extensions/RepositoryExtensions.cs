﻿using System;
using System.Linq;
using Ardalis.GuardClauses;
using Rutracker.Server.DataAccessLayer.Entities.Base;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Specifications;

namespace Rutracker.Server.DataAccessLayer.Extensions
{
    public static class RepositoryExtensions
    {
        public static IQueryable<TEntity> ApplySpecification<TEntity, TPrimaryKey>(this IQueryable<TEntity> query,
            ISpecification<TEntity, TPrimaryKey> specification)
            where TEntity : BaseEntity<TPrimaryKey>
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            Guard.Against.Null(query, nameof(query));
            Guard.Against.Null(specification, nameof(specification));

            return SpecificationEvaluator<TEntity, TPrimaryKey>.Apply(query, specification);
        }
    }
}