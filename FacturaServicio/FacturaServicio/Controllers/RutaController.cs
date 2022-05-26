using FacturaServicio.Models;
using FacturaServicio.Servicios;
using Microsoft.AspNetCore.Mvc;
using Shooping.Helpers;
using Vereyon.Web;
using static Shooping.Helpers.ModalHelper;

namespace FacturaServicio.Controllers
{
    public class RutaController : Controller
    {
        private readonly IRepositorioRuta _repositorioRuta;
        private readonly IServiciosUsuarios _serviciosUsuarios;
        private readonly IFlashMessage _flashMessage;

        public RutaController(IRepositorioRuta repositorioRuta, IServiciosUsuarios serviciosUsuarios,
           IFlashMessage flashMessage)
        {
            _repositorioRuta = repositorioRuta;
            _serviciosUsuarios = serviciosUsuarios;
            _flashMessage = flashMessage;
        }
        [HttpGet]
        public IActionResult Crear(Ruta ruta)
        {
            return View(ruta);
        }

        [HttpPost]
        public async Task<IActionResult> Crear1(Ruta ruta)
    
        {
            if (!ModelState.IsValid)
            {
                return View(ruta);
            }


            ruta.UsuarioId = _serviciosUsuarios.ObtenerUsuarioid();
            await _repositorioRuta.Crear (ruta);
            return RedirectToAction("index");

        }
        public async Task<IActionResult> Index()
        {
            var usuarioid = _serviciosUsuarios.ObtenerUsuarioid();
            var Ruta = await _repositorioRuta.Obtener(usuarioid);
            return View(Ruta);
        }
       

        public async Task<IActionResult> Delete(int id)

        {
            var usuarioid = _serviciosUsuarios.ObtenerUsuarioid();
            var rutas = await _repositorioRuta.Obtener(usuarioid);

            if (rutas is null)
            {
                return RedirectToAction("NoEncontrado2", "Home");
            }
            await _repositorioRuta.Delete(id);
            _flashMessage.Confirmation("Se  ha eliminado satisfactoriamente el reistro.");

            return RedirectToAction(nameof(Index));



        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(Ruta rutas, int id = 0)
        {

            if (id == 0)
            {

                return View(rutas);
            }

            var UsuarioId = _serviciosUsuarios.ObtenerUsuarioid();
            var ruta = await _repositorioRuta.ObtenerId(id, UsuarioId);

            return View(ruta);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, Ruta ruta)
        {
            if (!ModelState.IsValid)
            {
                return  View(); 
                

            }
            if (id == 0) //Insert
            {

                ruta.UsuarioId = _serviciosUsuarios.ObtenerUsuarioid();
                await _repositorioRuta.Crear(ruta);
                _flashMessage.Info("Registro creado.");
                return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAll",ruta) });
             
            }

            ruta.UsuarioId = _serviciosUsuarios.ObtenerUsuarioid();
            await _repositorioRuta.Update(ruta);

            _flashMessage.Info("Registro actualizado.");


            return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAll", ruta) });

        }
    }
    }

 



