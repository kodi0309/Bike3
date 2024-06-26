using Transit;
using MassTransit;
using AutoMapper;
using MongoDB.Entities;
using Microsoft.Extensions.Logging;

namespace SaleFinder;

public class SaleCreatedConsumer : IConsumer<SaleCreated>
{
    private readonly IMapper _mapper;
    public SaleCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<SaleCreated> context)
    {
        Console.WriteLine("--> Consumer sale created: " + context.Message.Id);

        var item = _mapper.Map<Item>(context.Message);

        await item.SaveAsync();
    }
}