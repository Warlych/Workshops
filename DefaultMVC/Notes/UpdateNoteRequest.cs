using System.ComponentModel.DataAnnotations;

namespace DefaultMVC.Notes;

public sealed class UpdateNoteRequest
{
    [Required]
    public string Text { get; init; } = "";
}

