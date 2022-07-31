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
    public class HabitacionesAlojamiento
    {
        [Key]
        public int HabitacionesAlojamientoId { get; set; }
      
        [DisplayName("Habitacion / Alojamiento")]
        [Required(ErrorMessage = "El numero es obligatorio")]
        [Column(TypeName = "nvarchar(10)")]
        public int NumeroHabitacionAlojamiento { get; set; }
        public int SedesId { get; set; }

        [Required(ErrorMessage = "La capacidad es obligatoria")]
        [Column(TypeName = "nvarchar(10)")]
        public int Capacidad { get; set; }

        [DisplayName("Tarifa dia ordinario")]
        [Required(ErrorMessage = "La tarifa ordinario es obligatorio")]
        [Column(TypeName = "nvarchar(10)")]
        public long TarifaOrdinario { get; set; }

        [DisplayName("Tarifa dia especial")]
        [Required(ErrorMessage = "La tarifa especial es obligatorio")]
        [Column(TypeName = "nvarchar(10)")]
        public long TarifaEspecial { get; set; }
      
        public bool Estado { get; set; }
        public virtual SedesRecreativasApartamento SedesRecreativasApartamento { get; set; }
    }
}
