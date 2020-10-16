using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.ViewModels.ProfilesViewModels;

namespace User.Services.IServices
{
    public interface IProfileService
    {
        Task<ProfileServiceModel> ByUser(string userId, bool allInformation = false);

        Task<Result> Update(
            string userId,
            string email,
            string userName,
            string name,
            string mainPhotoUrl,
            string webSite,
            string biography,
            bool gender,
            bool isPrivate);

        Task<bool> IsPublic(string userId);
    }
}
