﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubcategoryService(ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subcategory>> ListAsync(int categoryId)
        {
            Guard.Against.LessOne(categoryId, $"The {nameof(categoryId)} is less than 1.");

            var subcategories = await _subcategoryRepository.GetAll(x => x.CategoryId == categoryId).ToListAsync();

            Guard.Against.NullNotFound(subcategories, "The subcategories not found.");

            return subcategories;
        }

        public async Task<Subcategory> FindAsync(int id)
        {
            Guard.Against.LessOne(id, $"The {nameof(id)} is less than 1.");

            var subcategory = await _subcategoryRepository.GetAsync(id);

            Guard.Against.NullNotFound(subcategory, $"The subcategory with id '{id}' not found.");

            return subcategory;
        }

        public async Task<Subcategory> AddAsync(Subcategory subcategory)
        {
            await ThrowIfInvalidSubcategory(subcategory);

            if (!await _categoryRepository.ExistAsync(subcategory.CategoryId))
            {
                throw new RutrackerException($"The category with id '{subcategory.CategoryId}' not found.", ExceptionEventType.NotValidParameters);
            }

            await _subcategoryRepository.AddAsync(subcategory);
            await _unitOfWork.CompleteAsync();

            return subcategory;
        }

        public async Task<Subcategory> UpdateAsync(int id, Subcategory subcategory)
        {
            await ThrowIfInvalidSubcategory(subcategory);

            var result = await FindAsync(id);

            result.Name = subcategory.Name;

            _subcategoryRepository.Update(result);
            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<Subcategory> DeleteAsync(int id)
        {
            var subcategory = await FindAsync(id);

            _subcategoryRepository.Remove(subcategory);

            await _unitOfWork.CompleteAsync();

            return subcategory;
        }

        private async Task ThrowIfInvalidSubcategory(Subcategory subcategory)
        {
            Guard.Against.NullNotValid(subcategory, "Invalid subcategory model.");
            Guard.Against.NullOrWhiteSpace(subcategory.Name, message: "The subcategory must contain a name.");

            if (await _subcategoryRepository.ExistAsync(x => x.Name == subcategory.Name))
            {
                throw new RutrackerException($"Subcategory with name '{subcategory.Name}' already exists.", ExceptionEventType.NotValidParameters);
            }
        }
    }
}