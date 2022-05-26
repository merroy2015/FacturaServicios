using System.ComponentModel.DataAnnotations;

namespace FacturaServicio.Models
{
    public class Vivienda
    {
        public int id { get; set; }
        
        [Required (ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:  50, MinimumLength =3,ErrorMessage ="la longitud del campo {0} debe ser entre {2} y{1} caracteres")]
        public string Tipo { get; set; }
       
        public string Estado { get; set; }

        public int UsuarioId { get; set; }

    }
}
