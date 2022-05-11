using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacturaServicio.Models
{
    public class DireccionViewModel : Clientes
    {
        public int IdDireccion { get; set; }
      
        public string Tipo { get; set; }
        public string No_Principal { get; set; }
        public string Sufijo { get; set; }
        public string No_Secundario { get; set; }
        public string No_Complementario { get; set; }
        public string CasaPropia { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Today;
      

        public int IdRutas { get; set; }
        public int IdVivienda { get; set; }
       
        public IEnumerable<SelectListItem> TiposRuta { get; set; }
        public IEnumerable<SelectListItem> TiposdeVivienda { get; set; }
    }
}
