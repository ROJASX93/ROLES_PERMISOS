using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

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
    }
}
