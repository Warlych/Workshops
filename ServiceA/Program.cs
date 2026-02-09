using ProtoBuf.Grpc.Server;
using ServiceA.Grpc;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediator(x => x.ServiceLifetime = ServiceLifetime.Scoped);
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

app.MapGrpcService<OrderService>();

await app.RunAsync();
