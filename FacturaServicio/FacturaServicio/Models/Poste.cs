using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FacturaServicio.Models
{
    public class Poste
    {
        public int id { get; set; }
        [Display(Name = "Nombre de las rutas")]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        public int IdRutas { get; set; }

        public IEnumerable<SelectListItem> TiposRuta { get; set; }

        [Required(ErrorMessage ="El campo {0} es Obligatorio")]
        public string Nombre { get; set; }
        public string  Longitud { get; set; }
        public string Latitud { get; set; }

        public int UsuarioId { get; set; }

    }
}
