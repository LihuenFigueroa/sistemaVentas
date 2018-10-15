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
    public partial class FrmOcuparMesa : Form
    {
        public FrmOcuparMesa()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cantidadPersonas_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            this.Hide();
            
        }

        private void cantidadPersonas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                this.btnGrabar_Click(sender, e);
                
        }

    }
}
