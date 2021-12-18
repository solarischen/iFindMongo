using iFindMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace iFindMongo.Services;

public class SearchTermService
{
    private readonly IMongoCollection<SearchTerm> _searchTermCollection;

    public SearchTermService(
        IOptions<iFindDatabaseSettings> iFindStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            iFindStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            iFindStoreDatabaseSettings.Value.DatabaseName);

        _searchTermCollection = mongoDatabase.GetCollection<SearchTerm>(
            iFindStoreDatabaseSettings.Value.SearchTermCollectionName);
    }

    //获取所有文档的数量
    public async Task<long> GetDocumentsCountAsync() =>
        await _searchTermCollection.Find(_ => true).CountDocumentsAsync();

    //选择站点，查看所有数据，分页
    public async Task<List<SearchTerm>> GetAllAsync(string market) =>
        await _searchTermCollection.Find(st => st.Market == market).Limit(100).ToListAsync();

    //选择站点，精确查询关键词
    public async Task<SearchTerm> GetExactSearchTermAsync(string market, string name) =>
        await _searchTermCollection.Find(st => st.Name == name && st.Market == market).FirstOrDefaultAsync();

    //选择站点，模糊查询关键词
    public async Task<List<SearchTerm>> GetBoardSearchTermAsync(string market, string name) =>
        await _searchTermCollection.Find(st => st.Name.Contains(name) && st.Market == market).Limit(100).ToListAsync();

    //选择站点，通过 ASIN 查询
    public async Task<List<SearchTerm>> GetSearchTermWithASINShallowAsync(string market, string asin) =>
        await _searchTermCollection
        .Find(st => st.Market == market && (st.FirstASIN == asin
                                        || st.SecondASIN == asin
                                        || st.ThirdASIN == asin))
        .Limit(100)
        .ToListAsync();

    //选择站点，通过 关键词 深度查询

    //选择站点，通过 ASIN 深度查询
    public async Task<List<SearchTerm>> GetSearchTermWithASINDeepAsync(string market, string[] asins)
    {
        SearchTerm[]? firstResults = Array.Empty<SearchTerm>();
        foreach (var asin in asins)
        {
            var container = await _searchTermCollection.Find(st => st.Market == market && 
                                        (st.FirstASIN == asin
                                        || st.SecondASIN == asin
                                        || st.ThirdASIN == asin)).ToListAsync();
            firstResults.Concat(container);
        }

        string[] ASINs = Array.Empty<string>(); ;
        foreach (var st in firstResults)
        {
            if (!ASINs.Contains(st.FirstASIN))
            {
                ASINs.Append(st.FirstASIN);
            }
            if (!ASINs.Contains(st.SecondASIN))
            {
                ASINs.Append(st.SecondASIN);
            }
            if (!ASINs.Contains(st.ThirdASIN))
            {
                ASINs.Append(st.ThirdASIN);
            }
        }


        



        return null;
    }

}

