namespace Srv_Sale.Models;

public class Sale
{
    public Guid Id { get; set; }
    public Status Status { get; set; }
    public Item Item { get; set; }
    public DateTime? PublicationDate { get; set; } 
    public string Author { get; set; }

    public Sale()
    {
        PublicationDate = DateTime.UtcNow;
    }
}