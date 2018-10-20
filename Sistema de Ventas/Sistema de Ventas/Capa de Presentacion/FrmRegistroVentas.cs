using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevComponents.DotNetBar;
using CapaLogicaNegocio;
using LibPrintTicket;

namespace Capa_de_Presentacion
{
    public partial class FrmRegistroVentas : DevComponents.DotNetBar.Metro.MetroForm
    {
        clsVentas Ventas = new clsVentas();
        clsDetalleVenta Detalle = new clsDetalleVenta();
        clsMesa Mesa = new clsMesa();
        public int id_mesa_actual;
        public Button mesa_clickeada_actual;
        public System.Windows.Forms.FlowLayoutPanel cocina_actual;
        public List<clsVenta> lst = new List<clsVenta>();
        //System.Windows.Forms.DataGridView dataGridView2;

        public FrmRegistroVentas(int id_mesa,Button mesa_clikeada,System.Windows.Forms.FlowLayoutPanel flw)
        {
            InitializeComponent();
            id_mesa_actual = id_mesa;
            mesa_clickeada_actual = mesa_clikeada;
            cocina_actual = flw;
        }

        public FrmRegistroVentas()
        {
            InitializeComponent();            
        }


        private void rbnFactura_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnFactura.Checked == true)
                lblTipo.Text = "FACTURA";
            else
                lblTipo.Text = "BOLETA DE VENTA";
        }

        private void rbnBoleta_CheckedChanged(object sender, EventArgs e)
        {
            GenerarNumeroComprobante();
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            GenerarNumeroComprobante();
            GenerarIdVenta();
            GenerarSeriedeDocumento();
            this.txtIgv.Text = "0";
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            lblNumeroMesa.Text = Mesa.ObtenerNumeroMesaConId(id_mesa_actual).ToString();
            lblCantidadPersonas.Text = Mesa.ObtenerCantidadPersonas(id_mesa_actual).ToString();
            //////////////////////////////////////////////////////////////////

            clsVentas ventaAux = new clsVentas();
            DataTable dt = new DataTable();
            
            clsProducto productoAux = new clsProducto();
            dt = ventaAux.ObtenerDetallesVentas(id_mesa_actual);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                clsVenta detalleVentaAux = new clsVenta();
                Decimal Porcentaje = 0; Decimal SubTotal;
                detalleVentaAux.IdProducto = Convert.ToInt32(dt.Rows[i][1].ToString());
                detalleVentaAux.IdVenta = Convert.ToInt32(dt.Rows[i][2].ToString());
                detalleVentaAux.Descripcion = productoAux.ObtenerNombre(detalleVentaAux.IdProducto) + " - " + productoAux.ObtenerMarca(detalleVentaAux.IdProducto);
                detalleVentaAux.Cantidad = Convert.ToInt32(dt.Rows[i][3].ToString());
                detalleVentaAux.PrecioVenta = Convert.ToDecimal(dt.Rows[i][4].ToString());
                Porcentaje = (Convert.ToDecimal(dt.Rows[i][5].ToString()) / 100) + 1;
                SubTotal = ((Convert.ToDecimal(dt.Rows[i][4].ToString()) * Convert.ToInt32(dt.Rows[i][3].ToString())) / Porcentaje);
                detalleVentaAux.Igv = Math.Round(Convert.ToDecimal(SubTotal) * (Convert.ToDecimal(dt.Rows[i][5].ToString()) / (100)), 2);
                detalleVentaAux.SubTotal = Math.Round(SubTotal, 2);
                detalleVentaAux.Agregada = '1';
                lst.Add(detalleVentaAux);
            }
            //////////////////////////////////////////////////////////////////
            LlenarGrilla();
            //////////////////////////////////////////////////////////////////
            if (dt.Rows.Count != 0)
            {
                this.btnRegistrarVenta.Hide();
                this.btnAgregarACuenta.Show();
            }
            else
            {
                this.btnRegistrarVenta.Show();
                this.btnAgregarACuenta.Hide();

            }
            //////////////////////////////////////////////////////////////////
        }

        private void GenerarIdVenta() {
            txtIdVenta.Text = Ventas.GenerarIdVenta();
        }

        private void GenerarSeriedeDocumento() {
            lblSerie.Text = Ventas.GenerarSerieDocumento();
        }

        private void GenerarNumeroComprobante()
        {
            if (rbnBoleta.Checked == true)
                lblNroCorrelativo.Text = Ventas.NumeroComprobante("Boleta");
            else
                lblNroCorrelativo.Text = Ventas.NumeroComprobante("Factura");
        }
      
        private void FrmVentas_Activated(object sender, EventArgs e)
        {
             txtIdProducto.Text = Program.IdProducto+"";
            txtDescripcion.Text = Program.Descripcion;
            txtMarca.Text = Program.Marca;
            txtStock.Text = Program.Stock+"";
            txtPVenta.Text = Program.PrecioVenta+"";
        }

        private void btnBusquedaProducto_Click(object sender, EventArgs e)
        {
            FrmListadoProductos P = new FrmListadoProductos();
            P.Show();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsVenta V = new clsVenta();
            Decimal Porcentaje = 0; Decimal SubTotal;            
                if (txtDescripcion.Text.Trim() != ""){
                    if (txtCantidad.Text.Trim() != ""){
                        if (Convert.ToInt32(txtCantidad.Text) >= 0){
                            if (Convert.ToInt32(txtCantidad.Text) <= Convert.ToInt32(txtStock.Text)){
                                if (txtIgv.Text.Trim() != ""){
                                    V.IdProducto = Convert.ToInt32(txtIdProducto.Text);
                                    V.IdVenta = Convert.ToInt32(txtIdVenta.Text);                                      
                                    V.Descripcion = txtDescripcion.Text + " - " + txtMarca.Text;
                                    V.Cantidad = Convert.ToInt32(txtCantidad.Text);
                                    V.PrecioVenta = Convert.ToDecimal(txtPVenta.Text);
                                    Porcentaje = (Convert.ToDecimal(txtIgv.Text) / 100) + 1;
                                    SubTotal = ((Convert.ToDecimal(txtPVenta.Text) * Convert.ToInt32(txtCantidad.Text)) / Porcentaje);
                                    V.Igv = Math.Round(Convert.ToDecimal(SubTotal) * (Convert.ToDecimal(txtIgv.Text) / (100)), 2);
                                    V.SubTotal = Math.Round(SubTotal, 2);                                
                                    lst.Add(V);
                                    LlenarGrilla();
                                    Limpiar();
                                }else {
                                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese I.G.V.","Sistema de Ventas.",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                    txtIgv.Focus();
                                }
                            }else{
                                DevComponents.DotNetBar.MessageBoxEx.Show("Stock Insuficiente para Realizar la Venta.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            }
                        }else{
                            DevComponents.DotNetBar.MessageBoxEx.Show("Cantidad Ingresada no Válida.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            txtCantidad.Clear();
                            txtCantidad.Focus();
                        }
                    }
                    else {
                        DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Cantidad a Vender.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        txtCantidad.Focus();
                    }
                }
                else {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Busque el Producto a Vender.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            
        }

        private void LlenarGrilla() {
            Decimal SumaSubTotal = 0; Decimal SumaIgv=0; Decimal SumaTotal=0;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < lst.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = lst[i].IdVenta;
                dataGridView1.Rows[i].Cells[1].Value = lst[i].Cantidad;
                dataGridView1.Rows[i].Cells[2].Value = lst[i].Descripcion;
                dataGridView1.Rows[i].Cells[3].Value = lst[i].PrecioVenta;
                dataGridView1.Rows[i].Cells[4].Value = lst[i].SubTotal;
                dataGridView1.Rows[i].Cells[5].Value = lst[i].IdProducto;
                dataGridView1.Rows[i].Cells[6].Value = lst[i].Igv;
                dataGridView1.Rows[i].Cells[7].Value = lst[i].Agregada;
                SumaSubTotal += Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
                SumaIgv += Convert.ToDecimal(dataGridView1.Rows[i].Cells[6].Value);
            }

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows[lst.Count + 1].Cells[3].Value = "SUB-TOTAL  S/.";
            dataGridView1.Rows[lst.Count + 1].Cells[4].Value = SumaSubTotal;
            dataGridView1.Rows.Add();
            dataGridView1.Rows[lst.Count + 2].Cells[3].Value = "      I.G.V.        %";
            dataGridView1.Rows[lst.Count + 2].Cells[4].Value = SumaIgv;
            dataGridView1.Rows.Add();
            dataGridView1.Rows[lst.Count + 3].Cells[3].Value = "     TOTAL     S/.";
            SumaTotal += SumaSubTotal + SumaIgv;
            dataGridView1.Rows[lst.Count + 3].Cells[4].Value = SumaTotal;
            dataGridView1.ClearSelection();
        }

        private void Limpiar() {
            txtDescripcion.Clear();
            txtMarca.Clear();
            txtStock.Clear();
            txtPVenta.Clear();
            txtCantidad.Clear();
            btnBusquedaProducto.Focus();
            Program.Descripcion = "";
            Program.Stock = 0;
            Program.Marca = "";
            Program.PrecioVenta = 0;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes) {
                this.Close();
            }
            else
            {
                this.Show();
            }
            FrmMenuPrincipal MP = FrmMenuPrincipal.CrearInstancia();
            MP.BringToFront();
        }

        private void btnEliminarItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0){
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected == true){
                    if (Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value) != ""){
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    lst.RemoveAt(dataGridView1.CurrentRow.Index);
                        LlenarGrilla();
                        DevComponents.DotNetBar.MessageBoxEx.Show("Producto Eliminado de la Lista Ok.","Sistema de Ventas.",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }else{
                      DevComponents.DotNetBar.MessageBoxEx.Show("No Existe Ningun Elemento en la Lista.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      dataGridView1.ClearSelection();
                    }
                }else{
                 DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Seleccione Item a Eliminar de la Lista.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else {
                DevComponents.DotNetBar.MessageBoxEx.Show("No Existe Ningun Elemento en la Lista","Sistema de Ventas.",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0) {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
            }
        }

        private void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0){               
                if (Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value) != ""){
                    GuardarVenta();
                    try{
                        for (int i = 0; i < dataGridView1.Rows.Count; i++){
                            Decimal SumaIgv = 0; Decimal SumaSubTotal = 0;
                            if (Convert.ToString(dataGridView1.Rows[i].Cells[2].Value) != ""){
                                SumaIgv += Convert.ToDecimal(dataGridView1.Rows[i].Cells[6].Value);
                                SumaSubTotal += Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);                                
                                GuardarDetalleVenta(
                                Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value),
                                Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value),
                                Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value),
                                Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value),
                                SumaIgv, SumaSubTotal,'1'
                                );                                
                                //DevComponents.DotNetBar.MessageBoxEx.Show("Contiene Datos.");
                            }else{
                                //DevComponents.DotNetBar.MessageBoxEx.Show("Fila Vacia.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
                    }
                }else{
                    DevComponents.DotNetBar.MessageBoxEx.Show("No Existe Ningún Elemento en la Lista.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                DevComponents.DotNetBar.MessageBoxEx.Show("No Existe Ningún Elemento en la Lista.","Sistema de Ventas.",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void GuardarVenta()
        {
            decimal Total=0;
            if (Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value) != "") {
                for (int i = 0; i < dataGridView1.Rows.Count; i++){
			        Total=Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
			    }
            string TipoDocumento = "";
            TipoDocumento = rbnBoleta.Checked == true ? "Boleta" : "Factura";
            Ventas.IdEmpleado=Program.IdEmpleadoLogueado;
            Ventas.IdMesa = id_mesa_actual;
            Ventas.Serie=lblSerie.Text;
            Ventas.NroComprobante=lblNroCorrelativo.Text;
            Ventas.TipoDocumento=TipoDocumento;
            Ventas.FechaVenta=Convert.ToDateTime(dateTimePicker1.Value);
            Ventas.Total=Total;
            DevComponents.DotNetBar.MessageBoxEx.Show(Ventas.RegistrarVenta(),"Sistema de Ventas.",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void GuardarDetalleVenta(Int32 objIdProducto, Int32 objIdVenta, Int32 objCantidad, Decimal objPUnitario,
            Decimal objIgv, Decimal objSubTotal,char objAgregada){
                    Detalle.IdProducto = objIdProducto;
                    Detalle.IdVenta = objIdVenta;
                    Detalle.Cantidad = objCantidad;
                    Detalle.PUnitario = objPUnitario;
                    Detalle.Igv = objIgv;
                    Detalle.SubTotal = objSubTotal;
                    Detalle.Agregada = objAgregada;
                    Detalle.RegistrarDetalleVenta();
                    Limpiar1();
                    GenerarIdVenta();
                    GenerarNumeroComprobante();
        }

        private void Limpiar1() {
           // txtIgv.Clear();
            //dataGridView1.Rows.Clear();            
            //Program.IdEmpleadoLogueado = 0;           
            txtIdProducto.Clear();
            rbnBoleta.Checked = true;
        }

        private void btnCerrarMesa_Click(object sender, EventArgs e)
        {
            int id_venta=0;
            if (lst!=null)
            {
               id_venta = lst[0].IdVenta;
            }

            clsVentas ventaAux = new clsVentas();
            int nroTicket = ventaAux.ObtenerNroTicket(id_venta);
            ////////////////////////////////////////////////////////
            ////////////// IMPRIMIR TICKET FISCAL //////////////////
            ////////////////////////////////////////////////////////
            Ticket ticket = new Ticket();
            //Image img = Image.FromFile("C:/Users/Lihuen/Desktop/LibPrintTicket/LibPrintTicket/TestLib/Logomas58.jpg");
            //ticket.HeaderImage = img; //esta propiedad no es obligatoria

            ticket.AddHeaderLine("############ REST +58 ############");
            ticket.AddHeaderLine("EXPEDIDO EN: +58 SUCURSAL LA PLATA");
            ticket.AddHeaderLine("CALLE 11 E/ 47 Y 48");                      

            //El metodo AddSubHeaderLine es lo mismo al de AddHeaderLine con la diferencia
            //de que al final de cada linea agrega una linea punteada "=========="
            ticket.AddSubHeaderLine("Caja # 1 - Ticket # "+ lblNroCorrelativo.Text);            
            ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            decimal total = 0;
            //El metodo AddItem requeire 3 parametros, el primero es cantidad, el segundo es la descripcion
            //del producto y el tercero es el precio
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                if (this.dataGridView1.Rows[i].Cells[1].Value !=null)
                {
                    String cantidad = this.dataGridView1.Rows[i].Cells[1].Value.ToString();
                    String producto = this.dataGridView1.Rows[i].Cells[2].Value.ToString();
                    decimal cant = decimal.Parse(this.dataGridView1.Rows[i].Cells[1].Value.ToString());
                    decimal precio = decimal.Parse(this.dataGridView1.Rows[i].Cells[3].Value.ToString());
                    decimal importe_d = cant * precio;
                    String importe = importe_d.ToString();
                    ticket.AddItem(cantidad,producto,importe);
                    total += importe_d ;
                }

            }                       

            //El metodo AddTotal requiere 2 parametros, la descripcion del total, y el precio
            ticket.AddTotal("SUBTOTAL", total.ToString());
            ticket.AddTotal("IVA", "0.00");
            ticket.AddTotal("TOTAL", total.ToString());
            ticket.AddTotal("", ""); //Ponemos un total en blanco que sirve de espacio                      

            ticket.AddSubHeaderLine("");
            //El metodo AddFooterLine funciona igual que la cabecera
            ticket.AddFooterLine("       GRACIAS POR SU COMPRA       ");
            ticket.AddFooterLine("***********************************");
            ticket.AddFooterLine("       SEGUINOS EN INSTAGRAM       ");
            ticket.AddFooterLine("            @rest.mas58            ");

            //Y por ultimo llamamos al metodo PrintTicket para imprimir el ticket, este metodo necesita un
            //parametro de tipo string que debe de ser el nombre de la impresora.
            ticket.PrintTicket("Foxit Reader PDF Printer");
            ////////////////////////////////////////////////////////
            dataGridView1.Rows.Clear();
            ////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////


            clsDetalleVenta detalleVentaAux = new clsDetalleVenta();
            detalleVentaAux.MandarAlHistorial(id_mesa_actual);
            detalleVentaAux.EliminarDetallesVentas(id_mesa_actual);
            DevComponents.DotNetBar.MessageBoxEx.Show("Se cerro la mesa con exito", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Mesa.DesocuparMesa(id_mesa_actual);
            mesa_clickeada_actual.BackgroundImage = global::Capa_de_Presentacion.Properties.Resources.mesa_libre;
            this.Close();
            FrmMenuPrincipal MP = FrmMenuPrincipal.CrearInstancia();
            MP.BringToFront();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                this.btnAgregar_Click(sender, e);
        }

        private void txtIgv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                this.btnAgregar_Click(sender,e);
        }

        private void MandarACocina()
        {
            List<String> lstNombrePedidos = new List<string>();
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (this.dataGridView1.Rows[i].Cells[2].ToString()!="")
                {
                    lstNombrePedidos.Add(this.dataGridView1.Rows[i].Cells[2].ToString());
                }
                
            }
            
        }

        private void btnAgregarACuenta_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value) != "")
            {
                GuardarVenta();
                try
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[7].Value!=null)
                        {
                            if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "0")
                            {// SI NO ESTA AGREGADA//
                             //LA AGREGO//
                                Decimal SumaIgv = 0; Decimal SumaSubTotal = 0;
                                if (Convert.ToString(dataGridView1.Rows[i].Cells[2].Value) != "")
                                {
                                    SumaIgv += Convert.ToDecimal(dataGridView1.Rows[i].Cells[6].Value);
                                    SumaSubTotal += Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
                                    GuardarDetalleVenta(
                                    Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value),
                                    Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value),
                                    Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value),
                                    Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value),
                                    SumaIgv, SumaSubTotal, '1'
                                    );
                                    //DevComponents.DotNetBar.MessageBoxEx.Show("Contiene Datos.");
                                }
                                else
                                {
                                    //DevComponents.DotNetBar.MessageBoxEx.Show("Fila Vacia.");
                                }

                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
                }
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("No Existe Ningún Elemento en la Lista.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
