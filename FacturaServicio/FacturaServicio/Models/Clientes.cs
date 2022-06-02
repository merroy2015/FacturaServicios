using System.ComponentModel.DataAnnotations;

namespace FacturaServicio.Models
{
    public class Clientes
    {
        public int IdClientes { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string  Email { get; set; }
        public string Estado { get; set; }
        public int IdUsuario { get; set; }
    }
}
