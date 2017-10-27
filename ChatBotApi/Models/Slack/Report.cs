using System.Collections.Generic;

namespace Pockit.ChatBot.Api.Models.Slack
{
    public class Report
    {
        public IEnumerable<ReportProject> Projects;

        public override string ToString()
        {
            var projectStrings = new List<string>();
            foreach (var project in Projects)
            {
                var itemStrings = new List<string>();
                foreach (var item in project.Items)
                {
                    itemStrings.Add($"- {item}\r\n");
                }

                projectStrings.Add($"{project.ProjectName}:\r\n{string.Join("", itemStrings)}\r\n");
            }

            return string.Join("", projectStrings);
        }
    }
}
