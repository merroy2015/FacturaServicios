using Dapper;
using FacturaServicio.Models;
using FacturaServicio.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Shooping.Helpers;
using Vereyon.Web;
using static Shooping.Helpers.ModalHelper;

namespace FacturaServicio.Controllers
{
    public class ViviendaController : Controller
    {
        private readonly IRepositorioVivienda repositorioVivienda;
        private readonly IServiciosUsuarios servicioUsuarios;
        private readonly IFlashMessage flashMessage;

        public ViviendaController(IRepositorioVivienda repositorioVivienda,
         IServiciosUsuarios ServicioUsuarios, IFlashMessage flashMessage)
        {
            this.repositorioVivienda = repositorioVivienda;
            servicioUsuarios = ServicioUsuarios;
            this.flashMessage = flashMessage;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioid = servicioUsuarios.ObtenerUsuarioid();
            var vivienda = await repositorioVivienda.Obtener(usuarioid);
            return View(vivienda);
        }
        [NoDirectAccess]

        public async Task<IActionResult> Delete(int id)

        {
            var UsuarioId = servicioUsuarios.ObtenerUsuarioid();
            var Vivienda = await repositorioVivienda.ObtenerId(id, UsuarioId);

            if (Vivienda is null)
            {
                return RedirectToAction("NoEncontrado2", "Home");
            }
            await repositorioVivienda.Delete(id);
            flashMessage.Confirmation("Se  ha eliminado satisfactoriamente el reistro.");

            return RedirectToAction(nameof(Index));



        }

        [HttpPost]
        public IActionResult Crear(Vivienda vivienda)
        {
            if (!ModelState.IsValid)
            {
                return View ();
                       
            }
            repositorioVivienda.Crear(vivienda);

           return View(vivienda);

        }
        public async Task<IActionResult> Editar(int id)
        {
            var UsuarioId = servicioUsuarios.ObtenerUsuarioid();
            var Vivienda = await repositorioVivienda.ObtenerId(id, UsuarioId);

            if (Vivienda is null)
            {
                return RedirectToAction("NoEncontrado2", "Home");
            }
            return View(Vivienda);
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(Vivienda viviendas, int id = 0)
        {

            if (id == 0)
            {
                
                return View(viviendas);
            }
      
            var UsuarioId = servicioUsuarios.ObtenerUsuarioid();
            var vivienda = await repositorioVivienda.ObtenerId(id, UsuarioId);
           
            return View(vivienda);
            }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, Vivienda vivienda)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            if (id == 0) //Insert
                {
                vivienda.UsuarioId = servicioUsuarios.ObtenerUsuarioid();
                await repositorioVivienda.Crear(vivienda);
                flashMessage.Info("Registro creado.");
                return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAll", vivienda) });
                }
                vivienda.UsuarioId = servicioUsuarios.ObtenerUsuarioid();
                await repositorioVivienda.Update(vivienda);
              flashMessage.Info("Registro actualizado.");
            return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAll", vivienda) });

        }

    }

           

    
}