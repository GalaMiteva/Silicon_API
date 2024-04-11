

namespace Infrastructure.Dtos;

public class Contact
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? SelectedService { get; set; }
    public string Message { get; set; } = null!;

}
