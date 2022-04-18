using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class FiltrarArt
    {
        // Filtro Avanzado
        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List <Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id , A.Codigo , A.Nombre , A.Descripcion , IdMarca , M.Descripcion Marca, IdCategoria ,C.Descripcion Categoria, A.ImagenUrl , A.Precio Precio From ARTICULOS A , MARCAS M , CATEGORIAS C where A.IdMarca = M.Id and A.IdCategoria = C.Id And";

                if (campo == "Código")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += " A.Codigo like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += " A.Codigo like '%" + filtro + "'";
                            break;
                        case "Igual a":
                            consulta += " A.Codigo like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += " M.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += " M.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += " M.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Categoria")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += " C.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += " C.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += " C.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += " A.Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += " A.Precio < " + filtro;
                            break;
                        default:
                            consulta += " A.Precio = " + filtro;
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];  
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    //Validar Null imagen.
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
