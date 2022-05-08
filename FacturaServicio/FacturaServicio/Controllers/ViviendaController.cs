using Dapper;
using FacturaServicio.Models;
using FacturaServicio.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace FacturaServicio.Controllers
{
    public class ViviendaController: Controller
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
            var vivienda = await repositorioVivienda.Obtener (usuarioid);
            return View(vivienda);
        }
        public IActionResult Crear()
             
        {
         
            return View();  
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
        public async Task<IActionResult> Editar(int Id)
        {
            var UsuarioId = servicioUsuarios.ObtenerUsuarioid();
            var Vivienda = await repositorioVivienda.ObtenerId(Id, UsuarioId);

            if (Vivienda is null)
            {
                return RedirectToAction("NoEncontrado2", "Home");
            }
            return View(Vivienda);
        }
    }
}
