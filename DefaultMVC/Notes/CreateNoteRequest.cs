using System.ComponentModel.DataAnnotations;

namespace DefaultMVC.Notes;

public sealed class CreateNoteRequest
{
    [Required]
    public string Text { get; init; } = "";
}

