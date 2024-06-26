using AutoMapper;
using Transit;
using MassTransit;
using MongoDB.Entities;

namespace SaleFinder;

public class SaleUpdatedConsumer : IConsumer<SaleUpdated>
{
    private readonly IMapper _mapper;

    public SaleUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<SaleUpdated> context)
    {
        Console.WriteLine("sale updated: " + context.Message.Id);
        
        var item = _mapper.Map<Item>(context.Message);

        //Beter version needed
        var result = await DB.Update<Item>()
            .Match(i => i.ID == context.Message.Id)
            .ModifyOnly(x => new
            {
                x.Brand,
                x.Model,
                x.Year,
                x.ImageUrl,
                x.Status,
                x.PublicationDate,
                x.Author,
                x.Frame,
                x.Handlebar,
                x.Brakes,
                x.WheelsTires,
                x.Seat,
                x.DerailleursDrive,
                x.AdditionalAccessories
            }, item)
            .ExecuteAsync();

        if (!result.IsAcknowledged)
            throw new MessageException(typeof(SaleUpdated), "Problem during UPDATE MONGO");
    }
}
