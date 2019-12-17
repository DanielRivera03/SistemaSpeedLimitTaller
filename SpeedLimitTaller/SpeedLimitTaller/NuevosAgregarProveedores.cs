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
    public partial class NuevosAgregarProveedores : Form
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

        public NuevosAgregarProveedores()
        {
            InitializeComponent();
            // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
            SqlCommand cmd = new SqlCommand("Select * from ProveedoresProdutos", con);
            // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
            SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
            MostrarRegistros.SelectCommand = cmd;
            // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
            DataTable TablaRegistros = new DataTable();
            // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
            MostrarRegistros.Fill(TablaRegistros);
            // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
            // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {PROVEEDORES}
            DetallesProductosSistema.DataSource = TablaRegistros;
        }

        /*
          --> HABILITANDO FUNCION DE ARRASTRE DE FORMULARIO POR PARTE DEL USUARIO 
         */

        private void NuevosAgregarProveedores_MouseDown(object sender, MouseEventArgs e)
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
            --> BOTON AGREGAR NUEVO PROVEEDOR 
        */
        private void AgregarNuevoRegistroProveedor_Click(object sender, EventArgs e)
        {
            if (txtdireccionproveedor.Text.Length == 0 || txtemailproveedor.Text.Length == 0 || txtidproveedor.Text.Length == 0 || txtnombreproveedor.Text.Length == 0 || txtsectorcomercialproveedor.Text.Length == 0 || txttelefonoproveedor.Text.Length == 0)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos pero ha ocurrido un error, tome en cuenta lo siguiente:\n- No puede dejar campos vacíos.\n- Por favor sea cuidadoso a la hora de agregar un nuevo proveedor, respetando las métricas establecidas por la empresa.", "Error al Intentar Agregar Registro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) ;
                con.Close();// AL FINALIZAR CADA NUEVO REGISTRO, SE PROCEDE A CERRAR LA CONEXION A ESPERA DE UNA NUEVA PETICION
            }
            else
            {
                // CREANDO CADENA DE INSERCION query CON SU PASO DE PARAMETROS HACIA LA BASE DE DATOS
                string query = "INSERT INTO ProveedoresProdutos (ID_proveedor,Nombre_proveedor,Sector_Comercial,Direccion,Telefono,Email) VALUES (@idproveedor,@nombreproveedor,@sectorCproveedor,@direccionproveedor,@telefonoproveedor,@emailproveedor)";
                con.Open();// APERTURANDO CONEXION
                SqlCommand comando = new SqlCommand(query, con); // CREANDO COMANDO DE CONEXION
                // ENVIAR LOS DATOS INGRESADOS POR EL USUARIO EN LAS CAJAS DE TEXTO, A LOS PARAMETROS DE COMUNICACION
                // A LA BASE DE DATOS...
                comando.Parameters.AddWithValue("@idproveedor", txtidproveedor.Text);                       // ID PROVEEDOR
                comando.Parameters.AddWithValue("@nombreproveedor", txtnombreproveedor.Text);               // NOMBRE PROVEEDOR
                comando.Parameters.AddWithValue("@sectorCproveedor", txtsectorcomercialproveedor.Text);     // SECTOR COMERCIAL PROVEEDOR
                comando.Parameters.AddWithValue("@direccionproveedor", txtdireccionproveedor.Text);         // DIRECCION PROVEEDOR
                comando.Parameters.AddWithValue("@telefonoproveedor", txttelefonoproveedor.Text);           // TELEFONO PROVEEDOR
                comando.Parameters.AddWithValue("@emailproveedor", txtemailproveedor.Text);                 // EMAIL PROVEEDOR
                comando.ExecuteNonQuery(); // EJECUTANDO COMANDO Y ENVIANDO DATOS HACIA LA BASE DE DATOS
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Nuevo proveedor agregado con éxito!.", "Registrando Nuevo Proveedor",
                    MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
                /*---------------------------------------------------------------------------------------------
                 * AL MOMENTO QUE EL USUARIO INGRESA LOS DATOS, ESTE VUELVE A EJECUTAR EL PROCEDIMIENTO PARA  *
                 * REFRESCAR LA TABLA CONTENEDORA CON LOS N USUARIOS REGISTRADOS EN EL SISTEMA...             *
                 * --------------------------------------------------------------------------------------------
                 */
                // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
                SqlCommand cmd = new SqlCommand("Select * from ProveedoresProdutos", con);
                // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
                SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
                MostrarRegistros.SelectCommand = cmd;
                // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
                DataTable TablaRegistros = new DataTable();
                // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
                MostrarRegistros.Fill(TablaRegistros);
                // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
                // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {PROVEEDORES}
                DetallesProductosSistema.DataSource = TablaRegistros;
                /*------------------------------------------------------------------------------------------
                 * -> INICIALIZANDO TEXTBOX A SUS VALORES PREDETERMINADOS ANTES DE CERRAR CONEXION UNA VEZ
                 * REALIZADO EL NUEVO REGISTRO A LA BASE DE DATOS {CONSTRUCTORES}
                 -----------------------------------------------------------------------------------------*/
                txtidproveedor.Text = " "; txtdireccionproveedor.Text = " "; txtemailproveedor.Text = " ";
                txtnombreproveedor.Text = " "; txtsectorcomercialproveedor.Text = " "; txttelefonoproveedor.Text = " ";
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
