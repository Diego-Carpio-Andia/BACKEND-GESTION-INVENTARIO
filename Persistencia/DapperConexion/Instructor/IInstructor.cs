using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public interface IInstructor
    {
        //Task => asincrono
        Task<IEnumerable<InstructorModel>> ObtenerLista();
        Task<InstructorModel> ObtenerPorId(Guid id);
        Task<int> Nuevo(string nombre, string apellidos, string grado);
        Task<int> Actualiza(Guid InstructorId, string nombre, string apellidos, string grado);
        Task<int> Elimina(Guid id);
    }
}
