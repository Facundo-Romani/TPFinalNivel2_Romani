using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace AdministradorDeArticulos
{
    public partial class frmAgregarArticulo : Form
    {
        public frmAgregarArticulo()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Articulo nuevoArt = new Articulo();
            AgregarModificarArt cargarArt = new AgregarModificarArt();
            try
            {
                nuevoArt.Codigo = txtCodigo.Text;
                nuevoArt.Nombre = txtNombre.Text;
                nuevoArt.Descripcion = txtDescripcion.Text;
                nuevoArt.UrlImagen = txtUrlImagen.Text;
                nuevoArt.Precio = int.Parse(txtPrecio.Text);
                //Desplegables cbx.
                nuevoArt.Categoria = (Categoria)cbxCategoria.SelectedItem;
                nuevoArt.Marca = (Marca)cbxCategoria.SelectedItem;

                cargarArt.agregarArticulo(nuevoArt);
                MessageBox.Show(" Articulo Agregado Exitosamente");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAgregarArticulo_Load(object sender, EventArgs e)
        {   
            ListaCategoria categoria = new ListaCategoria();
            ListaMarca marca = new ListaMarca();

            try
            {
                cbxCategoria.DataSource = categoria.listarCategoria();
                cbxMarca.DataSource = marca.listarMarca();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
        }

        //private void frmAgregarArticulo_Load(object sender, EventArgs e)
        //{

        //}
    }
}
