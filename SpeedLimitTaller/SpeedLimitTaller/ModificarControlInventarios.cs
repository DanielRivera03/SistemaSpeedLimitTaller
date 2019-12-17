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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// IMPORTANDO LIBRERIA SERVICIO CLIENTE C# -> SQL SERVER
using System.Data.SqlClient;
// IMPORTANDO LIBRERIA QUE HABILITA EL EVENTO DE ARRASTRES DE FORMULARIOS POR PARTE DE LOS USUARIOS
using System.Runtime.InteropServices;

namespace SpeedLimitTaller
{
    public partial class ModificarControlInventarios : Form
    {

        /*********************************************************************************************
        * HABILITANDO EL ARRASTRE DEL FORMULARIO A X POSICION EN PANTALLA POR PARTE DEL USUARIO
        * --> CODIGO DE INICIALIZACION DEL EVENTO
        *********************************************************************************************/
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        // FIN INICIALIZACION DE EVENTO ARRASTRE DE FORMULARIOS


        // CONEXION C# A BASE DE DATOS
        SqlConnection con = new SqlConnection(@"Data Source=SONYVAIO\SQLEXPRESS;Initial Catalog=speedlimitdb;Integrated Security=True");


        // CREACION DE OBJETOS CONTROLADORES DE COMBOBOX
        ControlesCombobox DatosProveedores = new ControlesCombobox();   // DATOS DE PROVEEDORES
        ControlesCombobox DatosCategorias = new ControlesCombobox();    // DATOS DE CATEGORIAS


        public ModificarControlInventarios()
        {
            InitializeComponent();
            // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
            SqlCommand cmd = new SqlCommand("Select ID_inventario, Numeros_Unidades_Stock, Periodo_Garantia, ID_producto, Cod_producto, Nombre, Marca, Modelo, Precio, Nombre_proveedor, Nombre_Categoria FROM ControlInventarios", con);
            // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
            SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
            MostrarRegistros.SelectCommand = cmd;
            // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
            DataTable TablaRegistros = new DataTable();
            // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
            MostrarRegistros.Fill(TablaRegistros);
            // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
            // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {CONTROL DE INVENTARIOS}
            DetallesInformeInventarios.DataSource = TablaRegistros;
            // RESETEANDO LA COLUMNA DE SALARIOS PARA QUE MUESTRE UNICAMENTE .2 DECIMALES DESPUES DE LA CIFRA EXACTA
            DetallesInformeInventarios.Columns[8].DefaultCellStyle.Format = "N2";
            /*
             * INVOCANDO METODO PARA PASO DE DATOS DE COMBOBOS HACIA BASE DE DATOS
             */
            DatosProveedores.SeleccionarDatos(txtProveedorProducto);       // PROVEEDORES DE PRODUCTOS
            DatosCategorias.SeleccionarDatosCt(txtCategoriaProductos);     // CATEGORIAS DE PRODUCTOS

        }

        /*
        --> HABILITANDO FUNCION DE ARRASTRE DE FORMULARIO POR PARTE DEL USUARIO 
        */

