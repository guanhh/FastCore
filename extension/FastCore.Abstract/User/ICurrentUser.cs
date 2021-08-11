using System;
using System.Security.Claims;

namespace FastCore.Abstract
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }

        Guid UserId { get; }

        string UserName { get; }


        Claim FindClaim(string claimType);

        Claim[] FindClaims(string claimType);

        Claim[] GetAllClaims();
    }
}
