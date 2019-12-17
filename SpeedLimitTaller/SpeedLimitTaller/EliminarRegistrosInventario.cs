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
    public partial class EliminarRegistrosInventario : Form
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


        public EliminarRegistrosInventario()
        {
            InitializeComponent();
        }

        private void EliminarRegistrosInventario_Load(object sender, EventArgs e)
        {
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

        private void EliminarRegistrosInventario_MouseDown(object sender, MouseEventArgs e)
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
            --> BOTON SELECCIONAR REGISTRO A ELIMINAR {CONTROL DE INVENTARIOS} 
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
            }
            else
            {
                // SI USUARIO NO SELECCIONA UN REGISTRO...
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos pero debe seleccionar un registro, por favor seleccione el registro deseado ha actualizar.", "Error de Modificación",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK) ;
            }
        }

        /*
            --> BOTON ELIMINAR REGISTRO {CONTROL DE INVENTARIOS} 
        */

        private void RegistroNuevoInventario_Click(object sender, EventArgs e)
        {
            // CREANDO VARIABLE TIPO QUERY QUE ALMACENA LA CADENA DE ELIMINAR X REGISTRO CON SU CONDICION
            string query = "DELETE FROM ControlInventarios WHERE ID_inventario=@ID_inventario";
            // --> WHERE [ID_categoria]=@[ID_categoria]" IMPORTANTE: SI NO SE REFERENCIA POR DEFECTO NO MUESTRA ERROR PERO SE PRODUCE UN ERROR LOGICO, AL ELIMINAR ABSOLUTAMENTE
            // TODOS LOS DATOS DE LA TABLA REFERENCIADA EN LA BASE DE DATOS.
            con.Open();// APERTURANDO CONEXION
            SqlCommand comando = new SqlCommand(query, con);// CREANDO COMPONENTE DE COMUNICACION HACIA LA BASE DE DATOS
            // ENVIO DE PARAMETROS CON LOS DATOS SELECCIONADOS PREVIAMENTE POR EL USUARIO HACIA LA BASE DE DATOS
            comando.Parameters.AddWithValue("@ID_inventario", txtidinventarios.Text); // REFERENCIA DEL ID UNICO DE CADA EMPLEADO
            comando.ExecuteNonQuery(); // ENVIANDO COMPONENTE QUERY HACIA LA BASE DE DATOS CON NUEVO REGISTRO ACTUALIZADO
            // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
            // VALIDANDO QUE NO EXISTAN CAMPOS VACIOS Y QUE AL MENOS EL USUARIO SELECCIONE UN REGISTRO A ELIMINAR
            if (txtnumstockinventario.Text.Length == 0 || txtgarantiainventario.Text.Length == 0 || txtcodigounicoproducto.Text.Length == 0 || txtnombreproducto.Text.Length == 0 || txtmarcaproducto.Text.Length == 0 || txtmodeloproducto.Text.Length == 0 || txtprecioproducto.Text.Length == 0 || txtProveedorProducto.Text.Length == 0 || txtCategoriaProductos.Text.Length == 0)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos pero ha ocurrido un error, tome en cuenta lo siguiente:\n- No puede dejar campos vacíos.\n- Por favor sea cuidadoso a la hora de eliminar una categoría de producto, respetando las métricas establecidas por la empresa.", "Error al Intentar Eliminar Registro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) ;
                con.Close();// AL FINALIZAR CADA NUEVO REGISTRO, SE PROCEDE A CERRAR LA CONEXION A ESPERA DE UNA NUEVA PETICION
            }
            else
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Registro de Producto Control de Inventario Eliminado Con Exito", "Eliminando Registro de Control de Inventarios Existente",
                MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
                /*---------------------------------------------------------------------------------------------
                * AL MOMENTO QUE EL USUARIO INGRESA LOS DATOS, ESTE VUELVE A EJECUTAR EL PROCEDIMIENTO PARA  *
                * REFRESCAR LA TABLA CONTENEDORA CON LOS N USUARIOS REGISTRADOS EN EL SISTEMA...             *
                * --------------------------------------------------------------------------------------------
                */
                SqlCommand cmd = new SqlCommand("Select * from ControlInventarios", con);
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
