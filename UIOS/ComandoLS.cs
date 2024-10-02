using BEL;
using Servicio;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIOS
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
            
            // Obtenemos los componentes del directorio actual
            var mComponentes = mDirectorioActual.ObtenerComponentes();

            // Validamos que la cantidad de componentes no sea 0
            if(mComponentes.Count == 0)
            {
                Console.WriteLine("No hay directorios ni archivos en el directorio actual.");
                return;
            }

            //
            Console.WriteLine($"Directorio '{mDirectorioActual.Nombre}'");

            //un metodo que consiga le tamanio de cada directtorio


        }
    }
}
