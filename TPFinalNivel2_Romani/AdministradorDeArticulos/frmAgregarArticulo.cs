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

        private Articulo articulo = null;
        public frmAgregarArticulo()
        {
            InitializeComponent();
        }

        public frmAgregarArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
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
                nuevoArt.ImagenUrl = txtUrlImagen.Text;
                nuevoArt.Precio = decimal.Parse(txtPrecio.Text);
                //Desplegables cbx.
                nuevoArt.Marca = (Marca)cbxMarca.SelectedItem;
                nuevoArt.Categoria = (Categoria)cbxCategoria.SelectedItem;
               
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

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtUrlImagen.Text = articulo.ImagenUrl;
                    cargarImagen(articulo.ImagenUrl);
                    txtPrecio.Text = articulo.Precio.ToString();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void cargarImagen(string Imagen)
        {
            try
            {
                pbxAgregar.Load(Imagen);
            }
            catch (Exception ex)
            {
                pbxAgregar.Load("https://w7.pngwing.com/pngs/848/297/png-transparent-white-graphy-color-empty-banner-blue-angle-white.png");
            }
        }
    }
}
