using Transit;
using MassTransit;
using MongoDB.Entities;

namespace SaleFinder;

public class SaleDeletedConsumer : IConsumer<SaleDeleted>
{
    public async Task Consume(ConsumeContext<SaleDeleted> context)
    {
        Console.WriteLine("Sale deleted: " + context.Message.Id);

        var result = await DB.DeleteAsync<Item>(context.Message.Id);

        if (!result.IsAcknowledged) 
            throw new MessageException(typeof(SaleDeleted), "Problem Sale Delete");
    }
}
