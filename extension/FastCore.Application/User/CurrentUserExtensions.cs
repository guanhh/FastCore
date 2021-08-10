using FastCore.Abstract;

namespace FastCore.Application
{
    public static class CurrentUserExtensions
    {

        public static string FindClaimValue(this ICurrentUser currentUser, string claimType)
        {
            return currentUser.FindClaim(claimType)?.Value;
        }

        public static int GetId(this ICurrentUser currentUser)
        {
            return currentUser.UserId;
        }
    }
}
