using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioBL
    {
        UsuarioDAL usuarioDAL = new UsuarioDAL();
        public Usuario ValidarUsuario(string nom, string con)
        {
            return usuarioDAL.ValidarUsuario(nom, con);
        }
    }
}
