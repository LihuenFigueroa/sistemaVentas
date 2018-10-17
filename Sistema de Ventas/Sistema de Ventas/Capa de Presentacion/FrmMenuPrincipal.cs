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
                    System.Windows.Forms.Panel panelx = new System.Windows.Forms.Panel();
                    myTabPage.Controls.Add(panelx);
                    panelx.BackColor = System.Drawing.Color.MediumBlue;
                    panelx.Location = new System.Drawing.Point(0, 0);
                    panelx.Name = "panelx";
                    panelx.Size = new System.Drawing.Size(947, 426);
                    panelx.TabIndex = 0;
                    //////////////////////////////////////////////////////////
                    /// LEVANTAR LAS MESAS EXISTENTES DE LA BASE DE DATOS ////
                    //////////////////////////////////////////////////////////
                    int fil = 0;                  
                    DataTable dt2 = new DataTable();
                    clsSala salaActual = new clsSala();
                    dt2 = salaActual.ObtenerMesas(dt.Rows[i][1].ToString());
                    int cantidadMesas = dt2.Rows.Count;
                    clsMesa mesaActual = new clsMesa();
                    for (int j = 0; j < cantidadMesas; j++)
                    {
                        System.Windows.Forms.Button btnMesa = new System.Windows.Forms.Button();
                        btnMesa.AutoSize = true;
                        btnMesa.BackgroundImage = global::Capa_de_Presentacion.Properties.Resources.mesa_libre;
                        btnMesa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                        btnMesa.Font = new System.Drawing.Font("Lucida Fax", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        btnMesa.Location = new System.Drawing.Point(mesaActual.ObtenerPosX(int.Parse(dt2.Rows[fil][0].ToString())), mesaActual.ObtenerPosY(int.Parse(dt2.Rows[fil][0].ToString())));
                        btnMesa.Size = new System.Drawing.Size(mesaActual.ObtenerAncho(int.Parse(dt2.Rows[fil][0].ToString())), mesaActual.ObtenerAlto(int.Parse(dt2.Rows[fil][0].ToString())));
                        btnMesa.Name = "btnMesa";
                        btnMesa.Text = mesaActual.ObtenerNumeroMesaConId(int.Parse(dt2.Rows[fil][0].ToString())).ToString();
                        btnMesa.UseVisualStyleBackColor = true;
                        ControlMoverOrResizer.Init(btnMesa);
                        fil++;
                        panelx.Controls.Add(btnMesa);
                        btnMesa.Click += new System.EventHandler(this.btnMesa_Click);
                    }
                    /////////////////////////////////////////////////////////
                    myTabPage.SuspendLayout();
                    panelx.SuspendLayout();
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }

            foreach (TabPage tbp in tabControl1.TabPages)
            {
                tbp.ResumeLayout(false);

                foreach (System.Windows.Forms.Panel pnl in tbp.Controls)
                {
                    pnl.ResumeLayout(false);
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
         
        private void button37_Click(object sender, EventArgs e)
        {
            FrmCrearSala S = new FrmCrearSala();
            S.Show();
        }

        public void AgregarSolapa(string nombre_solapa)
        {
            
            TabPage myTabPage = new TabPage(nombre_solapa);
            this.tabControl1.TabPages.Add(myTabPage);
            System.Windows.Forms.FlowLayoutPanel flowLayoutPanelx = new System.Windows.Forms.FlowLayoutPanel();
            myTabPage.Controls.Add(flowLayoutPanelx);
            flowLayoutPanelx.BackColor = System.Drawing.Color.MediumBlue;
            flowLayoutPanelx.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanelx.Name = "flowLayoutPanelx";
            flowLayoutPanelx.Size = new System.Drawing.Size(940, 426);
            flowLayoutPanelx.TabIndex = 44;
            
        }

        private void btnCrearMesa_Click(object sender, EventArgs e)
        {
            clsMesa nuevaMesa = new clsMesa();
            clsSala salaActiva = new clsSala();
            String nombreSalaActiva = this.tabControl1.SelectedTab.Name.ToString();
            int id_sala = salaActiva.ObtenerIdSala(nombreSalaActiva);
            nuevaMesa.SetIdSala(id_sala);
            int numero_mesa = nuevaMesa.ObtenerNumeroMesa();
            nuevaMesa.SetNumeroMesa(numero_mesa);
            nuevaMesa.SetCantComensales(0);
            nuevaMesa.SetEstado('1');
            nuevaMesa.SetEsperarCuenta('0');
            nuevaMesa.SetCombinada('0');
            ////CREAR EL BOTON /////
            System.Windows.Forms.Button btnMesa = new System.Windows.Forms.Button();
            btnMesa.AutoSize = true;
            btnMesa.BackgroundImage = global::Capa_de_Presentacion.Properties.Resources.mesa_libre;
            btnMesa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            btnMesa.Font = new System.Drawing.Font("Lucida Fax", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnMesa.Location = new System.Drawing.Point(21, 19);
            btnMesa.Name = "btnMesa";
            btnMesa.Size = new System.Drawing.Size(117, 110);
            btnMesa.TabIndex = 27;
            btnMesa.Text = nuevaMesa.GetNumeroMesa().ToString();
            btnMesa.UseVisualStyleBackColor = true;
            btnMesa.Click += new System.EventHandler(this.btnMesa_Click);
            ControlMoverOrResizer.Init(btnMesa);
            ////////////////////////
            // ASIGNARSELO A LA MESA//
            nuevaMesa.SetBotonMesa(btnMesa);
            //////////////////////////
            // ASIGNAR LA MESA A LA SALA ACTIVA//
            foreach (System.Windows.Forms.Panel pnl in this.tabControl1.SelectedTab.Controls)
            {
                pnl.Controls.Add(btnMesa);
            }
            /////////////////////////////////////                                   
            // LLAMAR A REGISTRAR MESA//
            String mensaje=nuevaMesa.RegistrarMesa();
            /////////////////////////////////////
        }

        private void FrmMenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                dynamic mboxResult=DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error);                 
                    if (mboxResult == DialogResult.No)
                {
                    /* Cancel the Closing event from closing the form. */
                    e.Cancel = true;
                }

                else if (mboxResult == DialogResult.Yes)
                {
                    /* Closing the form. */
                    e.Cancel = false;
                    Application.Exit();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             foreach (System.Windows.Forms.Panel pnl in this.tabControl1.SelectedTab.Controls)
                {
                    //DENRO DEL PANEL, TENGO QUE ITERAR POR TODOS LOS BOTONES//
                    foreach (System.Windows.Forms.Button btn in pnl.Controls)
                    {
                    String numeroMesa = btn.Text;
                    clsMesa mesaActual = new clsMesa();
                    int posX=btn.Location.X;
                    int posY=btn.Location.Y;
                    int ancho = btn.Size.Width;
                    int alto = btn.Size.Height;                                      
                    mesaActual.ActualizarDimYPos(numeroMesa,posX,posY,ancho,alto);
                    }
                }
            
        }

        private void btnMesa_Click(object sender, EventArgs e)
        {

            Button mesaClickeada = sender as Button;
            clsMesa mesaActual = new clsMesa();
            int id_mesa = mesaActual.ObtenerIdMesa(int.Parse(mesaClickeada.Text));
            String libre=mesaActual.ObtenerEstado(id_mesa).ToString();
            switch (libre)
            {
                case "0":
                    FrmOcuparMesa fom = new FrmOcuparMesa();
                    DialogResult res = fom.ShowDialog(); //Llamamos nuestra ventana hija a manera de DialogResult
                    string cantComensales2;
                    if (res == DialogResult.OK) //Nos debe regresar un Dialogresult.OK
                    {
                        cantComensales2 = fom.cantComensales; //Y listo, nos traermos la informacion                
                        mesaActual.OcuparMesa(id_mesa, cantComensales2);
                        mesaClickeada.BackgroundImage = global::Capa_de_Presentacion.Properties.Resources.mesa_ocupada;
                    }
                    break;    
                case "1":
                    FrmRegistroVentas frv = new FrmRegistroVentas(id_mesa);
                    frv.Show();

                    break;
                default:
                    break;
            }
           
           
            


        }

    }
}
