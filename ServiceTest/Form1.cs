using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace ServiceTest
{
    public partial class Form1 : Form
    {
        private static string NOTIFICAR_ESTADO = "NotificarEstado";
        private static string NOTIFICAR_NUEVO_ESTADO = "NotificarNuevoEstado";
        public Form1()
        {
            InitializeComponent();
            //tb_url_servicio.Text = "http://localhost/WSMailroom/NotificacionService.svc"; 
            tb_url_servicio.Text = "http://testnet.w3itsolutions.net:9191/WSMailroom/NotificacionService.svc";
            //tb_url_servicio.Text = "http://192.168.1.11/WSMailroom/NotificacionService.svc";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add(NOTIFICAR_ESTADO);
            comboBox1.Items.Add(NOTIFICAR_NUEVO_ESTADO);

            // Bind combobox to dictionary
            Dictionary<int, string> test = new Dictionary<int, string>
            {
                { 1, "1 Ingresado" },
                { 2, "2 Entregado" },
                { 3, "3 Disponible para retiro" },
                { 4, "4 Retirado" },
                { 5, "5 Cancelado" }
            };

            cb_estados.DataSource = new BindingSource(test, null);
            cb_estados.DisplayMember = "Value";
            cb_estados.ValueMember = "Key";
        }

        private void B_ejecutar_Click(object sender, EventArgs e)
        {
            string itemSeleccionado = (string)comboBox1.SelectedItem;
            string codigoBarra = txtCodigoBarra.Text;
            int estado = ((KeyValuePair<int, string>)cb_estados.SelectedItem).Key;
            string email = tb_mail.Text;
            string fecha = tb_fecha.Text;

            string response = "NADA";
            Thread t = new Thread(() =>
            {
                try
                {
                    ServiceReference.NotificacionClient notificacionService = new ServiceReference.NotificacionClient();
                    notificacionService.Endpoint.Address = new System.ServiceModel.EndpointAddress(tb_url_servicio.Text);
                    if (itemSeleccionado == NOTIFICAR_ESTADO)
                        response = notificacionService.NotificarEstado(estado, codigoBarra);
                    else if (itemSeleccionado == NOTIFICAR_NUEVO_ESTADO)
                        response = notificacionService.NotificarNuevoEstado(estado, codigoBarra, email, fecha);
                }
                catch(Exception ex)
                {
                    response = "Ocurrió un error";
                }
            });
            t.Start();

            b_ejecutar.Enabled = false;
            progressBar1.Visible = true;

            while (t.IsAlive) { Application.DoEvents(); }

            b_ejecutar.Enabled = true;
            progressBar1.Visible = false;

            txtResponse.Text = response;
            MessageBox.Show(response);
            /*
            ServiceReference.Pedido pedidoWs = new ServiceReference.Pedido();
            ServiceReference.ImponerDestinatarioWSRequest destinatarioWs = new ServiceReference.ImponerDestinatarioWSRequest();
            ServiceReference.ImponerProveedorWSRequest proveedorWs = new ServiceReference.ImponerProveedorWSRequest();
            ServiceReference.ImponerRemitenteWSRequest remitenteWs = new ServiceReference.ImponerRemitenteWSRequest();

            destinatarioWs.Id = "000MDE";
            destinatarioWs.Bandeja = "00744000000064901020003170040102000317";
            destinatarioWs.Sector = "MESA";
            destinatarioWs.Canalizacion = "1";

            pedidoWs.Destinatario = destinatarioWs;

            proveedorWs.Id = "1";
            pedidoWs.Proveedor = proveedorWs;

            remitenteWs.Id = "000MDE";
            remitenteWs.Bandeja = "00744000000064901020003170040102000317";
            remitenteWs.Sector = "MESA";
            remitenteWs.Sucursal = "00000006490102000317";

            pedidoWs.Remitente = remitenteWs;

            destinatarioWs.Canalizacion = "1";

            pedidoWs.CodigoBarra = "";
            pedidoWs.Fecha = "";

            pedidoWs.NroDeOrdenCompra = "";
            pedidoWs.NroSeguimiento = "";
            pedidoWs.Observacion = "";

            pedidoWs.TipoProducto = "1";
            pedidoWs.AutorizadoRetirar = "Mi primer autorizado";

            string response = notificacionService.NotificarPedido(pedidoWs);
            */
            //txtResponse.Text = response;
        }

    }
}
