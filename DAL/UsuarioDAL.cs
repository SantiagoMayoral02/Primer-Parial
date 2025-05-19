using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seguridad;

namespace DAL
{
    public class UsuarioDAL
    {
        Encrypting EncryptManager = new Encrypting();
        public Usuario ValidarUsuario(string nom, string con)
        {
            try
            {
                string commandText = "BuscarUsuario";
                var parametros = new Dictionary<string, object>
                {
                    { "@nombre", nom }
                };
                DAO dAO = new DAO();
                DataSet ds = dAO.ExecuteDataSet(commandText, parametros);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    string nombreUsuario = row["Nombre"].ToString();
                    string contrasena = row["Contrasena"].ToString();
                    int id = Convert.ToInt32(row["id_usuario"]);

                    if (nombreUsuario == nom && EncryptManager.ValidarContraseña(con, contrasena))
                    {
                        Usuario usu = new Usuario();
                        usu.Nombre = nombreUsuario;
                        usu.Contrasena = contrasena;
                        usu.Id = id;

                        return usu;
                    }
                    else
                    {
                        Console.WriteLine("Contraseña incorrecta");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("Usuario no encontrado");
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
