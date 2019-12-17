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
    public partial class TiendasSpeedLimitAD : Form
    {

        // CONEXION C# A BASE DE DATOS
        SqlConnection con = new SqlConnection(@"Data Source=SONYVAIO\SQLEXPRESS;Initial Catalog=speedlimitdb;Integrated Security=True");

        public TiendasSpeedLimitAD()
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
        }

        /*
            --> BOTON VER REGISTRO DE TIENDAS SPEEDLIMIT TALLER S.A DE C.V 
        */

        private void RegistroTiendas_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioModificarTSPLT = new DetalleCompletoTiendasSPLT(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioModificarTSPLT.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON REGISTRO DE NUEVA TIENDA SPEEDLIMIT TALLER 
        */

        private void RegistroNuevaTiendaSPLT_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcodigoUtiendas.Text.Length == 0 || txtdireccionCtiendas.Text.Length == 0 || txttelefonotiendas.Text.Length == 0)
                {
                    // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Lo sentimos pero ha ocurrido un error, tome en cuenta lo siguiente:\n- No puede dejar campos vacíos.\n- Por favor sea cuidadoso a la hora de registrar una nueva tienda, respetando las métricas establecidas por la empresa.", "Error al Intentar Agregar Registro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) ;
                }
                else
                {
                    // CREANDO CADENA DE INSERCION query CON SU PASO DE PARAMETROS HACIA LA BASE DE DATOS
                    string query = "INSERT INTO TiendasSpeedLimit (Codigo_tienda, Tefefono_tienda, Direccion_tienda) VALUES (@codigoTSPLT, @telefonoTSPLT, @direccionTSPLT)";
                    con.Open();// APERTURANDO CONEXION
                    SqlCommand comando = new SqlCommand(query, con); // CREANDO COMANDO DE CONEXION
                                                                     // ENVIAR LOS DATOS INGRESADOS POR EL USUARIO EN LAS CAJAS DE TEXTO, A LOS PARAMETROS DE COMUNICACION
                                                                     // A LA BASE DE DATOS...
                    comando.Parameters.AddWithValue("@codigoTSPLT", txtcodigoUtiendas.Text);        // CODIGO UNICO DE TIENDAS SPLT
                    comando.Parameters.AddWithValue("@telefonoTSPLT", txttelefonotiendas.Text);     // TELEFONO DE TIENDAS
                    comando.Parameters.AddWithValue("@direccionTSPLT", txtdireccionCtiendas.Text);  // DIRECCION DE TIENDAS
                    comando.ExecuteNonQuery(); // EJECUTANDO COMANDO Y ENVIANDO DATOS HACIA LA BASE DE DATOS
                                               // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Nueva tienda registrada con éxito!.", "Registrando Nuevas Tiendas SpeedLimit Taller",
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
                    txtcodigoUtiendas.Text = " "; txtdireccionCtiendas.Text = " "; txttelefonotiendas.Text = " ";
                }
            }
            catch (Exception a)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos, pero la tienda que intenta registrar ya existe en la base de datos, por favor ingrese otro código de tienda", "Error Tienda SpeedLimit Ya Existe",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop) == DialogResult.OK) ;
                MessageBox.Show(a.Message);
            }
            finally
            {
                con.Close();// AL FINALIZAR CADA NUEVO REGISTRO, SE PROCEDE A CERRAR LA CONEXION A ESPERA DE UNA NUEVA PETICION
            }
        }

        /*
           --> BOTON MODIFICAR TIENDAS SPLT 
        */

        private void ModificarTiendas_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioModificarTSPLT = new ModificarTiendasSPLT(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioModificarTSPLT.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON ELIMINAR TIENDAS SPLT 
        */

        private void EliminarTiendas_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioEliminarTSPLT = new EliminarTiendasSPLT(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioEliminarTSPLT.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON ASIGNAR ENCARGADOS TIENDAS SPLT 
        */

        private void RegistrarEncargados_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioAsignarEncargadosTSPLT = new AsignarEncargadosTiendasSPLT(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioAsignarEncargadosTSPLT.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON MODIFICAR ENCARGADOS TIENDAD SPLT 
        */

        private void ModificarEncargados_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioModificarEncargadosTSPLT = new ModificarEncargadosTiendasSPLT(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioModificarEncargadosTSPLT.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON ELIMINAR ENCARGADOS TIENDAS SPLT 
        */

        private void EliminarEncargados_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioEliminarEncargadosTSPLT = new EliminarEncargadosTiendasSPLT(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioEliminarEncargadosTSPLT.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
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
