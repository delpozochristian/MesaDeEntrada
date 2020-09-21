using System;
using System.Collections.Generic;
using System.Linq;

namespace DB.ViewModels
{

    public class IngresosResponse
    {
        private static readonly string DATE_FORMAT = "dd/MM/yyyy";

        public List<IngresoResponse> ListaIngresos { get; set; }

        public Prefiltrado Prefiltros { get; set; }

        public string totalCount { get; set; }

        public static IngresoResponse Map(Pedidos item)
        {
            try
            {
                IngresoResponse response = new IngresoResponse
                {
                    Destinatario = item.Destinatarios.NombreDeUsuario,
                    AutorizadoRetirar = item.AutorizadoRetirar,
                    Direccion = item.Direcciones.Descripcion,
                    Estado = item.Estados.Nombre,
                    IdEstado = item.Estados.Id,
                    Fecha = item.FechaImposicion.Value.ToString(DATE_FORMAT),
                    NroSeguimiento = item.NroSeguimiento,
                    Proveedor = item.Proveedores.RazonSocial,
                    Sector = item.IdSector,
                    SectorDescripcion = item.Sectores.Descripcion,
                    Canalizacion = item.Canalizaciones.Descripcion,
                    TipoProducto = item.TipoDeProductos.Descripcion,
                    IdIngreso = item.Id,
                    Barcode = item.CodigoBarra ?? "0",
                    IdEgreso = 0,
                    EsEgreso = false,
                    TieneArchivo = false
                };

                if (item.EstadoFecha.HasValue)
                    response.EstadoFecha = item.EstadoFecha.Value.ToString(DATE_FORMAT);
                if (item.UsuarioEstadoPor != null)
                    response.UsuarioEstadoPor = item.UsuarioEstadoPor.email;

                if (item.Estado == Constants.Pedidos.ID_RETIRADO)
                {
                    EgresoPedidos egreso = item.EgresoPedidos.FirstOrDefault();
                    if (egreso != null)
                    {
                        response.IdEgreso = egreso.IdEgreso;
                        response.EsEgreso = true;
                    }
                    else
                    {
                        response.IdEgreso = 0;
                        response.EsEgreso = false;
                    }
                }
                else
                {
                    response.IdEgreso = 0;
                    response.EsEgreso = false;
                }

                if (item.Archivos.FirstOrDefault() != null)
                    response.TieneArchivo = true;


                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IngresoResponse MapDetail(Pedidos item)
        {
            try
            {
                IngresoResponse response = new IngresoResponse
                {
                    Destinatario = item.Destinatarios.NombreDeUsuario,
                    AutorizadoRetirar = item.AutorizadoRetirar,
                    Direccion = item.Direcciones.Descripcion,
                    Estado = item.Estados.Nombre,
                    IdEstado = item.Estados.Id,
                    Fecha = item.FechaImposicion.Value.ToString(DATE_FORMAT),
                    NroSeguimiento = item.NroSeguimiento,
                    Proveedor = item.Proveedores.RazonSocial,
                    Sector = item.IdSector,
                    SectorDescripcion = item.Sectores.Descripcion,
                    Canalizacion = item.Canalizaciones.Descripcion,
                    TipoProducto = item.TipoDeProductos.Descripcion,
                    IdIngreso = item.Id,
                    Barcode = item.CodigoBarra ?? "0",
                    IdEgreso = 0,
                    EsEgreso = false,
                    TieneArchivo = false
                };

                if (item.Estado == Constants.Pedidos.ID_RETIRADO)
                {
                    EgresoPedidos egreso = item.EgresoPedidos.FirstOrDefault();
                    if (egreso != null)
                    {
                        response.IdEgreso = egreso.IdEgreso;
                        response.EsEgreso = true;
                    }
                    else
                    {
                        response.IdEgreso = 0;
                        response.EsEgreso = false;
                    }
                }
                else
                {
                    response.IdEgreso = 0;
                    response.EsEgreso = false;
                }

                if (item.Archivos.FirstOrDefault() != null)
                    response.TieneArchivo = true;

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<IngresoResponse> MapList(List<Pedidos> pedidos)
        {
            return pedidos.Select(p => Map(p)).ToList();
        }

    }

    public class Prefiltrado
    {
        public string Seleccionado { get; set; }
        public EstadoResponse EstadoPrefiltradoIngresos { get; set; }
        public EstadoResponse EstadoPrefiltradoEgresos { get; set; }
        public CanalizacionResponse CanalizacionesPrefiltradoCorrespondencia { get; set; }
        public TipoProductoResponse TipoProductoPrefiltradoPaquetes { get; set; }
    }

    public class IngresoResponseVisible
    {
        public string NroSeguimiento { get; set; }
        public string Fecha { get; set; }
        public string TipoProducto { get; set; }
        public string Proveedor { get; set; }
        public string Destinatario { get; set; }
        public string Sector { get; set; }
        public string Canalizacion { get; set; }
        public string Estado { get; set; }

    }

    public class IngresoResponse: IngresoResponseVisible
    {
        public int IdIngreso { get; set; }
        public int IdEgreso { get; set; }
        public string AutorizadoRetirar { get; set; }
        public string SectorDescripcion { get; set; }
        public int IdEstado { get; set; }
        public bool TieneArchivo { get; set; }
        public bool EsEgreso { get; set; }
        public string EstadoFecha { get; set; }
        public string Barcode { get; set; }
        public string UsuarioEstadoPor { get; set; }
        public string Direccion { get; set; }

    }

    public class ArchivoIngresoResponse
    {
        public int IdIngreso { get; set; }
        public string Nombre { get; set; }
        public byte[] Data { get; set; }
    }
}
