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
        private int posX;
        private int posY;
        private int ancho;
        private int alto;



        public int GetIdMesa()
        { return this.IdMesa; }

        public void SetIdMesa(int id_mesa)
        { this.IdMesa = id_mesa; }

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

        public void SetEstado(char libre)
        { this.libre = libre; }

        public char GetEsperarCuenta()
        { return this.esperaCuenta; }

        public void SetEsperarCuenta(char espera_cuenta)
        { this.esperaCuenta = espera_cuenta; }

        public char GetCombinada()
        { return this.combinada; }

        public void SetCombinada(char combinada)
        { this.combinada = combinada; }

        public Button GetBotonMesa()
        { return this.botonMesa; }

        public void SetBotonMesa(Button btn_mesa)
        { this.botonMesa = btn_mesa; }

        public int GetPosX()
        {
            return this.posX;
        }

        public void SetPosX(int posX)
        {
            this.posX = posX;
        }

        public int GetPosY()
        {
            return this.posY;
        }

        public void SetPosY(int posY)
        {
            this.posY = posY;
        }

        public void SetPosition(int posX,int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }

        public int GetAncho()
        {
            return this.ancho;
        }

        public void SetAncho(int ancho) {
            this.ancho = ancho;
        }

        public int GetAlto()
        {
            return this.alto;
        }

        public void SetAlto(int alto)
        {
            this.alto = alto;
        }
    

        ////////////////////---METODOS---///////////////////////////

        public String RegistrarMesa()
        {
            List<clsParametro> lst = new List<clsParametro>();
            String mensaje = "";
            try
            {
                lst.Add(new clsParametro("@IdMesa", this.IdMesa));
                lst.Add(new clsParametro("@IdSala", this.IdSala));
                lst.Add(new clsParametro("@NumeroMesa", this.numeroMesa));
                lst.Add(new clsParametro("@CantComensales", this.cantComensales));
                lst.Add(new clsParametro("@Libre", this.libre));
                lst.Add(new clsParametro("@EsperaCuenta", this.esperaCuenta));
                lst.Add(new clsParametro("@Combinada", this.combinada));
                lst.Add(new clsParametro("@PosX", this.posX));
                lst.Add(new clsParametro("@PosY", this.posY));
                lst.Add(new clsParametro("@Ancho", this.ancho));
                lst.Add(new clsParametro("@Alto", this.alto));
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

        public int ObtenerNumeroMesa()
        {
            List<clsParametro> lst = new List<clsParametro>();
            lst.Add(new clsParametro("@NumeroMesa", "", SqlDbType.VarChar, ParameterDirection.Output, 100));
            M.EjecutarSP("ObtenerNumeroMesa", ref lst);
            return int.Parse(lst[0].Valor.ToString());
        }

        public int ObtenerIdMesa(int numero_mesa)
        {
            // DataTable dt= new DataTable();
            List<clsParametro> lst = new List<clsParametro>();
            lst.Add(new clsParametro("@NumeroMesa", numero_mesa));
            lst.Add(new clsParametro("@IdMesa", "", SqlDbType.VarChar, ParameterDirection.Output, 100));
            M.EjecutarSP("ObtenerIdMesa" +
                "", ref lst);
            return int.Parse(lst[1].Valor.ToString());
        }

        public void ActualizarDimYPos(String numero_mesa,int posX,int posY, int ancho, int alto)
        {
            List<clsParametro> lst = new List<clsParametro>();
            lst.Add(new clsParametro("IdMesa",this.ObtenerIdMesa(int.Parse(numero_mesa))));
            lst.Add(new clsParametro("PosX",posX));
            lst.Add(new clsParametro("PosY", posY));
            lst.Add(new clsParametro("Ancho",ancho));
            lst.Add(new clsParametro("Alto", alto));
            M.EjecutarSP("ActualizarDimYPos",ref lst);
        }
    } 
}
