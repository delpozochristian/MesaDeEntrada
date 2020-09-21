using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB.ViewModels
{
    public class ImponerWSResponse
    {
        public static ImponerWSResponse Map(string json)
        {
            var jobject = JObject.Parse(json);
            ImponerWSResponse response = new ImponerWSResponse();

            if (jobject["Pedidos"].Count() > 0)
            {
                PedidosImponerWS pedido = new PedidosImponerWS
                {
                    Status = jobject["Pedidos"][0]["Status"].Value<Int32>(),
                    NroSeguimiento = jobject["Pedidos"][0]["NroSeguimiento"].Value<String>(),
                    CodigoBarra = jobject["Pedidos"][0]["CodigoBarra"].Value<String>()
                };
                response.Pedidos = new List<PedidosImponerWS> { pedido };
            }

            return response;
        }

        public List<PedidosImponerWS> Pedidos { get; set; }

        public class PedidosImponerWS
        {
            public int Status { get; set; }
            public string NroSeguimiento { get; set; }
            public string CodigoBarra { get; set; }
        }


    }
}
