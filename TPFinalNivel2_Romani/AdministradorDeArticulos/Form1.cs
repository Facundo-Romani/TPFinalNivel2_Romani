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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            ListaArticulo articulo = new ListaArticulo();
            try
            {
                listaArticulo = articulo.listarArticulo();
                dgvListaDeArticulos.DataSource = listaArticulo; 
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen( string Imagen)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
