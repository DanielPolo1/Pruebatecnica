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

namespace Pruebatecnica.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IReservaService _reservaService;
        private readonly IHabitacionesService _habitacionesService;
        private readonly IClienteService _clienteService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ReservaController(IReservaService reservaService, IHabitacionesService habitacionesService , IClienteService clienteService , IWebHostEnvironment hostEnvironment)
        {
            _habitacionesService = habitacionesService;
            _reservaService = reservaService;
            _clienteService = clienteService;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _reservaService.ObtenerListaReserva());
        }

        [HttpGet]
        public async Task<IActionResult> CrearReserva(string id)
        {
            
            ReservaViewModel reserva = new();
            reserva.habitacionesAlojamientos = await _habitacionesService.ObtenerListaHA();
            
            return View(reserva);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CrearReserva(ReservaViewModel reservaViewModel, int id)
        {
            //preguntamos si el modelo es válido o no (comprueba validaciones)
            if (ModelState.IsValid)
            {
                //Cliente cliente = await _clienteService.ObtenerClientePorId(id);

                var DateAndTime = DateTime.Now;
                var Date = DateAndTime.Date.ToString("dd-MM-yyyy");
             
                Reserva reserva = new()
                {
                    FechaLlegada = reservaViewModel.FechaLlegada,
                    FechaSalida = reservaViewModel.FechaSalida,
                    ClienteId = ViewBag.idcliente,
                    Personas = reservaViewModel.Personas,
                    FechaReserva = Date,
                    Total = reservaViewModel.Total,
                    Estado = true
                    
                };
                //Calculamos los dias de un rango determinado de fechas
                DateTime fechaUno = Convert.ToDateTime(reservaViewModel.FechaLlegada).Date;
                DateTime fechados = Convert.ToDateTime(reservaViewModel.FechaSalida).Date;
                TimeSpan difFechas = fechaUno - fechados;
                int days = (int)difFechas.TotalDays;

                string dias = Convert.ToString(days);

                ViewBag.cantidaDias = dias;
                try
                {
                
                    await _reservaService.GuardarReserva(reserva);
                    TempData["Accion"] = "RegistrarSede";
                    TempData["Mensaje"] = "Sede guardado con éxito";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Accion"] = "Error";
                    TempData["Mensaje"] = "Error realizando la operación";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(reservaViewModel);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditarReserva(int id)
        {
            Reserva reserva = await _reservaService.ObtenerReservaPorId(id);
            ReservaViewModel reservaViewModel = new()
            {
                ReservaId = reserva.ReservaId,
                FechaLlegada = reserva.FechaLlegada,
                FechaSalida = reserva.FechaSalida,
                //Cliente = reservaViewModel.cli,
                Personas = reserva.Personas,
                FechaReserva = reserva.FechaReserva,
                Total = reserva.Total,
                Estado = true
            };
            return View(reservaViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditarReserva(ReservaViewModel reservaViewModel)
        {
            {
                if (ModelState.IsValid)
                {
                    Reserva reserva = new()
                    {
                        ReservaId = reservaViewModel.ReservaId,
                        FechaLlegada = reservaViewModel.FechaLlegada,
                        FechaSalida = reservaViewModel.FechaSalida,
                        //Cliente = reservaViewModel.cli,
                        Personas = reservaViewModel.Personas,
                        FechaReserva = reservaViewModel.FechaReserva,
                        Total = reservaViewModel.Total,
                        Estado = true
                    };               
                
                    try
                    {
                        await _reservaService.EditarReserva(reserva);
                        TempData["Accion"] = "EditarProducto";
                        TempData["Mensaje"] = "Insumo editado correctamente";
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
                    return View(reservaViewModel);
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarReserva(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Reserva reserva = await _reservaService.ObtenerReservaPorId(id);
                
                    await _reservaService.EliminarReserva(id);
                    TempData["Accion"] = "EliminarProducto";
                    TempData["Mensaje"] = "Producto eliminado correctamente";
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
    }
}