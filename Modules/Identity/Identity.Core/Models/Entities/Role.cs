﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Identity.Core.Entities
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }

        public virtual ICollection<RoleClaim> RoleClaims { get; set; }

        public Role()
            : base()
        {
            RoleClaims = new HashSet<RoleClaim>();
        }

        public Role(string roleName, string roleDescription = null)
            : base(roleName)
        {
            RoleClaims = new HashSet<RoleClaim>();
            Description = roleDescription;
        }
    }
}
