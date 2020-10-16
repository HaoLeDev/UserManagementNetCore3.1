using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Data.Context;
using User.Services.IServices;
using User.ViewModels.SearchModels;

namespace User.Services.Services
{
    public class SearchService : ISearchService
    {
        private readonly UserDbContext data;

        public SearchService(UserDbContext data) => this.data = data;

        public async Task<IEnumerable<ProfileSearchServiceModel>> Profiles(string query)
            => await this.data
                .Users
                .Where(u => u.UserName.ToLower().Contains(query.ToLower()) ||
                    u.Profile.Name.ToLower().Contains(query.ToLower()))
                .Select(u => new ProfileSearchServiceModel
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    ProfilePhotoUrl = u.Profile.MainPhotoUrl
                })
                .ToListAsync();
    }
}
