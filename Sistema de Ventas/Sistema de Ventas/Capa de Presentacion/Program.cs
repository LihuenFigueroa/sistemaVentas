using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.      
        /// </summary>
         public static int Evento;

      
        //Datos del Producto
         public static int IdProducto;
         public static String Descripcion;
         public static String Marca;
         public static Int32 Stock;
         public static Decimal PrecioVenta;

        //Datos del Empleado
         public static int IdCargo;
         public static String EstadoCivil="";
         public static int IdEmpleado;

        //Variables de Sesion
        public static int IdEmpleadoLogueado;
        public static String NombreEmpleadoLogueado;

        //DATOS ESTADISTICOS
        public static decimal TOTAL_DIA = 0;
        public static decimal TOTAL_POSTRES = 0;
        public static decimal TOTAL_BEBIDAS = 0;
        public static decimal TOTAL_AREPAS = 0;
        public static decimal TOTAL_CARNES = 0;
        public static decimal TOTAL_PLATOS_VENEZOLANOS = 0;
        public static decimal TOTAL_PLATOS_DEL_DIA = 0;
        public static decimal TOTAL_EMPANADAS_VENEZOLANAS = 0;
        public static decimal TOTAL_PLATOS_ARGENTINOS = 0;
        public static decimal TOTAL_CONTORNOS = 0;
        public static decimal TOTAL_PROMOS = 0;
        public static decimal TOTAL_VERDURAS = 0;

        // DATOS DE HARDWARE
        public const String PDF = "Foxit Reader PDF Printer";
        public const String COMANDERA = "POS-80C";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());      
        }
    }
}
