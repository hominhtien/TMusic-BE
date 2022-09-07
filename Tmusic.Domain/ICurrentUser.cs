﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Tmusic.Domain
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        Guid? Id { get; }
        string UserName { get; }
        string Name { get; }
        string PhoneNumber { get; }
        bool PhoneNumberVerified { get; }
        string Email { get; }
        bool EmailVerified { get; }
        string[] Roles { get; }
        string[] Permissions { get; }
        string ClientId { get; }
        string LanguageCode { get; }

        Claim FindClaim(string claimType);
        IEnumerable<Claim> FindClaims(string claimType);
        IEnumerable<Claim> GetAllClaims();
        bool IsInRole(string roleName);
    }
}
