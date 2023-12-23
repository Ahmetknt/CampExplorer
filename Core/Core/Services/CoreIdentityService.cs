using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class CoreIdentityService : ICoreIdentityService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public CoreIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
                                   
        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value; 
    }
}
