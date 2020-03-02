using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Day28_Identity_CodeAlong.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Day28_Identity_CodeAlong.Controllers
{
    [Authorize] //user is required to be logged in with [Authorize]
    public class PetsController : Controller
    {
        //setup Db Context
        private readonly IdentityUserPetsDbContext _context;

        public PetsController(IdentityUserPetsDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            //allows us to access the current (loged in) user's Id (PK of AspNetUsers)
            //HINT: ONLY USE THIS IN AUTHORIZED VIEWS (or include validation to be sure the id is not null)
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //sets up list of pets where the OwnerId(Pets Table) matches the Id of the logged in user(AspNetUsers Table)
            List<Pets> thisUsersPets = _context.Pets.Where(x => x.OwnerId == id).ToList();

            return View(thisUsersPets);
        }

        [HttpGet]
        public IActionResult AddPet()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult AddPet(Pets newPet)
        {                           //gets logged in user ID
            newPet.OwnerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(ModelState.IsValid)
            {
                _context.Pets.Add(newPet);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
        }
    }
}