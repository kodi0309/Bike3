using System.ComponentModel.DataAnnotations.Schema;
using Srv_Sale.Models;

namespace Srv_Sale
{
    [Table("Items")]
    public class Item
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }

        [Column(TypeName = "jsonb")]
        public ItemProperties AdditionalProperties { get; set; }

        public Sale Sale { get; set; }
        public Guid SaleId { get; set; }
    }

    public class ItemProperties
    {
        public List<Component> Frame { get; set; }
        public List<Component> Handlebar { get; set; }
        public List<Component> Brakes { get; set; }
        public List<Component> WheelsTires { get; set; }
        public List<Component> Seat { get; set; }
        public List<Component> DerailleursDrive { get; set; }
        public List<Component> AdditionalAccessories { get; set; }
    }
}

