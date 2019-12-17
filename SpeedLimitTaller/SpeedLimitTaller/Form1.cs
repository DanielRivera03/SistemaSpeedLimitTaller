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

// NOMBRE DE ESPACIO DE PROYECTO --> SpeedLimitTaller
namespace SpeedLimitTaller
{
    public partial class Form1 : Form
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


        public Form1()
        {
            InitializeComponent();
            // CONVIRTIENDO ELEMENTOS CON FONDO SOLIDO A TRANSPARENTE
            LogoLogin.BackColor = Color.Transparent;
            textoIniciarSesion.BackColor = Color.Transparent;
            textoContrasena.BackColor = Color.Transparent;
            imgUser.BackColor = Color.Transparent;
            imgPass.BackColor = Color.Transparent;
            // ESTILOS DE BORDES REDONDEADOS EN BOTONES
            BotonesRedondeados.BordesRedondeados(btnIniciarSesion); // BOTON INICIAR SESION
            BotonesRedondeados.BordesRedondeados(btnSalir); // BOTON SALIR
            // OPACIDAD GENERAL DE FORM
            this.Opacity = .97;
            // HABILITANDO FUNCION DE ARRASTRE DE FORMULARIO {APLICABLE A SOLO CONTENEDOR DE FORMULARIO}
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        /*INICIO DE VALIDACIONES FORMULARIO LOGIN*/
        // CONEXION C# A BASE DE DATOS
        SqlConnection con = new SqlConnection(@"Data Source=SONYVAIO\SQLEXPRESS;Initial Catalog=speedlimitdb;Integrated Security=True");
        // CREACION DE METODO LOGUEAR
        public void loguear(string usuario, string contrasena)
        {
            /*
               INICIO --> VALIDACION DE CAJAS DE TEXTO VACIAS
            */
            if (CajaUsuariosLogin.Text.Length == 0 || PassLogin.Text.Length == 0) // USUARIO VACIO
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("No puede dejar campos vacios", "Error de Ingreso",
                    MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK);
            }
            /*
               FIN --> VALIDACION DE CAJAS DE TEXTO VACIAS
            */
            // VALIDACION DE CONEXION DB
                try
            {
                // SI CONEXION ES EXITOSA, ENTONCES...
                con.Open(); // APERTURA CONEXION
                // CONSULTA DE USUARIOS QUE HAN SIDO INGRESADOS EN LA BASE DE DATOS
                SqlCommand cmd = new SqlCommand("SELECT Nombre, Tipo_usuario FROM usuario WHERE Usuario = @Usuario AND Password = @pas", con);
                // PARAMETROS DE CONSULTA
                cmd.Parameters.AddWithValue("usuario", usuario);
                cmd.Parameters.AddWithValue("pas", contrasena);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                // SI CONTADOR ES IGUAL A 1, EXISTEN REGISTROS EN LA BASE DE DATOS
                if (dt.Rows.Count == 1)
                {
                    this.Hide(); // OCULTA DATOS DE INGRESO
                    /*
                        USUARIOS NIVEL --> ADMINISTRADORES
                     */
                    if (dt.Rows[0][1].ToString() == "Admin")
                    {
                        /*
                          INVOCANDO PANTALLA DE ESPERA ANTES DE INICIAR LA APLICACION
                        */
                        this.Hide(); // SE OCULTA VENTANA LOGIN PARA MOSTRAR VENTANA DE CARGA
                        BienvenidaUsuarios vistabienvenida = new BienvenidaUsuarios(); // INSTANCIA (OBJETO) CREADO PARA INVOCAR VENTANA
                        vistabienvenida.ShowDialog(); // MUESTRA VENTANA TIPO --> VENTANA DE DIALOGO
                        /*
                            MUESTRA VENTANA DE ADMINISTRACION --> USUARIOS NIVEL ADMINISTRADOR
                         */
                        new Admin(dt.Rows[0][0].ToString()).Show();
                    }
                    /*
                        USUARIOS NIVEL --> GERENCIA GENERAL
                     */
                    else if (dt.Rows[0][1].ToString() == "Gerencia")
                    {
                        /*
                          INVOCANDO PANTALLA DE ESPERA ANTES DE INICIAR LA APLICACION
                        */
                        this.Hide(); // SE OCULTA VENTANA LOGIN PARA MOSTRAR VENTANA DE CARGA
                        BienvenidaUsuarios vistabienvenida = new BienvenidaUsuarios(); // INSTANCIA (OBJETO) CREADO PARA INVOCAR VENTANA
                        vistabienvenida.ShowDialog(); // MUESTRA VENTANA TIPO --> VENTANA DE DIALOGO
                        /*
                            MUESTRA VENTANA DE ADMINISTRACION --> USUARIOS NIVEL GERENCIA GENERAL
                         */
                        new GerenciaGeneral(dt.Rows[0][0].ToString()).Show();
                    }
                    /*
                        USUARIOS NIVEL --> EMPLEADOS {VENDEDORES}
                     */
                    else if (dt.Rows[0][1].ToString() == "Empleados")
                    {
                        /*
                          INVOCANDO PANTALLA DE ESPERA ANTES DE INICIAR LA APLICACION
                        */
                        this.Hide(); // SE OCULTA VENTANA LOGIN PARA MOSTRAR VENTANA DE CARGA
                        BienvenidaUsuarios vistabienvenida = new BienvenidaUsuarios(); // INSTANCIA (OBJETO) CREADO PARA INVOCAR VENTANA
                        vistabienvenida.ShowDialog(); // MUESTRA VENTANA TIPO --> VENTANA DE DIALOGO
                        /*
                            MUESTRA VENTANA DE ADMINISTRACION --> USUARIOS NIVEL EMPLEADOS VENDEDORES
                         */
                        new EmpleadosVendedores(dt.Rows[0][0].ToString()).Show();
                    }
                }
                else
                { // DE LO CONTRARIO, NO ACCEDE...
                    // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Lo sentimos, ingrese usuario y/o contraseña válidos", "Verificar Campos Ingresados",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK) ;
                }
            }
            // MENSAJE A DESPLEGAR EN CASO DE NO EXISTIR COMUNICACION HACIA LA BASE DE DATOS --> ERROR
            catch (Exception e)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("ERROR FATAL: Lo sentimos, pero no se pudo conectar a la base de datos Por favor informe al desarrollador inmediatamente a desarrollos@eblyk.net\nAgradecemos sus oportunos reportes de errores\nDESARROLLOS INFORMATICOS E.B.L.K & ASOCIADOS S.A DE C.V.", "Error de Comunicación Base de Datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop) == DialogResult.OK) ;
                MessageBox.Show(e.Message);
            }
            // SI CONEXION ES EXITOSA, PROCEDE A SU CIERRE Y REALIZA VALIDACIONES PARA CARGA DE INTERFACES
            finally
            {
                con.Close(); // CIERRE DE CONEXION
            }
        }// public void loguear(string usuario, string contrasena)

        /*FIN DE VALIDACIONES FORMULARIO LOGIN*/


        /*
            VALIDACION BOTON SALIDA --> LOGIN 
        */

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); // TERMINA EJECUCION Y CIERRA VENTANA
        }

        /*
            VALIDACION BOTON INGRESO {INICIAR SESION} --> LOGIN 
        */

        /*
            -> HABILITANDO EL EVENTO DE ARRASTRE PARA TODOS LOS ELEMENTOS PRINCIPALES DENTRO DEL FORMULARIO
            DE INICIO DE SESION, SIN LA INICIALIZACION DE ESTOS, UNICAMENTE LO ESTARIA APLICANDO AL FORMULARIO
            EN SI Y NO A LOS ELEMENTOS SOBREEXPUESTOS DENTRO DEL FORMULARIO... 
        */

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            loguear(this.CajaUsuariosLogin.Text, this.PassLogin.Text); // CAPTURA N DATOS INGRESADOS Y COMPARA CADENAS
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void LogoLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
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
