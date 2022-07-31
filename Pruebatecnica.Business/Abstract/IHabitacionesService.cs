using Pruebatecnica.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebatecnica.Business.Abstract
{
   public interface IHabitacionesService
    {
        Task<IEnumerable<HabitacionesAlojamiento>> ObtenerListaHabitacionesAlojamiento();
        Task GuardarHabitacionesAlojamiento(HabitacionesAlojamiento habitacionesAlojamiento);
        Task EditarHabitacionesAlojamiento(HabitacionesAlojamiento habitacionesAlojamiento);
        Task<HabitacionesAlojamiento> ObtenerHAPorId(int id);
        Task EliminarHabitacionesAlojamiento(int id);
        Task<List<HabitacionesAlojamiento>> ObtenerListaHA();
    }
}
