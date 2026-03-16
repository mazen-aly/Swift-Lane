using BusinessLogic.Interfaces;
using System.Security.Claims;

namespace UI.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecurityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetLoggedInUserId()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
