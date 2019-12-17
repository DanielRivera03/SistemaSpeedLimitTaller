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
    public partial class AsignarEncargadosTiendasSPLT : Form
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
        ControlesCombobox DatosIDEmpleados = new ControlesCombobox();        // ID DE EMPLEADOS
        ControlesCombobox DatosNombresEmpleados = new ControlesCombobox();   // NOMBRES DE EMPLEADOS
        ControlesCombobox DatosApellidosEmpleados = new ControlesCombobox(); // APELLIDOS DE EMPLEADOS


        public AsignarEncargadosTiendasSPLT()
        {
            InitializeComponent();
            // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
            SqlCommand cmd = new SqlCommand("Select * from TiendasSpeedLimit", con);
            // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
            SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
            MostrarRegistros.SelectCommand = cmd;
            // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
            DataTable TablaRegistros = new DataTable();
            // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
            MostrarRegistros.Fill(TablaRegistros);
            // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
            // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {Empleados}
            DetallesTiendasRegistradasVistaSimple.DataSource = TablaRegistros;
            /*
             * INVOCANDO METODO PARA PASO DE DATOS DE COMBOBOS HACIA BASE DE DATOS
             */
            DatosIDEmpleados.SeleccionarDatosEmSPLT(txtIDEmpleadoSPLT);                // ID DE EMPLEADOS
            DatosIDEmpleados.SeleccionarDatosEmNombresSPLT(txtNombreEmpleadoSPLT);     // NOMBRES DE EMPLEADOS
            DatosIDEmpleados.SeleccionarDatosEmApellidosSPLT(txtApellidoEmpleadoSPLT); // APELLIDOS DE EMPLEADOS
        }

        /*
            --> HABILITANDO FUNCION DE ARRASTRE DE FORMULARIO POR PARTE DEL USUARIO 
         */

        private void AsignarEncargadosTiendasSPLT_MouseDown(object sender, MouseEventArgs e)
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
            --> BOTON SELECCIONAR TIENDA 
        */

        private void SeleccionTiendaET_Click(object sender, EventArgs e)
        {
            // SI EL USUARIO HA SELECCIONADO UN REGISTRO, ENTONCES...
            if (DetallesTiendasRegistradasVistaSimple.SelectedRows.Count > 0)
            {
                /* EL DATAGRIDVIEW SE ENCARGA DE MOSTRAR AL USUARIO LOS DATOS ALMACENADOS EN LA BASE DE DATOS
                * POR LO TANTO AL MOMENTO QUE EL USUARIO SELECCIONE DICHO REGISTRO Y PRESIONE EL BOTON SELECCIONAR
                * USUARIO, SE PROCEDE A TOMAR CADA UNO DE LOS CAMPOS ALMACENADOS EN LOS TEXTBOX CON SUS IDENTIFICADORES
                * UNICOS Y REALIZA LA RESPECTIVA CONVERSION A CADENA PARA QUE ESA INFORMACION SEA VISIBLE AL USUARIO
                * FINAL Y PUEDA PROCEDER A ACTUALIZAR EL N REGISTRO QUE DESEE...
                */
                txtidtienda.Text = DetallesTiendasRegistradasVistaSimple.CurrentRow.Cells["ID_tienda"].Value.ToString(); // ID DE USUARIO
                txtcodigoUtiendas.Text = DetallesTiendasRegistradasVistaSimple.CurrentRow.Cells["Codigo_tienda"].Value.ToString(); // NOMBRE Y APELLIDO
                txttelefonotiendas.Text = DetallesTiendasRegistradasVistaSimple.CurrentRow.Cells["Tefefono_tienda"].Value.ToString(); // NOMBRE DE USUARIO
                txtdireccionCtiendas.Text = DetallesTiendasRegistradasVistaSimple.CurrentRow.Cells["Direccion_tienda"].Value.ToString(); // NOMBRE DE USUARIO
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
            --> BOTON REGISTRAR ENCARGADO 
        */

        private void RegistrarEncargadoTienda_Click(object sender, EventArgs e)
        {
            if (txtcodigoUtiendas.Text.Length == 0 || txtdireccionCtiendas.Text.Length == 0 || txttelefonotiendas.Text.Length == 0)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos pero ha ocurrido un error, tome en cuenta lo siguiente:\n- No puede dejar campos vacíos.\n- Por favor sea cuidadoso a la hora de registrar una nueva tienda, respetando las métricas establecidas por la empresa.", "Error al Intentar Agregar Registro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) ;
            }
            else
            {
                string query = "INSERT INTO EncargadosTiendas (Id_empleado, Nombre_empleado, Apellido_empleado, Codigo_tienda, Direccion_tienda, Tefefono_tienda) VALUES (@idempleado,@nombreEmpleado,@apellidoEmpleado,@codigoTienda,@direcciontienda,@telefonotienda)";
                con.Open();// APERTURANDO CONEXION
                SqlCommand comando = new SqlCommand(query, con); // CREANDO COMANDO DE CONEXION
                // ENVIAR LOS DATOS INGRESADOS POR EL USUARIO EN LAS CAJAS DE TEXTO, A LOS PARAMETROS DE COMUNICACION
                // A LA BASE DE DATOS...
                comando.Parameters.AddWithValue("@idempleado", txtIDEmpleadoSPLT.Text);             // ID DE EMPLEADO
                comando.Parameters.AddWithValue("@nombreEmpleado", txtNombreEmpleadoSPLT.Text);     // NOMBRES DE EMPLEADO
                comando.Parameters.AddWithValue("@apellidoEmpleado", txtApellidoEmpleadoSPLT.Text); // APELLIDOS DE EMPLEADO
                comando.Parameters.AddWithValue("@codigoTienda", txtcodigoUtiendas.Text);           // CODIGO UNICO DE TIENDA SPLT
                comando.Parameters.AddWithValue("@direcciontienda", txtdireccionCtiendas.Text);     // DIRECCION TIENDA SPLT
                comando.Parameters.AddWithValue("@telefonotienda", txttelefonotiendas.Text);        // TELEFONO TIENDA SPLT
                comando.ExecuteNonQuery(); // EJECUTANDO COMANDO Y ENVIANDO DATOS HACIA LA BASE DE DATOS
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Encargado de tienda registrado con éxito!.", "Registrando Encargados Tiendas SpeedLimit Taller",
                    MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
                /*---------------------------------------------------------------------------------------------
                 * AL MOMENTO QUE EL USUARIO INGRESA LOS DATOS, ESTE VUELVE A EJECUTAR EL PROCEDIMIENTO PARA  *
                 * REFRESCAR LA TABLA CONTENEDORA CON LOS N EMPLEADOS REGISTRADOS EN EL SISTEMA...            *
                 * --------------------------------------------------------------------------------------------
                 */
                // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
                SqlCommand cmd = new SqlCommand("Select * from TiendasSpeedLimit", con);
                // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
                SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
                MostrarRegistros.SelectCommand = cmd;
                // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
                DataTable TablaRegistros = new DataTable();
                // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
                MostrarRegistros.Fill(TablaRegistros);
                // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
                // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {Empleados}
                DetallesTiendasRegistradasVistaSimple.DataSource = TablaRegistros;
                /*------------------------------------------------------------------------------------------
                 * -> INICIALIZANDO TEXTBOX A SUS VALORES PREDETERMINADOS ANTES DE CERRAR CONEXION UNA VEZ
                 * REALIZADO EL NUEVO REGISTRO A LA BASE DE DATOS {CONSTRUCTORES}
                 -----------------------------------------------------------------------------------------*/
                txtApellidoEmpleadoSPLT.Text = " "; txtIDEmpleadoSPLT.Text = " "; txtidtienda.Text = " "; txtNombreEmpleadoSPLT.Text = " ";
                txtcodigoUtiendas.Text = " "; txtdireccionCtiendas.Text = " "; txttelefonotiendas.Text = " ";
                /*
                * INVOCANDO METODO PARA PASO DE DATOS DE COMBOBOX HACIA BASE DE DATOS
                * --> RESETEO DE COMBOBOX LUEGO DE EDICION DE DATOS
                */
                DatosIDEmpleados.SeleccionarDatos(txtIDEmpleadoSPLT);                               // ID DE EMPLEADOS A ASIGNAR TIENDAS SPLT
                DatosNombresEmpleados.SeleccionarDatosEmNombresSPLT(txtNombreEmpleadoSPLT);         // NOMBRES DE EMPLEADOS A ASIGNAR TIENDAS SPLT
                DatosApellidosEmpleados.SeleccionarDatosEmApellidosSPLT(txtApellidoEmpleadoSPLT);   // APELLIDOS DE EMPLEADOS A ASIGNAR TIENDAS SPLT
                con.Close();// AL FINALIZAR CADA NUEVO REGISTRO, SE PROCEDE A CERRAR LA CONEXION A ESPERA DE UNA NUEVA PETICION
            }
        }

        /*
            --> BOTON VER LISTADO PERSONALIZADO DE EMPLEADOS REGISTRADOS EN EL SISTEMA
                SPEEDLIMIT TALLER S.A DE C.V 
        */

        private void btnVerEmpleados_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioVistaEmpleadosPersonalizada = new VerListadoEmpleadosTiendas(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioVistaEmpleadosPersonalizada.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
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
