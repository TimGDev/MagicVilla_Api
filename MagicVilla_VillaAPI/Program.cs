//for SeriLog.aspnetcore and Serilog.SLinks.file for creating log file
//using Serilog;

using MagicVilla_VillaAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//option => { option.ReturnHttpNotAcceptable = true; for not accepting formats like xml
//.AddXmlDataContractSerializerFormatters() for accepting xml
//.AddNewtonsoftJson() for using patch support JSONPATCH

//for logging
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/villalogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();

//for database conection string and dbcontext 
builder.Services.AddDbContext<ApplicationDbContext>(option => { option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")); });
builder.Services.AddControllers(
    //option => { option.ReturnHttpNotAcceptable = true; }
    ).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Custom logger
//builder.Services.AddSingleton<ILogging, LoggingV2>();

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
