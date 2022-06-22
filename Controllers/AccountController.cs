using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.Models;
using WebApi.Services.Account;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountController:ControllerBase
{
    private readonly AccountService _accountService;
    private readonly UserService _userService;

    public AccountController(AccountService accountService, UserService userService)
    {
        _accountService = accountService;
        _userService = userService;
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<TokenDto> Login(LoginDto loginDto)
    {
        
        return await _accountService.Login(loginDto);
    }
    
    //register
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (ModelState.IsValid == false) return BadRequest(registerDto);
        var result = await _accountService.Register(registerDto);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpGet("AssignRole")]
    public async Task<bool> AssignRoleToUser(RoleDto role)
    {
       return await _userService.AssignUserRole(role);
    }
    
    //get user list
    [HttpGet("getUserList")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetUserList()
    {
        return Ok(await _userService.GetUsers());
    }
   
}