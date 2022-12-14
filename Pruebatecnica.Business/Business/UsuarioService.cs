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
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> ObtenerListaUsuarios()
        {
            return await _context.usuario.Include(u => u.IdentityUser).ToListAsync();
        }
        public async Task<Usuario> ObtenerUsuarioPorId(string id)
        {
            return await _context.usuario.Include(u => u.IdentityUser).FirstOrDefaultAsync(s => s.UsuarioId == id);
        }

        public async Task GuardarUsuario(Usuario usuario1)
        {
            _context.Add(usuario1);
            await _context.SaveChangesAsync();
        }

        public async Task EditarUsuario(Usuario usuario1)
        {
            _context.Update(usuario1);
            await _context.SaveChangesAsync();
        }

    }
}
