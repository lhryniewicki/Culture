using System;
using System.Collections.Generic;
using System.Text;
using Culture.Models;

namespace Culture.Contracts.DTOs
{
   public  class UserDetailsDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid OwnerId { get; set; }
        public string AvatarPath { get; set; }
        public UserConfigurationDto UserConfiguration { get; set; }
        public UserDetailsDto(AppUser user)
        {
            AvatarPath = user.AvatarPath;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            OwnerId = user.Id;
        }
    }
}
