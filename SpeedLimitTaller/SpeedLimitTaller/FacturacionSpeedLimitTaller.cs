/**
    @@@@  @@@@  @@@@  @@@@  @@@@  @     @@@@  @@    @@  @@@@  @@@@@@    @@@@  @@@@@  @     @     @@@@  @@@@@@
    @@    @  @  @     @     @  @  @      @@   @@ @@ @@   @@    @@        @@   @@ @@  @     @     @     @@ @@@
    @@@@  @@@@  @@@@  @@@@  @  @  @      @@   @@    @@   @@    @@        @@   @@@@@  @     @     @@@@  @@@@@@
      @@  @@    @     @     @  @  @      @@   @@    @@   @@    @@        @@   @@ @@  @     @     @     @@  @@
    @@@@  @@    @@@@  @@@@  @@@@  @@@@  @@@@  @@    @@  @@@@   @@        @@   @@ @@  @@@@  @@@@  @@@@  @@   @@
**/



/**********************************************************************
 **********************************************************************
 **********************************************************************
 * AUTOR: DANIEL RIVERA                                               *
 * SISTEMA DE GESTION PARA CADENA REPUESTOS AUTOMOTRICES Y TALLER     *
 * SPEEDLIMIT TALLER S.A DE C.V.                                      *
 * © COPYRIGHT 2019 RESERVADOS TODOS LOS DERECHOS.                    *
 * VERSIÓN MEJORADA CON INTERFAZ GRÁFICA --> [2.0].                   *
 * SISTEMA TOTALMENTE DESARROLLADO DE CERO.                           *
 * PROYECTO LIBERADO Y COMPARTIDO PARA FINES EDUCATIVOS.              *
 **********************************************************************
 * PARA UNA CORRECTA VISUALIZACIÓN, LE RECOMENDAMOS UNA RESOLUCIÓN    *
 *                MAYOR O IGUAL A 1280 X 768                          *
 **********************************************************************
 * VERSIÓN ANTERIOR [1.0] --> DESARROLLADA EN C++                     *                
 * https://github.com/DanielRivera03/Sistema-Venta-Repuestos          *
***********************************************************************
***********************************************************************
***********************************************************************/
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Drawing.Printing;
using System.Drawing;
// IMPORTANDO LIBRERIA SERVICIO CLIENTE C# -> SQL SERVER
using System.Data.SqlClient;


namespace SpeedLimitTaller
{
    public partial class FacturacionSpeedLimitTaller : Form
    {

        // CONEXION C# A BASE DE DATOS
        SqlConnection con = new SqlConnection(@"Data Source=SONYVAIO\SQLEXPRESS;Initial Catalog=speedlimitdb;Integrated Security=True");


        public FacturacionSpeedLimitTaller()
        {
            InitializeComponent();
            // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
            SqlCommand cmd = new SqlCommand("Select ID_inventario, ID_producto, Cod_producto, Nombre, Marca, Modelo, Precio, Numeros_Unidades_Stock from ControlInventarios", con);
            // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
            SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
            MostrarRegistros.SelectCommand = cmd;
            // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
            DataTable TablaRegistros = new DataTable();
            // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
            MostrarRegistros.Fill(TablaRegistros);
            // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
            // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {Empleados}
            ProductosFacturarSistema.DataSource = TablaRegistros;
            // RESETEANDO LA COLUMNA DE PRODUCTOS PARA QUE MUESTRE UNICAMENTE .2 DECIMALES DESPUES DE LA CIFRA EXACTA
            ProductosFacturarSistema.Columns[6].DefaultCellStyle.Format = "N2";
            DetallesFacturacionProductosSistema.Columns[6].DefaultCellStyle.Format = "N2";
            DetallesFacturacionProductosSistema.Columns[7].DefaultCellStyle.Format = "N2";
        }

