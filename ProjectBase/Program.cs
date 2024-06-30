using ProjectBase;
using ProjectBase.Domain.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var appConfig = builder.Configuration.Get<AppSettingConfiguration>();
builder.Services.AddSingleton(appConfig);
builder.Services.AddDbConnectionString(appConfig);
builder.Services.AddAWSService(appConfig);
//builder.Services.AddJwtAuthenticate(appConfig);
builder.Services.AddServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MigrationInitialisation();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
