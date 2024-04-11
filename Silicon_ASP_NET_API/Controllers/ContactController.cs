using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Silicon_ASP_NET_API.Filters;

namespace Silicon_ASP_NET_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;


    [HttpPost]
    [UseApiKey]
    public async Task<IActionResult> Create(Contact contactDto)
    {
        var contactEntity = new ContactEntity

        {
            FullName = contactDto.FullName,
            Email = contactDto.Email,
            SelectedService = contactDto.SelectedService,
            Message = contactDto.Message
        };

        _context.Contacts.Add(contactEntity);
        await _context.SaveChangesAsync();

        return CreatedAtRoute(new { }, new { message = "Contact added successfully" });

    }
}
