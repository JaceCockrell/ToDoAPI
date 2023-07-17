using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Step 10a. Add CORS funtionality to determine what websites can access the data in this application.
//CORS - Cross Origin Resource Sharing and by default browsers use this to block 
//websites from requesting data unless that website has permission to do so. This code below determines
// what websites have access to CORS with this API.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("OriginPolicy", "http://localhost:3000", "http://todo.jacecockrell.com").AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ToDoAPI.Models.ToDoContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ResourcesDB"));
        //The string above should match the connection string name in appsetting.json
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

app.UseCors();

app.Run();
