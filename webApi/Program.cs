// using Microsoft.EntityFrameworkCore;
// using webApi.Context;
// using webApi.Features.Services;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();

// builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddSwaggerGen();

// var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseMySql(
//         mySqlConnection,
//         ServerVersion.AutoDetect(mySqlConnection)
//     )
// );

// builder.Services.AddScoped<ECDImportService>();

// var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();

//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();

using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using webApi.Context;
using webApi.Features.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = long.MaxValue;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mysqlOptions =>
        {
            mysqlOptions.CommandTimeout(3600);
        });

    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    options.EnableSensitiveDataLogging(false);

    options.EnableDetailedErrors(false);
});

builder.Services.AddScoped<ECDImportService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();