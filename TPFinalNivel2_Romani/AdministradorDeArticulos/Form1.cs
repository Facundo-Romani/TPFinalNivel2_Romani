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
    public partial class Form1 : Form
    {

        private List<Articulo> listaArticulo;
        public Form1()
        {
            InitializeComponent();
            Text = "GESTOR DE ARTÍCULOS";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
            cboCampo.Items.Add("Código");
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Categoría");
            cboCampo.Items.Add("Precio");
        }

        private void cargar()
        {
            ListaArticulo articulo = new ListaArticulo();
            try
            {
                listaArticulo = articulo.listarArticulo();
                dgvListaDeArticulos.DataSource = listaArticulo;
                ocultarColumnas();
                cargarImagen(listaArticulo[0].ImagenUrl);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvListaDeArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvListaDeArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvListaDeArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);
            }        
        }

        private void ocultarColumnas()
        {
            dgvListaDeArticulos.Columns["Id"].Visible = false;
            dgvListaDeArticulos.Columns["ImagenUrl"].Visible = false;
        }

        private void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            frmAgregarArticulo agregar = new frmAgregarArticulo();
            agregar.ShowDialog();
            cargar();
        }

        private void btnModificarArticulo_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvListaDeArticulos.CurrentRow.DataBoundItem;

            frmAgregarArticulo modificar = new frmAgregarArticulo(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        // Eliminar físico el artículo.
        private void btnEliminarArticulo_Click(object sender, EventArgs e)
        {
            AgregarModificarArt eliminar = new AgregarModificarArt();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿De verdad querés eliminar este Artículo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvListaDeArticulos.CurrentRow.DataBoundItem;
                    eliminar.eliminar(seleccionado.Id);
                    cargar();
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Evento para ajustes del filtro rápido.
        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltroRapido.Text;

            if (validarFiltroRapido())
                return;

            if (filtro.Length >= 3)
            {
                listaFiltrada = listaArticulo.FindAll(Articulo => Articulo.Nombre.ToUpper().Contains(filtro.ToUpper()) || Articulo.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaArticulo;
            }

            dgvListaDeArticulos.DataSource = null;
            dgvListaDeArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();

            if (opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
        }

        private void btnFiltrarAvanzado_Click(object sender, EventArgs e)
        {
            FiltrarArt filtroArt = new FiltrarArt();

            try
            {
                if (validarFiltro())
                     return;

                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                dgvListaDeArticulos.DataSource = filtroArt.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Validación filtro Avanzado.
        private bool validarFiltro()
        {
            if (cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el campo para filtrar.");
                return true;
            }
            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el criterio para filtrar.");
                return true;
            }
            if (cboCampo.SelectedItem.ToString() == "Precio")
            {
                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Debes cargar el filtro para Precio");
                    return true;
                }
                if (!(soloNumeros(txtFiltroAvanzado.Text)))
                {
                    MessageBox.Show("Ingresar solo números, para filtrar por un campo Precio.");
                    return true;
                }

            }

            return false;
        }

        // Validar que solo ingrese número en txtFiltroAvanzado.
        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }

        // Validar filtro Rápido.
        private bool validarFiltroRapido()
        {
            if ((soloNumeros(txtFiltroRapido.Text)))
            {
                MessageBox.Show("Ingresar solo letras, para filtrar por Nombre de Artículo.");
                return true;
            }
            return false;
        }
        
        // Cargar Imagen.
        private void cargarImagen(string Imagen)
        {
            try
            {
                pbxImagenArticulo.Load(Imagen);
            }
            catch (Exception ex)
            {
                pbxImagenArticulo.Load("https://w7.pngwing.com/pngs/848/297/png-transparent-white-graphy-color-empty-banner-blue-angle-white.png");
            }
        }

        private void btnDetalleArticulo_Click(object sender, EventArgs e)
        {
            Articulo detalleArt;
            detalleArt = (Articulo)dgvListaDeArticulos.CurrentRow.DataBoundItem;

            FrmDetallesDelArticulo detallesArticulo = new FrmDetallesDelArticulo(detalleArt);
            detallesArticulo.ShowDialog();
            cargar();
        }
    }
}
