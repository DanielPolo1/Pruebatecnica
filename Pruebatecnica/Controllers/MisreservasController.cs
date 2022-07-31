using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Pruebatecnica.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pruebatecnica.Controllers
{
    public class MisreservasController : Controller
    {
     
        public IActionResult Misreservas()
        {
            return View();
        }
    }
}
