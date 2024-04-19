using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace online_shop_api.Models
{
  public class User
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public RoleEnum Role { get; set; }
  }
}
