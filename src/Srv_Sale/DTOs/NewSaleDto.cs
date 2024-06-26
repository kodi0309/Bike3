using System.ComponentModel.DataAnnotations;
using Srv_Sale.Models;

namespace Srv_Sale.DTOs;

public class NewSaleDto
{
    // Sale.cs
    [Required]
    public Status Status { get; set; }
    public DateTime? PublicationDate { get; set; }
    public string Author { get; set; }

    // Item.cs
    [Required]
    public string Brand { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public string ImageUrl { get; set; }

    // ItemProperties
    [Required]
    public List<Component> Frame { get; set; }
    [Required]
    public List<Component> Handlebar { get; set; }
    [Required]
    public List<Component> Brakes { get; set; }
    [Required]
    public List<Component> WheelsTires { get; set; }
    [Required]
    public List<Component> Seat { get; set; }
    [Required]
    public List<Component> DerailleursDrive { get; set; }
    public List<Component> AdditionalAccessories { get; set; }
}
