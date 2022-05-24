using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using ROLES_PERMISOS_ESCRITORIO.Models;
using System.Xml;
using System.Xml.Linq;

namespace ROLES_PERMISOS_ESCRITORIO.Datos
{
    public class CD_Usuario
    {
        public static int Loguear(string usuario, string clave)
        {
            int idUsuario = 0;

            using (SqlConnection cn = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_LoginUsuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("usuario", usuario);
                    cmd.Parameters.AddWithValue("clave", clave);
                    cmd.Parameters.AddWithValue("idUsuario", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    idUsuario = Convert.ToInt32(cmd.Parameters["idUsuario"].Value);
                }
                catch (Exception ex)
                {
                    idUsuario = 0;
                }
            }

            return idUsuario;
        }

        public static List<Menu> ObtenerPermisos(int idUsuario)
        {
            List<Menu> Permisos = new List<Menu>();

            using (SqlConnection cn = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerPermisos", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idUsuario", idUsuario);

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    //< PERMISOS >
                    //  < DetalleMenu >
                    //    < Menu >
                    //      < Nombre > Ventas </ Nombre >
                    //      < Icono >\Iconos\Ventas.png </ Icono >
                    //      < DetalleSubMenu >
                    //        < SubMenu >
                    //          < Nombre > Crear venta </ Nombre >
                    //          < NombreFormulario > frmCrearVenta </ NombreFormulario >
                    //        </ SubMenu >
                    //        < SubMenu >
                    //          < Nombre > Editar venta </ Nombre >
                    //          < NombreFormulario > frmEditarVenta </ NombreFormulario >
                    //        </ SubMenu >
                    //      </ DetalleSubMenu >
                    //    </ Menu >
                    //  </ DetalleMenu >
                    //</ PERMISOS >
                    XmlReader leerXML = cmd.ExecuteXmlReader();
                    while (leerXML.Read())
                    {
                        XDocument doc = XDocument.Load(leerXML);

                        if (doc.Element("PERMISOS") != null)
                        {
                            Permisos = doc.Element("PERMISOS").Element("DetalleMenu") == null ? new List<Menu>() :
                                (
                                    from menu in doc.Element("PERMISOS").Element("DetalleMenu").Elements("Menu")
                                    select new Menu()
                                    {
                                        Nombre = menu.Element("Nombre").Value,
                                        Icono = menu.Element("Icono").Value,
                                        ListaSubMenu = menu.Element("DetalleSubMenu") == null ? new List<SubMenu>() :
                                        (
                                            from SubMenu in menu.Element("DetalleSubMenu").Elements("SubMenu")
                                            select new SubMenu()
                                            {
                                                Nombre = SubMenu.Element("Nombre").Value,
                                                NombreFormulario = SubMenu.Element("NombreFormulario").Value
                                            }
                                        ).ToList()
                                    }
                                ).ToList();
                        }

                    }


                }
                catch (Exception ex)
                {
                    Permisos = new List<Menu>();
                }
            }

            return Permisos;
        }





    }
}
