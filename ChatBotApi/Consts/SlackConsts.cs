namespace Pockit.ChatBot.Api.Consts
{
    public static class SlackConsts
    {
        public const string IN_CHANNEL_RESONSE_TYPE = "in_channel";
        public const string EPHEMERAL_RESPONSE_TYPE = "ephemeral";

        public const string PEEK_COMMAND = "peek";
        public const string GENERATE_COMMAND = "generate";

        public const string AZURE_TABLE_NAME = "ReportBot";

        public const string UNKOWN_REQUEST_RESPONSE = "Sorry, I wasn't able to determine what you wanted.";
        public const string ITEM_ADDED_RESPONSE = "Thanks, your item has been added to the report!";
        public const string REPORT_GENERATED_RESPONSE = "Here's the report for this week:";
    }
}