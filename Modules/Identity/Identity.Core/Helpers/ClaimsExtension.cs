using Identity.Core.Constants;
using Identity.Core.Entities;
using Identity.Models.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Identity.Core.Helpers
{
    public static class ClaimsHelper
    {
        public static void GetAllPermissions(this List<RoleClaimModel> allPermissions)
        {
            foreach (var module in typeof(Permissions).GetNestedTypes())
            {
                string moduleName = string.Empty;
                string moduleDescription = string.Empty;

                if (module.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .FirstOrDefault() is DisplayNameAttribute displayNameAttribute)
                {
                    moduleName = displayNameAttribute.DisplayName;
                }

                if (module.GetCustomAttributes(typeof(DescriptionAttribute), true)
                    .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                {
                    moduleDescription = descriptionAttribute.Description;
                }

                foreach (var fi in module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
                {
                    object propertyValue = fi.GetValue(null);

                    if (propertyValue is not null)
                    {
                        allPermissions.Add(new() { Value = propertyValue.ToString(), Type = ApplicationClaimTypes.Permission, Group = moduleName, Description = moduleDescription });
                    }
                }
            }
        }

        public static async Task<IdentityResult> AddPermissionClaimAsync(this RoleManager<Role> roleManager, Role role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
            {
                return await roleManager.AddClaimAsync(role, new(ApplicationClaimTypes.Permission, permission));
            }

            return IdentityResult.Failed();
        }

        public static List<string> GetNestedClassesStaticStringValues(this Type type)
        {
            var values = new List<string>();
            foreach (var prop in type.GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                object propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                {
                    values.Add(propertyValue.ToString());
                }
            }

            return values;
        }
    }
}
