﻿using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDbContext : DbContext
    {

        private readonly ILoggerFactory _loggerFactory;
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Chapter>? Chapters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var logger = _loggerFactory.CreateLogger<UniversityDbContext>();
            // optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new[] { DbLoggerCategory.Database.Name }));
            // optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.LogTo(d => logger.Log(LogLevel.Warning, d, new[] { DbLoggerCategory.Database.Name }), LogLevel.Warning)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
        }

    }
}
