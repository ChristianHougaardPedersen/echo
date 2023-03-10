using Application.LogicInterfaces;
using Application.ProviderInterfaces;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserLogic logic;
    private readonly IUserProvider provider;

    public UserController(IUserLogic logic, IUserProvider provider)
    {
        this.logic = logic;
        this.provider = provider;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDTO dto)
    {
        try
        {
            User user = await logic.CreateAsync(dto);
            return Created($"/api/users/{user.Id}", user);
        }
        catch (UserValidationException e)
        {
            return StatusCode(400, e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? username)
    {
        try
        {
            SearchUserParametersDTO parameters = new(username);
            IEnumerable<User> users = await provider.GetAsync(parameters);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}