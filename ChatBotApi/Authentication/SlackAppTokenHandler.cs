using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Pockit.ChatBot.Api.Authentication
{
    public class SlackAppTokenHandler : AuthorizationHandler<SlackAppTokenRequirement>
    {
        private const string SLACK_APP_TOKEN_KEY = "token";
        private const string LOCAL_HOST = "localhost";

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SlackAppTokenRequirement requirement)
        {
            // If it's being run locally, then don't bother authenticating
            var request = ((AuthorizationFilterContext)context.Resource).HttpContext.Request;
            if (request.Host.Host == LOCAL_HOST)
            {
                context.Succeed(requirement);
            }
            else
            {
                // Read the token of the request from the body
                var body = await request.ReadFormAsync();

                var success = false;
                if (body != null && body.ContainsKey(SLACK_APP_TOKEN_KEY))
                {
                    var token = body[SLACK_APP_TOKEN_KEY].Single();

                    if (token == requirement.RequiredAppToken)
                    {
                        success = true;
                    }
                }

                if (success)
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
}
