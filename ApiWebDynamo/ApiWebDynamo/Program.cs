using ApiWebDynamo.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Agregamos la conexion de MySql
//var dataContext = new DataContext(builder.Configuration.GetConnectionString("WebUsersDatabase"));
//builder.Services.AddSingleton(dataContext);



//builder.Services.AddDbContext<DataContext>(opt =>
//{
//    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
//});



//builder.Services.AddDbContext<DataContext>(b =>
//{
//    b.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), new MySqlServerVersion(new Version(8,0,11)));
//});

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
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

app.MapControllers();

app.Run();
