using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace iFindMongo.Models;

public class SearchTerm
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Market { get; set; }
    public string? Name { get; set; }
    public int Rank { get; set; }
    public string? FirstASIN { get; set; }
    public string? FirstASINName { get; set; }
    public string? FirstASINClickShare { get; set; }
    public string? FirstASINConvertShare { get; set; }
    public string? SecondASIN { get; set; }
    public string? SecondASINName { get; set; }
    public string? SecondASINClickShare { get; set; }
    public string? SecondASINConvertShare { get; set; }
    public string? ThirdASIN { get; set; }
    public string? ThirdASINName { get; set; }
    public string? ThirdASINClickShare { get; set; }
    public string? ThirdASINConvertShare { get; set; }
}

