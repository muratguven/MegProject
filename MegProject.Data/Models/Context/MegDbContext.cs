using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MegProject.Data.Models.Context
{

    public class MegDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public MegDbContext() : base("name=MegAppContext")
        {
            
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MegDbContext>());
        }

        public DbSet<SystemControllers> SystemControllers { get; set; }

        public DbSet<SystemActions> SystemActions { get; set; }

        public DbSet<Users> Users { get; set; }
        
        public DbSet<UserRoles> UserRoles { get; set; }
        
        public DbSet<Permission> Permission { get; set; }
        
        public DbSet<PermissionDetails> PermissionDetails { get; set; }
        public DbSet<Roles> Roles { get; set; }    



    }

}