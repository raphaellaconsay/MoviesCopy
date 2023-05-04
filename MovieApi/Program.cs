using Microsoft.OpenApi.Models;
using MovieApi.Context;
using MovieApi.Contracts;
using MovieApi.Repositories;
using MovieApi.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Movie API",
        Description = "API for Movies and its related attributes: Genres, Actors, and Awards",
        Contact = new OpenApiContact
        {
            Name = "The Johns and a Flower",
            Url = new Uri("https://github.com/CITUCCS/csit327-project-group-8-the-johns-and-a-flower")
        }
    });
    //xml documentation
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Configure services
ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    //DI
    //DapperContext
    services.AddTransient<DapperContext>();

    //AutoMapper
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    //Services
    services.AddScoped<IMovieService, MovieService>();
    services.AddScoped<IGenreService, GenreService>();
    services.AddScoped<IActorService, ActorService>();
    services.AddScoped<IAwardService, AwardService>();
    services.AddScoped<IMovieActorService, MovieActorService>();
    services.AddScoped<IMovieGenreService, MovieGenreService>();

    //Repos
    services.AddScoped<IMovieRepository, MovieRepository>();
    services.AddScoped<IGenreRepository, GenreRepository>();
    services.AddScoped<IActorRepository, ActorRepository>();
    services.AddScoped<IAwardRepository, AwardRepository>();
}
