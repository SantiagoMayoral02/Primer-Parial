using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BLL
{
    public class BLSessionManager
    {
        private Usuario usuario;
        private Directorio rootDirectory;
        private Directorio currentDirectory;
        public Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        public Directorio RootDirectory
        {
            get { return rootDirectory; }
            set { rootDirectory = value; }
        }

        public Directorio CurrentDirectory
        {
            get { return currentDirectory; }
            set { currentDirectory = value; }
        }
        private static BLSessionManager _session;
        public static BLSessionManager GetInstance
        {
            get
            {
                return _session;
            }
        }

        public static void login(Usuario usu)
        {

            if (_session == null)
            {
                _session = new BLSessionManager();
                _session.usuario = usu;
                var root = new Directorio(usu.Nombre);
                _session.rootDirectory = root;
                _session.currentDirectory = root;
            }
            else
            {
                throw new Exception("Sesion no iniciada");
            }
        }

        public static void logaut()
        {
            if (_session != null)
            {
                _session = null;
            }
            else
            {
                throw new Exception("Sesion no iniciada");
            }
        }
        public string CurrentPath
        {
            get
            {
                List<string> path = new List<string>();
                var dir = currentDirectory;
                while (dir != null)
                {
                    path.Insert(0, dir.Nombre);
                    dir = dir.Padre;
                }
                return string.Join("/", path);
            }
        }
    }
}
