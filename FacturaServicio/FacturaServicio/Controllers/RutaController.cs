using FacturaServicio.Models;
using Microsoft.AspNetCore.Mvc;

namespace FacturaServicio.Controllers
{
    public class RutaController : Controller
    {

        [HttpGet]
        public IActionResult Crear(Ruta ruta)
        {
            return View(ruta);  
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
