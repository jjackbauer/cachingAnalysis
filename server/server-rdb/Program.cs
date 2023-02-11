using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
var conn = config.GetConnectionString("PostgresString");

builder.Services.AddDbContext<DomainDbContext>
(
    
    opt => opt.UseNpgsql(conn)
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DomainDbContext>();
    db.Database.Migrate();
}


using (var scope = app.Services.CreateScope())
{
    var repository = scope.ServiceProvider.GetRequiredService<IBalanceRepository>();
    await Dataset.EraseDatabase(repository);
    await Dataset.PopulateDatabase(repository, 10000);    
}


//app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
