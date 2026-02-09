using Mediator;
using ProtoBuf.Grpc;
using ServiceA.Handlers.Commands;
using Shared;

namespace ServiceA.Grpc;

public sealed class OrderService : IOrderService
{
    private readonly ISender _sender;

    public OrderService(ISender sender)
    {
        _sender = sender;
    }

    public async ValueTask<CreateOrderResponse> Create(CreateOrderRequest request, CallContext context = default)
    {
        var result = await _sender.Send(new CreateOrderCommand(request.Goods), context.CancellationToken);

        return new CreateOrderResponse
        {
            OrderId = result
        };
    }
}
