using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DHB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DHB.VM.Add;

namespace DHB.Controllers
{
    [Authorize]
    public class AddController:Controller
    {
        DHBContext db;

        public AddController(DHBContext user)
        {
            db = user;
        }
        [HttpGet]
        public IActionResult Plan()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Plan(VM.Add.Plan plan)
        {

        }
    }
}
