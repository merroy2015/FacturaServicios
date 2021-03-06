using System.ComponentModel.DataAnnotations;

namespace FacturaServicio.Models
{
    public class Ruta
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "la longitud del campo {0} debe ser entre {2} y{1} caracteres")]
        public string Nombre { get; set; }
        [Display(Name = "Longitud Incial")]
        public string LongitudIni { get; set; }
       
        [Display(Name = "Longitud Final")]
        public string LongitudFin { get; set; }
        [Display(Name = "Latitud Incial")]
        public string LatitudIni { get; set; }
        [Display(Name = "Latitud  Final")]
        public string LatitudFin { get; set; }
        public string Estado { get; set; }
        public int UsuarioId { get; set; }
    }
}
