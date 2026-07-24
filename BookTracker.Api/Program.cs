using BookTracker.Api.Application;
using BookTracker.Api.Application.BookList;
using BookTracker.Api.Application.CreateBook;
using BookTracker.Api.Application.DeleteBook;
using BookTracker.Api.Application.GetBookById;
using BookTracker.Api.Application.UpdateBook;
using BookTracker.Api.Endpoints;
using BookTracker.Api.Seeding;
using BookTracker.Api.Storage;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();
//builder.Services.AddSingleton<IBookRepository, EfBookRepository>();

//builder.Services.AddScoped<BookService>();

builder.Services.AddScoped<GetBookListQuery>();
builder.Services.AddScoped<GetBookByIdQuery>();

builder.Services.AddScoped<CreateBookCommandHandler>();
builder.Services.AddScoped<UpdateBookCommandHandler>();
builder.Services.AddScoped<DeleteBookCommandHandler>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("BookTracker"));
});

builder.Services.AddScoped<IBookRepository, EfBookRepository>();

var app = builder.Build();
/*
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreated();
    }
}
*/
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureCreated();

        if (builder.Configuration.GetValue<bool>("SeedDatabase")) DatabaseSeeder.SeedBooks(dbContext, 500);
    }
}
/*
app.MapGet("/books", async (BookService service) => Results.Ok(await service.GetAllBooks()));
app.MapGet("/books/{id:int}", async (int id, BookService service) =>
{
    var book = await service.GetBookById(id);

    if (book is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(book);
});
app.MapPost("/books", async (CreateBookRequest request, BookService service) =>
{
    var response = await service.CreateBook(request);
    return Results.Created($"/books/{response.Id}", response);
});
app.MapDelete("/books/{id:int}", async (int id, BookService service) =>
{
    var deleted = await service.DeleteBook(id);

    if (!deleted)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});
app.MapPut("/books/{id:int}", async (int id, UpdateBookRequest request, BookService service) =>
{
    var updated = await service.UpdateBook(id, request);

    if (!updated)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});
*/
app.MapBookEndpoints();
app.Run();


public partial class Program;



/*
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
*/