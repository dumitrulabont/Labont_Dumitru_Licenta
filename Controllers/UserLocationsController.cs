using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labont_Dumitru_Licenta.Data;
using Labont_Dumitru_Licenta.Models;
using Microsoft.AspNetCore.Identity;
using Labont_Dumitru_Licenta.Areas.Identity.Data;

namespace Labont_Dumitru_Licenta.Controllers
{
    public class UserLocationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Labont_Dumitru_LicentaUser> _userManager;
        public UserLocationsController(ApplicationDbContext context, UserManager<Labont_Dumitru_LicentaUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserLocations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserLocations.Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _context.UserLocations
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userLocation == null)
            {
                return NotFound();
            }

            return View(userLocation);
        }

        // GET: UserLocations/Create
        public IActionResult Create()
        {
            //ViewData["Labont_Dumitru_LicentaUserID"] = new SelectList(_context.Users, "Id", "Id");
            
            //Locatia se poate crea doar daca nu exista o locatie deja pentru utilizatorul curent
            var id = _userManager.GetUserId(User);
            var userLocation =  _context.UserLocations
                .Include(u => u.User)
                .FirstOrDefault(m => m.Labont_Dumitru_LicentaUserID.Equals(id));
            if(userLocation != null)
            {
                return NotFound();
               

            }
            return View();
        }

        // POST: UserLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,County,City,Street,StreetNumber,Labont_Dumitru_LicentaUserID")] UserLocation userLocation)
        {
            if (ModelState.IsValid)
            {
                //id-ul utilizatorului curent va fi FK cu tabela users 
                userLocation.Labont_Dumitru_LicentaUserID = _userManager.GetUserId(User);
                _context.Add(userLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Labont_Dumitru_LicentaUserID"] = new SelectList(_context.Users, "Id", "Id", userLocation.Labont_Dumitru_LicentaUserID);
            return View(userLocation);
        }

        // GET: UserLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _context.UserLocations.FindAsync(id);
            if (userLocation == null)
            {
                return NotFound();
            }
            ViewData["Labont_Dumitru_LicentaUserID"] = new SelectList(_context.Users, "Id", "Id", userLocation.Labont_Dumitru_LicentaUserID);
            return View(userLocation);
        }

        // POST: UserLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,County,City,Street,StreetNumber")] UserLocation userLocation)
        {
            userLocation.Labont_Dumitru_LicentaUserID = _userManager.GetUserId(User);

            if (id != userLocation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLocationExists(userLocation.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Labont_Dumitru_LicentaUserID"] = new SelectList(_context.Users, "Id", "Id", userLocation.Labont_Dumitru_LicentaUserID);
            return View(userLocation);
        }

        // GET: UserLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLocation = await _context.UserLocations
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userLocation == null)
            {
                return NotFound();
            }

            return View(userLocation);
        }

        // POST: UserLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userLocation = await _context.UserLocations.FindAsync(id);
            _context.UserLocations.Remove(userLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLocationExists(int id)
        {
            return _context.UserLocations.Any(e => e.ID == id);
        }
    }
}
