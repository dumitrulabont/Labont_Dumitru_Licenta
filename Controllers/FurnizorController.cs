using Labont_Dumitru_Licenta.Areas.Identity.Data;
using Labont_Dumitru_Licenta.Data;
using Labont_Dumitru_Licenta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Labont_Dumitru_Licenta.Controllers
{


    [Authorize(Roles = "furnizor")]
    public class FurnizorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Labont_Dumitru_LicentaUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FurnizorController(ApplicationDbContext context, UserManager<Labont_Dumitru_LicentaUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        private  bool LocationAdded()
        {
            var id = _userManager.GetUserId(User);
            var userLocation = _context.UserLocations
                .Include(u => u.User)
                .FirstOrDefault(m => m.Labont_Dumitru_LicentaUserID.Equals(id));
            if(userLocation == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool UserLocationExists(int id)
        {
            return _context.UserLocations.Any(e => e.ID == id);
        }
        public async Task<IActionResult> Index()
        {
            var cars = _context.Car.Where(car => car.CarOwnerID == _userManager.GetUserId(User)).Include(c => c.CarDetail);
            var result = LocationAdded();
            ViewData["LocationAdded"] = result;
            return View(await cars.ToListAsync());
           

            return View();
        }
        public IActionResult AddLocation()
        {
            //verifica daca a fost adaugata o locatie pentru utilizatorul curent
            if (LocationAdded())
            {
                return View();
            }
            else
            {
                //daca a fost adaugata locatia se redirectioneaza catre index
                return RedirectToAction(nameof(Index));
            }

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLocation([Bind("ID,County,City,Street,StreetNumber,Labont_Dumitru_LicentaUserID")] UserLocation userLocation)
        {
            if (ModelState.IsValid)
            {
                //id-ul utilizatorului curent va fi FK cu tabela users 
                userLocation.Labont_Dumitru_LicentaUserID = _userManager.GetUserId(User);
                _context.Add(userLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["Labont_Dumitru_LicentaUserID"] = new SelectList(_context.Users, "Id", "Id", userLocation.Labont_Dumitru_LicentaUserID);
            return View(userLocation);
        }
        //edit location
        public async Task<IActionResult> EditLocation()
        {
           

            var id = _userManager.GetUserId(User);

            if (id == null)
            {

                return NotFound();
            }

            var userLocation = _context.UserLocations
                .Include(u => u.User)
                .FirstOrDefault(m => m.Labont_Dumitru_LicentaUserID.Equals(id));
            
            //var userLocation = await _context.UserLocations.FindAsync(id);
            if (userLocation == null)
            {
                return NotFound();
            }
            //ViewData["Labont_Dumitru_LicentaUserID"] = new SelectList(_context.Users, "Id", "Id", userLocation.Labont_Dumitru_LicentaUserID);
            return View(userLocation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLocation([Bind("ID,County,City,Street,StreetNumber")] UserLocation userLocation)
        {
            userLocation.Labont_Dumitru_LicentaUserID = _userManager.GetUserId(User);

            

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
            //ViewData["Labont_Dumitru_LicentaUserID"] = new SelectList(_context.Users, "Id", "Id", userLocation.Labont_Dumitru_LicentaUserID);
            return View(userLocation);
        }

        public async Task<IActionResult> DetailsCar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.CarDetail)
                .Include(c => c.Contract)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }


        //
        public async Task<IActionResult> AddCar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCar([Bind("Brand,Model,Price,Color,Year,CarType")] CarDetail carDetail, [Bind("IsAvailable")] Car car)
        {
            _context.CarDetails.Add(carDetail);
            _context.SaveChanges();
            int CarDetailID = carDetail.ID;
            car.CarDetailID = CarDetailID;
            car.CarOwnerID = _userManager.GetUserId(User);
            _context.Car.Add(car);
            _context.SaveChanges();

            //calea catre folderul wwwroot ce contine fisierele statice
            string path = _hostingEnvironment.WebRootPath;
            //acces la fisierele trimise din form
            var files = HttpContext.Request.Form.Files;

            //obtinerea cailor si a extensilor
            var image_path = Path.Combine(path, @"images");
            var extension = Path.GetExtension(files[0].FileName);

            using (var fileStream = new FileStream(Path.Combine(image_path,car.ID + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }
            car.Image = @"\" + @"images" + @"\" + car.ID + extension;
             _context.SaveChanges();
                //Console.WriteLine("Car brand: " + car.IsAvailable);

            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> EditCar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            //var car = await _context.Car.FindAsync(id);
            var car = await _context.Car.Include(c => c.CarDetail).FirstOrDefaultAsync(c => c.ID == id);
            if (car == null)
            {
                
                return NotFound();
            }
            //Console.WriteLine(car.CarDetail.Year);
            if(car.ID == null)
            {
                Console.WriteLine("Este NULL");
            }
            else
            {
                Console.WriteLine("NU este null");
                Console.WriteLine(car.IsAvailable.ToString());
            }
            return View(car);
        }

        //// POST: Cars/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCar(int id, [Bind("ID,IsAvailable,CarDetailID")] Car car, [Bind("Brand, Model, Price, Color, Year, CarType")] CarDetail carDetail)
        {
            if (id != car.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine("model valid");
                try
                {
                    

                    //car.CarOwnerID = _userManager.GetUserId(User);
                    _context.Entry(car).Property(c => c.CarOwnerID).IsModified = false;
                    _context.SaveChanges();
                    
                    await _context.SaveChangesAsync();
                    var carDetailsUpdate = await _context.CarDetails.FindAsync(car.CarDetailID);
                    carDetailsUpdate.Brand = carDetail.Brand;
                    carDetailsUpdate.Model = carDetail.Model;
                    carDetailsUpdate.Price = carDetail.Price;
                    carDetailsUpdate.Color = carDetail.Color;
                    carDetailsUpdate.Year = carDetail.Year;
                    carDetailsUpdate.CarType = carDetail.CarType;
                    await _context.SaveChangesAsync();

                    //calea catre folderul wwwroot ce contine fisierele statice
                    string path = _hostingEnvironment.WebRootPath;
                    //acces la fisierele trimise din form
                    var files = HttpContext.Request.Form.Files;
                    var testArray =   files.ToArray();
                    //daca a fost incarcat un fisier se suprascrie cel existent
                    if (testArray.Length > 0 )
                    {
                        if(System.IO.File.Exists(car.Image))
                        {
                            System.IO.File.Delete(car.Image);
                        }
                        Console.WriteLine("Exista un fisier");
                        //obtinerea cailor si a extensilor
                        var image_path = Path.Combine(path, @"images");
                        var extension = Path.GetExtension(files[0].FileName);
                        //DateTime.Now permite ca dupa ce a fost modificata o poza aceasta sa nu mai fie preluata din cache
                        using (var fileStream = new FileStream(Path.Combine(image_path, car.ID  + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        car.Image = @"\" + @"images" + @"\" + car.ID  + extension;
                        _context.SaveChanges();
                        Console.WriteLine("nume imagine : " + car.Image);
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CarDetailID"] = new SelectList(_context.Set<CarDetail>(), "ID", "ID", car.CarDetailID);
            //ViewData["ContractID"] = new SelectList(_context.Set<Contract>(), "ID", "ID", car.ContractID);
            return View(car);
        }

        public async Task<IActionResult> DeleteCar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.CarDetail)
                //.Include(c => c.Contract)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCarConfirmed(int id)
        {
            var car = await _context.Car.FindAsync(id);
            _context.Car.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MyRequests()
        {
            //cereriele in care utilizatorul curent este Reciver
            var requests = await _context.Requests.Include(r => r.Car)
                .Include(r => r.Car.CarDetail)
                .Include(r => r.Sender)
                .Where(r => r.ReciverID == _userManager.GetUserId(User)).ToListAsync();

            return View(requests);
        }

      
        public async Task<IActionResult> ConfirmRequestAsync(int? id)
        {
            if(id == null)
            {
                Console.WriteLine("Erorare aici");
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmRequestAsync(int ID, bool RequestState)
        {
            Console.WriteLine(ID.ToString());
            Console.WriteLine(RequestState.ToString());

            var request = await _context.Requests.FindAsync(ID);
           if(request == null)
            {
                return NotFound();
            }
            request.RequestState = RequestState;
            _context.SaveChanges();
            return RedirectToAction(nameof(MyRequests));
        }

    }
}
