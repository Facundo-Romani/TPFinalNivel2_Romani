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
   
            cbxCategoria.Items.Add(categoria);
            cbxMarca.Items.Add(marca);
            
        }

        //private void frmAgregarArticulo_Load(object sender, EventArgs e)
        //{

        //}
    }
}
