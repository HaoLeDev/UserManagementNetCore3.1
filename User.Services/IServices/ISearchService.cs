using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.ViewModels.SearchModels;

namespace User.Services.IServices
{
    public interface ISearchService
    {
        Task<IEnumerable<ProfileSearchServiceModel>> Profiles(string query);
    }
}
