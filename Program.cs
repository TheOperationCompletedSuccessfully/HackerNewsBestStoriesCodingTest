using HackerNewsBestStories.Contracts;
using HackerNewsBestStories.Entities.Dtos;
using HackerNewsBestStories.Entities.Misc;
using HackerNewsBestStories.Services;

//AutoMapperConfig.Configure();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IFireBaseDataService<HackerNewsBestStory>, FireBaseDataService<HackerNewsBestStory>>();
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
