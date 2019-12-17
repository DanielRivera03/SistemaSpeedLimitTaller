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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// IMPORTANDO LIBRERIA SERVICIO CLIENTE C# -> SQL SERVER
using System.Data.SqlClient;

namespace SpeedLimitTaller
{
    class ControlesCombobox
    {
        // CONEXION C# A BASE DE DATOS
        SqlConnection con = new SqlConnection(@"Data Source=SONYVAIO\SQLEXPRESS;Initial Catalog=speedlimitdb;Integrated Security=True");

        /*
            --> DATOS DE PROVEEDORES DE PRODUCTOS
        */

        public void SeleccionarDatos(ComboBox DatosTablasRelacionadas)
        {
            DatosTablasRelacionadas.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM ProveedoresProdutos", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DatosTablasRelacionadas.Items.Add(dr[1].ToString());
            }
            con.Close();
            DatosTablasRelacionadas.Items.Insert(0, "-Seleccione un proveedor");
            DatosTablasRelacionadas.SelectedIndex = 0;
        }

        /*
            --> DATOS DE CATEGORIAS DE PRODUCTOS 
        */

        public void SeleccionarDatosCt(ComboBox DatosTablasRelacionadas)
        {
            DatosTablasRelacionadas.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM CategoriaProductos", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DatosTablasRelacionadas.Items.Add(dr[1].ToString());
            }
            con.Close();
            DatosTablasRelacionadas.Items.Insert(0, "-Seleccione una categoria");
            DatosTablasRelacionadas.SelectedIndex = 0;
        }

        /*
            --> DATOS ID DE EMPLEADOS 
        */

        public void SeleccionarDatosEmSPLT(ComboBox DatosTablasRelacionadas)
        {
            DatosTablasRelacionadas.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Empleados", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DatosTablasRelacionadas.Items.Add(dr[0].ToString());
            }
            con.Close();
            DatosTablasRelacionadas.Items.Insert(0, "-Seleccione el ID");
            DatosTablasRelacionadas.SelectedIndex = 0;
        }

        /*
            --> DATOS NOMBRES DE EMPLEADOS 
        */

        public void SeleccionarDatosEmNombresSPLT(ComboBox DatosTablasRelacionadas)
        {
            DatosTablasRelacionadas.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Empleados", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DatosTablasRelacionadas.Items.Add(dr[2].ToString());
            }
            con.Close();
            DatosTablasRelacionadas.Items.Insert(0, "-Seleccione el nombre");
            DatosTablasRelacionadas.SelectedIndex = 0;
        }

        /*
            --> DATOS APELLIDOS DE EMPLEADOS 
        */

        public void SeleccionarDatosEmApellidosSPLT(ComboBox DatosTablasRelacionadas)
        {
            DatosTablasRelacionadas.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Empleados", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DatosTablasRelacionadas.Items.Add(dr[3].ToString());
            }
            con.Close();
            DatosTablasRelacionadas.Items.Insert(0, "-Seleccione el apellido");
            DatosTablasRelacionadas.SelectedIndex = 0;
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
