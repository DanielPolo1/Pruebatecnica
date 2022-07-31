using Microsoft.AspNetCore.Mvc;
using Pruebatecnica.Business.Abstract;
using Pruebatecnica.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pruebatecnica.Controllers
{
    public class LandingController : Controller
    {
        private readonly ISedes _sedeService;
        private readonly AppDbContext _context;
        public LandingController(ISedes sedes, AppDbContext context)
        {
            _sedeService = sedes;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _sedeService.ObtenerListaSedes());
        }
    }
}
