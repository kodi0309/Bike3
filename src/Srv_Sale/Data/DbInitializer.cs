using Srv_Sale.Models;
using Microsoft.EntityFrameworkCore;

namespace Srv_Sale.Data;
public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<SaleDbContext>());
    }

    private static void SeedData(SaleDbContext context)
    {
        context.Database.Migrate();

        if (context.Sales.Any())
        {
            Console.WriteLine("No need to seed");
            return;
        }

        var sales = new List<Sale>()
        {
            new Sale
            {
                Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                Status = Status.InSale,
                Item = new Item
                {
                    Brand = "Santa Cruz",
                    Model = "Hightower 2",
                    Year = 2022,
                    ImageUrl = "https://piwnicarowerowa.pl/img/25576/santa-cruz-hightower-2-aluminium-29-smoke-grey-d",
                    AdditionalProperties = new ItemProperties
                    {
                        Frame = new List<Component>
                        {
                            new Component { Name = "Aluminum", Price = 1000m },
                            new Component { Name = "Carbon", Price = 1500m }
                        },
                        Handlebar = new List<Component>
                        {
                            new Component { Name = "Riser", Price = 200m }
                        },
                        Brakes = new List<Component>
                        {
                            new Component { Name = "Disc", Price = 300m }
                        },
                        WheelsTires = new List<Component>
                        {
                            new Component { Name = "29-inch", Price = 400m }
                        },
                        Seat = new List<Component>
                        {
                            new Component { Name = "Mountain", Price = 150m }
                        },
                        DerailleursDrive = new List<Component>
                        {
                            new Component { Name = "Shimano", Price = 500m }
                        },
                        AdditionalAccessories = new List<Component>
                        {
                            new Component { Name = "Bell", Price = 20m },
                            new Component { Name = "Lights", Price = 50m }
                        }
                    }
                }
            },
            new Sale
            {
                Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a1c"),
                Status = Status.InSale,
                Item = new Item
                {
                    Brand = "Santa Cruz",
                    Model = "Heckler",
                    Year = 2022,
                    ImageUrl = "https://piwnicarowerowa.pl/img/44001/rower-elektryczny-santa-cruz-heckler-sl-r-carbon-c",
                    AdditionalProperties = new ItemProperties
                    {
                        Frame = new List<Component>
                        {
                            new Component { Name = "Carbon", Price = 1500m }
                        },
                        Handlebar = new List<Component>
                        {
                            new Component { Name = "Flat", Price = 200m }
                        },
                        Brakes = new List<Component>
                        {
                            new Component { Name = "Disc", Price = 300m }
                        },
                        WheelsTires = new List<Component>
                        {
                            new Component { Name = "27.5-inch", Price = 350m }
                        },
                        Seat = new List<Component>
                        {
                            new Component { Name = "Electric", Price = 200m }
                        },
                        DerailleursDrive = new List<Component>
                        {
                            new Component { Name = "SRAM", Price = 500m }
                        },
                        AdditionalAccessories = new List<Component>
                        {
                            new Component { Name = "GPS", Price = 150m },
                            new Component { Name = "Phone Mount", Price = 50m }
                        }
                    }
                }
            }
        };

        context.AddRange(sales);
        context.SaveChanges();
    }
}