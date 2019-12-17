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

namespace SpeedLimitTaller
{
    public partial class BienvenidaUsuarios : Form
    {
        // INICIALIZANDON COMPONENTE VENTANA FORM --> BIENVENIDA AL USUARIO {TODOS LOS USUARIOS}
        public BienvenidaUsuarios()
        {
            InitializeComponent();
        }

        // CREANDO OBJETO TEMPORIZADOR PARA EFECTO SUAVIZADO DE CIERRE/APERTURA DE VENTANA
        // --> NUEVO INICIO DE SESION {APERTURA DE VENTANA}
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) this.Opacity += 0.08;
            circularProgressBar1.Value += 1;
            circularProgressBar1.Text = circularProgressBar1.Value.ToString();
            if (circularProgressBar1.Value == 100)
            {
                timer1.Stop();// TEMPORIZADOR 1 ALTO TOTAL
                timer2.Start();// TEMPORIZADOR 2 INICIO DE CONTEO
            }
            
        }

        // TEMPORIZADOR 2 --> {CIERRE DE VENTANA}
        private void timer2_Tick(object sender, EventArgs e)
        {
            // OPACIDAD Y EFECTO SUAVIZADO DE APERTURA / CIERRE
            this.Opacity -= 0.1;
            if (this.Opacity == 0)
            {
                timer2.Stop();
                this.Close();
            }
        }

        // INICIALIZANDO VALORES DE BARRA PROGRESO CIRCULAR
        private void BienvenidaUsuarios_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.0; // OPACIDAD 0
            circularProgressBar1.Value = 0; // INICIO EN CERO {0%}
            circularProgressBar1.Minimum = 0; // VALOR MINIMO CERO {0%}
            circularProgressBar1.Maximum = 100; // VALOR MAXIMO {100%}
            timer1.Start(); // TEMPORIZADOR 1 EN MARCHA AL CARGAR OBJETO DE BARRA PROGRESO
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
