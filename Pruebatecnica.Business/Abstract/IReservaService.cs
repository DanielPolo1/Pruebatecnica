using Pruebatecnica.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebatecnica.Business.Abstract
{
  public interface IReservaService
    {
        Task<IEnumerable<Reserva>> ObtenerListaReserva();
        Task GuardarReserva(Reserva reserva);
        Task EditarReserva(Reserva reserva);
        Task<Reserva> ObtenerReservaPorId(int id);
        Task EliminarReserva(int id);
    }
}
