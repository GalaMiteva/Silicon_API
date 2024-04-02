

using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CourseEntity> Courses { get; set; } = null!;
    public DbSet<SubscriberEntity> Subscribers { get; set; } = null!;
}
