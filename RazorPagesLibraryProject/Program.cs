using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using RazorPagesLibraryProject.DataAccess;
using RazorPagesLibraryProject.Mappers;
using RazorPagesLibraryProject.Repository.Interfaces;
using RazorPagesLibraryProject.Repository.Repositories;
using RazorPagesLibraryProject.Services.Interfaces;
using RazorPagesLibraryProject.Services.Services;
using Microsoft.AspNetCore.Identity;
using RazorPagesLibraryProject.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Get connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<UserEntity>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryDbContext>();

builder.Services.AddAutoMapper(typeof(BookEntityAutoMapperProfile).Assembly);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles", "images")),
    RequestPath = "/UploadedFiles/images"
});

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
