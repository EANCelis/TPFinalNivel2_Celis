using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
namespace negocio
{
    public class ProductoNegocio
    {
        public List<Producto> listar()
        {
            List <Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select a.Id Id, Codigo, Nombre, a.Descripcion Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio, m.Descripcion Marca, c.Descripcion Categoria from ARTICULOS a, MARCAS m, CATEGORIAS c where IdMarca = m.id AND IdCategoria = c.Id");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.CodigoArticulo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    }
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

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
        public void agregar(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into Articulos (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria, ImagenUrl) values ('" + producto.CodigoArticulo + "','" + producto.Nombre + "','" + producto.Descripcion + "','" + producto.Precio + "',"+ producto.Marca.Id + ","+ producto.Categoria.Id + ",'"+ producto.ImagenUrl + "')");
                datos.ejecutarAccion();

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
        public void modificar (Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update Articulos set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @Url, Precio = @Precio where id = @Id");
                datos.setearParametro("@Codigo", producto.CodigoArticulo);
                datos.setearParametro("@Nombre", producto.Nombre);
                datos.setearParametro("@Descripcion", producto.Descripcion);
                datos.setearParametro("@IdMarca", producto.Marca.Id);
                datos.setearParametro("@IdCategoria", producto.Categoria.Id);
                datos.setearParametro("@Url", producto.ImagenUrl);
                datos.setearParametro("@Precio", producto.Precio);
                datos.setearParametro("@Id", producto.Id);

                datos.ejecutarAccion();
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
        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("Delete from Articulos where Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Producto> filtrar (string campo, string criterio, string filtro)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "select a.Id Id, Codigo, Nombre, a.Descripcion Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio, m.Descripcion Marca, c.Descripcion Categoria from ARTICULOS a, MARCAS m, CATEGORIAS c where IdMarca = m.id AND IdCategoria = c.Id AND ";               
                if (campo == "Precio")
                {
                    if (criterio == "Mayor a")
                        consulta += campo + " > " + filtro;
                    else if (criterio == "Menor a")
                        consulta += campo + " < " + filtro;
                    else
                        consulta += campo + " = " + filtro;
                }
                else if (campo == "Marca")
                {
                    if (criterio == "Empieza con")
                        consulta += "m.descripcion like '" + filtro + "%'";
                    if (criterio == "Termina con")
                        consulta += "m.descripcion like '%" + filtro + "'";
                    if (criterio == "Contiene")
                        consulta += "m.descripcion like '%" + filtro + "%'";
                }
                else if (campo == "Categoría")
                {
                    if (criterio == "Empieza con")
                        consulta += "c.descripcion like '" + filtro + "%'";
                    if (criterio == "Termina con")
                        consulta += "c.descripcion like '%" + filtro + "'";
                    if (criterio == "Contiene")
                        consulta += "c.descripcion like '%" + filtro + "%'";
                }
                else
                {
                    if (criterio == "Empieza con")
                        consulta += campo + " like '" + filtro + "%'";
                    if (criterio == "Termina con")
                        consulta += campo + " like '%" + filtro + "'";
                    if (criterio == "Contiene")
                        consulta += campo + " like '%" + filtro + "%'";
                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.CodigoArticulo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    }
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

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
