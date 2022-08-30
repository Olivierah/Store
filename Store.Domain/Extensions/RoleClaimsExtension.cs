using Store.Domain.Entities;
using System.Security.Claims;

namespace Store.Domain.Extensions
{
    public static class RoleClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var result = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),                
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())              
            };
            return result;
        }
    }
}
