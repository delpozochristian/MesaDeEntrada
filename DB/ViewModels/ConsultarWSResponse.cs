using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB.ViewModels
{
    public class ConsultarWSResponse
    {
        public static ConsultarWSResponse Map(string json)
        {
            var jobject = JObject.Parse(json);
            ConsultarWSResponse response = new ConsultarWSResponse();

            if (jobject["Pedidos"].Count() > 0)
            {
                DestinatarioWS destinatario = new DestinatarioWS
                {
                    IDUsuario = jobject["Pedidos"][0]["Destinatario"]["IDUsuario"].Value<String>(),
                    Sucursal = jobject["Pedidos"][0]["Destinatario"]["Sucursal"].Value<String>(),
                    Bandeja = jobject["Pedidos"][0]["Destinatario"]["Bandeja"].Value<String>(),
                    Sector = jobject["Pedidos"][0]["Destinatario"]["Sector"].Value<String>()
                };

                RemitenteWS remitente = new RemitenteWS
                {
                    IDUsuario = jobject["Pedidos"][0]["Remitente"]["IDUsuario"].Value<String>(),
                    Sucursal = jobject["Pedidos"][0]["Remitente"]["Sucursal"].Value<String>(),
                    Bandeja = jobject["Pedidos"][0]["Remitente"]["Bandeja"].Value<String>(),
                    Sector = jobject["Pedidos"][0]["Remitente"]["Sector"].Value<String>()
                };

                PedidosWS pedido = new PedidosWS
                {
                    Status = jobject["Pedidos"][0]["Status"].Value<Int32>(),
                    NroSeguimiento = jobject["Pedidos"][0]["NroSeguimiento"].Value<String>(),
                    Observacion = jobject["Pedidos"][0]["Observacion"].Value<String>(),
                    Remitente = remitente,
                    Destinatario = destinatario
                };

                response.Pedidos = new List<PedidosWS> { pedido };
            }

            return response;

        }

        public List<PedidosWS> Pedidos { get; set; }
    }

    public class PedidosWS
    {
        public int Status { get; set; }
        public string NroSeguimiento { get; set; }
        public RemitenteWS Remitente { get; set; }
        public DestinatarioWS Destinatario { get; set; }
        public string Observacion { get; set; }
    }

    public class RemitenteWS
    {
        public string IDUsuario { get; set; }
        public string Sucursal { get; set; }
        public string Bandeja { get; set; }
        public string Sector { get; set; }
    }

    public class DestinatarioWS
    {
        public string IDUsuario { get; set; }
        public string Sucursal { get; set; }
        public string Bandeja { get; set; }
        public string Sector { get; set; }
    }
}
