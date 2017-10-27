using Microsoft.AspNetCore.Authorization;

namespace Pockit.ChatBot.Api.Authentication
{
    public class SlackAppTokenRequirement : IAuthorizationRequirement
    {
        public string RequiredAppToken { get; private set; }

        public SlackAppTokenRequirement(string requiredToken)
        {
            RequiredAppToken = requiredToken;
        }
    }
}
