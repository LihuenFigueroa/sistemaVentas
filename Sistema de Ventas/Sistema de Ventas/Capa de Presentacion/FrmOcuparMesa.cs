using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmOcuparMesa : DevComponents.DotNetBar.Metro.MetroForm
    {

        clsMesa M = new clsMesa();
        public string cantComensales; //Necesitaremos una clase global para obtener los datos desde la clase padre
        public FrmOcuparMesa()
        {
            InitializeComponent();
            
        }                       
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            cantComensales = cantidadPersonas.Text; //Asignacion a variable global de la que el Padre extraera el dato
            this.Close();
            
        }

        private void cantidadPersonas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                this.btnGrabar_Click(sender, e);                
        }

        private void cantidadPersonas_TextChanged(object sender, EventArgs e)
        {
            btnGrabar.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
