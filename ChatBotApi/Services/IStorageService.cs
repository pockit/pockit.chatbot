using Pockit.ChatBot.Api.Models.Slack;
using System.Threading.Tasks;

namespace Pockit.ChatBot.Api.Services
{
    public interface IStorageService
    {
        /// <summary>
        /// Adds an item to storage for the report.
        /// </summary>
        /// <param name="project">The name of the project the item belongs to.</param>
        /// <param name="item">The item to be added.</param>
        /// <param name="username">The username of the slack user adding the item.</param>
        /// <returns></returns>
        Task AddItemToReport(string project, string item, string username);

        /// <summary>
        /// Generates the report for the current week.
        /// </summary>
        /// <returns>The report for the current week.</returns>
        Task<Report> GenerateReport();
    }
}
