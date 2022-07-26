﻿using Identity.Core.Constants;
using Identity.Core.Entities;
using Identity.Core.Helpers;
using Identity.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Persistence
{
    internal class IdentityDbSeeder : IDatabaseSeeder
    {
        private readonly IIdentityDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public IdentityDbSeeder(
            IIdentityDbContext db,
            RoleManager<Role> roleManager,
            UserManager<User> userManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            AddDefaultRoles();
            AddSuperAdmin();
            _db.SaveChanges();
        }

        private void AddDefaultRoles()
        {
            Task.Run(async () =>
            {
                var roleList = new List<string> { RoleConstants.SuperAdmin, RoleConstants.Admin };
                foreach (string roleName in roleList)
                {
                    var role = new Role(roleName);
                    var roleInDb = await _roleManager.FindByNameAsync(roleName);
                    if (roleInDb == null)
                    {
                        await _roleManager.CreateAsync(role);
                    }
                }
            }).GetAwaiter().GetResult();
        }

        private void AddSuperAdmin()
        {
            Task.Run(async () =>
            {
                // Check if Role Exists
                var superAdminRole = new Role(RoleConstants.SuperAdmin);
                var superAdminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.SuperAdmin);
                if (superAdminRoleInDb == null)
                {
                    await _roleManager.CreateAsync(superAdminRole);
                    superAdminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.SuperAdmin);
                }

                // Check if User Exists
                var superUser = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "admin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    IsActive = true
                };
                var superUserInDb = await _userManager.FindByEmailAsync(superUser.Email);
                if (superUserInDb == null)
                {
                    await _userManager.CreateAsync(superUser, UserConstants.DefaultPassword);
                    var result = await _userManager.AddToRoleAsync(superUser, RoleConstants.SuperAdmin);
                }

                foreach (string permission in typeof(Core.Constants.Permissions).GetNestedClassesStaticStringValues())
                {
                    await _roleManager.AddPermissionClaimAsync(superAdminRoleInDb, permission);
                }
            }).GetAwaiter().GetResult();
        }
    }
}