        private void FacturacionSpeedLimitTaller_Load(object sender, EventArgs e)
        {
            FechaFacturacion.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        /*
            --> BOTON SELECCIONAR PRODUCTOS A FACTURAR 
        */

        private void SeleccionProductoFactura_Click(object sender, EventArgs e)
        {
            // SI EL USUARIO HA SELECCIONADO UN REGISTRO, ENTONCES...
            if (ProductosFacturarSistema.SelectedRows.Count > 0)
            {
                /* EL DATAGRIDVIEW SE ENCARGA DE MOSTRAR AL USUARIO LOS DATOS ALMACENADOS EN LA BASE DE DATOS
                * POR LO TANTO AL MOMENTO QUE EL USUARIO SELECCIONE DICHO REGISTRO Y PRESIONE EL BOTON SELECCIONAR
                * USUARIO, SE PROCEDE A TOMAR CADA UNO DE LOS CAMPOS ALMACENADOS EN LOS TEXTBOX CON SUS IDENTIFICADORES
                * UNICOS Y REALIZA LA RESPECTIVA CONVERSION A CADENA PARA QUE ESA INFORMACION SEA VISIBLE AL USUARIO
                * FINAL Y PUEDA PROCEDER A ACTUALIZAR EL N REGISTRO QUE DESEE...
                */
                txtcodigounicoproducto.Text = ProductosFacturarSistema.CurrentRow.Cells["Cod_producto"].Value.ToString();               // CODIGO UNICO PRODUCTO
                txtnombreproducto.Text = ProductosFacturarSistema.CurrentRow.Cells["Nombre"].Value.ToString();                          // NOMBRE PRODUCTO
                txtmarcaproducto.Text = ProductosFacturarSistema.CurrentRow.Cells["Marca"].Value.ToString();                            // MARCA PRODUCTO
                txtmodeloproducto.Text = ProductosFacturarSistema.CurrentRow.Cells["Modelo"].Value.ToString();                          // MODELO PRODUCTO
                txtprecioproducto.Text = ProductosFacturarSistema.CurrentRow.Cells["Precio"].Value.ToString();                          // PRECIO PRODUCTO
                txtnumstockinventario.Text = ProductosFacturarSistema.CurrentRow.Cells["Numeros_Unidades_Stock"].Value.ToString();      // NUMERO DE UNIDADES EN STOCK
                txtidinventario.Text = ProductosFacturarSistema.CurrentRow.Cells["ID_inventario"].Value.ToString();                     // ID DE REFERENCIA DE INVENTARIO X PRODUCTO A PROCESAR
                con.Close();  // CIERRE DE CONEXION UNA VEZ TOMADOS LOS PARAMETROS Y ARGUMENTOS PREVIO A LA FACTURACION          
            }// DE LO CONTRARIO...
            else
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos pero debe seleccionar un registro, por favor seleccione el registro deseado a facturar.", "Error de Facturación",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK) ;
            }
        }

        /*
           --> REGISTRAR PRODUCTOS A CARRITO DE COMPRAS GENERAL 
       */

