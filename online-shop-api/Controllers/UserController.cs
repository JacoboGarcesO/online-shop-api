using Microsoft.AspNetCore.Mvc;
using online_shop_api.Models;
using online_shop_api.Services;

namespace online_shop_api.Controllers
{
  [ApiController]
  [Route("api/user")]
  public class UserController : ControllerBase
  {
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
      _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User user)
    {
      try
      {
        await _userService.CreateAsync(user);
        return StatusCode(StatusCodes.Status201Created, user);
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status400BadRequest, new { Error = $"Duplicate email: {user.Email}" });
      }

    }

    [HttpPost("auth")]
    public async Task<ActionResult<User>> Auth(UserCredentials user)
    {
      var result = await _userService.Authenticate(user);

      if (result is null)
      {
        return Unauthorized();
      }

      return StatusCode(StatusCodes.Status200OK, result);
    }
  }
}
