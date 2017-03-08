using Intake.Data.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Intake.Data
{
    public interface IDataContext
    {
        DbSet<Answer> Answers { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<ProjectRequestor> ProjectRequestors { get; set; }
        DbSet<ProjectSponsor> ProjectSponsors { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Survey> Surveys { get; set; }
        DbSet<Tenant> Tenants { get; set; }
        DbSet<Option> Options { get; set; }
        Task<int> SaveChangesAsync();
    }

    public class DataContext: DbContext, IDataContext
    {
        public DataContext()
        {

        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectRequestor> ProjectRequestors { get; set; }
        public DbSet<ProjectSponsor> ProjectSponsors { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
    }
}
