using Crud_test.Data;
using Crud_test.Models;
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
        public IActionResult Login(Users user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                if (existingUser != null)
                {
                    // Successful login
                    return RedirectToAction("Index", "Products");
                }
                ModelState.AddModelError("", "Invalid username or password.");
                // _context.Users.Add(user);
                // _context.SaveChanges();
            }
            return View(user);
        }
    }
}
