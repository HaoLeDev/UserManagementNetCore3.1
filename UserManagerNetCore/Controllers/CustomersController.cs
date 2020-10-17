using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<CustomerViewModel>> Get(string id)
        {
            try
            {
                var model = await _customerService.GetById(id);
                if (model == null)
                {
                    return NoContent();
                }
                var respone = _mapper.Map<Customer, CustomerViewModel>(model);
                return respone;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        [Route(nameof(Post))]
        public async Task<ActionResult<CustomerViewModel>> Post(Customer model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                for(int i=1; i <= 10000; i++)
                {

                     model = new Customer();
                    model.FirstName = "Khách hàng ";
                    model.LastName = i.ToString();
                    if (i % 2 == 0)
                        model.Gender = false;
                    else
                        model.Gender = true;
                    model.CreatedBy = _currentUserService.GetId();
                    model.CreatedDate = DateTime.Now;
                    model.Deleted = false;
                    var result = await _customerService.Add(model);
                }

                //model.CreatedBy = _currentUserService.GetId();
                //model.CreatedDate = DateTime.Now;
                //model.Deleted = false;
                //var result = await _customerService.Add(model);
                return Ok("Thêm mới thành công.");
                //return CreatedAtAction("Get", new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPut]
        [Route(nameof(Put))]
        public async Task<ActionResult<CustomerViewModel>> Put(CustomerViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // var model = _customerService.GetById(viewModel.Id.ToString());

                Customer model = new Customer();
                viewModel.UpdatedBy = _currentUserService.GetId();
                viewModel.UpdatedDate = DateTime.Now;
                viewModel.Deleted = false;
                model.UpdateCustomer(viewModel);
                var result = await _customerService.Update(model);
                return CreatedAtAction("Get", new { id = viewModel.Id }, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<ActionResult<CustomerViewModel>> Delete(string id)
        {
            try
            {
                var result = await _customerService.Delete(id);
                var respone = _mapper.Map<Customer, CustomerViewModel>(result);
                return respone;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
