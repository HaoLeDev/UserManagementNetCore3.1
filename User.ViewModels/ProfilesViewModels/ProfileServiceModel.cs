using System;
using System.Collections.Generic;
using System.Text;

namespace User.ViewModels.ProfilesViewModels
{
    public class ProfileServiceModel
    {
        public string Name { get; set; }

        public string MainPhotoUrl { get; set; }

        public bool IsPrivate { get; set; }
    }
}
