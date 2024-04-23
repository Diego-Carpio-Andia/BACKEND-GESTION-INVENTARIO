using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.DapperConexion.Paginacion
{
    public class PaginacionModel
    {
        //se devolvera un json en formato de array de todo el ouput del cliente
        public List<IDictionary<string,object>> ListaRecords { get; set; }
        public int TotalRecords {  get; set; }
        public int NumeroPaginas { get; set; }
        
    }
}
