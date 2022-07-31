using Pruebatecnica.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebatecnica.Business.Abstract
{
    public interface ISedes
    {
        Task<IEnumerable<SedesRecreativasApartamento>> ObtenerListaSedes();
        Task<IEnumerable<SedesRecreativasApartamento>> ObtenerListaSedesEstado();
        Task RegistrarSede(SedesRecreativasApartamento sedesRecreativasApartamento);
        Task<SedesRecreativasApartamento> ObtenerSedePorId(int id);
        Task EditarSede(SedesRecreativasApartamento sedesRecreativasApartamento);
        Task EliminarSede(int id);
        //Task AgregarCantidad(int SedeId, int cantidad);
        //Task<SedesRecreativasApartamento> NombreInsumoExiste(string Nombre);
        Task<List<SedesRecreativasApartamento>> ObtenerListaSedesSolicitud();
    }
}
