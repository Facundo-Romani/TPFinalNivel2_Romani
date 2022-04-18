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
using System.IO;
using System.Configuration;


namespace AdministradorDeArticulos
{
    public partial class frmAgregarArticulo : Form
    {
        
        private Articulo articulo = null;
        private OpenFileDialog archivo = null;
        public frmAgregarArticulo()
        {
            InitializeComponent();
            Text = "Nuevo Artículo";
        }

        public frmAgregarArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Artículo";
        }

        // Este botón corresponde al Aceptar.
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarModificarArt cargarArt = new AgregarModificarArt();
            
            try
            {
                if (articulo == null) 
                    articulo = new Articulo();

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.ImagenUrl = txtUrlImagen.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                // Desplegables cbx.
                articulo.Marca = (Marca)cbxMarca.SelectedItem;
                articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
               
                if (articulo.Id != 0)
                {
                    cargarArt.modificarArticulo(articulo);
                    MessageBox.Show(" Modificado Exitosamente ");
                }
                else
                {
                    cargarArt.agregarArticulo(articulo);
                    MessageBox.Show(" Articulo Agregado Exitosamente ");
                }
                // Guardo imagen si la levantó localmente:
                if (archivo != null && !(txtUrlImagen.Text.ToUpper().Contains("HTTP")))
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["ImgCatalogo"] + archivo.SafeFileName);

                Close();
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
                    //Selección de desplegables para el modificar.
                    cbxMarca.SelectedValue = articulo.Marca.Id;
                    cbxCategoria.SelectedValue = articulo.Categoria.Id;
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

        private void btnAgregarImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg;|png|*.png"; 

            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtUrlImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
            }
        }
    }
}
