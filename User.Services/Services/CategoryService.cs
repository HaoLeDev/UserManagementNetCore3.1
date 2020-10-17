using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.Repositories;
using User.Models.Models;

namespace User.Services.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<Category>> GetAll(string keyword);
        Task<Category> Add(Category entity);
        Task<Category> GetById(string id);
        Task<Category> Delete(string id);
        Task<Category> Update(Category entity);
    }
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<Category> Add(Category entity)
        {
            return await _categoryRepository.Add(entity);
        }

        public async Task<Category> Delete(string id)
        {
            return await _categoryRepository.Delete(id);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<IEnumerable<Category>> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return await _categoryRepository.GetAllByCondition(x => x.CategoryName.Contains( keyword));
            return await _categoryRepository.GetAll();
        }

        public async Task<Category> GetById(string id)
        {
            return await _categoryRepository.GetById(id);
        }


        public async Task<Category> Update(Category entity)
        {
            return await _categoryRepository.Update(entity);
        }
    }
}
