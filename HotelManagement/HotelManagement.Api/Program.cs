using HotelManagement.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddServicesConfiguration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());

// Add Database to the container.
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("api", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});

builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//using (var scope = app.Services.CreateScope())
//using (var context = scope.ServiceProvider.GetService<AppDbContext>())
//{
//    context.Database.EnsureCreated();
//}

app.UseCors("api");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
