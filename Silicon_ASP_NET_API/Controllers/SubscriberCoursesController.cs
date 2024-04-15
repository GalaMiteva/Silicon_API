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

    [UseApiKey]
    [HttpPost]
    
    public async Task<ActionResult<SubscriberCourse>> CreateSubscriberCourse(SubscriberCourse subscriberCourse)
    {

        try
        {
            var existingUserCourse = await _context.SubscriberCourses
                .FirstOrDefaultAsync(uc => uc.UserId == subscriberCourse.UserId && uc.CourseId == subscriberCourse.CourseId);

            if (existingUserCourse != null)
            {
                return Conflict("Course already exists for this user.");
            }

            var subscriberCourseEntity = new SubscriberCourseEntity
            {
                UserId = subscriberCourse.UserId,
                CourseId = subscriberCourse.CourseId
            };

            _context.SubscriberCourses.Add(subscriberCourseEntity);
            await _context.SaveChangesAsync();

            return CreatedAtRoute(new { }, new { message = "Course added successfully" });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Failed to add course. Please try again later.");
        }
    }


    [UseApiKey]
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

    [UseApiKey]
    [HttpDelete("{userId}/{courseId}")]
    public async Task<IActionResult> DeleteSubscriberCourse(string userId, int courseId)
    {
        try
        {
            var subscriberCourse = await _context.SubscriberCourses.FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);

            if (subscriberCourse == null)
            {
                return NotFound("Subscriber course not found.");
            }

            _context.SubscriberCourses.Remove(subscriberCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Failed to delete course. Please try again later.");
        }
    }

    [UseApiKey]
    [HttpDelete("all/{userId}")]
    public async Task<IActionResult> DeleteAllSubscriberCourses(string userId)
    {
        try
        {
            var subscriberCourses = await _context.SubscriberCourses.Where(uc => uc.UserId == userId).ToListAsync();

            if (!subscriberCourses.Any())
            {
                return NotFound("No courses found for the user.");
            }

            _context.SubscriberCourses.RemoveRange(subscriberCourses);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Failed to delete courses. Please try again later.");
        }
    }

}



