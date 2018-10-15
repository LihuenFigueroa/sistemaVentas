using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CapaLogicaNegocio;

namespace Capa_de_Presentacion
{
    public partial class FrmCrearSala : DevComponents.DotNetBar.Metro.MetroForm
    {
        clsSala S = new clsSala();

        public FrmCrearSala()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (this.txtNombre.Text.Trim() != "")
            {
                String Mensaje = "";
                S.SetNombre(txtNombre.Text);
                Mensaje=S.RegistrarSala();
                if (Mensaje == "El nombre de la Sala ya existe.")
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show(Mensaje, "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    txtNombre.Clear();
                    txtNombre.Focus(); 
                }
                else
                {
                    this.Close();
                    FrmMenuPrincipal MP = FrmMenuPrincipal.CrearInstancia();
                    MP.AgregarSolapa(S.GetNombre());
                }
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                this.btnIngresar_Click(sender, e);
            else
            {
                if (e.KeyChar == (char)27)
                    this.btnCancelar_Click(sender,e);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
