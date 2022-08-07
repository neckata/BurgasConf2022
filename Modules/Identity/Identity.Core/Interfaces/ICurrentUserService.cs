using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace Identity.Core.Interfaces
{
    public interface ICurrentUserService
    {
        string Name { get; }

        bool IsAuthenticated();

        bool IsInRole(string role);

        IEnumerable<Claim> GetUserClaims();

        HttpContext GetHttpContext();
    }
}
