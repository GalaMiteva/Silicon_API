using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon_ASP_NET_API.Filters;

namespace Silicon_ASP_NET_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscriberCoursesController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpPost]

    public async Task<ActionResult<SubscriberCourse>> CreateSubscriberCourse(SubscriberCourse subscriberCourse)
    {

        var subscriberCourseEntity = new SubscriberCourseEntity
        {

            UserId = subscriberCourse.UserId,
            CourseId = subscriberCourse.CourseId
        };

        _context.SubscriberCourses.Add(subscriberCourseEntity);
        await _context.SaveChangesAsync();

        return CreatedAtRoute(new { }, new { message = "Course added successfully" });
    }

    [HttpGet("{userId}")]


    public async Task<ActionResult<List<SavedCourse>>> GetSavedCourses(string userId)
    {

        try
        {

            var savedCourses = await _context.SubscriberCourses
                .Where(x => x.UserId == userId)
                .Select(x => new SavedCourse
                {
                    CourseId = x.CourseId,
                    Title = x.Course!.Title,
                    Price = x.Course.Price,
                    DiscountPrice = x.Course.DiscountPrice,
                    Hours = x.Course.Hours,
                    IsBestseller = x.Course.IsBestseller,
                    LikesInNumbers = x.Course.LikesInNumbers,
                    LikesInProcent = x.Course.LikesInProcent,
                    Author = x.Course.Author,
                    Img = x.Course.Img

                })
                .ToListAsync();

            return Ok(savedCourses);
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Failed to extract saved courses. Please try again later.");
        }

    }
}



