using System;
using Nest;

namespace Infrastructure.Data;
public class ElasticSearchService
{
    private readonly ElasticClient _client;
    public ElasticSearchService()
    {
        var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("logs") 
            .PrettyJson() 
            .DisableDirectStreaming();
        _client = new ElasticClient(settings);
    }
    public ElasticClient GetClient() => _client;
}