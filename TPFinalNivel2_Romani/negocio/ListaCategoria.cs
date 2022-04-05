using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;


namespace negocio
{
    public class ListaCategoria
    {
       public List <Categoria> listarCategoria()
        {   
            AccesoDatos datos = new AccesoDatos();
            List <Categoria> lista = new List <Categoria>();

            try
            {
                datos.setearConsulta("select Id , Descripcion from CATEGORIAS");
                datos.ejecutarLectura();

                while(datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }

                return lista;
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

        
           
    }
}
