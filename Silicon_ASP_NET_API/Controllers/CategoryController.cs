﻿using Infrastructure.Contexts;
using Infrastructure.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Silicon_ASP_NET_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(DataContext dataContext) : ControllerBase
{
    private readonly DataContext _dataContext = dataContext;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _dataContext.Categories.OrderBy(o => o.CategoryName).ToListAsync();
        return Ok(CategoryFactory.Create(categories));
    }
}
