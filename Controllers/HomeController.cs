using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using assignment10.Models;
using Microsoft.EntityFrameworkCore;
using assignment10.Models.ViewModels;

namespace assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //create variable to hold the db context
        private readonly BowlingLeagueContext context;

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index(long? id, string team, int pageNum = 1)
        {
            //number of results per page
            int pageSize = 5;

            //set the selected team name to the view bag under the Team attribute
            ViewBag.Team = team;

            //return function calls a new Index view model and populates it
            return View(new IndexViewModel
            {
                Bowlers = (context.Bowlers
                .Where(x => x.TeamId == id || id == null)
                .OrderBy(x => x.BowlerFirstName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                ),

                //page number info
                PageNumberingInfo = new PageNumberingInfo
                {
                    itemsPerPage = pageSize,
                    currentPage = pageNum,
                    totalItems = (id == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(x => x.TeamId == id).Count())
                },

                team = team
            }) ; 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
