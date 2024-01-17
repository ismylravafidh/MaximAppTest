using MaximAppTest.Areas.Manage.ViewModels;
using MaximAppTest.DAL;
using MaximAppTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaximAppTest.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class ServiceController : Controller
    {
        AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Service> services = await _context.Services.ToListAsync();
            return View(services);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateVm serviceVm)
        {
            if (!ModelState.IsValid)
            {
                return View(serviceVm);
            }
            if (serviceVm==null)
            {
                ModelState.AddModelError("", "Service Null Ola Bilmez");
            }
            Service service = new Service()
            {
                Title = serviceVm.Title,
                Description = serviceVm.Description,
                Icon = serviceVm.Icon
            };
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var oldService = await _context.Services.Where(s=>s.Id==id).FirstOrDefaultAsync();
            if (oldService == null)
            {
                ModelState.AddModelError("", "Service Bosdur.");
            }
            ServiceUpdateVm serviceVm = new ServiceUpdateVm()
            {
                Title = oldService.Title,
                Description = oldService.Description,
                Icon = oldService.Icon
            };
            return View(serviceVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ServiceUpdateVm serviceVm)
        {
            if(!ModelState.IsValid)
            {
                return View(serviceVm);
            }
            if (serviceVm == null)
            {
                ModelState.AddModelError("", "Service Bos Ola Bilmez");
            }
            Service oldService = await _context.Services.FindAsync(serviceVm.Id);

            oldService.Title=serviceVm.Title;
            oldService.Description=serviceVm.Description;
            oldService.Icon = serviceVm.Icon;
            _context.Services.Update(oldService);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Service service = await _context.Services.FindAsync(id);
            if (service==null)
            {
                ModelState.AddModelError("", "Service Bosdur");
            }
            _context.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
