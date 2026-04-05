using Microsoft.EntityFrameworkCore;
using TaskManagementSystemApi.Data;
using TaskManagementSystemApi.Data.Seed;
using TaskManagementSystemApi.Services;
using TaskManagementSystemApi.Services.Interfaces;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
// 本機開發環境採user secret 佈署環境採environment variable
/*var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("DB_CONNECTION");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string is not configured.");
}*/
// testing
var connectionString = "Server=tcp:taskdemo-sql-lee.database.windows.net,1433;Initial Catalog=TaskManagementSystemDb;User ID=sqladmin;Password=TaskDemo123!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

Console.WriteLine($"DefaultConnection from config is null: {builder.Configuration.GetConnectionString("DefaultConnection") == null}");

var rawDbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION");
Console.WriteLine($"DB_CONNECTION is null: {rawDbConnection == null}");

if (rawDbConnection != null)
{
    Console.WriteLine($"DB_CONNECTION length: {rawDbConnection.Length}");
    Console.WriteLine($"DB_CONNECTION first 20 chars: [{rawDbConnection.Substring(0, Math.Min(20, rawDbConnection.Length))}]");
    Console.WriteLine($"DB_CONNECTION first char code: {(int)rawDbConnection[0]}");
}

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // 將 API 輸出的 JSON 欄位名稱轉成 snake_case
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 註冊 AppDbContext，並使用 SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 註冊Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskCommentService, TaskCommentService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowFrontend");

app.MapControllers();

// code first，初次產生範例資料用
if(app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        DbSeeder.Seed(dbContext);
    }

}

app.Run();
