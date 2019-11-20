using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MikeDemoDBEntities.Models
{
    public partial class MikeDBContext : DbContext
    {
        public MikeDBContext() : base()
        { }

        public MikeDBContext(DbContextOptions<MikeDBContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //DB Needs to Exist in local SQLExpressServer
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=MikeDB;Trusted_Connection=True;");
            }
        }

        public virtual DbSet<Automobiles> Automobiles { get; set; }
        public virtual DbSet<AutomobileTypes> AutomobileTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Automobiles>()
                .Property(c => c.AutomobileId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<AutomobileTypes>()
                .Property(c => c.AutomobileTypeId)
                .ValueGeneratedOnAdd();
            //Seed Data
            modelBuilder.Entity<AutomobileTypes>().HasData(
                new AutomobileTypes { AutomobileTypeId = 1, description = "A Truck", type = "Truck", wheels = 4 },
                new AutomobileTypes { AutomobileTypeId = 2, description = "A Car", type = "Car", wheels = 4 },
                new AutomobileTypes { AutomobileTypeId = 3, description = "A Motorbike", type = "MotorBike", wheels = 2 } 
            );
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
