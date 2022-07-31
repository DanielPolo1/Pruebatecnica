using Microsoft.EntityFrameworkCore;
using Pruebatecnica.Business.Abstract;
using Pruebatecnica.Model.DAL;
using Pruebatecnica.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebatecnica.Business.Business
{
 public class ReservaService : IReservaService
   {
       private readonly AppDbContext _context;
       public ReservaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reserva>> ObtenerListaReserva()
        {
            return await _context.reserva.ToListAsync();
        }
        public async Task<Reserva> ObtenerReservaPorId(int Id)
        {
            return await _context.reserva.FirstOrDefaultAsync(s => s.ReservaId == Id);
        }

        public async Task GuardarReserva(Reserva reserva)
        {
            _context.Add(reserva);
            await _context.SaveChangesAsync();
        }

       public async Task EditarReserva(Reserva reserva)
       {
           _context.Update(reserva);
           await _context.SaveChangesAsync();
       }
       public async Task EliminarReserva(int id)
      {
          var reserva = await ObtenerReservaPorId(id);
           _context.Remove(reserva);
            await _context.SaveChangesAsync();
        }
   }
}
