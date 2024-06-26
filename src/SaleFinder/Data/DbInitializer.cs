using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;

namespace SaleFinder;

public class DbInitializer
{

    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync("FinderDb", MongoClientSettings
            .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));
        await DB.Index<Item>()
            .Key(x => x.Brand, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Item>();

        using var scope = app.Services.CreateScope();

        var httpClient = scope.ServiceProvider.GetRequiredService<Srv_SaleHC>();

        var items = await httpClient.GetItemsForFindDb();

        Console.WriteLine(items.Count + " returned from the sale service");

        if (items.Count > 0) await DB.SaveAsync(items);
    }

}
