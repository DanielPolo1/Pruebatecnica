using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Pruebatecnica.Model.Entities;

namespace Pruebatecnica.ViewModels.Reserva
{
    public class ReservaViewModel
    {
        public int ReservaId { get; set; }
     
        [DisplayName("Fecha reserva")]
        public string FechaReserva { get; set; }
        [Required(ErrorMessage = "La fecha llegada es obligatoria")]
        [DisplayName("Fecha llegada")]
        public string FechaLlegada { get; set; }
        [Required(ErrorMessage = "La fecha salida es obligatoria")]
        [DisplayName("Fecha salida")]
        public string FechaSalida { get; set; }
        [Required(ErrorMessage = "La personas es obligatoria")]
        public int Personas { get; set; }
        public long Total { get; set; }
        public bool Estado { get; set; }
        public List<HabitacionesAlojamiento> habitacionesAlojamientos { get; set; }
    }
}
