using MongoDB.Driver;
using MongoDB.Entities;
using System.Threading.Tasks;

namespace Srv_Config
{
    public static class DbInitializer
    {
        public static async Task InitDbAsync(string connectionString)
        {
            await DB.InitAsync("ConfigurationDb", MongoClientSettings.FromConnectionString(connectionString));
        }
    }
}