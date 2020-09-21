using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.ViewModels
{
    public class RecepcionResponse
    {
        public int IdIngreso { get; set; }
        public string NroSeguimiento { get; set; }
        public int IdFile { get; set; }
        public string NameFile { get; set; }
        public string CodigoBarra { get; set; }
    }
}
