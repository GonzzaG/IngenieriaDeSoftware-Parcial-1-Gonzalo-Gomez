using BEL;
using BLL;
using Servicio;
using System.Security.Cryptography;
using UIOS.Abstracciones;

namespace UIOS.Comandos
{
    internal class ComandoLS : IComando
    {
        private UaiOS _UaiOS;

        public ComandoLS(UaiOS UaiOS)
        {
            _UaiOS = UaiOS;
        }

        public void Ejecutar(string[] pArgumentos)
        {
            if (pArgumentos.Length != 0)
            {
                Console.WriteLine("Uso: LS");
                return;
            }

            // Obtenemos el directorio donde estamos parados
            DirectorioComposite mDirectorioActual = (DirectorioComposite)_UaiOS.ObtenerDirectorioActual();

            // Obtenemos los componentes (directorios y archivos) del directorio actual
            List<DirectorioComponente> mListaComponentes = new DirectorioBL().ListarComponentes(mDirectorioActual.Id);

            mDirectorioActual.ListarComponentes(mListaComponentes);

            // Validamos si la lista tiene algo
            if (mListaComponentes.Count == 0)
            {
                Console.WriteLine("No hay datos en el directorio actual.");
                return;
            }

            // Recorrer todos los componentes del directorio actual y sus subdirectorios
            Console.WriteLine($"\nContenido del directorio '{mDirectorioActual.Nombre}':");
            ListarComponentesEnMemoria(mDirectorioActual);

            MostrarDetallesComponente(_UaiOS.ObtenerDirectorioActual(), 0);
            Console.WriteLine(" ");
        }

        public void ListarComponentesEnMemoria(DirectorioComposite pDirectorioActual)
        {

            foreach (DirectorioComponente mComponente in pDirectorioActual.ObtenerComponentes())
            {
                // Validamos si es un composite, si lo es, tenemos que listar en memoria, los componentes que contenga en la base de datos
                if (mComponente is DirectorioComposite mDC)
                {
                    // Obtenemos lista de componentes en base de datos
                    List<DirectorioComponente> mLista = new DirectorioBL().ListarComponentes(mDC.Id);

                    // Listamos la lista de la BD en memoria
                    if (mLista.Count > 0)
                    {
                        // Listamos los componentes en memoria del directorio
                        mDC.ListarComponentes(mLista);

                        // Vamos a iterar entre los subcomponentes para cargar en memoria los componentes que poseen
                        foreach (DirectorioComponente mSubcomponente in mDC.ObtenerComponentes())
                            ListarComponentesEnMemoria(mDC);
                    }
                }
            }
        }

        // Método para mostrar detalles de cada componente recursivamente
        private void MostrarDetallesComponente(DirectorioComponente pComponente, int pNivel)
        {
            // Instanciamos una sangría para separar directorios y subdirectorios/archivos
            string mSangria = new string(' ', pNivel * 2);

            // Mostrar información del componente
            if (pComponente is Archivo mArchivo)
            {
                // Si es un archivo, mostramos el nombre y el tamaño
                Console.WriteLine($"{mSangria}- Archivo: {mArchivo.Nombre} (Tamaño: {mArchivo.ObtenerTamano()} bytes)");
            }
            else if (pComponente is DirectorioComposite mDirectorio)
            {
                // Si es un directorio, mostramos el nombre
                Console.WriteLine($"{mSangria}+ Directorio: {mDirectorio.Nombre}/");

                // Obtenemos los componentes del directorio actual
                List<DirectorioComponente> mSubComponentes = mDirectorio.ObtenerComponentes();

                if (mSubComponentes.Count == 0)
                {
                    // Si el directorio está vacío, lo indicamos
                    Console.WriteLine($"{mSangria}  (vacío)");
                }
                else
                {
                    // Recorrer la lista de componentes del directorio actual
                    foreach (var mSubComponente in mSubComponentes)
                    {
                        // Aquí hacemos la llamada recursiva para iterar la lista de subcomponentes
                        MostrarDetallesComponente(mSubComponente, pNivel + 1);
                    }
                }
            }
        }
    }
}