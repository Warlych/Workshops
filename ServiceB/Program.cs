using Grpc.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using ProtoBuf.Grpc.ClientFactory;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddCodeFirstGrpcClient<IOrderService>("OrderService",
                                              x =>
                                              {
                                                  x.Address = new Uri("http://localhost:5000");
                                              })
       .ConfigureChannel(x =>
       {
           x.Credentials = ChannelCredentials.Insecure;
       });

var app = builder.Build();

app.MapPost("/",
            async (IOrderService orderService) =>
            {
                var result = await orderService.Create(new CreateOrderRequest
                {
                    Goods = ""
                });

                return result;
            });

await app.RunAsync();
