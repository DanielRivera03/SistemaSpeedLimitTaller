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
    public partial class ModificarEmpleados : Form
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


        public ModificarEmpleados()
        {
            InitializeComponent();
            // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
            SqlCommand cmd = new SqlCommand("Select * from Empleados", con);
            // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
            SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
            MostrarRegistros.SelectCommand = cmd;
            // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
            DataTable TablaRegistros = new DataTable();
            // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
            MostrarRegistros.Fill(TablaRegistros);
            // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
            // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {Empleados}
            DetallesEmpleadosSistema.DataSource = TablaRegistros;
            // RESETEANDO LA COLUMNA DE SALARIOS PARA QUE MUESTRE UNICAMENTE .2 DECIMALES DESPUES DE LA CIFRA EXACTA
            DetallesEmpleadosSistema.Columns[9].DefaultCellStyle.Format = "N2";
            // HABILITANDO FUNCION DE ARRASTRE DE FORMULARIO {APLICABLE A SOLO CONTENEDOR DE FORMULARIO}
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /*
            --> HABILITANDO FUNCION DE ARRASTRE DE FORMULARIO POR PARTE DEL USUARIO 
        */

        private void ModificarEmpleados_MouseDown(object sender, MouseEventArgs e)
        {
            // HABILITANDO FUNCION DE ARRASTRE DE FORMULARIO
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /*
            --> MINIMIZAR VENTANA 
        */

        private void MinimizarVentana_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /*
          --> CERRAR VENTANA 
        */

        private void CerrarVentana_Click_1(object sender, EventArgs e)
        {
            this.Hide(); // UNICAMENTE OCULTA VENTANA FLOTANTE, NO OBSTANTE CIERRA LA CONEXION PARA ASI PODER PROCEDER A REALIZAR NUEVAS ACTUALIZACIONES REQUERIDAS
        }

        /*
            --> BOTON SELECCIONAR EMPLEADO A ACTUALIZAR REGISTRO 
        */

        private void SeleccionarRegistroE_Click(object sender, EventArgs e)
        {
            // SI EL USUARIO HA SELECCIONADO UN REGISTRO, ENTONCES...
            if (DetallesEmpleadosSistema.SelectedRows.Count > 0)
            {
                /* EL DATAGRIDVIEW SE ENCARGA DE MOSTRAR AL USUARIO LOS DATOS ALMACENADOS EN LA BASE DE DATOS
                * POR LO TANTO AL MOMENTO QUE EL USUARIO SELECCIONE DICHO REGISTRO Y PRESIONE EL BOTON SELECCIONAR
                * USUARIO, SE PROCEDE A TOMAR CADA UNO DE LOS CAMPOS ALMACENADOS EN LOS TEXTBOX CON SUS IDENTIFICADORES
                * UNICOS Y REALIZA LA RESPECTIVA CONVERSION A CADENA PARA QUE ESA INFORMACION SEA VISIBLE AL USUARIO
                * FINAL Y PUEDA PROCEDER A ACTUALIZAR EL N REGISTRO QUE DESEE...
                */
                txtidempleado.Text = DetallesEmpleadosSistema.CurrentRow.Cells["Id_empleado"].Value.ToString();                     // ID DE EMPLEADO
                txtcodigoempleado.Text = DetallesEmpleadosSistema.CurrentRow.Cells["Codigo_empleado"].Value.ToString();             // CODIGO DE EMPLEADO
                txtnombreempleado.Text = DetallesEmpleadosSistema.CurrentRow.Cells["Nombre_empleado"].Value.ToString();             // NOMBRES DE EMPLEADO
                txtapellidoempleado.Text = DetallesEmpleadosSistema.CurrentRow.Cells["Apellido_empleado"].Value.ToString();         // APELLIDOS DE EMPLEADO
                txtgeneroempleado.Text = DetallesEmpleadosSistema.CurrentRow.Cells["Genero_empleado"].Value.ToString();             // GENERO DE EMPLEADO
                txtemailempleado.Text = DetallesEmpleadosSistema.CurrentRow.Cells["Email_empleado"].Value.ToString();               // E-MAIL DE EMPLEADO
                txtfechacontratacion.Text = DetallesEmpleadosSistema.CurrentRow.Cells["Contratacion_Empleado"].Value.ToString();    // FECHA CONTRATACION DE EMPLEADO
                txtdui.Text = DetallesEmpleadosSistema.CurrentRow.Cells["Dui_empleado"].Value.ToString();                           // DUI EMPLEADO
                txtnit.Text = DetallesEmpleadosSistema.CurrentRow.Cells["Nit_empleado"].Value.ToString();                           // NIT EMPLEADO
                txtsalario.Text = DetallesEmpleadosSistema.CurrentRow.Cells["Salario_empleado"].Value.ToString();                   // SALARIO EMPLEADO
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
            --> BOTON MODIFICAR EMPLEADO A ACTUALIZAR REGISTRO 
        */

        private void ModificarRegistrosEmpleados_Click(object sender, EventArgs e)
        {
            try
            {
                // VALIDANDO QUE NO EXISTAN CAMPOS VACIOS Y QUE AL MENOS EL USUARIO SELECCIONE UN REGISTRO A ACTUALIZAR
                if (txtidempleado.Text.Length == 0 || txtcodigoempleado.Text.Length == 0 || txtnombreempleado.Text.Length == 0 || txtapellidoempleado.Text.Length == 0 || txtgeneroempleado.Text.Length == 0 || txtemailempleado.Text.Length == 0 || txtfechacontratacion.Text.Length == 0 || txtdui.Text.Length == 0 || txtnit.Text.Length == 0 || txtsalario.Text.Length == 0)
                {
                    // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Lo sentimos pero ha ocurrido un error, tome en cuenta lo siguiente:\n- Debe seleccionar al menos un registro a editar.\n- No puede dejar ningún campo vacío.", "Error de Modificación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) ;
                }
                else // SI SE HA SELECCIONADO UN EMPLEADO, ENTONCES... 
                {
                    // CREANDO VARIABLE TIPO QUERY QUE ALMACENA LA CADENA DE NUEVA MODIFICACION CON SU CONDICION
                    string query = "UPDATE Empleados SET Codigo_empleado = @codigoE, Nombre_empleado = @nombreE, Apellido_empleado = @apellidoE, Genero_empleado = @generoE, Email_empleado = @emailE, Contratacion_Empleado = @FechaCE, Dui_empleado = @duiE, Nit_empleado = @nitE, Salario_empleado = @salarioE WHERE Id_empleado=@Id_empleado";
                    // --> WHERE Id_empleado=@Id_empleado" IMPORTANTE: SI NO SE REFERENCIA POR DEFECTO NO MUESTRA ERROR PERO SE PRODUCE UN ERROR LOGICO, AL ACTUALIZAR ABSOLUTAMENTE
                    // TODOS LOS DATOS DE LA TABLA REFERENCIADA EN LA BASE DE DATOS.
                    con.Open();// APERTURANDO CONEXION
                    SqlCommand comando = new SqlCommand(query, con);// CREANDO COMPONENTE DE COMUNICACION HACIA LA BASE DE DATOS
                                                                    // ENVIO DE PARAMETROS CON LOS DATOS SELECCIONADOS PREVIAMENTE POR EL USUARIO HACIA LA BASE DE DATOS
                    comando.Parameters.AddWithValue("@codigoE", txtcodigoempleado.Text);        // CODIGO UNICO DE EMPLEADOS
                    comando.Parameters.AddWithValue("@nombreE", txtnombreempleado.Text);        // NOMBRES EMPLEADOS
                    comando.Parameters.AddWithValue("@apellidoE", txtapellidoempleado.Text);    // APELLIDOS EMPLEADOS
                    comando.Parameters.AddWithValue("@generoE", txtgeneroempleado.Text);        // GENERO DE EMPLEADOS
                    comando.Parameters.AddWithValue("@emailE", txtemailempleado.Text);          // E-MAIL DE EMPLEADOS
                    comando.Parameters.AddWithValue("@FechaCE", txtfechacontratacion.Text);     // FECHA CONTRATACION EMPLEADOS
                    comando.Parameters.AddWithValue("@duiE", txtdui.Text);                      // DUI EMPLEADOS
                    comando.Parameters.AddWithValue("@nitE", txtnit.Text);                      // NIT EMPLEADOS
                    comando.Parameters.AddWithValue("@salarioE", txtsalario.Text);              // SALARIO EMPLEADOS
                    comando.Parameters.AddWithValue("@Id_empleado", txtidempleado.Text);         // REFERENCIA DEL ID UNICO DEL EMPLEADO REGISTRADO
                    comando.ExecuteNonQuery(); // EJECUTANDO COMANDO Y ENVIANDO DATOS HACIA LA BASE DE DATOS
                                               // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Empleado Modificado Con Exito", "Modificando Empleado Existente",
                        MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
                    /*---------------------------------------------------------------------------------------------
                     * AL MOMENTO QUE EL USUARIO INGRESA LOS DATOS, ESTE VUELVE A EJECUTAR EL PROCEDIMIENTO PARA  *
                     * REFRESCAR LA TABLA CONTENEDORA CON LOS N EMPLEADOS REGISTRADOS EN EL SISTEMA...            *
                     * --------------------------------------------------------------------------------------------
                     */
                    // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
                    SqlCommand cmd = new SqlCommand("Select * from Empleados", con);
                    // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
                    SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
                    MostrarRegistros.SelectCommand = cmd;
                    // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
                    DataTable TablaRegistros = new DataTable();
                    // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
                    MostrarRegistros.Fill(TablaRegistros);
                    // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
                    // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {Empleados}
                    DetallesEmpleadosSistema.DataSource = TablaRegistros;
                    /*------------------------------------------------------------------------------------------
                     * -> INICIALIZANDO TEXTBOX A SUS VALORES PREDETERMINADOS ANTES DE CERRAR CONEXION UNA VEZ
                     * REALIZADO EL NUEVO REGISTRO A LA BASE DE DATOS {CONSTRUCTORES}
                     -----------------------------------------------------------------------------------------*/
                    txtidempleado.Text = " "; txtcodigoempleado.Text = " "; txtnombreempleado.Text = " "; txtapellidoempleado.Text = " "; txtgeneroempleado.Text = " "; txtemailempleado.Text = " "; txtfechacontratacion.Text = " "; txtdui.Text = " "; txtnit.Text = " "; txtsalario.Text = " ";
                }
              }
            catch (Exception a)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos, pero el empleado que intenta modificar ya existe en la base de datos, por favor intente con otro código de empleado", "Error Empleado Ya Existe",
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
