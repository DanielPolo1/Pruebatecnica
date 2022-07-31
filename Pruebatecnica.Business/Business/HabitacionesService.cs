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
 public class HabitacionesService : IHabitacionesService
    {
       private readonly AppDbContext _context;
       public HabitacionesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HabitacionesAlojamiento>> ObtenerListaHabitacionesAlojamiento()
        {
            return await _context.habitacionesAlojamiento.ToListAsync();
        }
        public async Task<HabitacionesAlojamiento> ObtenerHAPorId(int Id)
        {
            return await _context.habitacionesAlojamiento.FirstOrDefaultAsync(s => s.HabitacionesAlojamientoId == Id);
        }

        public async Task GuardarHabitacionesAlojamiento(HabitacionesAlojamiento habitacionesAlojamiento)
        {
            _context.Add(habitacionesAlojamiento);
            await _context.SaveChangesAsync();
        }

       public async Task EditarHabitacionesAlojamiento(HabitacionesAlojamiento habitacionesAlojamiento)
       {
           _context.Update(habitacionesAlojamiento);
           await _context.SaveChangesAsync();
       }
        public async Task<List<HabitacionesAlojamiento>> ObtenerListaHA()
        {
            return await _context.habitacionesAlojamiento.Where(s => s.Estado == true).ToListAsync();
        }
        public async Task EliminarHabitacionesAlojamiento(int id)
      {
          var habitacionesAlojamiento = await ObtenerHAPorId(id);
           _context.Remove(habitacionesAlojamiento);
            await _context.SaveChangesAsync();
        }
   }
}
