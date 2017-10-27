using Microsoft.AspNetCore.Http;

namespace Pockit.ChatBot.Api.Models.Slack
{
    public class SlashCommandRequest
    {
        public string Token { get; set; }
        public string TeamId { get; set; }
        public string TeamDomain { get; set; }
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Command { get; set; }
        public string Text { get; set; }
        public string ResponseUrl { get; set; }
        public string TriggerId { get; set; }

        public SlashCommandRequest(IFormCollection requestForm)
        {
            Token = requestForm["token"][0];
            TeamId = requestForm["team_id"][0];
            TeamDomain = requestForm["team_domain"][0];
            ChannelId = requestForm["channel_id"][0];
            ChannelName = requestForm["channel_name"][0];
            UserId = requestForm["user_id"][0];
            Username = requestForm["user_name"][0];
            Command = requestForm["command"][0];
            Text = requestForm["text"][0];
            ResponseUrl = requestForm["response_url"][0];
            TriggerId = requestForm["trigger_id"][0];
        }
    }
}
