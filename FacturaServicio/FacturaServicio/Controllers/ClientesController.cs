using FacturaServicio.Models;
using FacturaServicio.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace FacturaServicio.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IRepositorioClientes repositorioClientes;
        private readonly IServiciosUsuarios serviciosUsuarios;

        public ClientesController(IRepositorioClientes repositorioClientes,
            IServiciosUsuarios serviciosUsuarios)
        {
            this.repositorioClientes = repositorioClientes;
            this.serviciosUsuarios = serviciosUsuarios;
        }
        public async Task<IActionResult> Index()
        {
            var usuarioid = serviciosUsuarios.ObtenerUsuarioid();
            var cliente = await repositorioClientes.Obtener(usuarioid);
            return View(cliente);
        }
        [HttpGet]

        public IActionResult Crear(Clientes cliente)

        {

            return View(cliente);
        }

        public async Task<IActionResult> AddDireccion(int IdClientes)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioid();
            var Direccion = await repositorioClientes.IdDireccion(IdClientes, usuarioId);

            if (Direccion is null)
            {
                return RedirectToAction("NoEncontrado1", "Home");
            }
            return View(Direccion);
        }
        [HttpPost]
        public async Task<IActionResult> AddDireccion(DireccionViewModel DireccionAdd)
        {
            if (!ModelState.IsValid)
            {
                return View(DireccionAdd);
            }
            var usuarioId = serviciosUsuarios.ObtenerUsuarioid();
            var Direccion = await repositorioClientes.IdDireccion(DireccionAdd.IdClientes, usuarioId);

            if (Direccion is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            DireccionAdd.IdUsuario = usuarioId;
            await repositorioClientes.Actualizar(DireccionAdd);
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Editar(int IdClientes)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioid();
            var clientes = await repositorioClientes.ObtenerId(IdClientes, usuarioId);

            if (clientes is null)
            {
                return RedirectToAction("NoEncontrado1", "Home");
            }
            return View(clientes);
        }

    }
}

