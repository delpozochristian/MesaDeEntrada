using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.ViewModels
{
    public class DashboardResponse
    {
        public int CantidadRecepciones { get; set; }
        public int CantidadEgresos { get; set; }
        public int CantidadCorrespondencia { get; set; }
        public int CantidadPaquetes { get; set; }
    }
}
