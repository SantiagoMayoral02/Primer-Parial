using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Sistema_Operativo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UsuarioBL usuarioBL = new UsuarioBL();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Bienvenido a UaiOS");
                Console.Write("Usuario: ");
                string username = Console.ReadLine();
                Console.Write("Contraseña: ");
                string password = Console.ReadLine();
                Usuario usu = usuarioBL.ValidarUsuario(username, password);
                if (usu != null)
                {
                    BLSessionManager.login(usu);
                    Console.Clear();
                    Console.WriteLine($"Bienvenido, {usu.Nombre}!");
                    Run();
                }
                else
                {
                    Console.WriteLine("Credenciales incorrectas");
                    Thread.Sleep(2000);
                }
            }
        }
        public static void Run()
        {
            while (true)
            {
                var session = BLSessionManager.GetInstance;
                Console.Write($"{session.CurrentPath}> ");
                string input = Console.ReadLine();
                var parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 0)
                {
                    continue;
                }

                string cmd = parts[0].ToUpper();

                switch (cmd)
                {
                    case "MD":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Debe especificar el nombre del directorio.");
                        }
                        else
                        {
                            try
                            {
                                var nuevoDir = new Directorio(parts[1]);
                                ((Directorio)session.CurrentDirectory).AgregarHijo(nuevoDir);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        break;

                    case "CD":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Debe especificar el nombre del directorio.");
                        }
                        else
                        {
                            var target = parts[1];
                            if (target == "..")
                            {
                                if (session.CurrentDirectory.Padre != null)
                                    session.CurrentDirectory = session.CurrentDirectory.Padre;
                                else
                                    Console.WriteLine("Ya estás en el directorio raíz.");
                            }
                            else
                            {
                                var hijo = session.CurrentDirectory.Hijos?
                                    .FirstOrDefault(c => c.Nombre == target && c is Directorio);

                                if (hijo != null)
                                    session.CurrentDirectory = (Directorio)hijo;
                                else
                                    Console.WriteLine("El directorio no existe.");
                            }
                        }
                        break;

                    case "MF":
                        if (parts.Length < 3 || !int.TryParse(parts[2], out int tamaño))
                        {
                            Console.WriteLine("Uso: MF nombrearchivo tamaño");
                        }
                        else
                        {
                            try
                            {
                                var nuevoArchivo = new Archivo(parts[1], tamaño);
                                ((Directorio)session.CurrentDirectory).AgregarHijo(nuevoArchivo);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        break;

                    case "LS":
                        var hijos = session.CurrentDirectory.Hijos;
                        if (hijos == null || hijos.Count == 0)
                        {
                            Console.WriteLine("No hay datos.");
                        }
                        else
                        {
                            foreach (var hijo in hijos)
                            {
                                var tipo = hijo is Directorio ? "[DIR]" : "[FILE]";
                                Console.WriteLine($"{tipo} {hijo.Nombre} - {hijo.ObtenerTamaño()} bytes");
                            }
                            Console.WriteLine($"Tamaño total: {session.CurrentDirectory.ObtenerTamaño()} bytes");
                        }
                        break;

                    case "DI":
                        Console.WriteLine("Sesión cerrada.");
                        BLSessionManager.logaut();
                        return;

                    default:
                        Console.WriteLine("Comando no reconocido.");
                        break;
                }
            }
        }
    }
}
