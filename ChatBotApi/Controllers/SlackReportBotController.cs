using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pockit.ChatBot.Api.Models.Slack;
using Pockit.ChatBot.Api.Providers;
using Pockit.ChatBot.Api.Consts;
using Microsoft.AspNetCore.Authorization;

namespace Pockit.ChatBot.Api.Controllers
{
    [Produces("application/json")]
    [Authorize(Policy = "IsReportBotRequest")]
    [Route("api/slack/reportbot")]
    public class SlackReportBotController : Controller
    {
        private readonly ISlackReportBotProvider Provider;

        public SlackReportBotController(ISlackReportBotProvider provider)
        {
            Provider = provider;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SlashCommand()
        {
            var form = await Request.ReadFormAsync();
            var slashRequest = new SlashCommandRequest(form);

            var commandType = DetermineCommand(slashRequest);
            SlashCommandResponse response = null;

            switch (commandType)
            {
                case ReportBotCommand.AddItem:
                    response = await ProcessAddItem(slashRequest);
                    break;
                case ReportBotCommand.GenerateReport:
                    response = await ProcessGenerate(slashRequest);
                    break;
                default:
                    response = ProcessUnknownRequest(slashRequest);
                    break;
            }

            return Ok(response);
        }

        private ReportBotCommand DetermineCommand(SlashCommandRequest request)
        {
            if (request.Text.Trim().StartsWith(SlackConsts.GENERATE_COMMAND))
            {
                return ReportBotCommand.GenerateReport;
            }
            else if (request.Text.Contains("-"))
            {
                return ReportBotCommand.AddItem;
            }
            else
            {
                return ReportBotCommand.Unknown;
            }
        }

        private async Task<SlashCommandResponse> ProcessAddItem(SlashCommandRequest request)
        {
            var dashIndex = request.Text.IndexOf('-');
            var project = request.Text.Substring(0, dashIndex).Trim();
            var item = request.Text.Substring(dashIndex + 1).Trim();

            return await Provider.AddItemToReport(project, item, request.Username);
        }

        private async Task<SlashCommandResponse> ProcessGenerate(SlashCommandRequest request)
        {
            return await Provider.GenerateReport();
        }

        private SlashCommandResponse ProcessUnknownRequest(SlashCommandRequest request)
        {
            return new SlashCommandResponse(SlackConsts.UNKOWN_REQUEST_RESPONSE, true);
        }
    }
}