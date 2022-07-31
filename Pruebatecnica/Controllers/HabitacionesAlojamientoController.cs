using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Pruebatecnica.Business.Abstract;
using Pruebatecnica.Model.Entities;
using Pruebatecnica.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pruebatecnica.ViewModels.Sedes;
using Pruebatecnica.ViewModels.Reserva;
using Pruebatecnica.ViewModels.HabitacionesAlojamiento1;
using Pruebatecnica.Model.DAL;
using Microsoft.EntityFrameworkCore;

namespace Pruebatecnica.Controllers
{
    public class HabitacionesAlojamientoController : Controller
    {
        private readonly IReservaService _reservaService;
        private readonly ISedes _sedeService;
        private readonly AppDbContext _context;
        private readonly IHabitacionesService _habitacionesAlojamiento;
        private readonly IClienteService _clienteService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HabitacionesAlojamientoController(IReservaService reservaService, AppDbContext context, IClienteService clienteService, ISedes sedeService, IHabitacionesService habitacionesAlojamiento , IWebHostEnvironment hostEnvironment)
        {
            _sedeService = sedeService;
            _context = context;
            _reservaService = reservaService;
            _clienteService = clienteService;
            _habitacionesAlojamiento = habitacionesAlojamiento;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _habitacionesAlojamiento.ObtenerListaHabitacionesAlojamiento());
        }
        // HA es habitacionesAlojamiento

        [HttpGet]
        public IActionResult CrearHA()
        {
           
            return View(new HAViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> CrearHA(HAViewModel hAViewModel, int id)
        {
            //preguntamos si el modelo es válido o no (comprueba validaciones)
            if (ModelState.IsValid)
            {
                       
                HabitacionesAlojamiento habitacionesAlojamiento = new()
                {
                    Capacidad = hAViewModel.Capacidad,
                    NumeroHabitacionAlojamiento = hAViewModel.NumeroHabitacionAlojamiento,
                    
                    TarifaEspecial = hAViewModel.TarifaEspecial,
                    TarifaOrdinario= hAViewModel.TarifaOrdinario,                   
                    Estado = true
                    
                };                              
                try
                {
                
                    await _habitacionesAlojamiento.GuardarHabitacionesAlojamiento(habitacionesAlojamiento);
                    TempData["Accion"] = "Registrar Habitacion / alojamiento";
                    TempData["Mensaje"] = "guardado con éxito";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Error realizando la operación";
                    return RedirectToAction("Index");
                }
            }
            TempData["Accion"] = "Error";
            TempData["Mensaje"] = "Ingresaste un valor inválido";
            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> EditarHA(int id)
        {
            HabitacionesAlojamiento habitacionesAlojamiento = await _habitacionesAlojamiento.ObtenerHAPorId(id);
            ViewBag.id = _habitacionesAlojamiento.ObtenerHAPorId(id);
            HAViewModel hAViewModel = new()
            {
                
                Capacidad = habitacionesAlojamiento.Capacidad,
                NumeroHabitacionAlojamiento = habitacionesAlojamiento.NumeroHabitacionAlojamiento,
          
                TarifaEspecial = habitacionesAlojamiento.TarifaEspecial,
                TarifaOrdinario = habitacionesAlojamiento.TarifaOrdinario,
                Estado = true
            };
            return View(hAViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditarHA(HAViewModel hAViewModel)
        {
            {
                if (ModelState.IsValid)
                {
                 
                    HabitacionesAlojamiento habitacionesAlojamiento = new()
                    {
                        Capacidad = hAViewModel.Capacidad,
                        NumeroHabitacionAlojamiento = hAViewModel.NumeroHabitacionAlojamiento,             
                        TarifaEspecial = hAViewModel.TarifaEspecial,
                        TarifaOrdinario = hAViewModel.TarifaOrdinario,
                        Estado = true
                    };
                                 
                    try
                    {
                        await _habitacionesAlojamiento.EditarHabitacionesAlojamiento(habitacionesAlojamiento);
                        TempData["Accion"] = "Editar Habitacion / alojamiento ";
                        TempData["Mensaje"] = "Editado correctamente";
                        return RedirectToAction("index");
                    }
                    catch (Exception)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "Ocurrió un error";
                        return RedirectToAction("index");
                    }

                }
                else
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Ocurrió un error";
                    return View(hAViewModel);
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarHA(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HabitacionesAlojamiento habitacionesAlojamiento = await _habitacionesAlojamiento.ObtenerHAPorId(id);

                    await _habitacionesAlojamiento.EliminarHabitacionesAlojamiento(id);
                    TempData["Accion"] = "Eliminar Habitacion / alojamiento";
                    TempData["Mensaje"] = "Eeliminado correctamente";
                    return RedirectToAction("index");
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Ocurrió un error";
                    return RedirectToAction("index");
                }
            }
            else
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ocurrió un error";
                return RedirectToAction("index");
            }

        }

        public async Task<IActionResult> EditarEstado(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Error";
                return RedirectToAction("index");
            }
            Reserva reserva = await _reservaService.ObtenerReservaPorId(id.Value);
            try
            {
                if (reserva.Estado == true)
                    reserva.Estado = false;
                else if (reserva.Estado == false)
                    reserva.Estado = true;

                await _reservaService.EditarReserva(reserva);
                TempData["Accion"] = "EditarEstado";
                TempData["Mensaje"] = "Estado editado correctamente";
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                TempData["Accion"] = "Error";
                TempData["Mensaje"] = "Ingresaste un valor inválido";
                return RedirectToAction("index");
            }
        }

        [HttpGet]
        public IActionResult mostrarInsumo(string rutaImagen)
        {
            ViewBag.RutaImagen = rutaImagen;
            return View();
        }
    }
}