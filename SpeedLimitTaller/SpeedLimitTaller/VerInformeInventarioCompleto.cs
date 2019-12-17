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
// IMPORTANDO LIBRERIA QUE HABILITA EL EVENTO DE ARRASTRES DE FORMULARIOS POR PARTE DE LOS USUARIOS
using System.Runtime.InteropServices;

namespace SpeedLimitTaller
{
    public partial class VerInformeInventarioCompleto : Form
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


        public VerInformeInventarioCompleto()
        {
            InitializeComponent();
            // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
            SqlCommand cmd = new SqlCommand("Select ID_inventario, Numeros_Unidades_Stock, Periodo_Garantia, ID_producto, Cod_producto, Nombre, Marca, Modelo, Precio, Nombre_proveedor, Nombre_Categoria FROM ControlInventarios", con);
            // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
            SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
            MostrarRegistros.SelectCommand = cmd;
            // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
            DataTable TablaRegistros = new DataTable();
            // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
            MostrarRegistros.Fill(TablaRegistros);
            // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
            // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {CONTROL DE INVENTARIOS}
            DetallesInformeInventarios.DataSource = TablaRegistros;
            // RESETEANDO LA COLUMNA DE SALARIOS PARA QUE MUESTRE UNICAMENTE .2 DECIMALES DESPUES DE LA CIFRA EXACTA
            DetallesInformeInventarios.Columns[8].DefaultCellStyle.Format = "N2";
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

        private void VerInformeInventarioCompleto_MouseDown(object sender, MouseEventArgs e)
        {
            // HABILITANDO FUNCION DE ARRASTRE DE FORMULARIO
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /*
            --> BOTON EXPORTAR INFORME COMPLETO INVENTARIOS {EXCEL} 
        */

        private void ExportarInventarioEX_Click(object sender, EventArgs e)
        {
            // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
            if (MessageBox.Show("Estamos procesando la información y creando el archivo final, por favor espere un momento", "Generando Archivo Final",
                MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
            exportaraexcel(DetallesInformeInventarios);
        }

        /*
            --> METODO PARA EXPORTAR TABLA INFORME COMPLETO INVENTARIOS A FORMATO EXCEL      
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

        /*
            --> BOTON EXPORTAR INFORME COMPLETO INVENTARIOS {PDF} 
        */

        private void ExportarEmpleadosPDF_Click(object sender, EventArgs e)
        {
            // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
            if (MessageBox.Show("Estimado usuario, por favor tome en cuenta lo siguiente:\n- Luego de leer las indicaciones y cerrar esta ventana, le aparecerá una ventana donde usted puede elegir el destino deseado del archivo que usted está intentando guardar.\n- Por favor evite la necesidad de agregar una extensión final, está se genera automáticamente, usted tiene la libertad de dejar el mismo nombre, o cambiarlo por el que le sea mas conveniente.", "Generando Archivo Final",
                MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
            ExportarPDF(DetallesInformeInventarios, "Registro Completo Control de Inventarios");
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
    }
}



/**
    @@@@  @@@@  @@@@  @@@@  @@@@  @     @@@@  @@    @@  @@@@  @@@@@@    @@@@  @@@@@  @     @     @@@@  @@@@@@
    @@    @  @  @     @     @  @  @      @@   @@ @@ @@   @@    @@        @@   @@ @@  @     @     @     @@ @@@
    @@@@  @@@@  @@@@  @@@@  @  @  @      @@   @@    @@   @@    @@        @@   @@@@@  @     @     @@@@  @@@@@@
      @@  @@    @     @     @  @  @      @@   @@    @@   @@    @@        @@   @@ @@  @     @     @     @@  @@
    @@@@  @@    @@@@  @@@@  @@@@  @@@@  @@@@  @@    @@  @@@@   @@        @@   @@ @@  @@@@  @@@@  @@@@  @@   @@
**/
