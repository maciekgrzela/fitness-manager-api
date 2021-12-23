using System.Security.Claims;
using FitnessManager.BusinessLogic.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FitnessManager.Infrastructure.Accessors
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserName()
        {
            var name = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return name;
        }

        public string GetUserRole()
        {
            var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            return role;
        }
    }
}