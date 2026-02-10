using FluentValidation;
using Mediator;
using ProtoBuf.Grpc.Server;
using ServiceA.Grpc;
using ServiceA.Handlers.Commands;
using ServiceA.PipelineBehaviors.FluentValidator;
using ServiceA.PipelineBehaviors.FluentValidator.Validators;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(typeof(CreateOrderCommandValidator).Assembly);

builder.Services.AddMediator(x =>
{
    x.ServiceLifetime = ServiceLifetime.Scoped;
    x.Assemblies = [typeof(CreateOrderCommand).Assembly];
});

builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

app.MapGrpcService<OrderService>();

await app.RunAsync();
