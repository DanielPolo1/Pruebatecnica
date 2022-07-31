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

namespace Pruebatecnica.Controllers
{
    public class SedesController : Controller
    {
        private readonly ISedes _sedeService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SedesController(ISedes sedeService, IWebHostEnvironment hostEnvironment)
        {
            _sedeService = sedeService;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _sedeService.ObtenerListaSedes());
        }

        [HttpGet]
        public IActionResult CrearSede()
        {
            return View(new SedesViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> CrearSede(SedesViewModel sedesViewModel)
        {         
            if (ModelState.IsValid)
            {             
                SedesRecreativasApartamento sedesRecreativasApartamento = new()
                {
                    Nombre = sedesViewModel.Nombre,
                    Tipo = sedesViewModel.Tipo,
                    Ubicacion = sedesViewModel.Ubicacion,
                    Descripcion = sedesViewModel.Descripcion,
                    Estado = true
                };
              
                string path = null;
                string wwwRootPath = null;

                // si se utiliza una imagen entonces
                if (sedesViewModel.Imagen != null)
                {
                    //obtenemos la ruta raiz de nuestro proyecto
                    wwwRootPath = _hostEnvironment.WebRootPath;
                    //obtenemos el nombre de la imagen
                    string nombreImagen = Path.GetFileNameWithoutExtension(sedesViewModel.Imagen.FileName);
                    //obtenemos la extensión de la imagen .jpg - .png etc
                    string extension = Path.GetExtension(sedesViewModel.Imagen.FileName);
                    //concatenamos el nombre de la imagen con el año-minuto-segundos-fraciones de segundo + la extensión
                    sedesRecreativasApartamento.RutaImagen = nombreImagen + DateTime.Now.ToString("yymmssfff") + extension;
                    //Obetenemos la ruta en donde vamos a guardar la imagen
                    path = Path.Combine(wwwRootPath + "/imagenes/" + sedesRecreativasApartamento.RutaImagen);
                }
                try
                {
                    // si se va a guardar una imagen
                    if (path != null)
                    {
                        //copiamos la imagen a la ruta especifica
                       using var fileStream = new FileStream(path, FileMode.Create);
                        await sedesViewModel.Imagen.CopyToAsync(fileStream);
                    }

                    await _sedeService.RegistrarSede(sedesRecreativasApartamento);
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
                return View(sedesViewModel);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditarSede(int id)
        {
            SedesRecreativasApartamento sedesRecreativasApartamento = await _sedeService.ObtenerSedePorId(id);
            SedesViewModel sedesViewModel = new()
            {
                SedesId = sedesRecreativasApartamento.SedesId,
                Nombre = sedesRecreativasApartamento.Nombre,
                Tipo = sedesRecreativasApartamento.Tipo,
                Ubicacion = sedesRecreativasApartamento.Ubicacion,
                Descripcion = sedesRecreativasApartamento.Descripcion,
                Estado = sedesRecreativasApartamento.Estado,
                RutaImagen = sedesRecreativasApartamento.RutaImagen
            };
            return View(sedesViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditarSede(SedesViewModel sedesViewModel)
        {
            {
                if (ModelState.IsValid)
                {
                    SedesRecreativasApartamento sedesRecreativasApartamento = new()
                    {
                        SedesId = sedesViewModel.SedesId,
                        Nombre = sedesViewModel.Nombre,
                        Tipo = sedesViewModel.Tipo,
                        Ubicacion =  sedesViewModel.Ubicacion,
                        Descripcion = sedesViewModel.Descripcion,
                        Estado = sedesViewModel.Estado,
                    };            
                    string wwwRootPath = null;
                    string path = null;

                    if (sedesViewModel.Imagen != null)
                    {
                        wwwRootPath = _hostEnvironment.WebRootPath;
                        string nombreImagen = Path.GetFileNameWithoutExtension(sedesViewModel.Imagen.FileName);
                        string extension = Path.GetExtension(sedesViewModel.Imagen.FileName);
                        sedesRecreativasApartamento.RutaImagen = nombreImagen + DateTime.Now.ToString("yymmssfff") + extension;
                        path = Path.Combine(wwwRootPath + "/imagenes/" + sedesRecreativasApartamento.RutaImagen);
                    }

                    try
                    {
                        if (path != null)
                        {
                            using var fileStream = new FileStream(path, FileMode.Create);
                            await sedesViewModel.Imagen.CopyToAsync(fileStream);
                            if (sedesViewModel.RutaImagen != null)
                            {
                                FileInfo file = new FileInfo(wwwRootPath + "/imagenes/" + sedesViewModel.RutaImagen);
                                file.Delete();
                            }
                        }
                        else
                        {
                            sedesRecreativasApartamento.RutaImagen = sedesViewModel.RutaImagen;
                        }


                        await _sedeService.EditarSede(sedesRecreativasApartamento);
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
                    return View(sedesViewModel);
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarSede(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SedesRecreativasApartamento sedesRecreativasApartamento = await _sedeService.ObtenerSedePorId(id);

                    if (sedesRecreativasApartamento.RutaImagen != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        FileInfo file = new FileInfo(wwwRootPath + "/imagenes/" + sedesRecreativasApartamento.RutaImagen);
                        file.Delete();
                    }

                    await _sedeService.EliminarSede(id);
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
            SedesRecreativasApartamento sedesRecreativasApartamento = await _sedeService.ObtenerSedePorId(id.Value);
            try
            {
                if (sedesRecreativasApartamento.Estado == true)
                    sedesRecreativasApartamento.Estado = false;
                else if (sedesRecreativasApartamento.Estado == false)
                    sedesRecreativasApartamento.Estado = true;

                await _sedeService.EditarSede(sedesRecreativasApartamento);
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