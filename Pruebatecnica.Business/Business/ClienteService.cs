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
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> ObtenerListaClientes()
        {
            return await _context.clientes.Include(u => u.IdentityUser).ToListAsync();
        }
        public async Task<Cliente> ObtenerClientePorId(string id)
        {
            return await _context.clientes.Include(u => u.IdentityUser).FirstOrDefaultAsync(s => s.ClienteId.Contains(id));
        }

        public async Task GuardarCliente(Cliente cliente)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task EditarCliente(Cliente cliente)
        {
            _context.Update(cliente);
            await _context.SaveChangesAsync();
        }
        public async Task EliminarCliente(string id)
        {
            var cliente = await ObtenerClientePorId(id);
            _context.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
