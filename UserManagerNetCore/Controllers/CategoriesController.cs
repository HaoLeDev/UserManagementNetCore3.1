using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Commons.Services;
using User.Models.Models;
using User.Services.Services;
using User.ViewModels.ViewModels;
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
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> Get()
        {
            try
            {
                var model = await _categoryService.GetAll();
                var query = model.OrderByDescending(x => x.CreatedDate);
                var responseData = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(query).ToList();
                return responseData;
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
        public async Task<ActionResult<CategoryViewModel>> Post(Category model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.CreatedBy = _currentUserService.GetId();
                model.CreatedDate = DateTime.Now;
                model.Deleted = false;
                var result= await _categoryService.Add(model);
                
                return CreatedAtAction("Get", new { id = model.Id }, model);
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
                return CreatedAtAction("Get", new { id = viewModel.Id }, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<ActionResult<CategoryViewModel>> Delete(string id)
        {
            try
            {
                var result = await _categoryService.Delete(id);
                var respone = _mapper.Map<Category, CategoryViewModel>(result);
                return respone;
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
