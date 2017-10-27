using System.Threading.Tasks;
using Pockit.ChatBot.Api.Models.Slack;

namespace Pockit.ChatBot.Api.Providers
{
    public interface ISlackReportBotProvider
    {
        /// <summary>
        /// Adds an item to the report.
        /// </summary>
        /// <param name="project">The name of the project the item belongs to.</param>
        /// <param name="item">The item to be added.</param>
        /// <param name="username">The username of the user that added the item.</param>
        /// <returns>The response to the request</returns>
        Task<SlashCommandResponse> AddItemToReport(string project, string item, string username);

        /// <summary>
        /// Generates the report, removing the items from storage.
        /// </summary>
        /// <returns>A response containing the contents of the report.</returns>
        Task<SlashCommandResponse> GenerateReport();
    }
}
