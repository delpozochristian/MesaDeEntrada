using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.ViewModels
{
    public class TiposProductoResponse
    {
        public static TipoProductoResponse Map(TipoDeProductos item)
        {
            TipoProductoResponse response = new TipoProductoResponse();
            response.Id = item.Id;
            response.Nombre = item.Descripcion;
            return response;
        }

    }

    public class TipoProductoResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
