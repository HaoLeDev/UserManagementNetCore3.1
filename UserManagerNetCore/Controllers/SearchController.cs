using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Services.IServices;
using User.ViewModels.SearchModels;
using UserManagerNetCore.Infrastructure.Core;

namespace UserManagerNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ApiController
    {
        private readonly ISearchService search;

        public SearchController(ISearchService search) => this.search = search;

        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(Profiles))]
        public async Task<IEnumerable<ProfileSearchServiceModel>> Profiles(string query)
            => await this.search.Profiles(query);
    }
}
