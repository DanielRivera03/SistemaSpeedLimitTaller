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

namespace SpeedLimitTaller
{
    public partial class UsuariosSistema : Form
    {
        // CONEXION C# A BASE DE DATOS
        SqlConnection con = new SqlConnection(@"Data Source=SONYVAIO\SQLEXPRESS;Initial Catalog=speedlimitdb;Integrated Security=True");
        public UsuariosSistema()
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
           
        }
        /*
            --> BOTON REGISTRO DE NUEVO USUARIO AL SISTEMA 
        */
        private void RegistroNuevoUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtnombres.Text.Length == 0 || txtusuario.Text.Length == 0 || txtpass.Text.Length == 0 || txttipo.Text.Length == 0)
                {
                    // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Lo sentimos pero ha ocurrido un error, tome en cuenta lo siguiente:\n- No puede dejar campos vacíos.\n- Por favor sea cuidadoso a la hora de agregar un nuevo usuario, respetando las métricas establecidas por la empresa.", "Error al Intentar Agregar Registro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) ;
                }
                else
                {
                    // CREANDO CADENA DE INSERCION query CON SU PASO DE PARAMETROS HACIA LA BASE DE DATOS
                    string query = "INSERT INTO usuario (Nombre,Usuario,Password,Tipo_usuario) VALUES (@nombre,@usuario,@password,@tipo_usuario)";
                    con.Open();// APERTURANDO CONEXION
                    SqlCommand comando = new SqlCommand(query, con); // CREANDO COMANDO DE CONEXION
                                                                     // ENVIAR LOS DATOS INGRESADOS POR EL USUARIO EN LAS CAJAS DE TEXTO, A LOS PARAMETROS DE COMUNICACION
                                                                     // A LA BASE DE DATOS...
                    comando.Parameters.AddWithValue("@nombre", txtnombres.Text); // NOMBRE Y APELLIDO
                    comando.Parameters.AddWithValue("@usuario", txtusuario.Text); // NOMBRE DE USUARIO
                    comando.Parameters.AddWithValue("@password", txtpass.Text); // CONTRASEÑA DE INGRESO
                    comando.Parameters.AddWithValue("@tipo_usuario", txttipo.Text); // TIPO DE USUARIO {ROL DE USUARIO}
                    comando.ExecuteNonQuery(); // EJECUTANDO COMANDO Y ENVIANDO DATOS HACIA LA BASE DE DATOS
                                               // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Nuevo usuario agregado con éxito!.", "Registrando Nuevo Usuario",
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
                    txtnombres.Text = " "; txtusuario.Text = " "; txtpass.Text = " "; txttipo.Text = " ";
                }
            }
            catch (Exception a)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos, pero el usuario que intenta registrar ya existe en la base de datos, por favor intente con otro usuario", "Error Usuario Ya Existe",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop) == DialogResult.OK) ;
                MessageBox.Show(a.Message);
            }
            finally
            {
                con.Close();// AL FINALIZAR CADA NUEVO REGISTRO, SE PROCEDE A CERRAR LA CONEXION A ESPERA DE UNA NUEVA PETICION
            }
        }
        
        /*
            --> BOTON MODIFICAR USUARIOS
        */
        private void ModificarUsuarios_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioModificar = new ModificarUsuarios(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioModificar.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON ELIMINAR USUARIOS
        */

        private void EliminarUsuarios_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioEliminar = new EliminarUsuarios(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioEliminar.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
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
