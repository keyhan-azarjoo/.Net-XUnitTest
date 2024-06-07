using Domain.FirstLearning;
using Microsoft.AspNetCore.Mvc;
namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculateController : ControllerBase
    {
        private readonly IUserService _userService;

        public CalculateController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {

            // We Call this function to check it in the TestUserController => Get_OnSuccess_InvokesUserServiceExactlyOnce to check howmany time this function has been invoked.
            var users = await _userService.GetAllUsers();
            if (users.Any())
            {
                return Ok(users);
            }

            return NotFound();
        }


        [HttpGet("Add({left}/{right})")]
        public int add(int left, int right)
        {

            var result = new Calculator().Sum(left, right);
            return result;
        }
    }
}
