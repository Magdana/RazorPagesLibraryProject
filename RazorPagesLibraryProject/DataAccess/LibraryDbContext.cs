using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RazorPagesLibraryProject.Entities;
using System.Collections.Generic;

namespace RazorPagesLibraryProject.DataAccess
{
    public class LibraryDbContext: IdentityDbContext<UserEntity>
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }
        public DbSet<BookEntity>? Books { get; set; }
        public DbSet<GenreEntity>? Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var client = new IdentityRole("client");
            client.NormalizedName = "client";
            builder.Entity<IdentityRole>().HasData(admin, client);
        }
    }
    
}
