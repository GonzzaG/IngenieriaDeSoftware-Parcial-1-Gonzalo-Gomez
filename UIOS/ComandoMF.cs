using BEL;
using BLL;
using DAL;
using Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIOS
{
    internal class ComandoMF : IComando
    {
        private UaiOS _UaiOS;

        public ComandoMF(UaiOS sistemaOperativo)
        {
            _UaiOS = sistemaOperativo;
        }

        public void Ejecutar(string[] pArgumentos)
        {
            if (pArgumentos.Length != 2)
            {
                Console.WriteLine("Uso: MF nombreArchivo (Tamaño)");
                return;
            }


            string mNombreArchivo = pArgumentos[0];

            // Verificamos si la posicion 1 de argumentos es un int (va el tamano)
            if (!int.TryParse(pArgumentos[1], out int pTamanoArchivo))
            {
                Console.WriteLine("El tamaño debe ser un número entero o con decimales.");
                return;
            }

            // Obtengo el directorio actual, el cual contendra una lista de componentes
            DirectorioComposite mDirectorioActual = (DirectorioComposite)_UaiOS.ObtenerDirectorioActual();

            //OPCION 2
     
            // Buscamos si existe un archivo con el nombre que se coloco, perteneciente al id del directorio actual
            if (new ArchivoBL().ObtenerPorNombre(pArgumentos[0], mDirectorioActual.Id) != null)
            {
                Console.WriteLine("Error: El directorio ya existe.");
                return;
            }

            // Si no existe, se crea el nuevo archivo
            Archivo mNuevoArchivo = new Archivo
            {
                Nombre = pArgumentos[0],
                Tamano = int.Parse(pArgumentos[1]),
                DirectorioId = mDirectorioActual.Id, // Le colocamos el id directorio actual al que pertenece
            };

            // guardamos el archivo creado en la base de datos
            var mDirectorioBL = new ArchivoBL().Guardar(mNuevoArchivo);
            mDirectorioActual.Agregar(mNuevoArchivo);
            Console.WriteLine($"Archivo '{mNuevoArchivo.Nombre}' creado satisfactoriamente.");

        }
    }
}
