using Mediator;
using VSA.Domain;
using VSA.Infrastructure;

namespace VSA.Features.Notes.GetAll;

public record GetAllNotesQuery() : IQuery<List<Note>>;

public sealed class GetAllNotesQueryHandler : IQueryHandler<GetAllNotesQuery, List<Note>>
{
    private readonly MemoryList _memoryList;

    public GetAllNotesQueryHandler(MemoryList memoryList)
    {
        _memoryList = memoryList;
    }

    public ValueTask<List<Note>> Handle(GetAllNotesQuery query, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(_memoryList.Notes);
    }
}
