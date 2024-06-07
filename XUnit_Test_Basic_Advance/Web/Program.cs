using Domain.Advance.Config;

var builder = WebApplication.CreateBuilder(args);



// Inject My Service to the application
ConfigurServices(builder.Services);




// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();






// You can configur Your service here
void ConfigurServices(IServiceCollection services)
{
    services.AddTransient<IUserService, UserService>();

    services.AddHttpClient<IUserService, UserService>();

    services.Configure<UserApiOptions>(
        builder.Configuration.GetSection("UsersApiOptions"));// We add the url to the configuration as our real API. we dont use it in the test.
}