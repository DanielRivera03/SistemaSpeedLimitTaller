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
    public partial class ModificarProductos : Form
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


        public ModificarProductos()
        {
            InitializeComponent();
            // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
            SqlCommand cmd = new SqlCommand("Select * from Productos", con);
            // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
            SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
            MostrarRegistros.SelectCommand = cmd;
            // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
            DataTable TablaRegistros = new DataTable();
            // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
            MostrarRegistros.Fill(TablaRegistros);
            // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
            // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {Empleados}
            DetallesProductosSistema.DataSource = TablaRegistros;
            // RESETEANDO LA COLUMNA DE SALARIOS PARA QUE MUESTRE UNICAMENTE .2 DECIMALES DESPUES DE LA CIFRA EXACTA
            DetallesProductosSistema.Columns[5].DefaultCellStyle.Format = "N2";
            /*
             * INVOCANDO METODO PARA PASO DE DATOS DE COMBOBOS HACIA BASE DE DATOS
             */
            DatosProveedores.SeleccionarDatos(txtProveedorProducto);       // PROVEEDORES DE PRODUCTOS
            DatosCategorias.SeleccionarDatosCt(txtCategoriaProductos);     // CATEGORIAS DE PRODUCTOS
            
        }

        /*
          --> HABILITANDO FUNCION DE ARRASTRE DE FORMULARIO POR PARTE DEL USUARIO 
      */

        private void ModificarProductos_MouseDown(object sender, MouseEventArgs e)
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
            --> BOTON SELECCIONAR PRODUCTO A ACTUALIZAR EN EL REGISTRO 
        */

        private void SeleccionProductos_Click(object sender, EventArgs e)
        {
            // SI EL USUARIO HA SELECCIONADO UN REGISTRO, ENTONCES...
            if (DetallesProductosSistema.SelectedRows.Count > 0)
            {
                /* EL DATAGRIDVIEW SE ENCARGA DE MOSTRAR AL USUARIO LOS DATOS ALMACENADOS EN LA BASE DE DATOS
                * POR LO TANTO AL MOMENTO QUE EL USUARIO SELECCIONE DICHO REGISTRO Y PRESIONE EL BOTON SELECCIONAR
                * USUARIO, SE PROCEDE A TOMAR CADA UNO DE LOS CAMPOS ALMACENADOS EN LOS TEXTBOX CON SUS IDENTIFICADORES
                * UNICOS Y REALIZA LA RESPECTIVA CONVERSION A CADENA PARA QUE ESA INFORMACION SEA VISIBLE AL USUARIO
                * FINAL Y PUEDA PROCEDER A ACTUALIZAR EL N REGISTRO QUE DESEE...
                */
                txtidproductos.Text = DetallesProductosSistema.CurrentRow.Cells["ID_producto"].Value.ToString();                // ID PRODUCTO
                txtcodigounicoproducto.Text = DetallesProductosSistema.CurrentRow.Cells["Cod_producto"].Value.ToString();       // CODIGO UNICO PRODUCTO
                txtnombreproducto.Text = DetallesProductosSistema.CurrentRow.Cells["Nombre"].Value.ToString();                  // NOMBRE PRODUCTO
                txtmarcaproducto.Text = DetallesProductosSistema.CurrentRow.Cells["Marca"].Value.ToString();                    // MARCA PRODUCTO
                txtmodeloproducto.Text = DetallesProductosSistema.CurrentRow.Cells["Modelo"].Value.ToString();                  // MODELO PRODUCTO
                txtprecioproducto.Text = DetallesProductosSistema.CurrentRow.Cells["Precio"].Value.ToString();
                txtProveedorProducto.Text = DetallesProductosSistema.CurrentRow.Cells["Nombre_proveedor"].Value.ToString();     // PROVEEDOR PRODUCTO
                txtCategoriaProductos.Text = DetallesProductosSistema.CurrentRow.Cells["Nombre_Categoria"].Value.ToString();    // CATEGORIA PRODUCTO
                con.Close();  // CIERRE DE CONEXION UNA VEZ TOMADOS LOS PARAMETROS Y ARGUMENTOS PREVIO A LA ACTUALIZACION          
            }// DE LO CONTRARIO...
            else
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos pero debe seleccionar un registro, por favor seleccione el registro deseado ha actualizar.", "Error de Modificación",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK) ;
            }

        }

        /*
            --> BOTON MODIFICAR PRODUCTOS 
        */

        private void ModificarRegistroProductos_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcodigounicoproducto.Text.Length == 0 || txtnombreproducto.Text.Length == 0 || txtmarcaproducto.Text.Length == 0 || txtmodeloproducto.Text.Length == 0 || txtprecioproducto.Text.Length == 0 || txtProveedorProducto.Text.Length == 0 || txtCategoriaProductos.Text.Length == 0)
                {
                    // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Lo sentimos pero ha ocurrido un error, tome en cuenta lo siguiente:\n- No puede dejar campos vacíos.\n- Por favor sea cuidadoso a la hora de modificar un nuevo producto, respetando las métricas establecidas por la empresa.", "Error al Intentar Agregar Registro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) ;
                }
                else // SI HA SELECCIONADO UN REGISTRO, ENTONCES...
                {
                    // CREANDO VARIABLE TIPO QUERY QUE ALMACENA LA CADENA DE NUEVA MODIFICACION CON SU CONDICION
                    string query = "UPDATE Productos SET Cod_producto = @Codigop, Nombre = @Nombrep, Marca = @Marcap, Modelo = @Modelop, Precio = @Preciop, Nombre_proveedor = @NombreProveedorp, Nombre_Categoria = @NombreCategoriap WHERE ID_producto=@ID_producto";
                    // --> WHERE ID_producto=@ID_producto" IMPORTANTE: SI NO SE REFERENCIA POR DEFECTO NO MUESTRA ERROR PERO SE PRODUCE UN ERROR LOGICO, AL ACTUALIZAR ABSOLUTAMENTE
                    // TODOS LOS DATOS DE LA TABLA REFERENCIADA EN LA BASE DE DATOS.
                    con.Open();// APERTURANDO CONEXION
                    SqlCommand comando = new SqlCommand(query, con);// CREANDO COMPONENTE DE COMUNICACION HACIA LA BASE DE DATOS
                                                                    // ENVIO DE PARAMETROS CON LOS DATOS SELECCIONADOS PREVIAMENTE POR EL USUARIO HACIA LA BASE DE DATOS
                    comando.Parameters.AddWithValue("@Codigop", txtcodigounicoproducto.Text);               // CODIGO UNICO DE PRODUCTO
                    comando.Parameters.AddWithValue("@Nombrep", txtnombreproducto.Text);                    // NOMBRE DE PRODUCTO
                    comando.Parameters.AddWithValue("@Marcap", txtmarcaproducto.Text);                      // MARCA DE PRODUCTO
                    comando.Parameters.AddWithValue("@Modelop", txtmodeloproducto.Text);                    // MODELO DE PRODUCTO
                    comando.Parameters.AddWithValue("@Preciop", txtprecioproducto.Text);                    // PRECIO DE PRODUCTO
                    comando.Parameters.AddWithValue("@NombreProveedorp", txtProveedorProducto.Text);        // PROVEEDOR DE PRODUCTO
                    comando.Parameters.AddWithValue("@NombreCategoriap", txtCategoriaProductos.Text);       // CATEGORIA DE PRODUCTO
                    comando.Parameters.AddWithValue("@ID_producto", txtidproductos.Text);                   // REFERENCIA ID DE PRODUCTO A MODIFICAR
                    comando.ExecuteNonQuery(); // EJECUTANDO COMANDO Y ENVIANDO DATOS HACIA LA BASE DE DATOS
                                               // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Producto Modificado Con Exito", "Modificando Producto Existente",
                        MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
                    // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
                    SqlCommand cmd = new SqlCommand("Select * from Productos", con);
                    // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
                    SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
                    MostrarRegistros.SelectCommand = cmd;
                    // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
                    DataTable TablaRegistros = new DataTable();
                    // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
                    MostrarRegistros.Fill(TablaRegistros);
                    // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
                    // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {Empleados}
                    DetallesProductosSistema.DataSource = TablaRegistros;
                    // RESETEANDO LA COLUMNA DE SALARIOS PARA QUE MUESTRE UNICAMENTE .2 DECIMALES DESPUES DE LA CIFRA EXACTA
                    DetallesProductosSistema.Columns[5].DefaultCellStyle.Format = "N2";
                    /*------------------------------------------------------------------------------------------
                     * -> INICIALIZANDO TEXTBOX A SUS VALORES PREDETERMINADOS ANTES DE CERRAR CONEXION UNA VEZ
                     * REALIZADO EL NUEVO REGISTRO A LA BASE DE DATOS {CONSTRUCTORES}
                     -----------------------------------------------------------------------------------------*/
                    txtidproductos.Text = " "; txtCategoriaProductos.Text = " "; txtcodigounicoproducto.Text = " ";
                    txtmarcaproducto.Text = " "; txtmodeloproducto.Text = " "; txtnombreproducto.Text = " ";
                    txtprecioproducto.Text = " "; txtProveedorProducto.Text = " ";
                    /*
                    * INVOCANDO METODO PARA PASO DE DATOS DE COMBOBOS HACIA BASE DE DATOS
                    * --> RESETEO DE COMBOBOX LUEGO DE EDICION DE DATOS
                    */
                    DatosProveedores.SeleccionarDatos(txtProveedorProducto);       // PROVEEDORES DE PRODUCTOS
                    DatosCategorias.SeleccionarDatosCt(txtCategoriaProductos);     // CATEGORIAS DE PRODUCTOS
                }
            }
            catch (Exception a)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos, pero el producto que intenta modificar ya existe en la base de datos, por favor ingrese otro código de producto", "Error Producto Ya Existe",
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
