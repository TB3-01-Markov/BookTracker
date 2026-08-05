using BookTracker.Api.Endpoints.Books;
using BookTracker.Api.Wiring;
using BookTracker.Api.Endpoints.Members;

var builder = WebApplication.CreateBuilder(args);
builder.AddBookTracker();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseBookTracker();
app.UseCors();


app.Run();

public partial class Program;
/*
using BookTracker.Api.Application;
using BookTracker.Api.Application.CreateBook;
using BookTracker.Api.Application.DeleteBook;
using BookTracker.Api.Application.GetBookDetails;
using BookTracker.Api.Application.GetBookSummaries;
using BookTracker.Api.Application.Books.UpdateBook;
using BookTracker.Api.Endpoints;
using BookTracker.Api.Seeding;
using BookTracker.Api.Storage;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GetBookSummariesQueryHandler>();
builder.Services.AddScoped<GetBookDetailsQueryHandler>();

builder.Services.AddScoped<CreateBookCommandHandler>();
builder.Services.AddScoped<UpdateBookCommandHandler>();
builder.Services.AddScoped<DeleteBookCommandHandler>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("BookTracker"));
});

builder.Services.AddScoped<IBookRepository, EfBookRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureCreated();

        if (builder.Configuration.GetValue<bool>("SeedDatabase")) DatabaseSeeder.SeedBooks(dbContext, 500);
    }
}

app.MapBookEndpoints();
app.Run();

public partial class Program;
*/