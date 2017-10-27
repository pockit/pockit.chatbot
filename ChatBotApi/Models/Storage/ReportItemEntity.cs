using System;
using Microsoft.WindowsAzure.Storage.Table;
using Pockit.ChatBot.Api.Extensions;

namespace Pockit.ChatBot.Api.Models.Storage
{
    public class ReportItemEntity : TableEntity
    {
        public string ProjectName { get; set; }
        public string Item { get; set; }
        public string Username { get; set; }

        public ReportItemEntity(string projectName, string item, string username)
        {
            var weekNumber = DateTime.UtcNow.GetWeekOfYear();

            PartitionKey = $"eng_weekly_{weekNumber}";
            RowKey = Guid.NewGuid().ToString();

            ProjectName = projectName;
            Item = item;
            Username = username;
        }

        public ReportItemEntity()
        {

        }
    }
}