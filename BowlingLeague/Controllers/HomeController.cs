using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private BowlersDbContext _context { get; set; }

        //Constructor
        public HomeController(BowlersDbContext temp)
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            var bowl = _context.Bowlers.ToList();
            return View(bowl);
        }

        [HttpPost]
        public IActionResult MovieForm(Bowler ab)
        {
            ViewBag.Teams = BowlersDbContext.Teams.ToList();
            if (ModelState.IsValid)
            {
                BowlersDbContext.Add(ab);
                BowlersDbContext.SaveChanges();

                return View("Confirmation", ab);
            }
            else
            {
                return View(ab);
            }

        }
        [HttpGet]
        public IActionResult BowlerList()
        {
            var movie = BowlersDbContext.Bowlers
            .Include(x => x.TeamName)
            .OrderBy(x => x.TeamID)
            .ToList();
            return View(Bowler);
        }
        [HttpGet]
        public IActionResult Edit(int BowlerID)
        {
            ViewBag.Teams = BowlersDbContext.Teams.ToList();
            var category = BowlersDbContext.Bowlers.Single(x => x.BowlerID == BowlerID);
            return View("BowlerForm", Team);
        }
        [HttpPost]
        public IActionResult Edit(Bowler ab)
        {
            BowlersDbContext.Update(ab);
            BowlersDbContext.SaveChanges();
            return RedirectToAction("BowlerList");
        }
        [HttpGet]
        public IActionResult Delete(int BowlerID)
        {
            var b = BowlersDbContext.Bowlers.Single(x => x.BowlerID == BowlerID);
            return View(b);
        }
        [HttpPost]
        public IActionResult Delete(Bowler ab)
        {
            BowlersDbContext.Bowlers.Remove(ab);
            BowlersDbContext.SaveChanges();
            return RedirectToAction("BowlerList");
        }
    }
}
