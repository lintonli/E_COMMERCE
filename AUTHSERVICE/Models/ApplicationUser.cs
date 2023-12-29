﻿using Microsoft.AspNetCore.Identity;

namespace AUTHSERVICE.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}
