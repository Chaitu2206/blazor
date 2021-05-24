using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NRediSearch;
using StackExchange.Redis;
using Zindagi.Domain;
using Zindagi.Domain.RequestsAggregate;
using Zindagi.Domain.RequestsAggregate.ViewModels;
using Zindagi.Infra.Redis;
using Zindagi.SeedWork;

namespace Zindagi.Infra.Repositories
{
    public class BloodRequestsSearchRepository : IBloodRequestsSearchRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly Client _redisSearchClient;

        public BloodRequestsSearchRepository(IConnectionMultiplexer connectionMultiplexer, ILogger<UserRepository> logger)
        {
            _logger = logger;

            _redisSearchClient = new Client(RedisConstants.BloodRequestsSearchSchema, connectionMultiplexer.GetDatabase());

            if (IndexExistsAsync(RedisConstants.BloodRequestsSearchSchema))
                return;
            var schema = new Schema();
            schema.AddTextField("requestId", 2)
                .AddTextField("patientName")
                .AddTextField("reason")
                .AddTextField("donationType")
                .AddTextField("bloodGroup")
                .AddTextField("priority", 2)
                .AddNumericField("units");
            _redisSearchClient.CreateIndex(schema, new Client.ConfiguredIndexOptions());
        }

        private bool IndexExistsAsync(string checkIndexName)
        {
            try
            {
                var resultParsed = _redisSearchClient.GetInfoParsed();
                var indexName = resultParsed.IndexName;
                return indexName == checkIndexName;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task CreateBloodRequestRecord(BloodRequest request)
        {
            var fields = new Dictionary<string, RedisValue>
            {
                { "requestId", request.Id },
                { "requestorId", request.RequestorId },
                { "patientName", request.PatientName },
                { "reason", request.Reason },
                { "donationType", request.DonationType.Id },
                { "bloodGroup", request.BloodGroup.Id },
                { "status", request.Status.Id },
                { "priority", request.Priority.Id },
                { "units", request.QuantityInUnits }
            };
            await _redisSearchClient.AddDocumentAsync(new Document(request.GetPersistenceKey(), fields));
        }

        public async Task<List<BloodRequestSearchRecordDto>> GetSearchResultsAsync(string searchString)
        {
            var requests = new List<BloodRequestSearchRecordDto>();
            SearchResult? searchResult = null;

            try
            {
                searchResult = await _redisSearchClient.SearchAsync(new Query(searchString) { WithPayloads = true });
                _logger.LogInformation("search result: {info}", searchResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Redis Search for Blood Requests");
            }

            if (searchResult == null)
                return requests;

            requests.AddRange(from doc in searchResult.Documents
                              let record = doc.GetProperties().ToList()
                              let status = int.Parse(record.FirstOrDefault(p => p.Key == "status").Value.ToString() ?? string.Empty, CultureInfo.InvariantCulture)
                              where status <= 2
                              select new BloodRequestSearchRecordDto
                              {
                                  SearchId = doc.Id,
                                  SearchScore = doc.Score,
                                  RequestId = long.Parse(doc.Id, CultureInfo.InvariantCulture),
                                  PatientName = record.FirstOrDefault(p => p.Key == "patientName").Value.ToString(),
                                  Reason = record.FirstOrDefault(p => p.Key == "reason").Value.ToString(),
                                  QuantityInUnits = double.Parse(record.FirstOrDefault(p => p.Key == "units").Value.ToString(), CultureInfo.InvariantCulture),
                                  DonationType = Enumeration.FromValue<BloodDonationType>(record.FirstOrDefault(p => p.Key == "donationType").Value.ToString()),
                                  BloodGroup = Enumeration.FromValue<BloodGroup>(record.FirstOrDefault(p => p.Key == "bloodGroup").Value.ToString()),
                                  Priority = Enumeration.FromValue<BloodRequestPriority>(record.FirstOrDefault(p => p.Key == "priority").Value.ToString()),
                                  Status = Enumeration.FromValue<DetailedStatus>(status)
                              });
            return requests;
        }
    }
}
