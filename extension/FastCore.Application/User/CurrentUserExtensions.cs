using FastCore.Abstract;
using System;

namespace FastCore.Application
{
    public static class CurrentUserExtensions
    {

        public static string FindClaimValue(this ICurrentUser currentUser, string claimType)
        {
            return currentUser.FindClaim(claimType)?.Value;
        }

        public static Guid GetId(this ICurrentUser currentUser)
        {
            return currentUser.UserId;
        }
    }
}
