using ComputerSystem.Common;
using ComputerSystem.Infrastructure;
using ComputerSystem.Infrastructure.Repositories;
using ComputerSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var dbPath = "computersystem.db";
var connectionString = $"Data Source={dbPath}";
builder.Services.AddDbContext<ComputerSystemContext>(options =>
    options.UseSqlite(connectionString));


builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(ICrudServiceAsync<>), typeof(CrudServiceWithRepo<>));

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


using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<ComputerSystemContext>();
	context.Database.EnsureCreated();
}

app.Run();