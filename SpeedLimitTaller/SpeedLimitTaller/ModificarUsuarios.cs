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
    public partial class ModificarUsuarios : Form
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
        public ModificarUsuarios()
        {
            InitializeComponent();
            // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
            SqlCommand cmd = new SqlCommand("Select * from usuario", con);
            // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
            SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
            MostrarRegistros.SelectCommand = cmd;
            // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
            DataTable TablaRegistros = new DataTable();
            // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE USUARIOS
            MostrarRegistros.Fill(TablaRegistros);
            // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
            // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {usuario}
            DetallesUsuariosSistema.DataSource = TablaRegistros;
            // ESTILOS DE BORDES REDONDEADOS EN BOTONES
            BotonesRedondeados.BordesRedondeados(ModificarRegistrosUsuarios); // BOTON INICIAR SESION
            BotonesRedondeados.BordesRedondeados(SeleccionarRegistro); // BOTON SALIR
            // HABILITANDO FUNCION DE ARRASTRE DE FORMULARIO {APLICABLE A SOLO CONTENEDOR DE FORMULARIO}
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
            --> HABILITANDO FUNCION DE ARRASTRE DE FORMULARIO POR PARTE DEL USUARIO 
        */

        private void ModificarUsuarios_MouseDown(object sender, MouseEventArgs e)
        {
            // HABILITANDO FUNCION DE ARRASTRE DE FORMULARIO
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /*
            --> BOTON SELECCIONAR USUARIO A ACTUALIZAR REGISTRO 
        */

        private void SeleccionarRegistro_Click(object sender, EventArgs e)
        {
            // SI EL USUARIO HA SELECCIONADO UN REGISTRO, ENTONCES...
            if (DetallesUsuariosSistema.SelectedRows.Count > 0)
            {
                /* EL DATAGRIDVIEW SE ENCARGA DE MOSTRAR AL USUARIO LOS DATOS ALMACENADOS EN LA BASE DE DATOS
                * POR LO TANTO AL MOMENTO QUE EL USUARIO SELECCIONE DICHO REGISTRO Y PRESIONE EL BOTON SELECCIONAR
                * USUARIO, SE PROCEDE A TOMAR CADA UNO DE LOS CAMPOS ALMACENADOS EN LOS TEXTBOX CON SUS IDENTIFICADORES
                * UNICOS Y REALIZA LA RESPECTIVA CONVERSION A CADENA PARA QUE ESA INFORMACION SEA VISIBLE AL USUARIO
                * FINAL Y PUEDA PROCEDER A ACTUALIZAR EL N REGISTRO QUE DESEE...
                */
                txtid.Text = DetallesUsuariosSistema.CurrentRow.Cells["Id_usuario"].Value.ToString(); // ID DE USUARIO
                txtnombres.Text = DetallesUsuariosSistema.CurrentRow.Cells["Nombre"].Value.ToString(); // NOMBRE Y APELLIDO
                txtusuario.Text = DetallesUsuariosSistema.CurrentRow.Cells["Usuario"].Value.ToString(); // NOMBRE DE USUARIO
                txtpass.Text = DetallesUsuariosSistema.CurrentRow.Cells["Password"].Value.ToString(); // CLAVE DE ACCESO {PASSWORD}
                txttipo.Text = DetallesUsuariosSistema.CurrentRow.Cells["Tipo_usuario"].Value.ToString(); // TIPO DE USUARIO {ROLES DE USUARIOS}
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
            --> BOTON MODIFICAR USUARIO 
        */

        private void ModificarRegistrosUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                // VALIDANDO QUE NO EXISTAN CAMPOS VACIOS Y QUE AL MENOS EL USUARIO SELECCIONE UN REGISTRO A ACTUALIZAR
                if (txtid.Text.Length == 0 || txtnombres.Text.Length == 0 || txtusuario.Text.Length == 0 || txtpass.Text.Length == 0 || txttipo.Text.Length == 0)
                {
                    // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Lo sentimos pero ha ocurrido un error, tome en cuenta lo siguiente:\n- Debe seleccionar al menos un registro a editar.\n- No puede dejar ningún campo vacío.", "Error de Modificación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) ;
                }
                else
                { // SI EL USUARIO HA SELECCIONADO UN REGISTRO, ENTONCES...
                  // CREANDO VARIABLE TIPO QUERY QUE ALMACENA LA CADENA DE NUEVA MODIFICACION CON SU CONDICION
                    string query = "UPDATE usuario SET Nombre = @nombre, Usuario = @usuario, Password = @pass, Tipo_usuario = @tipousuario WHERE Id_usuario=@Id_usuario";
                    // --> WHERE Id_usuario=@Id_usuario" IMPORTANTE: SI NO SE REFERENCIA POR DEFECTO NO MUESTRA ERROR PERO SE PRODUCE UN ERROR LOGICO, AL ACTUALIZAR ABSOLUTAMENTE
                    // TODOS LOS DATOS DE LA TABLA REFERENCIADA EN LA BASE DE DATOS.
                    con.Open();// APERTURANDO CONEXION
                    SqlCommand comando = new SqlCommand(query, con);// CREANDO COMPONENTE DE COMUNICACION HACIA LA BASE DE DATOS
                                                                    // ENVIO DE PARAMETROS CON LOS DATOS SELECCIONADOS PREVIAMENTE POR EL USUARIO HACIA LA BASE DE DATOS
                    comando.Parameters.AddWithValue("@nombre", txtnombres.Text); // NOMBRE Y APELLIDO
                    comando.Parameters.AddWithValue("@usuario", txtusuario.Text); // NOMBRE DE USUARIO
                    comando.Parameters.AddWithValue("@pass", txtpass.Text); // PASSWORD DE ACCESO {CLAVE DE ACCESO}
                    comando.Parameters.AddWithValue("@tipousuario", txttipo.Text); // TIPO DE USUARIO {ROL DE USUARIO}
                    comando.Parameters.AddWithValue("@Id_usuario", txtid.Text); // REFERENCIA DEL ID UNICO DE CADA USUARIO
                    comando.ExecuteNonQuery(); // ENVIANDO COMPONENTE QUERY HACIA LA BASE DE DATOS CON NUEVO REGISTRO ACTUALIZADO
                                               // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Usuario Modificado Con Exito", "Modificando Usuario Existente",
                        MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
                    /*---------------------------------------------------------------------------------------------
                     * AL MOMENTO QUE EL USUARIO INGRESA LOS DATOS, ESTE VUELVE A EJECUTAR EL PROCEDIMIENTO PARA  *
                     * REFRESCAR LA TABLA CONTENEDORA CON LOS N USUARIOS REGISTRADOS EN EL SISTEMA...             *
                     * --------------------------------------------------------------------------------------------
                     */
                    // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
                    SqlCommand cmd = new SqlCommand("Select * from usuario", con);
                    // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
                    SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
                    MostrarRegistros.SelectCommand = cmd;
                    // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
                    DataTable TablaRegistros = new DataTable();
                    // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE USUARIOS
                    MostrarRegistros.Fill(TablaRegistros);
                    // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
                    // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {usuario}
                    DetallesUsuariosSistema.DataSource = TablaRegistros;
                    /*------------------------------------------------------------------------------------------
                    * -> INICIALIZANDO TEXTBOX A SUS VALORES PREDETERMINADOS ANTES DE CERRAR CONEXION UNA VEZ
                    * REALIZADO EL NUEVO REGISTRO A LA BASE DE DATOS {CONSTRUCTORES}
                    -----------------------------------------------------------------------------------------*/
                    txtid.Text = " "; txtnombres.Text = " "; txtusuario.Text = " "; txtpass.Text = " "; txttipo.Text = " ";
                    con.Close();// AL FINALIZAR CADA NUEVO REGISTRO, SE PROCEDE A CERRAR LA CONEXION A ESPERA DE UNA NUEVA PETICION
                  }
                }
                catch (Exception a)
                {
                    // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Lo sentimos, pero el usuario que intenta modificar ya existe en la base de datos, por favor intente con otro usuario", "Error Usuario Ya Existe",
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
