using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebatecnica.Model.Entities
{
    public class SedesRecreativasApartamento
    {
        [Key]
        public int SedesId { get; set; }

        [DisplayName("Imagen")]
        public string RutaImagen { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column(TypeName = "nvarchar(50)")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [Column(TypeName = "nvarchar(100)")]
        public string Descripcion { get; set; }
 
        public string Tipo { get; set; }

        [Required(ErrorMessage = "La ubicacion es obligatoria")]
        [Column(TypeName = "nvarchar(50)")]
        public string Ubicacion { get; set; }
        public bool Estado { get; set; }
    }
}
