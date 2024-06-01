using TimeCapsule.Entities.Context;
using TimeCapsule.Services;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder().AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddSingleton(configurationBuilder);
builder.Services.AddScoped<ITaskTypeService, TaskTypeService>();
builder.Services.AddScoped<ITaskStopwatchService, TaskStopwatchService>();
builder.Services.AddScoped<ITaskSchedulerService, TaskSchedulerService>();

builder.Services.AddDbContext<TimeCapsuleDbContext>();

builder.Services.AddControllers();
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

// Temporary solution. Find a better way to deal with cors error.
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();