        private void RegistroProductosCarritoCompraFacturacion_Click(object sender, EventArgs e)
        {
            // SIMULANDO UNA CAJA REGISTRADORA, POR LO CUAL SI PUEDEN EXISTIR PRODUCTOS REPETIDOS...
            if(txtidinventario.Text.Length == 0 || txtCantidadProductos.Text.Length == 0 || txtcodigounicoproducto.Text.Length == 0 || txtmarcaproducto.Text.Length == 0 || txtmodeloproducto.Text.Length == 0 || txtnombreproducto.Text.Length == 0 || txtnumstockinventario.Text.Length == 0 || txtprecioproducto.Text.Length == 0)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos pero debe seleccionar un registro, por favor seleccione el registro deseado a facturar.", "Error de Facturación",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK) ;
            }
            else
            {// SI NO HAY CAMPOS VACIOS, PROCEDE A FACTURAR...
                // CALCULANDO PRODUCTOS CON IVA INCLUIDO (13%)
                double CalculoProductos = (double.Parse(txtprecioproducto.Text)) * (double.Parse(txtCantidadProductos.Text)); // --> CALCULA EL TOTAL DE LOS PRODUCTOS
                double CalculoIVA = CalculoProductos * .13; // --> CALCULA EL TOTAL DE LOS PRODUCTOS POR (*) EL 13% DE IVA
                double TotalFinalIVA = CalculoProductos + CalculoIVA; // -> SUMA EL RESULTADO ANTERIOR + EL CALCULO DEL 13% DE IVA
                // IMPRESION DE TABLA DE FACTURACION...
                DetallesFacturacionProductosSistema.Rows.Add(txtcodigounicoproducto.Text, txtnombreproducto.Text, txtmarcaproducto.Text, txtmodeloproducto.Text, txtprecioproducto.Text, txtCantidadProductos.Text, (double.Parse(txtprecioproducto.Text) * (double.Parse(txtCantidadProductos.Text))), TotalFinalIVA);
                string query = "UPDATE ControlInventarios SET Numeros_Unidades_Stock = Numeros_Unidades_Stock - @numstockventa  WHERE Cod_producto=@Cod_producto";
                // --> WHERE ID_inventario=@ID_inventario" IMPORTANTE: SI NO SE REFERENCIA POR DEFECTO NO MUESTRA ERROR PERO SE PRODUCE UN ERROR LOGICO, AL ACTUALIZAR ABSOLUTAMENTE
                // TODOS LOS DATOS DE LA TABLA REFERENCIADA EN LA BASE DE DATOS.
                con.Open();// APERTURANDO CONEXION
                SqlCommand comando = new SqlCommand(query, con);// CREANDO COMPONENTE DE COMUNICACION HACIA LA BASE DE DATOS
                // ENVIO DE PARAMETROS CON LOS DATOS SELECCIONADOS PREVIAMENTE POR EL USUARIO HACIA LA BASE DE DATOS
                comando.Parameters.AddWithValue("@numstockventa", txtCantidadProductos.Text);   // NUMERO DE STOCK DE PRODUCTOS A PROCESAR EN FACTURACION
                comando.Parameters.AddWithValue("@Cod_producto", txtcodigounicoproducto.Text);  // REFERENCIANDO ID INVENTARIO A PROCESAR EN FACTURACION
                comando.ExecuteNonQuery(); // EJECUTANDO COMANDO Y ENVIANDO DATOS HACIA LA BASE DE DATOS
                con.Close(); // CERRANDO CONEXION ENVIANDO LAS NUEVA ACTUALIZACION HACIA LA BASE DE DATOS
                // INICIALIZANDO TODAS LAS CAJAS DE TEXTO LUEGO DE LA TRANSACCION
                txtCantidadProductos.Text = " "; txtcodigounicoproducto.Text = " "; txtidinventario.Text = " ";
                txtmarcaproducto.Text = " "; txtmodeloproducto.Text = " "; txtnombreproducto.Text = " ";
                txtnumstockinventario.Text = " "; txtprecioproducto.Text = " ";
                /*
                    --> ACTUALIZANDO VISTA TABLA DE PRODUCTOS CON STOCK TOTALMENTE ACTUALIZADO SEGUN N TRANSACCIONES 
                    QUE SE REALICEN EN EL SISTEMA... 
                */
                // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
                SqlCommand cmd = new SqlCommand("Select ID_inventario, ID_producto, Cod_producto, Nombre, Marca, Modelo, Precio, Numeros_Unidades_Stock from ControlInventarios", con);
                // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
                SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
                MostrarRegistros.SelectCommand = cmd;
                // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
                DataTable TablaRegistros = new DataTable();
                // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
                MostrarRegistros.Fill(TablaRegistros);
                // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
                // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {Empleados}
                ProductosFacturarSistema.DataSource = TablaRegistros;
            }
        }

