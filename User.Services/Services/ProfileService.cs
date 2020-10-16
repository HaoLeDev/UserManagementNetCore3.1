﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Data.Context;
using User.Models.Models;
using User.Services.IServices;
using User.ViewModels.ProfilesViewModels;

namespace User.Services.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserDbContext data;

        public ProfileService(UserDbContext data) => this.data = data;

        public async Task<ProfileServiceModel> ByUser(string userId, bool allInformation = false)
            => await this.data
                .Users
                .Where(u => u.Id == userId)
                .Select(u => allInformation
                    ? new PublicProfileServiceModel
                    {
                        Name = u.Profile.Name,
                        MainPhotoUrl = u.Profile.MainPhotoUrl,
                        WebSite = u.Profile.WebSite,
                        Biography = u.Profile.Biography,
                        Gender = u.Profile.Gender.ToString(),
                        IsPrivate = u.Profile.IsPrivate
                    }
                    : new ProfileServiceModel
                    {
                        Name = u.Profile.Name,
                        MainPhotoUrl = u.Profile.MainPhotoUrl,
                        IsPrivate = u.Profile.IsPrivate
                    })
                .FirstOrDefaultAsync();

        public async Task<Result> Update(
            string userId,
            string email,
            string userName,
            string name,
            string mainPhotoUrl,
            string webSite,
            string biography,
            bool gender,
            bool isPrivate)
        {
            var user = await this.data
                .Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(p => p.Id == userId);

            if (user == null)
            {
                return "User does not exist.";
            }

            if (user.Profile == null)
            {
                user.Profile = new Profile();
            }

            var result = await this.ChangeEmail(user, userId, email);
            if (result.Failure)
            {
                return result;
            }

            result = await this.ChangeUserName(user, userId, email);
            if (result.Failure)
            {
                return result;
            }

            this.ChangeProfile(
                user.Profile,
                name,
                mainPhotoUrl,
                webSite,
                biography,
                gender,
                isPrivate);

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsPublic(string userId)
            => await this.data
                .Profiles
                .Where(p => (p.UserId).ToString() == userId)
                .Select(p => !p.IsPrivate)
                .FirstOrDefaultAsync();

        private async Task<Result> ChangeEmail(Models.Models.AppUser user, string userId, string email)
        {
            if (!string.IsNullOrWhiteSpace(email) && user.Email != email)
            {
                var emailExists = await this.data
                    .Users
                    .AnyAsync(u => u.Id != userId && u.Email == email);

                if (emailExists)
                {
                    return "The provided e-mail is already taken.";
                }

                user.Email = email;
            }

            return true;
        }

        private async Task<Result> ChangeUserName(Models.Models.AppUser user, string userId, string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName) && user.UserName != userName)
            {
                var userNameExists = await this.data
                    .Users
                    .AnyAsync(u => u.Id != userId && u.UserName == userName);

                if (userNameExists)
                {
                    return "The provided user name is already taken.";
                }

                user.UserName = userName;
            }

            return true;
        }

        private void ChangeProfile(
            Profile profile,
            string name,
            string mainPhotoUrl,
            string webSite,
            string biography,
            bool gender,
            bool isPrivate)
        {
            if (profile.Name != name)
            {
                profile.Name = name;
            }

            if (profile.MainPhotoUrl != mainPhotoUrl)
            {
                profile.MainPhotoUrl = mainPhotoUrl;
            }

            if (profile.WebSite != webSite)
            {
                profile.WebSite = webSite;
            }

            if (profile.Biography != biography)
            {
                profile.Biography = biography;
            }

            if (profile.Gender != gender)
            {
                profile.Gender = gender;
            }

            if (profile.IsPrivate != isPrivate)
            {
                profile.IsPrivate = isPrivate;
            }
        }
    }
}
