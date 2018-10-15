using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEnlaceDatos;
using System.Data;

namespace CapaLogicaNegocio
{
    public class clsSala
    {
        clsManejador M = new clsManejador();

        private int IdSala;
        private String Nombre;

        public int GetIdSala()
        {
            return this.IdSala;
        }

        public void SetIdSala(int id_sala)
        {
            this.IdSala = id_sala;
        }

        public String GetNombre()
        {
            return this.Nombre;
        }

        public void SetNombre(String un_nombre)
        {
            this.Nombre = un_nombre;
        }

        public String RegistrarSala()
        {
            List<clsParametro> lst = new List<clsParametro>();
            String Mensaje = "";
            try
            {
                lst.Add(new clsParametro("@IdSala",this.IdSala));
                lst.Add(new clsParametro("@Nombre", this.Nombre));
                lst.Add(new clsParametro("@Mensaje", "", SqlDbType.VarChar, ParameterDirection.Output, 100));
                M.EjecutarSP("RegistrarSala",ref lst);
                return Mensaje = lst[2].Valor.ToString();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
