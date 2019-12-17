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
// IMPORTANDO LIBRERIA QUE HABILITA EL EVENTO DE ARRASTRES DE FORMULARIOS POR PARTE DE LOS USUARIOS
using System.Runtime.InteropServices;

namespace SpeedLimitTaller
{
    public partial class GerenciaGeneral : Form
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



        public GerenciaGeneral(string nombre)
        {
            InitializeComponent();
            NombreUsuarioGG.Text = nombre;
            timer1.Enabled = true;
            // INVOCANDO FORMULARIO DE BIENVENIDA PARA USUARIOS GERENCIA GENERAL (MISMA INTERFAZ ADMINISTRADORES)
            MostrarFormularios(new InicioAdmins());
        }

        /*
            HORA DEL SISTEMA 
        */

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            HoraSistemaGG.Text = DateTime.Now.ToString();
        }

        /*
            CERRAR VENTANA 
        */

        private void CerrarVentana_Click(object sender, EventArgs e)
        {
            Application.Exit(); // CIERRE DIRECTO DE APLICACION, CON DESCONEXION INCLUIDA DE BASE DE DATOS
        }

        /*
            MINIMIZAR VENTANA 
        */

        private void MinimizarVentana_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /*
            MAXIMIZAR VENTANA 
        */

        private void MaximizarVentana_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            MaximizarVentana.Visible = true;
            Restaurar.Visible = true;
        }

        /*
            RESTAURAR VENTANA 
        */

        private void Restaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            Restaurar.Visible = false;
        }


        /*
            --> CREANDO FUNCION CON OBJETO DE PARAMETRO, QUE SE ENCARGARA DE MANEJAR DE 
                ABRIR TODOS LOS FORMULARIOS {SUBPROCESOS DEL SISTEMA} AL FORMULARIO PRINCIPAL
                MOSTRADO AL CLIENTE. 
        */
        private void MostrarFormularios(object MostrandoSubFormularios)
        {
            // SI EL FORMULARIO PRESENTA CONTROLES POR DEFECTO, OJO NO CREADOS EXTERNAMENTE
            // ESTE LOS ELIMINARA...
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            // CREANDO INSTANCIA PARA MOSTRAR SUBFORMULARIOS DEL SISTEMA
            Form FormulariosSistema = MostrandoSubFormularios as Form;
            FormulariosSistema.TopLevel = false; // FORMUNARIOS DE NO ALTO NIVEL {TIPO SECUNDARIOS A MOSTRAR}
            FormulariosSistema.Dock = DockStyle.Fill; // RELLENAR FORMULARIO AL ANCHO TOTAL DEL PANEL CONTENEDOR
            this.panelContenedor.Controls.Add(FormulariosSistema); // MOSTRAR TODOS LOS ELEMENTOS DEL FORMULARIO
            this.panelContenedor.Tag = FormulariosSistema; // DECLARANDO INSTANCIA AL PANEL CONTENEDOR
            FormulariosSistema.Show(); // MOSTRAR EL X FORMULARIO A TRATAR EN EL SISTEMA
        }

        // BOTON MENU OPCION --> {EMPLEADOS}
        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            // INVOCANDO FUNCION Y MOSTRANDO EXCLUSIVAMENTE FORMULARIO DE EMPLEADOS DEL SISTEMA
            MostrarFormularios(new Empleados());
        }

        // BOTON MENU OPCION --> {PRODUCTOS}
        private void btnProductos_Click(object sender, EventArgs e)
        {
            // INVOCANDO FUNCION Y MOSTRANDO EXCLUSIVAMENTE FORMULARIO DE PRODUCTOS DEL SISTEMA
            MostrarFormularios(new Productos());
        }

        // BOTON MENU OPCION --> {INVENTARIO}
        private void btnInventario_Click(object sender, EventArgs e)
        {
            // INVOCANDO FUNCION Y MOSTRANDO EXCLUSIVAMENTE FORMULARIO DE CONTROL DE INVENTARIOS DEL SISTEMA
            MostrarFormularios(new Inventarios());
        }

        // BOTON MENU OPCION --> {FACTURAS}
        private void bntFacturas_Click(object sender, EventArgs e)
        {
            MostrarFormularios(new FacturacionSpeedLimitTaller());
        }

        // BOTON MENU OPCION --> {TIENDAS}
        private void btnTaller_Click(object sender, EventArgs e)
        {
            MostrarFormularios(new TiendasSpeedLimitAD());
        }

        /*
            BOTON CERRAR SESION --> MENU DE NAVEGACION 
        */

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea realmente cerrar sesión?", "Cerrando Sesión",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();// CIERRE COMPLETO UNICAMENTE DE LA BASE DE DATOS --> {USUARIO DADO DE ALTA EN ESE MOMENTO}
                this.Hide();// OCULTA PANEL DE ADMINISTRACION PARA CERRAR SESION
                Form1 cerrandosesion = new Form1();// CREANDO INSTANCIA DE CIERRE DE SESION
                cerrandosesion.ShowDialog();// MUESTRA NUEVAMENTE VENTANA DE LOGIN
            }
        }

        // MUESTRA VENTANA DE INICIO --> USUARIOS ROL {GERENCIA GENERA / MISMA INTERFAZ ADMINISTRADORES}

        private void IconInicioRetorno_Click(object sender, EventArgs e)
        {
            // INVOCANDO FORMULARIO DE BIENVENIDA PARA USUARIOS GERENCIA GENERAL
            MostrarFormularios(new InicioAdmins());
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