        private void ModificarControlInventarios_MouseDown(object sender, MouseEventArgs e)
        {
            // HABILITANDO FUNCION DE ARRASTRE DE FORMULARIO
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /*
            --> MINIMIZAR VENTANA 
        */

        private void MinimizarVentana_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /*
            --> CERRAR VENTANA 
        */

        private void CerrarVentana_Click(object sender, EventArgs e)
        {
            this.Hide(); // UNICAMENTE OCULTA VENTANA FLOTANTE, NO OBSTANTE CIERRA LA CONEXION PARA ASI PODER PROCEDER A REALIZAR NUEVAS ACTUALIZACIONES REQUERIDAS
        }

        /*
            --> BOTON SELECCIONAR REGISTRO A MODIFICAR {CONTROL DE INVENTARIOS} 
        */

        private void SeleccionarProductoInventario_Click(object sender, EventArgs e)
        {
            // SI EL USUARIO HA SELECCIONADO UN REGISTRO, ENTONCES...
            if (DetallesInformeInventarios.SelectedRows.Count > 0)
            {
                /* EL DATAGRIDVIEW SE ENCARGA DE MOSTRAR AL USUARIO LOS DATOS ALMACENADOS EN LA BASE DE DATOS
                * POR LO TANTO AL MOMENTO QUE EL USUARIO SELECCIONE DICHO REGISTRO Y PRESIONE EL BOTON SELECCIONAR
                * USUARIO, SE PROCEDE A TOMAR CADA UNO DE LOS CAMPOS ALMACENADOS EN LOS TEXTBOX CON SUS IDENTIFICADORES
                * UNICOS Y REALIZA LA RESPECTIVA CONVERSION A CADENA PARA QUE ESA INFORMACION SEA VISIBLE AL USUARIO
                * FINAL Y PUEDA PROCEDER A ACTUALIZAR EL N REGISTRO QUE DESEE...
                */
                txtidinventarios.Text = DetallesInformeInventarios.CurrentRow.Cells["ID_inventario"].Value.ToString();
                txtnumstockinventario.Text = DetallesInformeInventarios.CurrentRow.Cells["Numeros_Unidades_Stock"].Value.ToString();
                txtgarantiainventario.Text = DetallesInformeInventarios.CurrentRow.Cells["Periodo_Garantia"].Value.ToString();
                txtidproductos.Text = DetallesInformeInventarios.CurrentRow.Cells["ID_producto"].Value.ToString();
                txtcodigounicoproducto.Text = DetallesInformeInventarios.CurrentRow.Cells["Cod_producto"].Value.ToString();
                txtnombreproducto.Text = DetallesInformeInventarios.CurrentRow.Cells["Nombre"].Value.ToString();
                txtmarcaproducto.Text = DetallesInformeInventarios.CurrentRow.Cells["Marca"].Value.ToString();
                txtmodeloproducto.Text = DetallesInformeInventarios.CurrentRow.Cells["Modelo"].Value.ToString();
                txtprecioproducto.Text = DetallesInformeInventarios.CurrentRow.Cells["Precio"].Value.ToString();
                txtProveedorProducto.Text = DetallesInformeInventarios.CurrentRow.Cells["Nombre_proveedor"].Value.ToString();
                txtCategoriaProductos.Text = DetallesInformeInventarios.CurrentRow.Cells["Nombre_Categoria"].Value.ToString();
            } else
            {
                // SI USUARIO NO SELECCIONA UN REGISTRO...
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos pero debe seleccionar un registro, por favor seleccione el registro deseado ha actualizar.", "Error de Modificación",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK) ;
            }
        }

        /*
            --> BOTON MODIFICAR REGISTRO CONTROL DE INVENTARIOS 
        */

        private void RegistroNuevoInventario_Click(object sender, EventArgs e)
        {
            try
            {
                // SI EXISTEN CAMPOS VACIOS, ENTONCES...
                if (txtnumstockinventario.Text.Length == 0 || txtgarantiainventario.Text.Length == 0 || txtcodigounicoproducto.Text.Length == 0 || txtnombreproducto.Text.Length == 0 || txtmarcaproducto.Text.Length == 0 || txtmodeloproducto.Text.Length == 0 || txtprecioproducto.Text.Length == 0 || txtProveedorProducto.Text.Length == 0 || txtCategoriaProductos.Text.Length == 0)
                {
                    // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Lo sentimos pero ha ocurrido un error, tome en cuenta lo siguiente:\n- No puede dejar campos vacíos.\n- Por favor sea cuidadoso a la hora de modificar un registro de control de inventarios, respetando las métricas establecidas por la empresa.", "Error al Intentar Modificar Registro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) ;
                    con.Close();// AL FINALIZAR CADA NUEVO REGISTRO, SE PROCEDE A CERRAR LA CONEXION A ESPERA DE UNA NUEVA PETICION
                }// DE LO CONTRARIO, SI HAY SELECCION DE REGISTRO, ENTONCES...
                else
                {
                    // CREANDO VARIABLE TIPO QUERY QUE ALMACENA LA CADENA DE NUEVA MODIFICACION CON SU CONDICION
                    string query = "UPDATE ControlInventarios SET Numeros_Unidades_Stock = @numunidadesStock, Periodo_Garantia = @periodogarantia, Cod_producto = @Codigoi, Nombre = @Nombrei, Marca = @Marcai, Modelo = @Modeloi, Precio = @Precioi, Nombre_proveedor = @NombreProveedori, Nombre_Categoria = @NombreCategoriai WHERE ID_inventario=@ID_inventario";
                    // --> WHERE ID_inventario=@ID_inventario" IMPORTANTE: SI NO SE REFERENCIA POR DEFECTO NO MUESTRA ERROR PERO SE PRODUCE UN ERROR LOGICO, AL ACTUALIZAR ABSOLUTAMENTE
                    // TODOS LOS DATOS DE LA TABLA REFERENCIADA EN LA BASE DE DATOS.
                    con.Open();// APERTURANDO CONEXION
                    SqlCommand comando = new SqlCommand(query, con);// CREANDO COMPONENTE DE COMUNICACION HACIA LA BASE DE DATOS
                                                                    // ENVIO DE PARAMETROS CON LOS DATOS SELECCIONADOS PREVIAMENTE POR EL USUARIO HACIA LA BASE DE DATOS
                    comando.Parameters.AddWithValue("@numunidadesStock", txtnumstockinventario.Text);       // NUMERO DE UNIDADES EN STOCK
                    comando.Parameters.AddWithValue("@periodogarantia", txtgarantiainventario.Text);        // PERIODO DE GARANTIA
                    comando.Parameters.AddWithValue("@ID_producto", txtidproductos.Text);                   // ID DE PRODUCTO
                    comando.Parameters.AddWithValue("@Codigoi", txtcodigounicoproducto.Text);               // CODIGO UNICO DE PRODUCTO
                    comando.Parameters.AddWithValue("@Nombrei", txtnombreproducto.Text);                    // NOMBRE DE PRODUCTO
                    comando.Parameters.AddWithValue("@Marcai", txtmarcaproducto.Text);                      // MARCA DE PRODUCTO
                    comando.Parameters.AddWithValue("@Modeloi", txtmodeloproducto.Text);                    // MODELO DE PRODUCTO
                    comando.Parameters.AddWithValue("@Precioi", txtprecioproducto.Text);                    // PRECIO DE PRODUCTO
                    comando.Parameters.AddWithValue("@NombreProveedori", txtProveedorProducto.Text);        // PROVEEDOR DE PRODUCTO
                    comando.Parameters.AddWithValue("@NombreCategoriai", txtCategoriaProductos.Text);       // CATEGORIA DE PRODUCTO
                    comando.Parameters.AddWithValue("@ID_inventario", txtidinventarios.Text);                 // REFERENCIA ID DE INVENTARIO {CONTROL DE INVENTARIOS} A MODIFICAR
                    comando.ExecuteNonQuery(); // EJECUTANDO COMANDO Y ENVIANDO DATOS HACIA LA BASE DE DATOS
                                               // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Registro Control de Inventario Modificado Con Exito", "Modificando Registro Control de Inventario Existente",
                        MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
                    // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
                    SqlCommand cmd = new SqlCommand("Select ID_inventario, Numeros_Unidades_Stock, Periodo_Garantia, ID_producto, Cod_producto, Nombre, Marca, Modelo, Precio, Nombre_proveedor, Nombre_Categoria FROM ControlInventarios", con);
                    // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
                    SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
                    MostrarRegistros.SelectCommand = cmd;
                    // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
                    DataTable TablaRegistros = new DataTable();
                    // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
                    MostrarRegistros.Fill(TablaRegistros);
                    // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
                    // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {CONTROL DE INVENTARIOS}
                    DetallesInformeInventarios.DataSource = TablaRegistros;
                    // RESETEANDO LA COLUMNA DE SALARIOS PARA QUE MUESTRE UNICAMENTE .2 DECIMALES DESPUES DE LA CIFRA EXACTA
                    DetallesInformeInventarios.Columns[8].DefaultCellStyle.Format = "N2";
                    /*------------------------------------------------------------------------------------------
                     * -> INICIALIZANDO TEXTBOX A SUS VALORES PREDETERMINADOS ANTES DE CERRAR CONEXION UNA VEZ
                     * REALIZADO EL NUEVO REGISTRO A LA BASE DE DATOS {CONSTRUCTORES}
                     -----------------------------------------------------------------------------------------*/
                    txtCategoriaProductos.Text = " "; txtcodigounicoproducto.Text = " "; txtgarantiainventario.Text = " ";
                    txtidinventarios.Text = " "; txtidproductos.Text = " "; txtmarcaproducto.Text = " ";
                    txtmodeloproducto.Text = " "; txtnombreproducto.Text = " "; txtnumstockinventario.Text = " ";
                    txtprecioproducto.Text = " "; txtProveedorProducto.Text = " ";
                }
            }

            catch (Exception a)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos, pero el producto no se ha podido registrar en control de inventarios, recuerde que debe cambiar por un (.) Punto el precio al momento de seleccionar el producto a registrar en control de inventarios", "Error Al Ingresar Producto Ha Inventario",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop) == DialogResult.OK) ;
                MessageBox.Show(a.Message);
            }
            finally
            {
                con.Close();// AL FINALIZAR CADA NUEVO REGISTRO, SE PROCEDE A CERRAR LA CONEXION A ESPERA DE UNA NUEVA PETICION
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
