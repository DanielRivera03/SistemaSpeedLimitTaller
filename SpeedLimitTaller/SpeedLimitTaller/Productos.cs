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
    public partial class Productos : Form
    {
        // CONEXION C# A BASE DE DATOS
        SqlConnection con = new SqlConnection(@"Data Source=SONYVAIO\SQLEXPRESS;Initial Catalog=speedlimitdb;Integrated Security=True");


        // CREACION DE OBJETOS CONTROLADORES DE COMBOBOX
        ControlesCombobox DatosProveedores = new ControlesCombobox();   // DATOS DE PROVEEDORES
        ControlesCombobox DatosCategorias = new ControlesCombobox();    // DATOS DE CATEGORIAS


        public Productos()
        {
            InitializeComponent();
            // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
            SqlCommand cmd = new SqlCommand("Select * from Productos", con);
            // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
            SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
            MostrarRegistros.SelectCommand = cmd;
            // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
            DataTable TablaRegistros = new DataTable();
            // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
            MostrarRegistros.Fill(TablaRegistros);
            // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
            // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {Empleados}
            DetallesProductosSistema.DataSource = TablaRegistros;
            // RESETEANDO LA COLUMNA DE SALARIOS PARA QUE MUESTRE UNICAMENTE .2 DECIMALES DESPUES DE LA CIFRA EXACTA
            DetallesProductosSistema.Columns[5].DefaultCellStyle.Format = "N2";
            /*
             * INVOCANDO METODO PARA PASO DE DATOS DE COMBOBOS HACIA BASE DE DATOS
             */
            DatosProveedores.SeleccionarDatos(txtProveedorProducto);       // PROVEEDORES DE PRODUCTOS
            DatosCategorias.SeleccionarDatosCt(txtCategoriaProductos);     // CATEGORIAS DE PRODUCTOS

            /*
             * RESETEO DE COMBOBOX PARA UN TAMAÑO FIJO PREDETERMINADO
             */
            txtProveedorProducto.Size = new System.Drawing.Size(273, 35);      // PROVEEDORES DE PRODUCTOS
            txtCategoriaProductos.Size = new System.Drawing.Size(273, 35);     // CATEGORIAS DE PRODUCTOS
        }

        /*
            --> BOTON AGREGAR NUEVOS PRODUCTOS AL SISTEMA {BOTON PRINCIPAL} 
        */

        private void RegistroNuevoProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcodigounicoproducto.Text.Length == 0 || txtnombreproducto.Text.Length == 0 || txtmarcaproducto.Text.Length == 0 || txtmodeloproducto.Text.Length == 0 || txtprecioproducto.Text.Length == 0 || txtProveedorProducto.Text.Length == 0 || txtCategoriaProductos.Text.Length == 0)
                {
                    // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Lo sentimos pero ha ocurrido un error, tome en cuenta lo siguiente:\n- No puede dejar campos vacíos.\n- Por favor sea cuidadoso a la hora de agregar un nuevo producto, respetando las métricas establecidas por la empresa.", "Error al Intentar Agregar Registro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) ;
                }
                else
                {
                    // CREANDO CADENA DE INSERCION query CON SU PASO DE PARAMETROS HACIA LA BASE DE DATOS
                    string query = "INSERT INTO Productos (Cod_producto, Nombre, Marca, Modelo, Precio, Nombre_proveedor, Nombre_Categoria) VALUES (@cod_producto,@nombre,@marca,@modelo,@precio,@nombre_proveedor,@nombre_categoria)";
                    con.Open();// APERTURANDO CONEXION
                    SqlCommand comando = new SqlCommand(query, con); // CREANDO COMANDO DE CONEXION
                                                                     // ENVIAR LOS DATOS INGRESADOS POR EL USUARIO EN LAS CAJAS DE TEXTO, A LOS PARAMETROS DE COMUNICACION
                                                                     // A LA BASE DE DATOS...
                    comando.Parameters.AddWithValue("@cod_producto", txtcodigounicoproducto.Text);      // CODIGO UNICO DE PRODUCTO
                    comando.Parameters.AddWithValue("@nombre", txtnombreproducto.Text);                 // NOMBRE DE PRODUCTO
                    comando.Parameters.AddWithValue("@marca", txtmarcaproducto.Text);                   // MARCA DE PRODUCTO
                    comando.Parameters.AddWithValue("@modelo", txtmodeloproducto.Text);                 // MODELO DE PRODUCTO
                    comando.Parameters.AddWithValue("@precio", txtprecioproducto.Text);                 // PRECIO DE PRODUCTO
                    comando.Parameters.AddWithValue("@nombre_proveedor", txtProveedorProducto.Text);    // PROVEEDOR DE PRODUCTO
                    comando.Parameters.AddWithValue("@nombre_categoria", txtCategoriaProductos.Text);   // CATEGORIA DE PRODUCTO
                    comando.ExecuteNonQuery(); // EJECUTANDO COMANDO Y ENVIANDO DATOS HACIA LA BASE DE DATOS
                                               // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Nuevo producto agregado con éxito!.", "Registrando Nuevo Usuario",
                        MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
                    /*---------------------------------------------------------------------------------------------
                     * AL MOMENTO QUE EL USUARIO INGRESA LOS DATOS, ESTE VUELVE A EJECUTAR EL PROCEDIMIENTO PARA  *
                     * REFRESCAR LA TABLA CONTENEDORA CON LOS N USUARIOS REGISTRADOS EN EL SISTEMA...             *
                     * --------------------------------------------------------------------------------------------
                     */
                    txtcodigounicoproducto.Text = " "; txtmarcaproducto.Text = " ";
                    txtmodeloproducto.Text = " "; txtnombreproducto.Text = " "; txtprecioproducto.Text = " ";
                    DatosProveedores.SeleccionarDatos(txtProveedorProducto);       // PROVEEDORES DE PRODUCTOS
                    DatosCategorias.SeleccionarDatosCt(txtCategoriaProductos);     // CATEGORIAS DE PRODUCTOS
                    // REFERENCIANDO EL NOMBRE DE LA TABLA A MOSTRAR LOS N DATOS CONTENIDOS EN ELLA
                    SqlCommand cmd = new SqlCommand("Select * from Productos", con);
                    // CREANDO ADAPTADOR DE COMUNICACION HACIA LA BASE DE DATOS PARA EL LLENADO DE REGISTROS...
                    SqlDataAdapter MostrarRegistros = new SqlDataAdapter();
                    MostrarRegistros.SelectCommand = cmd;
                    // REFERENCIANDO LA INVOCACION DE UN NUEVO OBJETO PARA MANEJO DE DATAGRIDVIEW
                    DataTable TablaRegistros = new DataTable();
                    // LLENANDO CON TODOS LOS REGISTROS CONTENIDOS EN LA TABLA DE EMPLEADOS
                    MostrarRegistros.Fill(TablaRegistros);
                    // INVOCANDO LA REFERENCIA QUE SEA IGUAL AL ADAPTADOR DE LLENADO DE DATOS,
                    // ES DECIR, TODOS LOS REGISTROS CONTENIDOS DENTRO DE LA TABLA {PRODUCTOS}
                    DetallesProductosSistema.DataSource = TablaRegistros;
                    // RESETEANDO LA COLUMNA DE SALARIOS PARA QUE MUESTRE UNICAMENTE .2 DECIMALES DESPUES DE LA CIFRA EXACTA
                    DetallesProductosSistema.Columns[6].DefaultCellStyle.Format = "N2";
                }
            }
            catch (Exception a)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos, pero el producto que intenta registrar ya existe en la base de datos, por favor ingrese otro código de producto", "Error Producto Ya Existe",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop) == DialogResult.OK) ;
                MessageBox.Show(a.Message);
            }
            finally
            {
                con.Close();// AL FINALIZAR CADA NUEVO REGISTRO, SE PROCEDE A CERRAR LA CONEXION A ESPERA DE UNA NUEVA PETICION
            }
        }

        /*
            --> BOTON MODIFICAR PRODUCTOS 
        */

        private void ModificarProductos_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioModificarP = new ModificarProductos(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioModificarP.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON ELIMINAR PRODUCTOS 
        */

        private void EliminarProductos_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioEliminarP = new EliminarProductos(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioEliminarP.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON NUEVO / VER PROVEEDORES 
        */

        private void NuevoVerProveedores_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioNAProveedores = new NuevosAgregarProveedores(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioNAProveedores.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON MODIFICAR (EDITAR) PROVEEDORES 
        */

        private void ModificarProveedores_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioEditarProveedores = new ModificarProveedores(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioEditarProveedores.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON ELIMINAR PROVEEDORES 
        */

        private void EliminarProveedores_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioEliminarProveedores = new EliminarProveedor(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioEliminarProveedores.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON NUEVO / VER CATEGORIAS 
        */

        private void NuevoVerCategorias_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioNACategorias = new NuevoVerCategorias(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioNACategorias.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON MODIFICAR (EDITAR) CATEGORIAS 
        */

        private void ModificarCategorias_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioModificarCategorias = new ModificarCategorias(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioModificarCategorias.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON ELIMINAR CATEGORIAS 
        */

        private void EliminarCategorias_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioEliminarCategorias = new EliminarCategorias(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioEliminarCategorias.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
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
