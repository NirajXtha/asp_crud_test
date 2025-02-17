using System.Security.Claims;
using Crud_test.Data;
using Crud_test.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Crud_test.Controllers
{
    public class UserController : Controller
    {
        private readonly ProductContext _context;

        public UserController(ProductContext context)
        {
            _context = context;
        }
        // GET: UserController
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(Users user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username 
                                                                && u.Password == user.Password);
                if (existingUser != null)
                {
                    // Successful login
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, existingUser.Username),
                        new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString())
                    };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    // Sign in the user
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Products");
                }
                ModelState.AddModelError("", "Invalid username or password.");
                // _context.Users.Add(user);
                // _context.SaveChanges();
            }
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToRoute(new { controller = "User", action = "Login" });
        }
    }
}
