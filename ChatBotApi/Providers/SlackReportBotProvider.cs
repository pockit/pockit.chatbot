using Pockit.ChatBot.Api.Consts;
using Pockit.ChatBot.Api.Models.Slack;
using Pockit.ChatBot.Api.Services;
using System.Threading.Tasks;

namespace Pockit.ChatBot.Api.Providers
{
    public class SlackReportBotProvider : ISlackReportBotProvider
    {
        private IStorageService StorageService;

        public SlackReportBotProvider(IStorageService storageService)
        {
            StorageService = storageService;
        }

        public async Task<SlashCommandResponse> AddItemToReport(string project, string item, string username)
        {
            await StorageService.AddItemToReport(project, item, username);

            return new SlashCommandResponse(SlackConsts.ITEM_ADDED_RESPONSE, true);
        }

        public async Task<SlashCommandResponse> GenerateReport()
        {
            var report = await StorageService.GenerateReport();

            return new SlashCommandResponse($"{SlackConsts.REPORT_GENERATED_RESPONSE}\r\n\r\n{report.ToString()}", true);
        }
    }
}
