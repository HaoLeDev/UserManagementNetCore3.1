using System;
using System.Collections.Generic;
using System.Text;

namespace User.ViewModels.ProfilesViewModels
{
    public class PublicProfileServiceModel : ProfileServiceModel
    {
        public string WebSite { get; set; }

        public string Biography { get; set; }

        public string Gender { get; set; }
    }
}
