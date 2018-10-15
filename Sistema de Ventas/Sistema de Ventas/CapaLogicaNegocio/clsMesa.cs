using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEnlaceDatos;
using System.Windows.Forms;

namespace CapaLogicaNegocio
{
    public class clsMesa
    {
        private clsManejador M = new clsManejador();
        // Atributos

        private int IdMesa;
        private int IdSala;
        private int numeroMesa;
        private int cantComensales;
        private char libre;
        private char esperaCuenta;
        private char combinada;
        private Button botonMesa;

       

        public int GetIdMesa()
        {   return this.IdMesa;}

        public void SetIdMesa(int id_mesa)
        {   this.IdMesa = id_mesa; }

        public int GetIdSala()
        { return this.IdSala; }

        public void SetIdSala(int id_sala)
        { this.IdSala = id_sala; }
        
        public int GetNumeroMesa()
        { return this.numeroMesa; }

        public void SetNumeroMesa(int num_mesa)
        { this.numeroMesa = num_mesa; }

        public int GetCantComensales()
        { return this.cantComensales; }

        public void SetCantComensales(int cant_comensales)
        { this.cantComensales = cant_comensales; }

        public char VerEstado()
        { return this.libre; }

        public void SetEstado (char libre)
        { this.libre = libre; }

        public char GetEsperarCuenta()
        { return this.esperaCuenta; }

        public void SetEsperarCuenta(char espera_cuenta)
        { this.esperaCuenta = espera_cuenta; }

        public char GetCombinada()
        { return this.combinada; }

        public void SetCombinada(char combinada)
        { this.combinada =combinada; }

        public Button GetBotonMesa()
        { return this.botonMesa; }

        public void SetBotonMesa(Button btn_mesa)
        {this.botonMesa = btn_mesa;}


        ////////////////////---METODOS---///////////////////////////
        
        public String RegistrarMesa()
        {
            List<clsParametro> lst = new List<clsParametro>();
            String mensaje = "";
            try
            {
                lst.Add(new clsParametro("@IdMesa", this.GetIdMesa()));
                lst.Add(new clsParametro("@IdSala", this.GetIdSala()));
                lst.Add(new clsParametro("@NumeroMesa", this.GetNumeroMesa()));
                lst.Add(new clsParametro("@CantComensales", this.GetCantComensales()));
                lst.Add(new clsParametro("@Libre", this.VerEstado()));
                lst.Add(new clsParametro("@EsperaCuenta", this.GetEsperarCuenta()));
                lst.Add(new clsParametro("@Combinada", this.GetCombinada()));
                lst.Add(new clsParametro("@Mensaje", "", SqlDbType.VarChar, ParameterDirection.Output, 50));
                M.EjecutarSP("RegistrarMesa", ref lst);
                mensaje = lst[7].Valor.ToString();
            }
            catch (Exception e)
            { throw e; }






            return mensaje;
        }

        public DataTable ListadoMesas()
        {
            return M.Listado("ListadoMesas", null);
        }

        public void OcuparMesa()
        {
            this.libre = '0';
        }

        public void LiberarMesa()
        {
            this.libre = '1';
        }
    }
}
