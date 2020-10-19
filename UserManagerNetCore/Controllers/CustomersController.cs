using AspNetCore.CacheOutput;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CustomersController : ApiController
    {
        private readonly ICustomerService _customerService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, ICurrentUserService currentUserService, IMapper mapper)
        {
            this._customerService = customerService;
            this._currentUserService = currentUserService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route(nameof(Get))]
        [CacheOutput(ClientTimeSpan = 3600, ServerTimeSpan = 3600)]
        public async Task<ActionResult> Get(int page = 0, int pageSize = 10, string keyword = null)
        {
            try
            {
                int totalRecord = 0;
                var model = await _customerService.GetAll(keyword);
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(query).ToList();
                totalRecord = model.Count();

                var paginationSet = new PagedResponse<CustomerViewModel>(responseData, page, pageSize, totalRecord);
                return Ok(paginationSet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        //[CacheOutput(ClientTimeSpan = 3600, ServerTimeSpan = 3600)]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                var model = await _customerService.GetById(id);
                if (model == null)
                {
                    return NoContent();
                }
                var respone = _mapper.Map<Customer, CustomerViewModel>(model);
                return Ok(respone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route(nameof(Post))]
        [InvalidateCacheOutput(nameof(Get))]
        public async Task<ActionResult> Post(CustomerViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Customer model = new Customer();
                viewModel.CreatedBy = _currentUserService.GetId();
                viewModel.CreatedDate = DateTime.Now;
                viewModel.Deleted = false;
                model.UpdateCustomer(viewModel);
                var result = await _customerService.Add(model);
                var response = _mapper.Map<Customer, CustomerViewModel>(result);
                var resultFilter = new ResultFilter<CustomerViewModel>(response, StatusCodes.Status201Created, null);
                return Ok(resultFilter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route(nameof(Put))]
        [InvalidateCacheOutput(nameof(Get))]
        public async Task<ActionResult<CustomerViewModel>> Put(CustomerViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Customer model = new Customer();
                viewModel.UpdatedBy = _currentUserService.GetId();
                viewModel.UpdatedDate = DateTime.Now;
                viewModel.Deleted = false;
                model.UpdateCustomer(viewModel);
                var result = await _customerService.Update(model);
                var response = _mapper.Map<Customer, CustomerViewModel>(result);
                var resultFilter = new ResultFilter<CustomerViewModel>(response, StatusCodes.Status201Created, null);
                return Ok(resultFilter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [InvalidateCacheOutput(nameof(Get))]
        public async Task<ActionResult<CustomerViewModel>> Delete(string id)
        {
            try
            {
                var result = await _customerService.Delete(id);
                var response = _mapper.Map<Customer, CustomerViewModel>(result);
                var resultFilter = new ResultFilter<CustomerViewModel>(response, StatusCodes.Status200OK, null);
                return Ok(resultFilter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}