using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MegProject.Data.Core.IdentityBase;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MegProject.Data.Models.Context
{

    public class MegDbContext : IdentityDbContext<ApplicationUser>
    {

        public MegDbContext() : base("name=MegAppContext")
        {

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MegDbContext>());
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            //modelBuilder.Entity<Users>().HasKey<string>(l => l.Id).HasMany(x => x.UserRoles);
            //modelBuilder.Entity<IdentityUserLogin>().HasKey(l => new { l.UserId,l.ProviderKey});
            //modelBuilder.Entity<Roles>().HasKey<string>(r => r.Id);
            //modelBuilder.Entity<UserRoles>().HasKey(x => new { x.Id,x.UserId, x.RoleId });
        }

        /// <summary>
        /// Identity Systems
        /// </summary>
        /// <returns></returns>
        public static MegDbContext Create()
        {
            return new MegDbContext();
        }


        #region Dbsets
        public DbSet<SystemControllers> SystemControllers { get; set; }

        public DbSet<SystemActions> SystemActions { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }

        public DbSet<Permission> Permission { get; set; }

        public DbSet<PermissionDetails> PermissionDetails { get; set; }
        public DbSet<Roles> Roles { get; set; }
        #endregion






    }

}