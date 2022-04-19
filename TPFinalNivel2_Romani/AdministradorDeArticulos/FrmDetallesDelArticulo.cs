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
    public partial class FrmDetallesDelArticulo : Form
    {
        private Articulo articulo = null;
        public FrmDetallesDelArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Detalles del Artículo";
        }

        private void FrmDetallesDelArticulo_Load(object sender, EventArgs e)
        {
            ListaCategoria categoria = new ListaCategoria();
            ListaMarca marca = new ListaMarca();
            try
            {
                cbxCategoria.DataSource = categoria.listarCategoria();
                cbxMarca.DataSource = marca.listarMarca();
                //Despplegable manejo de clave valor.
                cbxMarca.DataSource = marca.listarMarca();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";
                cbxCategoria.DataSource = categoria.listarCategoria();
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";

                if (articulo != null) 
                { 
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtUrlImagen.Text = articulo.ImagenUrl;
                cargarImagen(articulo.ImagenUrl);
                txtPrecio.Text = articulo.Precio.ToString();
                cbxMarca.SelectedValue = articulo.Marca.Id;
                cbxCategoria.SelectedValue = articulo.Categoria.Id;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void cargarImagen(string Imagen)
        {
            try
            {
                pbxDetalles.Load(Imagen);
            }
            catch (Exception ex)
            {
                pbxDetalles.Load("https://w7.pngwing.com/pngs/848/297/png-transparent-white-graphy-color-empty-banner-blue-angle-white.png");
            }
        }
    }
}
