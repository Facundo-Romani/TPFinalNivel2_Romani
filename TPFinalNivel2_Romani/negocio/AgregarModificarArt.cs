using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class AgregarModificarArt
    {

        public void agregarArticulo(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, ImagenUrl, Precio) values(" + nuevo.Codigo + ", '" + nuevo.Nombre + "', '" + nuevo.Descripcion + "',@ImagenUrl , @IdMarca, @IdCategoria )");
                datos.setearParametros("@ImagenUrl ", nuevo.UrlImagen);
                datos.setearParametros("@IdMarca ", nuevo.Marca.Id);
                datos.setearParametros("@IdCategoria ", nuevo.Categoria.Id);
                datos.ejecurtarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificarArticulo(Articulo articulo)
        {

        }
    }
}
