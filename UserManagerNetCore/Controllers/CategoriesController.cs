using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Commons.Services;
using User.Models.Models;
using User.Services.Services;

namespace UserManagerNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly ICurrentUserService _currentUserService;

        public CategoriesController(ICategoryService categoryService, ICurrentUserService currentUserService)
        {
            this._categoryService = categoryService;
            this._currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var model = await _categoryService.GetAll();
            var query = model.OrderByDescending(x => x.CreatedDate);
            return query.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post(Category movie)
        {
            
            movie.CreatedBy = _currentUserService.GetId();
            movie.CreatedDate = DateTime.Now;
            movie.Deleted = false;
            await _categoryService.Add(movie);
            //return CreatedAtAction("Get", new { id = movie.Id }, movie);
            return movie;
        }
    }
}
