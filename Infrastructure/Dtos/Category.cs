

using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dtos;

public class Category
{

    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
}
