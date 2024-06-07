using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Domain.Advance.Models;
using Domain.Advance.Config;

public interface IUserService
{
    public Task<List<User>> GetAllUsers();
}

public class UserService : IUserService
{
    // You should add Httpclient to the program.cs =>  services.AddHttpClient<IUserService, UserService>();
    private readonly HttpClient _httpClient;


    // We need to add this to program.csservices.Configure<UserApiOptions>(builder.Configuration.GetSection("UserApiOptions"));
    private readonly UserApiOptions _apiConfig;

    // install Microsoft.Extensions.Options; from nugetpackagemanagment if it is not installed
    public UserService(HttpClient httpClient, IOptions<UserApiOptions> apiConfig)
    {
        // We Create a dummy HttpClient with User data  and passed it here to test.
        _httpClient = httpClient;
        _apiConfig = apiConfig.Value;
    }


    public async Task<List<User>> GetAllUsers()
    {

        // We use the httpclient and call a url as our data source but we put our data befor in the SetupBasicGetResourceList class
        var userResponce = await _httpClient
            .GetAsync(_apiConfig.Endpoint);

        if (userResponce.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<User>();
        }

        var responceContent = userResponce.Content;
        var allUsers = await responceContent.ReadFromJsonAsync<List<User>>();
        return allUsers.ToList();
    }
}
