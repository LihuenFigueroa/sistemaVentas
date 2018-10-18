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
    public partial class FrmBorrarMesa : DevComponents.DotNetBar.Metro.MetroForm
    {
        clsMesa mesaAux = new clsMesa();
        public string mesaABorrar; //Necesitaremos una clase global para obtener los datos desde la clase padre
        public FrmBorrarMesa()
        {
            InitializeComponent();
        }

        private void FrmBorrarMesa_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'demoPracticaDataSet.Mesa' Puede moverla o quitarla según sea necesario.
            cbxMesas.DataSource = mesaAux.ListarNumerosMesas();
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.mesaTableAdapter.FillBy(this.demoPracticaDataSet.Mesa);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.mesaTableAdapter.FillBy1(this.demoPracticaDataSet.Mesa);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            mesaABorrar = cbxMesas.Text;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
