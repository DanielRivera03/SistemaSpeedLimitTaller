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
    public partial class Inventarios : Form
    {

        // CONEXION C# A BASE DE DATOS
        SqlConnection con = new SqlConnection(@"Data Source=SONYVAIO\SQLEXPRESS;Initial Catalog=speedlimitdb;Integrated Security=True");


        // CREACION DE OBJETOS CONTROLADORES DE COMBOBOX
        ControlesCombobox DatosProveedores = new ControlesCombobox();   // DATOS DE PROVEEDORES
        ControlesCombobox DatosCategorias = new ControlesCombobox();    // DATOS DE CATEGORIAS



        public Inventarios()
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
            DetallesProductosSistema.Columns[5].DefaultCellStyle.Format = "C1";
            /*
             * INVOCANDO METODO PARA PASO DE DATOS DE COMBOBOX HACIA BASE DE DATOS
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
            --> BOTON SELECCIONAR PRODUCTO A INGRESAR EN INVENTARIO 
        */

        private void SeleccionarProductoInventario_Click(object sender, EventArgs e)
        {
            // SI EL USUARIO HA SELECCIONADO UN REGISTRO, ENTONCES...
            if (DetallesProductosSistema.SelectedRows.Count > 0)
            {
                /* EL DATAGRIDVIEW SE ENCARGA DE MOSTRAR AL USUARIO LOS DATOS ALMACENADOS EN LA BASE DE DATOS
                * POR LO TANTO AL MOMENTO QUE EL USUARIO SELECCIONE DICHO REGISTRO Y PRESIONE EL BOTON SELECCIONAR
                * USUARIO, SE PROCEDE A TOMAR CADA UNO DE LOS CAMPOS ALMACENADOS EN LOS TEXTBOX CON SUS IDENTIFICADORES
                * UNICOS Y REALIZA LA RESPECTIVA CONVERSION A CADENA PARA QUE ESA INFORMACION SEA VISIBLE AL USUARIO
                * FINAL Y PUEDA PROCEDER A ACTUALIZAR EL N REGISTRO QUE DESEE...
                */
                txtidproductos.Text = DetallesProductosSistema.CurrentRow.Cells["ID_producto"].Value.ToString();
                txtcodigounicoproducto.Text = DetallesProductosSistema.CurrentRow.Cells["Cod_producto"].Value.ToString();       // CODIGO UNICO PRODUCTO
                txtnombreproducto.Text = DetallesProductosSistema.CurrentRow.Cells["Nombre"].Value.ToString();                  // NOMBRE PRODUCTO
                txtmarcaproducto.Text = DetallesProductosSistema.CurrentRow.Cells["Marca"].Value.ToString();                    // MARCA PRODUCTO
                txtmodeloproducto.Text = DetallesProductosSistema.CurrentRow.Cells["Modelo"].Value.ToString();                  // MODELO PRODUCTO
                txtprecioproducto.Text = DetallesProductosSistema.CurrentRow.Cells["Precio"].Value.ToString();                  // PRECIO PRODUCTO
                txtProveedorProducto.Text = DetallesProductosSistema.CurrentRow.Cells["Nombre_proveedor"].Value.ToString();     // PROVEEDOR PRODUCTO
                txtCategoriaProductos.Text = DetallesProductosSistema.CurrentRow.Cells["Nombre_Categoria"].Value.ToString();    // CATEGORIA PRODUCTO
                con.Close();  // CIERRE DE CONEXION UNA VEZ TOMADOS LOS PARAMETROS Y ARGUMENTOS PREVIO A LA ACTUALIZACION          
            }// DE LO CONTRARIO...
            else
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos pero debe seleccionar un registro, por favor seleccione el registro deseado ha actualizar.", "Error de Modificación",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK) ;
            }
        }

        /*
            --> BOTON REGISTRAR PRODUCTOS A INVENTARIO 
        */

        private void RegistroNuevoInventario_Click(object sender, EventArgs e)
        {
            if (txtnumstockinventario.Text.Length == 0  || txtgarantiainventario.Text.Length == 0 ||  txtcodigounicoproducto.Text.Length == 0 || txtnombreproducto.Text.Length == 0 || txtmarcaproducto.Text.Length == 0 || txtmodeloproducto.Text.Length == 0 || txtprecioproducto.Text.Length == 0 || txtProveedorProducto.Text.Length == 0 || txtCategoriaProductos.Text.Length == 0)
            {
                // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                if (MessageBox.Show("Lo sentimos pero ha ocurrido un error, tome en cuenta lo siguiente:\n- No puede dejar campos vacíos.\n- Por favor sea cuidadoso a la hora de agregar un nuevo producto en control de inventarios, respetando las métricas establecidas por la empresa.\n- Le recordamos que usted no puede modificar los campos de productos, estos son los que han sido registrados previamente en el sistema los cuales usted puede procesar.", "Error al Intentar Agregar Registro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) ;
                con.Close();// AL FINALIZAR CADA NUEVO REGISTRO, SE PROCEDE A CERRAR LA CONEXION A ESPERA DE UNA NUEVA PETICION
            }
            else
            {
                try
                {
                    // CREANDO CADENA DE INSERCION query CON SU PASO DE PARAMETROS HACIA LA BASE DE DATOS
                    string query = "INSERT INTO ControlInventarios (Numeros_Unidades_Stock, Periodo_Garantia, ID_producto, Cod_producto, Nombre, Marca, Modelo, Precio, Nombre_proveedor, Nombre_Categoria) VALUES (@numstock,@periodogarantia,@idproducto,@cod_producto,@nombre,@marca,@modelo,@precio,@nombre_proveedor,@nombre_categoria)";
                    con.Open();// APERTURANDO CONEXION
                    SqlCommand comando = new SqlCommand(query, con); // CREANDO COMANDO DE CONEXION
                                                                     // ENVIAR LOS DATOS INGRESADOS POR EL USUARIO EN LAS CAJAS DE TEXTO, A LOS PARAMETROS DE COMUNICACION
                                                                     // A LA BASE DE DATOS...
                    comando.Parameters.AddWithValue("@numstock", txtnumstockinventario.Text);           // NUMERO DE UNIDADES EN STOCK {PRODUCTOS}
                    comando.Parameters.AddWithValue("@periodogarantia", txtgarantiainventario.Text);    // PERIODO DE GARANTIA EN MESES {PRODUCTOS}
                    comando.Parameters.AddWithValue("@idproducto", txtidproductos.Text);                 // ID UNICO DE PRODUCTOS
                    comando.Parameters.AddWithValue("@cod_producto", txtcodigounicoproducto.Text);      // CODIGO UNICO DE PRODUCTO
                    comando.Parameters.AddWithValue("@nombre", txtnombreproducto.Text);                 // NOMBRE DE PRODUCTO
                    comando.Parameters.AddWithValue("@marca", txtmarcaproducto.Text);                   // MARCA DE PRODUCTO
                    comando.Parameters.AddWithValue("@modelo", txtmodeloproducto.Text);                 // MODELO DE PRODUCTO
                    comando.Parameters.AddWithValue("@precio", txtprecioproducto.Text);                 // PRECIO DE PRODUCTO
                    comando.Parameters.AddWithValue("@nombre_proveedor", txtProveedorProducto.Text);    // PROVEEDOR DE PRODUCTO
                    comando.Parameters.AddWithValue("@nombre_categoria", txtCategoriaProductos.Text);   // CATEGORIA DE PRODUCTO
                    comando.ExecuteNonQuery(); // EJECUTANDO COMANDO Y ENVIANDO DATOS HACIA LA BASE DE DATOS
                                               // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Nuevo producto agregado con éxito en inventario!.", "Registrando Nuevo Producto | Control de Inventarios",
                        MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
                    /*------------------------------------------------------------------------------------------
                     * -> INICIALIZANDO TEXTBOX A SUS VALORES PREDETERMINADOS ANTES DE CERRAR CONEXION UNA VEZ
                     * REALIZADO EL NUEVO REGISTRO A LA BASE DE DATOS {CONSTRUCTORES}
                     -----------------------------------------------------------------------------------------*/
                    txtnumstockinventario.Text = " "; txtCategoriaProductos.Text = " "; txtcodigounicoproducto.Text = " ";
                    txtmarcaproducto.Text = " "; txtmodeloproducto.Text = " "; txtnombreproducto.Text = " "; txtidproductos.Text = " ";
                    txtprecioproducto.Text = " "; txtProveedorProducto.Text = " "; txtgarantiainventario.Text = " ";
                    /*
                    * INVOCANDO METODO PARA PASO DE DATOS DE COMBOBOX HACIA BASE DE DATOS
                    * --> RESETEO DE COMBOBOX LUEGO DE EDICION DE DATOS
                    */
                    DatosProveedores.SeleccionarDatos(txtProveedorProducto);       // PROVEEDORES DE PRODUCTOS
                    DatosCategorias.SeleccionarDatosCt(txtCategoriaProductos);     // CATEGORIAS DE PRODUCTOS
                }

                catch (Exception a)
                {
                    // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
                    if (MessageBox.Show("Lo sentimos, pero el producto no se ha podido registrar en control de inventarios, recuerde que debe cambiar por un (.) Punto el precio al momento de seleccionar el producto a registrar en control de inventarios", "Error Al Ingresar Producto Ha Inventario",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop) == DialogResult.OK) ;
                    MessageBox.Show(a.Message);
                }
                finally
                {
                    con.Close();// AL FINALIZAR CADA NUEVO REGISTRO, SE PROCEDE A CERRAR LA CONEXION A ESPERA DE UNA NUEVA PETICION
                }
            }
        }

        /*
            --> BOTON INSTRUCCIONES DE USO INVENTARIOS 
        */

        private void InstruccionesInventarios_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioInstruccionesInventarios = new InstruccionesInventariosNEm(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioInstruccionesInventarios.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON VER INFORME COMPLETO DE INVENTARIOS 
        */

        private void VerInventario_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioVerInformeInventarios = new VerInformeInventarioCompleto(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioVerInformeInventarios.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON MODIFICAR INFORME {REGISTROS} DE INVENTARIOS 
        */

        private void ModificarInventario_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioModificarRegistrosInventarios = new ModificarControlInventarios(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioModificarRegistrosInventarios.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON BORRAR INFORME COMPLETO DE CONTROL DE INVENTARIOS 
        */

        private void EliminarInventario_Click(object sender, EventArgs e)
        {
            Form LlamarFormularioEliminarRegistrosInventarios = new EliminarRegistrosInventario(); // CREANDO NUEVO OBJETO DE TIPO FORMULARIO
            LlamarFormularioEliminarRegistrosInventarios.Show(); // INCOVANDO SUBFORMULARIO A FORMULARIO PADRE PARA MOSTRAR SUS ACCIONES DE MANTENIMIENTO
        }

        /*
            --> BOTON ELIMINAR INFORME COMPLETO DE CONTROL DE INVENTARIOS 
        */

        private void btnBorrarInformeCompletoInventarios_Click(object sender, EventArgs e)
        {
            // CREANDO VARIABLE TIPO QUERY QUE ALMACENA LA CADENA DE ELIMINAR X REGISTRO CON SU CONDICION
            string query = "TRUNCATE TABLE ControlInventarios";
            con.Open();// APERTURANDO CONEXION
            SqlCommand comando = new SqlCommand(query, con);// CREANDO COMPONENTE DE COMUNICACION HACIA LA BASE DE DATOS
            comando.ExecuteNonQuery(); // ENVIANDO COMPONENTE QUERY HACIA LA BASE DE DATOS CON NUEVO REGISTRO ACTUALIZADO
            // CREANDO MENSAJE EN VENTANA FLOTANTE PERSONALIZADO
            // --> ADVERTENCIA EJECUCIO IRREVERSIBLE
            if (MessageBox.Show("¡ADVERTENCIA! Acaba de ejecutar una acción irreversible, tome en cuenta que todos los registros serán eliminados a partir de este momento.", "¡ADVERTENCIA CONTROL DE INVENTARIOS!",
                MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK) ;
            // CONFIRMACION PROCEDIMIENTO CONCLUIDO CON EXITO
            if (MessageBox.Show("Todos los registros de Control de Inventarios eliminados con éxito", "Eliminando Registros de Control de Inventarios",
                MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) ;
            con.Close();// AL FINALIZAR CADA NUEVO REGISTRO, SE PROCEDE A CERRAR LA CONEXION A ESPERA DE UNA NUEVA PETICION
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
