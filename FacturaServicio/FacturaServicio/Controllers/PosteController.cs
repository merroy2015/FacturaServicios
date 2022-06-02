using FacturaServicio.Models;
using FacturaServicio.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shooping.Helpers;
using Vereyon.Web;
using static Shooping.Helpers.ModalHelper;

namespace FacturaServicio.Controllers
{
    public class PosteController : Controller

    {

        private readonly IRepositorioPoste repositorioPoste;
        private readonly IServiciosUsuarios serviciosUsuarios;
        private readonly IFlashMessage flashMessage;

        public PosteController(IRepositorioPoste repositorioPoste,
                                IServiciosUsuarios serviciosUsuarios, IFlashMessage flashMessage)
        {
            this.repositorioPoste = repositorioPoste;
            this.serviciosUsuarios = serviciosUsuarios;
            this.flashMessage = flashMessage;
        }
       
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var usuarioid = serviciosUsuarios.ObtenerUsuarioid();
            var rutas = await repositorioPoste.Obtener(usuarioid);
            var modelo = new PosteCreacionViewModel();
            modelo.TiposRutas = rutas.Select(x => new SelectListItem(x.Nombre, x.id.ToString()));

            return View(modelo);
        }
        [HttpPost]
        public async Task<IActionResult> Crear1(PosteCreacionViewModel poste)
        {  
            {
                if (!ModelState.IsValid)
                {
                    return View(poste);
                }


                poste.UsuarioId = serviciosUsuarios.ObtenerUsuarioid();
                await repositorioPoste.Crear(poste);
                return RedirectToAction("index");

            }
        }

        public async Task<IActionResult> Index()
        {
            var usuarioid = serviciosUsuarios.ObtenerUsuarioid();
            var Poste = await repositorioPoste.Listado(usuarioid);
            return View(Poste);
        }
        public async Task<IActionResult> Delete(int id)

        {
            var usuarioid = serviciosUsuarios.ObtenerUsuarioid();
            var rutas = await repositorioPoste.Obtener(usuarioid);

            if (rutas is null)
            {
                return RedirectToAction("NoEncontrado2", "Home");
            }
            await repositorioPoste.Delete(id);
            flashMessage.Confirmation("Se  ha eliminado satisfactoriamente el reistro.");

            return RedirectToAction(nameof(Index));

        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(PosteCreacionViewModel postes, int id = 0)
        {
   
            if (id == 0)
            {

                return View(postes);
            }

            var UsuarioId = serviciosUsuarios.ObtenerUsuarioid();
            var ruta = await repositorioPoste.ObtenerId(id, UsuarioId);


            return View(ruta);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id,PosteCreacionViewModel poste)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            if (id == 0) //Insert
            {

                poste.UsuarioId = serviciosUsuarios.ObtenerUsuarioid();
                await repositorioPoste.Crear(poste);
                flashMessage.Info("Registro creado.");
                return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAll", poste) });
            }

            poste.UsuarioId = serviciosUsuarios.ObtenerUsuarioid();
            await repositorioPoste.Update(poste);

            flashMessage.Info("Registro actualizado.");


            return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAll", poste) });

        }
    }
}
