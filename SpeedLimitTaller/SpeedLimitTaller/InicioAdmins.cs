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
// MANEJADOR DE EVENTOS URL --> NAVEGADORES {ENLACES}
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeedLimitTaller
{
    public partial class InicioAdmins : Form
    {
        public InicioAdmins()
        {
            InitializeComponent();
        }
        // ESTADISTICAS SPLT --> FUTUROS USOS...
        private void EstadisticasSPLT_Paint(object sender, PaintEventArgs e)
        {
            // TODO CODIGO ESTADISTICAS...
        }

        // CALENDARIO SPLT
        private void Calendario_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioCalendario = new CalendarioSPLT(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioCalendario.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        // ESTADISTICAS SPLT
        private void EstadisticasSPLT_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioEstadisticasSPLT = new EstadisticasSPLT(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioEstadisticasSPLT.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        // APLICACION SPLT
        private void APPSPLT_Click(object sender, EventArgs e)
        {
            // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
            if (MessageBox.Show("En estos momentos será redirigido hacia la página principal donde se aloja nuestra aplicación, gracias por su confianza en nosotros", "Aplicación SpeedLimit Taller",
                MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
            ProcessStartInfo sInfo = new ProcessStartInfo("https://play.google.com/store/apps/details?id=com.imoves.superrepuestos&hl=es_SV");
            Process.Start(sInfo);
        }

        // ACERCA DE SPLT
        private void AcercaDeSPLT_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioAcercaDESPLT = new AcercaDeSPLT(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioAcercaDESPLT.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
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
