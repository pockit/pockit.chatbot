using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Pockit.ChatBot.Api.Consts;
using Pockit.ChatBot.Api.Extensions;
using Pockit.ChatBot.Api.Models.Slack;
using Pockit.ChatBot.Api.Models.Storage;
using Pockit.ChatBot.Api.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pockit.ChatBot.Api.Services
{
    public class AzureStorageService : IStorageService
    {
        private readonly string StorageConnectionString;

        public AzureStorageService(IOptions<StorageOptions> options)
        {
            StorageConnectionString = options.Value.StorageConnectionString;
        }

        public async Task AddItemToReport(string projectName, string item, string username)
        {
            var table = GetTable();
            var tableItem = new ReportItemEntity(projectName, item, username);
            var operation = TableOperation.InsertOrReplace(tableItem);

            await table.ExecuteAsync(operation);
        }

        public async Task<Report> GenerateReport()
        {
            var currentWeek = DateTime.UtcNow.GetWeekOfYear();
            var partitionKey = $"eng_weekly_{currentWeek}";
            var query = new TableQuery<ReportItemEntity>().Where($"PartitionKey eq '{partitionKey}'");

            var entities = await PerformTableQuery(query);
            var groups = entities.GroupBy(e => e.ProjectName);

            return new Report()
            {
                Projects = groups.Select(
                    g => new ReportProject()
                    {
                        ProjectName = g.Key,
                        Items = g.Select(i => i.Item)
                    })
            };
        }

        private async Task<List<T>> PerformTableQuery<T>(TableQuery<T> query) where T : ITableEntity, new()
        {
            var results = new List<T>();
            TableContinuationToken token = null;
            var table = GetTable();

            do
            {
                var segment = await table.ExecuteQuerySegmentedAsync(query, token);
                results.AddRange(segment.Results);
                token = segment.ContinuationToken;
            } while (token != null);

            return results;
        }

        private CloudTable GetTable()
        {
            var account = CloudStorageAccount.Parse(StorageConnectionString);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference(SlackConsts.AZURE_TABLE_NAME);
            table.CreateIfNotExistsAsync();

            return table;
        }
    }
}
