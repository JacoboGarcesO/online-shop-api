using Microsoft.Extensions.Options;
using MongoDB.Driver;
using online_shop_api.Config;
using online_shop_api.Models;

namespace online_shop_api.Services
{
  public class UserService
  {

    private readonly IMongoCollection<User> _usersCollection;

    public UserService(IOptions<OnlineShopDatabaseSettings> settings)
    {
      var mongoClient = new MongoClient(
          settings.Value.ConnectionString);

      var mongoDatabase = mongoClient.GetDatabase(
          settings.Value.DatabaseName);

      _usersCollection = mongoDatabase.GetCollection<User>(
          settings.Value.UsersCollectionName);

      var indexOptions = new CreateIndexOptions { Unique = true };
      var indexModel = new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u => u.Email), indexOptions);
      _usersCollection.Indexes.CreateOne(indexModel);
    }

    public async Task CreateAsync(User user) =>
      await _usersCollection.InsertOneAsync(user);

    public async Task<User?> Authenticate(UserCredentials user) =>
      await _usersCollection.Find(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password)).FirstOrDefaultAsync();
  }
}
