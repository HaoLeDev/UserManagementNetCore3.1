using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Commons.Services;
using User.Models.Models;
using User.Services.Services;
using User.ViewModels.ViewModels;
using UserManagerNetCore.Infrastructure.Core;
using UserManagerNetCore.Infrastructure.Filters;

namespace UserManagerNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, ICurrentUserService currentUserService, IMapper mapper)
        {
            this._categoryService = categoryService;
            this._currentUserService = currentUserService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route(nameof(Get))]
        public async Task<ActionResult> Get(int page=0, int pageSize=10, string keyword=null)
        {
            try
            {
                int totalRecord = 0;
                var model = await _categoryService.GetAll(keyword);
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page*pageSize).Take(pageSize);
                var responseData = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(query).ToList();
                totalRecord = model.Count();

                var paginationSet = new PagedResponse<CategoryViewModel>(responseData, page, pageSize, totalRecord);
                return Ok(paginationSet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult<CategoryViewModel>> Get(string id)
        {
            try
            {
                var model = await _categoryService.GetById(id);
                if (model == null)
                {
                    return NoContent();
                }
                var respone = _mapper.Map<Category, CategoryViewModel>(model);
                return respone;
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpPost]
        [Route(nameof(Post))]
        public async Task<ActionResult<CategoryViewModel>> Post(CategoryViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Category model = new Category();
                viewModel.CreatedBy = _currentUserService.GetId();
                viewModel.CreatedDate = DateTime.Now;
                viewModel.Deleted = false;
                model.UpdateCategory(viewModel);
                var result = await _categoryService.Add(model);
                var response = _mapper.Map<Category, CategoryViewModel>(result);
                var resultFilter = new ResultFilter<CategoryViewModel>(response, StatusCodes.Status201Created, null);
                return Ok(resultFilter);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPut]
        [Route(nameof(Put))]
        public async Task<ActionResult<CategoryViewModel>> Put(CategoryViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // var model = _categoryService.GetById(viewModel.Id.ToString());

                Category model = new Category();
                viewModel.UpdatedBy = _currentUserService.GetId();
                viewModel.UpdatedDate = DateTime.Now;
                viewModel.Deleted = false;
                model.UpdateCategory(viewModel);
                var result = await _categoryService.Update(model);
                var response = _mapper.Map<Category, CategoryViewModel>(result);
                var resultFilter = new ResultFilter<CategoryViewModel>(response, StatusCodes.Status201Created, null);
                return Ok(resultFilter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var result = await _categoryService.Delete(id);
                var respone = _mapper.Map<Category, CategoryViewModel>(result);
                var resultFilter = new ResultFilter<CategoryViewModel>(respone, StatusCodes.Status200OK, null);
                return Ok(resultFilter);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
