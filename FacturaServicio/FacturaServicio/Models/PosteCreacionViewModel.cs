using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacturaServicio.Models
{
    public class PosteCreacionViewModel : Poste
    {
        public IEnumerable<SelectListItem> TiposRutas { get; set; }

    }
}
