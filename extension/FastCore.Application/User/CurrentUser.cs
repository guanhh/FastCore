using FastCore.Abstract;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace FastCore.Application
{
    public class CurrentUser : ICurrentUser
    {

        private static readonly Claim[] EmptyClaimsArray = new Claim[0];
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public int UserId => int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public string UserName => FindClaim(ClaimTypes.Name).Value;

        //public string Email => FindClaim("email").Value;

        public string UserType => FindClaim(ClaimTypes.Role).Value;

        public Claim FindClaim(string claimType)
        {
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == claimType);
        }

        public Claim[] FindClaims(string claimType)
        {
            return _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == claimType).ToArray() ?? EmptyClaimsArray;
        }

        public Claim[] GetAllClaims()
        {
            return _httpContextAccessor.HttpContext.User.Claims.ToArray() ?? EmptyClaimsArray;
        }
    }
}
