using System.Reflection;
using Mediator;
using Microsoft.AspNetCore.Http.HttpResults;
using VSA.Common;
using VSA.Features.Notes.GetAll;
using VSA.Infrastructure;
using VSA.PipelineBehaviours;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MemoryList>();

builder.Services.AddMediator(x =>
{
    x.ServiceLifetime = ServiceLifetime.Scoped;
});

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

var app = builder.Build();

var endpointTypes = Assembly.GetExecutingAssembly()
                            .GetTypes()
                            .Where(x => x is { IsAbstract: false, IsInterface: false }
                                        && typeof(IEndpointConfiguration).IsAssignableFrom(x))
                            .OrderBy(x => x.FullName);

foreach (var endpointType in endpointTypes)
{
    var endpoint = (IEndpointConfiguration)Activator.CreateInstance(endpointType)!;
    endpoint.Map(app);
}

app.MapGet("api/notes",
           async (IMediator mediator) =>
           {
               var result = await mediator.Send(new GetAllNotesQuery());
               
               return result;
           });

await app.RunAsync();
