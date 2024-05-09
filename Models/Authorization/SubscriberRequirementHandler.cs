using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoNetCore.Models.Services.ApplicationLayer;
using Microsoft.AspNetCore.Authorization;

namespace CorsoNetCore.Models.Authorization
{
    public class SubscriberRequirementHandler : AuthorizationHandler<SubscriberRequirement>
    {
        private readonly ICoursesService _service;
        private readonly IHttpContextAccessor _httpContext;

        public SubscriberRequirementHandler(ICoursesService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContext = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SubscriberRequirement requirement)
        {
            var IsSubscribed = await _service.IsSubscribed(Convert.ToInt32(_httpContext.HttpContext.Request.RouteValues["id"]));

            if (IsSubscribed)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}