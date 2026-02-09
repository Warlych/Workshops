using System.Runtime.Serialization;
using System.ServiceModel;
using ProtoBuf;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Shared;

[Service]
public interface IOrderService
{
    [Operation]
    ValueTask<CreateOrderResponse> Create(CreateOrderRequest request, CallContext context = default);
}

[ProtoContract]
public class CreateOrderRequest
{
    [ProtoMember(1)]
    public string Goods { get; set; }
}

[ProtoContract]
public class CreateOrderResponse
{
    [ProtoMember(1)]
    public Guid OrderId { get; set; }
}