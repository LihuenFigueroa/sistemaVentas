using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogicaNegocio;
using DevComponents.DotNetBar;

namespace Capa_de_Presentacion
{
    public partial class FrmMenuPrincipal : DevComponents.DotNetBar.Metro.MetroForm
    {
        int EnviarFecha = 0;
        private static FrmMenuPrincipal _singleton;
        private FrmMenuPrincipal()
        {            
            InitializeComponent();
            this.S = new clsSala();
            try
            {
                DataTable dt = new DataTable();
                dt = S.ListarSalas();
                int cantidadSalas = dt.Rows.Count;
                for (int i = 0; i < cantidadSalas; i++)
                {
                    TabPage myTabPage = new TabPage(dt.Rows[i][1].ToString());
                    myTabPage.Name = dt.Rows[i][1].ToString();
                    myTabPage.Text = dt.Rows[i][1].ToString();
                    this.tabControl1.TabPages.Add(myTabPage);
                    System.Windows.Forms.FlowLayoutPanel flowLayoutPanelx = new System.Windows.Forms.FlowLayoutPanel();
                    myTabPage.Controls.Add(flowLayoutPanelx);
                    flowLayoutPanelx.BackColor = System.Drawing.Color.MediumBlue;
                    flowLayoutPanelx.Location = new System.Drawing.Point(0, 0);
                    flowLayoutPanelx.Name = "flowLayoutPanelx";
                    flowLayoutPanelx.Size = new System.Drawing.Size(940, 426);
                    flowLayoutPanelx.TabIndex = 44;
                    myTabPage.SuspendLayout();
                    flowLayoutPanelx.SuspendLayout();
                }
            }
            catch (System.Exception e)
            {

                throw e;
            }
            foreach (TabPage tbp in tabControl1.TabPages)
            {
                tbp.ResumeLayout(false);
                foreach (System.Windows.Forms.FlowLayoutPanel flw in tbp.Controls)
                {
                    flw.ResumeLayout(false);
                }
            }
        }

        public static FrmMenuPrincipal CrearInstancia()
        {
            if (_singleton == null)
            {
                _singleton = new FrmMenuPrincipal();
            }
            return _singleton;
        }

        private void FrmMenuPrincipal_Activated(object sender, EventArgs e)
        {
            lblUsuario.Text = Program.NombreEmpleadoLogueado;
        }

        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Start();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            FrmListadoProductos P = new FrmListadoProductos();
            P.Show();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            FrmListadoClientes C = new FrmListadoClientes();
            C.Show();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            FrmRegistroVentas V = new FrmRegistroVentas();
            V.Show();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FrmRegistrarUsuarios U = new FrmRegistrarUsuarios();
            U.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch(EnviarFecha){
                case 0: CapturarFechaSistema(); break;
            }
        }

        private void CapturarFechaSistema() {
            lblFecha.Text = DateTime.Now.ToShortDateString();
            lblHora.Text = DateTime.Now.ToShortTimeString();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            FrmListadoEmpleados E = new FrmListadoEmpleados();
            E.Show();
        }

        private void FrmMenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }     


        private void button37_Click(object sender, EventArgs e)
        {
            FrmCrearSala S = new FrmCrearSala();
            S.Show();
        }

        public void AgregarSolapa(string nombre_solapa)
        {
            /*
            TabPage myTabPage = new TabPage(nombre_solapa);
            this.tabControl1.TabPages.Add(myTabPage);
            System.Windows.Forms.FlowLayoutPanel flowLayoutPanelx = new System.Windows.Forms.FlowLayoutPanel();
            myTabPage.Controls.Add(flowLayoutPanelx);
            flowLayoutPanelx.BackColor = System.Drawing.Color.MediumBlue;
            flowLayoutPanelx.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanelx.Name = "flowLayoutPanelx";
            flowLayoutPanelx.Size = new System.Drawing.Size(940, 426);
            flowLayoutPanelx.TabIndex = 44;
            */
        }
    }
}
