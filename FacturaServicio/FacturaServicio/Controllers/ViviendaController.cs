using Dapper;
using FacturaServicio.Models;
using FacturaServicio.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Shooping.Helpers;
using static Shooping.Helpers.ModalHelper;

namespace FacturaServicio.Controllers
{
    public class ViviendaController : Controller
    {
        private readonly IRepositorioVivienda repositorioVivienda;
        private readonly IServiciosUsuarios servicioUsuarios;

        public ViviendaController(IRepositorioVivienda repositorioVivienda,
           IServiciosUsuarios ServicioUsuarios)
        {
            this.repositorioVivienda = repositorioVivienda;
            servicioUsuarios = ServicioUsuarios;
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

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public IActionResult Crear(Vivienda vivienda)
        {
            if (!ModelState.IsValid)
            {
                return View(vivienda);
            }
            repositorioVivienda.Crear(vivienda);

            return View();
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

        public async Task<IActionResult> AddOrEdit(int id)
        {
            var UsuarioId = servicioUsuarios.ObtenerUsuarioid();

            var vivienda = await repositorioVivienda.ObtenerId(id, UsuarioId);

            if (vivienda is null)
            {
                return RedirectToAction("NoEncontrado1", "Home");
            }
            return View(vivienda);

        }
      }
}

