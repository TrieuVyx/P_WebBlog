using Microsoft.EntityFrameworkCore;
using WebBlogAPI.Models;

namespace WebBlogAPI.Data
{
    public class mvcContextDb : DbContext
    {
        public mvcContextDb() { }

        public mvcContextDb(DbContextOptions<mvcContextDb> options) : base(options) { }

        public DbSet<UserModels> Users { get; set; }
        public DbSet<ProfileModel> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModels>().ToTable("Users");
            modelBuilder.Entity<UserModels>().HasKey(u => u.Id);
            // Định nghĩa các thuộc tính khác của UserModels

            modelBuilder.Entity<ProfileModel>().ToTable("Profiles");
            modelBuilder.Entity<ProfileModel>().HasKey(p => p.Id);
            // Định nghĩa các thuộc tính khác của ProfileModel

            // Định nghĩa các quan hệ giữa các bảng (nếu có)
        }
    }
}