        private void FinalizarCarritoComprasFacturacion_Click(object sender, EventArgs e)
        {
            // SIMULANDO UNA CAJA REGISTRADORA, POR LO CUAL SI PUEDEN EXISTIR PRODUCTOS REPETIDOS...
            if (txtidinventario.Text.Length == 0 || txtCantidadProductos.Text.Length == 0 || txtcodigounicoproducto.Text.Length == 0 || txtmarcaproducto.Text.Length == 0 || txtmodeloproducto.Text.Length == 0 || txtnombreproducto.Text.Length == 0 || txtnumstockinventario.Text.Length == 0 || txtprecioproducto.Text.Length == 0)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos pero debe seleccionar un registro, no podemos procesar ventas vacías, por favor seleccione el registro deseado a facturar.", "Error de Facturación",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK) ;
            } else {
                // CREANDO VARIABLE TIPO QUERY QUE ALMACENA LA CADENA DE NUEVA MODIFICACION CON SU CONDICION
                // TOMANDO VALOR INGRESADO POR EMPLEADO Y DESCONTANDO ESE N VALOR AL N VALOR ALMACENADO DE STOCK DE PRODUCTOS QUE SE ESTAN VENDIENDO
                
                /*
                    --> ACTUALIZANDO VISTA TABLA DE PRODUCTOS CON STOCK TOTALMENTE ACTUALIZADO SEGUN N VENTAS 
                    QUE SE REALICEN EN EL SISTEMA... 
                */
                // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
                SqlCommand cmd = new SqlCommand("Select ID_inventario, ID_producto, Cod_producto, Nombre, Marca, Modelo, Precio, Numeros_Unidades_Stock from ControlInventarios", con);
                // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
                SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
                MostrarRegistros.SelectCommand = cmd;
                // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
                DataTable TablaRegistros = new DataTable();
                // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
                MostrarRegistros.Fill(TablaRegistros);
                // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
                // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {Empleados}
                ProductosFacturarSistema.DataSource = TablaRegistros;
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Gracias por preferirnos, en estos momentos se le desplegará una ventana donde está el detalle final de productos adquiridos por el cliente, acto donde le instamos a guardar cada reporte de ventas según lo indica el manual del sistema.\n\tAtte: Gerencia SpeedLimit Taller S.A de C.V", "Venta de Productos Finalizada",
                    MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
                /*
                    --> MANDAR A IMPRESORA FACTURA FINAL DE VENTA REALIZADA {NO GUARDANDO ARCHIVO} 
                    --> IMPRESION DE FACTURA FINAL CON LOS PRODUCTOS REGISTRADOS A LA COMPRA DE LOS CLIENTES 
                */
                PrintDocument doc = new PrintDocument();
                doc.DefaultPageSettings.Landscape = true;
                doc.PrinterSettings.PrinterName = "Microsoft XPS Document Writer"; // --> NOMBRE DE LA IMPRESORA {ASIGNADA POR DEFECTO DEL SISTEMA}
                PrintPreviewDialog ppd = new PrintPreviewDialog { Document = doc };
                ((Form)ppd).WindowState = FormWindowState.Maximized;
                doc.PrintPage += delegate (object ev, PrintPageEventArgs ep)
                {
                    const int EspaciadoFactura = 55;
                    int left = ep.MarginBounds.Left, top = ep.MarginBounds.Top;

                    foreach (DataGridViewColumn col in DetallesFacturacionProductosSistema.Columns)
                    {
                        ep.Graphics.DrawString(col.HeaderText, new Font("Verdana", 10, FontStyle.Bold), Brushes.DarkSalmon, left, top);
                        left += col.Width;

                        if (col.Index < DetallesFacturacionProductosSistema.ColumnCount)
                            ep.Graphics.DrawLine(Pens.Gray, left - 5, top, left - 5, top + 50 + (DetallesFacturacionProductosSistema.RowCount) * EspaciadoFactura);
                    }
                    left = ep.MarginBounds.Left;
                    ep.Graphics.FillRectangle(Brushes.Black, left, top + 40, ep.MarginBounds.Right - left, 3);
                    top += 43;
                    foreach (DataGridViewRow row in DetallesFacturacionProductosSistema.Rows)
                    {
                        if (row.Index == DetallesFacturacionProductosSistema.RowCount) break;
                        left = ep.MarginBounds.Left;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            ep.Graphics.DrawString(Convert.ToString(cell.Value), new Font("Arial", 10), Brushes.Black, left, top + 20);
                            left += cell.OwningColumn.Width;
                        }
                        top += EspaciadoFactura;
                        ep.Graphics.DrawLine(Pens.Gray, ep.MarginBounds.Left, top, ep.MarginBounds.Right, top);
                    }
                };
                ppd.ShowDialog();

            }
        }

        /*
            --> BOTON ELIMINAR PRODUCTOS DE FACTURACION FINAL 
        */
        private void BorrarProductoFactura_Click(object sender, EventArgs e)
        {
            if (txtidinventario.Text.Length == 0 || txtCantidadProductos.Text.Length == 0 || txtcodigounicoproducto.Text.Length == 0 || txtmarcaproducto.Text.Length == 0 || txtmodeloproducto.Text.Length == 0 || txtnombreproducto.Text.Length == 0 || txtnumstockinventario.Text.Length == 0 || txtprecioproducto.Text.Length == 0)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos pero al menos debe haber un producto registrado para proceder a su depuración, por favor ingrese los productos a facturar y elimine en el caso de ser necesario el producto que no se desee facturar.", "Error Al Intentar Eliminar Un Producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK) ;
            }
            else
            {
                DetallesFacturacionProductosSistema.Rows.RemoveAt(DetallesFacturacionProductosSistema.CurrentRow.Index);
            }
        }

        /*
            --> BOTON CANCELAR TRANSACCION {VENTA DE PRODUCTOS} 
        */

        private void CancelarTransaccion_Click(object sender, EventArgs e)
        {
            if (txtidinventario.Text.Length == 0 || txtCantidadProductos.Text.Length == 0 || txtcodigounicoproducto.Text.Length == 0 || txtmarcaproducto.Text.Length == 0 || txtmodeloproducto.Text.Length == 0 || txtnombreproducto.Text.Length == 0 || txtnumstockinventario.Text.Length == 0 || txtprecioproducto.Text.Length == 0)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos, pero debe encontrarse al menos un registro facturado para poder cancelar la transacción", "Error de Cancelación",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK) ;
            } else {
                string query = "UPDATE ControlInventarios SET Numeros_Unidades_Stock = Numeros_Unidades_Stock + @numstockventa  WHERE Cod_producto=@Cod_producto";
                // --> WHERE ID_inventario=@ID_inventario" IMPORTANTE: SI NO SE REFERENCIA POR DEFECTO NO MUESTRA ERROR PERO SE PRODUCE UN ERROR LOGICO, AL ACTUALIZAR ABSOLUTAMENTE
                // TODOS LOS DATOS DE LA TABLA REFERENCIADA EN LA BASE DE DATOS.
                con.Open();// APERTURANDO CONEXION
                SqlCommand comando = new SqlCommand(query, con);// CREANDO COMPONENTE DE COMUNICACION HACIA LA BASE DE DATOS
                                                                // ENVIO DE PARAMETROS CON LOS DATOS SELECCIONADOS PREVIAMENTE POR EL USUARIO HACIA LA BASE DE DATOS
                comando.Parameters.AddWithValue("@numstockventa", txtCantidadProductos.Text);   // NUMERO DE STOCK DE PRODUCTOS A PROCESAR EN FACTURACION
                comando.Parameters.AddWithValue("@Cod_producto", txtcodigounicoproducto.Text);  // REFERENCIANDO ID INVENTARIO A PROCESAR EN FACTURACION
                comando.ExecuteNonQuery(); // EJECUTANDO COMANDO Y ENVIANDO DATOS HACIA LA BASE DE DATOS
                /*
                  --> ACTUALIZANDO VISTA TABLA DE PRODUCTOS CON STOCK TOTALMENTE ACTUALIZADO SEGUN N VENTAS 
                  QUE SE REALICEN EN EL SISTEMA... 
                */
                // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
                SqlCommand cmd = new SqlCommand("Select ID_inventario, ID_producto, Cod_producto, Nombre, Marca, Modelo, Precio, Numeros_Unidades_Stock from ControlInventarios", con);
                // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
                SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
                MostrarRegistros.SelectCommand = cmd;
                // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
                DataTable TablaRegistros = new DataTable();
                // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
                MostrarRegistros.Fill(TablaRegistros);
                // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
                // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {Empleados}
                ProductosFacturarSistema.DataSource = TablaRegistros;
                con.Close(); // CERRANDO CONEXION ENVIANDO LAS NUEVA ACTUALIZACION HACIA LA BASE DE DATOS
            }
        }
    }
}



/**
    @@@@  @@@@  @@@@  @@@@  @@@@  @     @@@@  @@    @@  @@@@  @@@@@@    @@@@  @@@@@  @     @     @@@@  @@@@@@
    @@    @  @  @     @     @  @  @      @@   @@ @@ @@   @@    @@        @@   @@ @@  @     @     @     @@ @@@
    @@@@  @@@@  @@@@  @@@@  @  @  @      @@   @@    @@   @@    @@        @@   @@@@@  @     @     @@@@  @@@@@@
      @@  @@    @     @     @  @  @      @@   @@    @@   @@    @@        @@   @@ @@  @     @     @     @@  @@
    @@@@  @@    @@@@  @@@@  @@@@  @@@@  @@@@  @@    @@  @@@@   @@        @@   @@ @@  @@@@  @@@@  @@@@  @@   @@
**/
