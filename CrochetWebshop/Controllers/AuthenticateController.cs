using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CrochetWebshop.Controllers
{
    public class AuthenticateController : Controller
    {
        private iUserService _userService;

        public AuthenticateController(iUserService iUserService)
        {
            _userService = iUserService;
        }

        [HttpGet("LogIn")]
        public IActionResult LogIn()
        {
            return View();
        }

        /*[HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([Bind("Email,Password")] User user)
        {
            if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Password))
            {
                return View(user);
            }
            else
            {
                if (await _userService.ValidateUser(user.Email, user.Password) == true)
                {
                    User? retrievedUser = await _userService.GetUserByEmailAsync(user.Email);
                    if (retrievedUser is not null)
                    {
                        HttpContext.Session.SetInt32("UserId", retrievedUser.UserId);
                        HttpContext.Session.SetString("UserEmail", retrievedUser.Email);
                        HttpContext.Session.SetString("UserRole", retrievedUser.Role);
                        return RedirectToAction(nameof(Register));
                    }
                    else
                    {
                        return View(user);
                    }
                }
                else
                {
                    return View(user);
                }
            }
        }*/

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([Bind("Email,Password")] User user)
        {
            if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Password))
            {
                return View(user);
            }
            else
            {
                if (await _userService.ValidateUser(user.Email, user.Password) == true)
                {
                    User? retrievedUser = await _userService.GetUserByEmailAsync(user.Email);
                    if (retrievedUser is not null)
                    {
                        HttpContext.Session.SetInt32("UserId", retrievedUser.UserId);
                        HttpContext.Session.SetString("UserEmail", retrievedUser.Email);
                        HttpContext.Session.SetString("UserRole", retrievedUser.Role);
                        var claims = new List<Claim>
                        { new Claim(ClaimTypes.Name, retrievedUser.Email),
                            new Claim(ClaimTypes.Role, retrievedUser.Role) };

                        var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        // Maak de authenticatie cookie
                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View(user);
                    }
                }
                else
                {
                    return View(user);
                }
            }
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([Bind("Email,Password")] User user)
        {
            if (await _userService.AddUserAsync(user) == true)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            else { return View(user); }
        }
    }
}