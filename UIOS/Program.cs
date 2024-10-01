using BEL;

namespace UIOS
{
    class Program
    {
        private static Usuario usuarioActual;

        static void Main(string[] args)
        {
            // Mostrar mensaje de bienvenida
            Console.WriteLine("Bienvenido a UaiOS. Por favor, inicie sesión.");

            // Solicitar nombre de usuario y contraseña
            IniciarSesion();

            // Mostrar ruta actual y permitir comandos
            EjecutarComandos();
        }

        private static void IniciarSesion()
        {
            Console.Write("Nombre de usuario: ");
            string mNombreUsuario = Console.ReadLine();

            Console.Write("Contraseña: ");
            string mContrasena = Console.ReadLine();

            // Simulación de usuario para la demostración
            usuarioActual = new Usuario(mNombreUsuario, mContrasena);

            if (!usuarioActual.Validar(mContrasena))
            {
                Console.WriteLine("Nombre de usuario o contraseña incorrectos. Intente nuevamente.");
                IniciarSesion();
            }
            else
            {
                Console.WriteLine($"Bienvenido {usuarioActual.Nombre}!");
                usuarioActual.DirectorioRaiz = new DirectorioComposite("Raiz"); // Inicializar el directorio raíz
            }
        }

        private static void EjecutarComandos()
        {
            string mComando;
            while (true)
            {
                Console.Write($"{usuarioActual.Nombre}/{usuarioActual.DirectorioRaiz.Nombre}> ");
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
                        CrearDirectorio(nuevoDirectorio);
                        break;

                    case "CD":
                        // Cambiar a otro directorio
                        if (partesComando.Length < 2)
                        {
                            Console.WriteLine("Uso: CD nombredirectorio");
                            break;
                        }
                        string directorioDestino = partesComando[1];
                        CambiarDirectorio(directorioDestino);
                        break;

                    case "MF":
                        // Crear un nuevo archivo
                        if (partesComando.Length < 3)
                        {
                            Console.WriteLine("Uso: MF nombrearchivo tamaño");
                            break;
                        }
                        string nombreArchivo = partesComando[1];
                        if (int.TryParse(partesComando[2], out int tamañoArchivo))
                        {
                            CrearArchivo(nombreArchivo, tamañoArchivo);
                        }
                        else
                        {
                            Console.WriteLine("El tamaño debe ser un número entero.");
                        }
                        break;

                    case "LS":
                        // Listar archivos y directorios
                        ListarArchivosYDirectorios();
                        break;

                    case "DI":
                        // Desconectarse del sistema operativo
                        Console.WriteLine("Desconectándose...");
                        return;

                    default:
                        Console.WriteLine("Comando no reconocido.");
                        break;
                }
            }
        }

        private static void CrearDirectorio(string nombre)
        {
            // Aquí deberías implementar la lógica para crear un directorio
            Console.WriteLine($"Directorio '{nombre}' creado.");
        }

        private static void CambiarDirectorio(string nombre)
        {
            // Aquí deberías implementar la lógica para cambiar de directorio
            Console.WriteLine($"Cambiado al directorio '{nombre}'.");
        }

        private static void CrearArchivo(string nombre, int tamaño)
        {
            // Aquí deberías implementar la lógica para crear un archivo
            Console.WriteLine($"Archivo '{nombre}' creado con tamaño {tamaño} bytes.");
        }

        private static void ListarArchivosYDirectorios()
        {
            // Aquí deberías implementar la lógica para listar archivos y directorios
            Console.WriteLine("Listando archivos y directorios...");
        }
    }
}
