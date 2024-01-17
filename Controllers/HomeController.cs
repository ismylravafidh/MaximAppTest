using MaximAppTest.DAL;
using MaximAppTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaximAppTest.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Service> services = await _context.Services.ToListAsync();
            return View(services);
        }
    }
}