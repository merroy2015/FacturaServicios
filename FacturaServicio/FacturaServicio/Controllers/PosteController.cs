using FacturaServicio.Models;
using FacturaServicio.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Shooping.Helpers.ModalHelper;

namespace FacturaServicio.Controllers
{
    public class PosteController : Controller

    {

        private readonly IRepositorioPoste repositorioPoste;
        private readonly IServiciosUsuarios serviciosUsuarios;

        public PosteController(IRepositorioPoste repositorioPoste,
                                IServiciosUsuarios serviciosUsuarios)
        {
            this.repositorioPoste = repositorioPoste;
            this.serviciosUsuarios = serviciosUsuarios;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var usuarioid = serviciosUsuarios.ObtenerUsuarioid();
            var rutas = await repositorioPoste.Obtener(usuarioid);
            var modelo = new PosteCreacionViewModel();
       
            modelo.TiposRuta = rutas.Select(x => new SelectListItem(x.Nombre, x.id.ToString()));
          
            return View(modelo);
        }
        
    }
}
