using Newtonsoft.Json;
using Pockit.ChatBot.Api.Consts;

namespace Pockit.ChatBot.Api.Models.Slack
{
    public class SlashCommandResponse
    {
        [JsonProperty("response_type")]
        public string ResponseType { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        public SlashCommandResponse(string text, bool isEphemeral)
        {
            Text = text;
            ResponseType = isEphemeral ? SlackConsts.EPHEMERAL_RESPONSE_TYPE : SlackConsts.IN_CHANNEL_RESONSE_TYPE;
        }
    }
}