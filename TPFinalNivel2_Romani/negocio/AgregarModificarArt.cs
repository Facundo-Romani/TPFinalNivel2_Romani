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
                datos.setearConsulta("Insert into ARTICULOS ( Codigo, Nombre, Descripcion, Precio, ImagenUrl, IdMarca, IdCategoria ) values ( @Codigo, @Nombre, @Descripcion, @Precio , @ImagenUrl ,@IdMarca, @IdCategoria )");
                datos.setearParametros("@Codigo", nuevo.Codigo);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Descripcion", nuevo.Descripcion);
                datos.setearParametros("@Precio", nuevo.Precio);
                datos.setearParametros("@ImagenUrl", nuevo.ImagenUrl);
                datos.setearParametros("@IdMarca", nuevo.Marca.Id);
                datos.setearParametros("@IdCategoria", nuevo.Categoria.Id);
               
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
            AccesoDatos actualizar  = new AccesoDatos();
            try
            {
                actualizar.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion ,IdMarca = @IdMarca, IdCategoria = @IdCategoria , ImagenUrl = @ImagenUrl, Precio = @Precio Where Id = @Id");
                actualizar.setearParametros("@Codigo", articulo.Codigo);
                actualizar.setearParametros("@Nombre", articulo.Nombre);
                actualizar.setearParametros("@Descripcion", articulo.Descripcion);
                actualizar.setearParametros("@IdMarca", articulo.Marca.Id);
                actualizar.setearParametros("@IdCategoria", articulo.Categoria.Id);
                actualizar.setearParametros("@ImagenUrl", articulo.ImagenUrl);
                actualizar.setearParametros("@Precio", articulo.Precio);

                actualizar.ejecurtarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
