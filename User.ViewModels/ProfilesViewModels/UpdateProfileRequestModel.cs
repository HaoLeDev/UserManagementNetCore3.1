using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace User.ViewModels.ProfilesViewModels
{
    public class UpdateProfileRequestModel
    {
        [EmailAddress]
        public string Email { get; set; }

        public string UserName { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        public string MainPhotoUrl { get; set; }

        public string WebSite { get; set; }

        [MaxLength(4000)]
        public string Biography { get; set; }

        public bool Gender { get; set; }

        public bool IsPrivate { get; set; }
    }
}
