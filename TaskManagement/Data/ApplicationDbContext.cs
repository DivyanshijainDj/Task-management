using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Team)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TeamID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Manager)
                .WithMany()
                .HasForeignKey(t => t.ManagerID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Task>()
                .HasOne(t => t.AssignedUser)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssignedUserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Team)
                .WithMany(t => t.Reports)
                .HasForeignKey(r => r.TeamID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!; 
        public int? TeamID { get; set; }

        public Team? Team { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }

    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; } = null!;
        public int? ManagerID { get; set; }

        public User? Manager { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }

    public class Task
    {
        public int TaskID { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!; 
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AssignedUserID { get; set; }

        public User? AssignedUser { get; set; }
    }

    public class Report
    {
        public int ReportID { get; set; }
        public DateTime ReportDate { get; set; }
        public int? TeamID { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public int OverdueTasks { get; set; }

        public Team? Team { get; set; }
    }
}
