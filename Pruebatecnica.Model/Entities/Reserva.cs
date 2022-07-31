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
    public class Reserva
    {
        [Key]
        public int ReservaId { get; set; }
        public string ClienteId { get; set; }
        [DisplayName("Fecha reserva")]
        public string FechaReserva { get; set; }

        [DisplayName("Fecha llegada")]
        public string FechaLlegada { get; set; }
        [DisplayName("Fecha salida")]
        public string FechaSalida { get; set; }
        public int Personas { get; set; }
        public long Total { get; set; }
        public bool Estado { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual HabitacionesAlojamiento HabitacionesAlojamiento { get; set; }
    }
}
