using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public class InstructorRepository : IInstructor
    {
        private readonly IFactoryConnection _factoryConnection;

        public InstructorRepository(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }

        public async Task<int> Actualiza(Guid instructorId,string nombre, string apellidos, string grado)
        {
            var storeProcedure = "usp_instructor_actualizar";
            try
            {
                var connection = _factoryConnection.GetConnection();
                var resultado = await connection.ExecuteAsync(
                storeProcedure,
                new
                {
                    InstructorId = instructorId,
                    Nombre = nombre,
                    Apellidos = apellidos,
                    Grado = grado
                },
                commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();

                return resultado;
            } 
            catch(Exception ex) { 
            
                throw new Exception("no se pudo ACtualizar el nuevo instructor", ex);
            }
        }

        public async Task<int> Elimina(Guid id)
        {
            var storeProcedure = "usp_instructor_eliminar";
            try
            {
                var connection = _factoryConnection.GetConnection();
                var resultado = await connection.ExecuteAsync(
                    storeProcedure,
                    new
                    {
                        InstructorId = id
                    },
                    commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();

                return resultado;
            }
            catch(Exception ex) { throw new Exception("no se pudo eliminar hubo un error", ex); }
        }

        public async Task<int> Nuevo(string nombre, string apellidos, string grado)
        {
            var storeProcedure = "usp_instructor_nuevo";
            try
            {
                var connection = _factoryConnection.GetConnection();
                var resultado = await connection.ExecuteAsync(
                storeProcedure, 
                new{
                    //parametros del procedimiento almacenado
                    InstructorId = Guid.NewGuid(),
                    Nombre = nombre,
                    Apellidos = apellidos,
                    Grado = grado,
                },
                commandType: CommandType.StoredProcedure
                );

                _factoryConnection.CloseConnection();

                return resultado;
            }catch (Exception ex)
            {
                throw new Exception("no se pudo guardar el nuevo instructor", ex);
            }          

        }

        public async Task<IEnumerable<InstructorModel>> ObtenerLista()
        {
            //Comenzamos por crear un objeto de la lista de resultados
            IEnumerable<InstructorModel> InstructorList = null;
            //nombre del procedimiento almacenado de SQL server
            var storeProcedure = "usp_Obtener_Instructores";
            try
            {
                //logica del programa
                var connection = _factoryConnection.GetConnection();
                //data que retorna InstructorModelo   - Parametros del storeProcedure que se ingresara - qquery o stored procedure
                InstructorList = await connection.QueryAsync<InstructorModel>(storeProcedure, null, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                //error dentro de la logica del programa
                throw new Exception("Error en la consulta de datos", ex);

            }
            finally
            {
                //es un segmento que SIEMPRE se ejecuta asi ocurra un try o catch
                //Cerraremos la conexiton
                _factoryConnection.CloseConnection();
            }
            return InstructorList;
        }

        public async Task<InstructorModel> ObtenerPorId(Guid id)
        {
            var storeProcedure = "usp_obtener_instructor_por_id";
            InstructorModel instructor = null;
            try
            {
                var connection = _factoryConnection.GetConnection();
                //me retornara un objeto pero tienes que mapearlo
                instructor = await connection.QueryFirstAsync<InstructorModel>(
                    storeProcedure,
                    new
                    {
                        //este Id en mayuscula al inicio debe de ir tal cual en el procedimiento almacenado
                        Id = id,
                    },
                    commandType: CommandType.StoredProcedure
                );
                return instructor;

            }catch(Exception ex) {
                throw new Exception("No se pudo encontrar el instructor", ex);
            }
        }
    }
}
