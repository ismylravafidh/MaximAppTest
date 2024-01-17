using MaximAppTest.DAL;
using MaximAppTest.Models;
using MaximAppTest.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaximAppTest.Controllers
{
    public class AccountController : Controller
    {
        AppDbContext _context;
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            if (login == null) throw new Exception("Error");
            AppUser user = await _userManager.FindByEmailAsync(login.UserNameOrEmail) ?? await _userManager.FindByNameAsync(login.UserNameOrEmail);
            if (user == null) throw new Exception("Username/Email or Password incorrect");
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password,false);
            if (!result.Succeeded) throw new Exception("Username/Email or Password incorrect");
            await _signInManager.SignInAsync(user, isPersistent: true);
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            if (register == null) throw new Exception("Error");
            AppUser user = new AppUser()
            {
                Name = register.Name,
                Surname = register.Surname,
                UserName = register.UserName,
                Email = register.Email,
            };
            AppUser oldUserEmailResult = await _context.Users.FirstOrDefaultAsync(s => s.Email == user.Email);
            if (oldUserEmailResult != null) throw new Exception("Bu Emaille qeydiyuyatdan kecilib");

            AppUser oldUserUserNameResult = await _context.Users.FirstOrDefaultAsync(s => s.Email == user.Email);
            if (oldUserUserNameResult != null) throw new Exception("Bu userName istifade olunub");

            var result = await _userManager.CreateAsync(user,register.Password);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> LogOut()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
