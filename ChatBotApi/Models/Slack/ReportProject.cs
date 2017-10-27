using System.Collections.Generic;

namespace Pockit.ChatBot.Api.Models.Slack
{
    public class ReportProject
    {
        public string ProjectName { get; set; }
        public IEnumerable<string> Items { get; set; }
    }
}
