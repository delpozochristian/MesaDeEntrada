namespace DB
{
    public class Constants
    {
        public const string LOG_UBICACION_REPORTES = "BLReportes";
        public const string LOG_UBICACION_PROVEEDORES = "BLProveedores";
        public const string LOG_UBICACION_DESTINATARIOS = "BLDestinatarios";
        public const string LOG_UBICACION_DASHBOARD = "BLDashboard";
        public const string LOG_UBICACION_RECEPCION = "BLRecepcion";

        public class Pedidos
        {
            /// <summary>
            /// 1
            /// </summary>
            public static int ID_INGRESADO = 1;
            /// <summary>
            /// 2
            /// </summary>
            public static int ID_ENTREGADO = 2;
            /// <summary>
            /// 3
            /// </summary>
            public static int ID_DISPONIBLE_PARA_RETIRO = 3;
            /// <summary>
            /// 4
            /// </summary>
            public static int ID_RETIRADO = 4;
            /// <summary>
            /// 5
            /// </summary>
            public static int ID_CANCELADO = 5;
        }
    }
}
