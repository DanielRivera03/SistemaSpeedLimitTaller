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
// IMPORTANDO LIBRERIAS CONTROL EXPORTACION A PDF
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace SpeedLimitTaller
{
    public partial class Empleados : Form
    {
        // CONEXION C# A BASE DE DATOS
        SqlConnection con = new SqlConnection(@"Data Source=SONYVAIO\SQLEXPRESS;Initial Catalog=speedlimitdb;Integrated Security=True");
        public Empleados()
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
        }

        /*
            --> BOTON REGISTRO DE NUEVO EMPLEADO AL SISTEMA 
        */

        private void RegistroNuevoEmpleado_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcodigoempleado.Text.Length == 0 || txtnombreempleado.Text.Length == 0 || txtapellidoempleado.Text.Length == 0 || txtgeneroempleado.Text.Length == 0 || txtemailempleado.Text.Length == 0 || txtfechacontratacion.Text.Length == 0 || txtdui.Text.Length == 0 || txtnit.Text.Length == 0 || txtsalario.Text.Length == 0)
                {
                    // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Lo sentimos pero ha ocurrido un error, tome en cuenta lo siguiente:\n- No puede dejar campos vacíos.\n- Por favor sea cuidadoso a la hora de agregar un nuevo empleado, respetando las métricas establecidas por la empresa.", "Error al Intentar Agregar Registro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) ;
                }
                else
                {
                    // CREANDO CADENA DE INSERCION query CON SU PASO DE PARAMETROS HACIA LA BASE DE DATOS
                    string query = "INSERT INTO Empleados (Codigo_empleado,Nombre_empleado,Apellido_empleado,Genero_empleado,Email_empleado,Contratacion_Empleado,Dui_empleado,Nit_empleado,Salario_empleado) VALUES (@codigoE,@nombreE,@apellidoE,@generoE,@emailE,@FechaCE,@duiE,@nitE,@salarioE)";
                    con.Open();// APERTURANDO CONEXION
                    SqlCommand comando = new SqlCommand(query, con); // CREANDO COMANDO DE CONEXION
                                                                     // ENVIAR LOS DATOS INGRESADOS POR EL USUARIO EN LAS CAJAS DE TEXTO, A LOS PARAMETROS DE COMUNICACION
                                                                     // A LA BASE DE DATOS...
                    comando.Parameters.AddWithValue("@codigoE", txtcodigoempleado.Text);        // CODIGO UNICO DE EMPLEADOS
                    comando.Parameters.AddWithValue("@nombreE", txtnombreempleado.Text);        // NOMBRES EMPLEADOS
                    comando.Parameters.AddWithValue("@apellidoE", txtapellidoempleado.Text);    // APELLIDOS EMPLEADOS
                    comando.Parameters.AddWithValue("@generoE", txtgeneroempleado.Text);        // GENERO DE EMPLEADOS
                    comando.Parameters.AddWithValue("@emailE", txtemailempleado.Text);          // E-MAIL DE EMPLEADOS
                    comando.Parameters.AddWithValue("@FechaCE", txtfechacontratacion.Text);     // FECHA CONTRATACION EMPLEADOS
                    comando.Parameters.AddWithValue("@duiE", txtdui.Text);                      // DUI EMPLEADOS
                    comando.Parameters.AddWithValue("@nitE", txtnit.Text);                      // NIT EMPLEADOS
                    comando.Parameters.AddWithValue("@salarioE", txtsalario.Text);              // SALARIO EMPLEADOS
                    comando.ExecuteNonQuery(); // EJECUTANDO COMANDO Y ENVIANDO DATOS HACIA LA BASE DE DATOS
                                               // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Nuevo empleado agregado con éxito!.", "Registrando Nuevo Usuario",
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
                    txtcodigoempleado.Text = " "; txtnombreempleado.Text = " "; txtapellidoempleado.Text = " "; txtgeneroempleado.Text = " "; txtemailempleado.Text = " "; txtfechacontratacion.Text = " "; txtdui.Text = " "; txtnit.Text = " "; txtsalario.Text = " ";
                }
            }
            catch (Exception a)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos, pero el empleado que intenta registrar ya existe en la base de datos, por favor intente con otro código de empleado", "Error Empleado Ya Existe",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop) == DialogResult.OK) ;
                MessageBox.Show(a.Message);
            }
            finally
            {
                con.Close();// AL FINALIZAR CADA NUEVO REGISTRO, SE PROCEDE A CERRAR LA CONEXION A ESPERA DE UNA NUEVA PETICION
            }
        }

        /*
            --> METODO PARA EXPORTAR TABLA DE USUARIOS A FORMATO EXCEL      
        */
        public void exportaraexcel(DataGridView tabla)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            int IndiceColumna = 0;
            foreach (DataGridViewColumn col in tabla.Columns) // Columnas
            {
                IndiceColumna++;
                excel.Cells[1, IndiceColumna] = col.Name;
            }
            int IndeceFila = 0;
            foreach (DataGridViewRow row in tabla.Rows) // Filas
            {
                IndeceFila++;
                IndiceColumna = 0;
                foreach (DataGridViewColumn col in tabla.Columns)
                {
                    IndiceColumna++;
                    excel.Cells[IndeceFila + 1, IndiceColumna] = row.Cells[col.Name].Value;
                }
            }
            excel.Visible = true;
        }

        private void ExportarEmpleados_Click(object sender, EventArgs e)
        {
            // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
            if (MessageBox.Show("Estamos procesando la información y creando el archivo final, por favor espere un momento", "Generando Archivo Final",
                MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
            exportaraexcel(DetallesEmpleadosSistema);
        }

        /*
            --> METODO PARA EXPORTAR TABLA A PDF 
        */
        public void ExportarPDF(DataGridView RegistroInformesSistema, string NombreArchivoFinal)
        {
            BaseFont EstilosFuentes = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(RegistroInformesSistema.Columns.Count);
            pdftable.DefaultCell.Padding = 14;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_CENTER;
            pdftable.DefaultCell.BorderWidth = 4;
            iTextSharp.text.Font text = new iTextSharp.text.Font(EstilosFuentes, 19, iTextSharp.text.Font.NORMAL);
            foreach (DataGridViewColumn colum in RegistroInformesSistema.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(colum.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                pdftable.AddCell(cell);
            }

            foreach (DataGridViewRow row in RegistroInformesSistema.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdftable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }

            var EstilosFinalesArchivoFinal = new SaveFileDialog();
            EstilosFinalesArchivoFinal.FileName = NombreArchivoFinal;
            EstilosFinalesArchivoFinal.DefaultExt = ".pdf";
            if (EstilosFinalesArchivoFinal.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(EstilosFinalesArchivoFinal.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A1.Rotate(), 40, 40, 40, 40);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdftable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }
        }

        private void ExportarEmpleadosPDF_Click(object sender, EventArgs e)
        {
            // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
            if (MessageBox.Show("Estimado usuario, por favor tome en cuenta lo siguiente:\n- Luego de leer las indicaciones y cerrar esta ventana, le aparecerá una ventana donde usted puede elegir el destino deseado del archivo que usted está intentando guardar.\n- Por favor evite la necesidad de agregar una extensión final, está se genera automáticamente, usted tiene la libertad de dejar el mismo nombre, o cambiarlo por el que le sea mas conveniente.", "Generando Archivo Final",
                MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
            ExportarPDF(DetallesEmpleadosSistema,"Registro Completo Empleados");
        }

        /*
         * --> BOTON MODIFICAR EMPLEADOS
         */

        private void ModificarEmpleados_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioModificarE = new ModificarEmpleados(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioModificarE.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
         * --> BOTON ELIMINAR EMPLEADOS
         */

        private void EliminarEmpleados_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioEliminarE = new EliminarEmpleados(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioEliminarE.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON IMPRIMIR LISTA COMPLETA DE EMPLEADOS REGISTRADOS 
        */

        private void ImprimirListaEmpleados_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioImprimirListadoE = new ImpresionListaEmpleados(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioImprimirListadoE.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
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
