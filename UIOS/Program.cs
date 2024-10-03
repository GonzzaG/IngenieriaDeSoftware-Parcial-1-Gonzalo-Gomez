using Servicio;
using UIOS.Abstracciones;
using UIOS.Comandos;

namespace UIOS
{
    internal class Program
    {
        //private static Usuario usuarioActual;
        private static string mNombreUsuario;

        private static string mContrasena;

        private static void Main()
        {
            // Verificar y obtener la instancia del sistema operativo con singleton de la capa de servicio
            UaiOS mSistemaOperativo = UaiOS.ObtenerInstancia();
            Console.WriteLine($"Bienvenido a UaiOS!\n\n");

            // Inicio de sesion con la instancia del sistema operativo
            IniciarSesion(mSistemaOperativo);

            // Mostrar ruta actual y permitir comandos
            EjecutarComandos(mSistemaOperativo);
        }

        private static void IniciarSesion(UaiOS pOS)
        {
            Console.Write("Nombre de usuario: ");
            mNombreUsuario = Console.ReadLine();

            Console.Write("Contraseña: ");
            mContrasena = Console.ReadLine();

            //valida el usuario en la capa de servicio
            if (!pOS.IniciarSesion(mNombreUsuario, mContrasena))
            {
                Console.Clear();
                Console.WriteLine($"Bienvenido a UaiOS!\n");
                Console.WriteLine("Inicio de sesión fallido. Por favor, intente de nuevo.");

                // Funcion recursiva para volver a intentar el inicio de sesion
                IniciarSesion(pOS);
            }
            Console.Clear();
            Console.WriteLine($"Bienvenido a UaiOS!\n");
            Console.WriteLine($"Inicio de sesión exitoso. Hola {mNombreUsuario}!");
            return;
        }

        private static void EjecutarComandos(UaiOS pSistemaOperativo)
        {
            IComando mComandoActual = null;
            string mComando;
            while (true)
            {
                Console.Write(pSistemaOperativo.ObtenerRuta());
                //ListamosLosComponentes en el directorio actual
                mComando = Console.ReadLine();

                // Separar el comando y los argumentos
                string[] partesComando = mComando.Split(' ');
                string nombreComando = partesComando[0].ToUpper();

                switch (nombreComando)
                {
                    case "MD":
                        // Crear un nuevo directorio
                        if (partesComando.Length < 2)
                        {
                            Console.WriteLine("Uso: MD nombredirectorio");

                            break;
                        }
                        string nuevoDirectorio = partesComando[1];
                        mComandoActual = new ComandoMD(pSistemaOperativo);
                        mComandoActual.Ejecutar(partesComando.Skip(1).ToArray());

                        break;

                    case "CD":
                        // Cambiar a otro directorio
                        if (partesComando.Length < 2)
                        {
                            Console.WriteLine("Uso: CD nombredirectorio");
                            break;
                        }

                        mComandoActual = new ComandoCD(pSistemaOperativo);
                        mComandoActual.Ejecutar(partesComando.Skip(1).ToArray());

                        break;

                    case "CD..":
                        // Retroceder al directorio padre
                        if (partesComando.Length > 1)
                        {
                            Console.WriteLine("Uso: CD..");
                        }

                        mComandoActual = new ComandoCDBack(pSistemaOperativo);
                        mComandoActual.Ejecutar(partesComando.Skip(1).ToArray());

                        break;

                    case "MF":
                        // Crear un nuevo archivo
                        if (partesComando.Length < 3)
                        {
                            Console.WriteLine("Uso: MF nombrearchivo (tamaño)");
                            break;
                        }
                        string mNombreArchivo = partesComando[1];

                        if (float.TryParse(partesComando[2], out float tamañoArchivo))
                        {
                            mComandoActual = new ComandoMF(pSistemaOperativo);
                            mComandoActual.Ejecutar(partesComando.Skip(1).ToArray());
          
                        }
                        else
                        {
                            Console.WriteLine("El tamaño debe ser un número entero.");
                        }
                        break;

                    case "LS":
                        // Retroceder al directorio padre
                        if (partesComando.Length > 1)
                        {
                            Console.WriteLine("Uso: LS");
                        }

                        mComandoActual = new ComandoLS(pSistemaOperativo);
                        mComandoActual.Ejecutar(partesComando.Skip(1).ToArray());

                        break;

                    case "DI":
                        // Desconectarse del sistema operativo
                        Console.WriteLine("Desconectándose...");
                        Console.Clear();
                        // Volvemos al main para iniciar sesion.
                        Main();
                        return;

                    case "CLEAR":
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Comando no reconocido.");
                        break;
                }
            }
        }

    }
}