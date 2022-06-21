using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Services.Account;

public class UserService
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<IdentityUser> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }
    
    //get user list
    public async Task<List<UserDto>> GetUsers()
    {
        return await _userManager.Users.Select(user=> new UserDto()
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.UserName
        }).ToListAsync();
    }
    
    //get user by id
    public async Task<IdentityUser> GetUserById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
}