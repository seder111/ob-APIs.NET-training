using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDBContext : DbContext
    {

        public UniversityDBContext(DbContextOptions<UniversityDBContext> options): base(options) { }

        public DbSet<User>? users { get; set; }

        public DbSet<Curso>? curso { get; set; }
    }
}
