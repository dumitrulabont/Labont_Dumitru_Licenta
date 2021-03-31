using Labont_Dumitru_Licenta.Areas.Identity.Data;
using Labont_Dumitru_Licenta.Data;
using Labont_Dumitru_Licenta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labont_Dumitru_Licenta.Controllers
{
    [Authorize(Roles = "consumator")]
    public class ConsumatorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Labont_Dumitru_LicentaUser> _userManager;
        public ConsumatorController(ApplicationDbContext context, UserManager<Labont_Dumitru_LicentaUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string? BrandAndModel, int? Year, string? Location)
        {

            //returneaza toate masinile care sunt disponibile
            var cars = _context.Car.Include(c => c.CarDetail).Include(c => c.CarOwner.UserLocation).Where(c => c.IsAvailable == true);
            //cauta pentru Brand sau Model, 
            if(BrandAndModel != null)
            {
                cars = cars.Where(c => c.CarDetail.Model == BrandAndModel || c.CarDetail.Brand == BrandAndModel);
            }
            if(Year != null)
            {
                cars = cars.Where(c => c.CarDetail.Year == Year);
            }
            if(Location != null)
            {
                cars = cars.Where(c => c.CarOwner.UserLocation.County == Location || c.CarOwner.UserLocation.County == Location);
            }
            return View(await cars.ToListAsync());
        }

        public async Task<IActionResult> DetailsCar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.CarDetail)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (car == null)
            {
                return NotFound();
            }
           
            return View(car);
        }
        
        public async Task<IActionResult> RequestCar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            

            var car = await _context.Car
                .Include(c => c.CarDetail)
                .FirstOrDefaultAsync(m => m.ID == id);

            ViewData["Brand"] = car.CarDetail.Brand;
            ViewData["Model"] = car.CarDetail.Model;
            ViewData["CarId"] = car.ID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestCar([Bind("CarId,StartDate,FinishDate")] Request request)
        {
            if (ModelState.IsValid)
            {
                request.RequestDate = DateTime.Now;
                var car = await _context.Car.FindAsync(request.CarId);
                request.ReciverID = car.CarOwnerID;
                request.SenderID = _userManager.GetUserId(User);
                _context.Requests.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
            //return View(userLocation);
        }
        public async Task<IActionResult> ShowMyRequests()
        {
            //cererile ce au ca si Sender user-ul curent
            var requests = await _context.Requests
                .Include(r => r.Car)
                .Include(r => r.Car.CarDetail)
                .Where(r=> r.SenderID == _userManager.GetUserId(User)).ToListAsync();

            return View(requests);
        }

        public IActionResult Contacts(string? id)
        {
            //se primeste id-ul cererii
            if(id == null)
            {
                Console.WriteLine("id null");
                return NotFound();
            }
            //include userii si locatia acestora
            //var details = _context.UserLocations.Include(u => u.User).Include(u => u.).FirstOrDefault(u => u);

            var details = _context.UserLocations.Include(u => u.User).FirstOrDefault(u => u.User.Id == id);
            
            return View(details);
            //return View(details);
        }
    }

    
}
