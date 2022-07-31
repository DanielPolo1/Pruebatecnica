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
   public class SedeService : ISedes
    {
        private readonly AppDbContext _context;

        public SedeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SedesRecreativasApartamento>> ObtenerListaSedes()
        {
            return await _context.sedesRecreativasApartamento.ToListAsync();
        }

        public async Task<IEnumerable<SedesRecreativasApartamento>> ObtenerListaSedesEstado()
        {
            return await _context.sedesRecreativasApartamento.Where(s => s.Estado == true).ToListAsync();
        }
        public async Task<SedesRecreativasApartamento> ObtenerSedePorId(int Id)
        {
            return await _context.sedesRecreativasApartamento.FirstOrDefaultAsync(s => s.SedesId == Id);
        }
        public async Task RegistrarSede(SedesRecreativasApartamento sedesRecreativasApartamento)
        {
            _context.Add(sedesRecreativasApartamento);
            await _context.SaveChangesAsync();
        }

        public async Task EditarSede(SedesRecreativasApartamento sedesRecreativasApartamento)
        {
            _context.Update(sedesRecreativasApartamento);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarSede(int id)
        {
            var Sede = await ObtenerSedePorId(id);
            _context.Remove(Sede);
            await _context.SaveChangesAsync();
        }

        //public async Task AgregarCantidad(int SedeId, int cantidad)
        //{
        //    var producto = await ObtenerProductoPorId(SedeId);
        //    producto.cantidad += cantidad;
        //    _context.Update(producto);
        //    await _context.SaveChangesAsync();
        //}
        //public async Task<SedesRecreativasApartamento> NombreInsumoExiste(string Nombre)
        //{
        //    return await _context.sedesRecreativasApartamento.FirstOrDefaultAsync(n => n.Nombre == Nombre);
        //}
        public async Task<List<SedesRecreativasApartamento>> ObtenerListaSedesSolicitud()
        {
            return await _context.sedesRecreativasApartamento.Where(s => s.Estado == true).ToListAsync();
        }
    }
}
