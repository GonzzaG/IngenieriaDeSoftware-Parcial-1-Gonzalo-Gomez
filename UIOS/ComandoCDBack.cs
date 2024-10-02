using BEL;
using BLL;
using Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIOS
{
    internal class ComandoCDBack : IComando
    {
        private readonly UaiOS _UaiOS;

        public ComandoCDBack(UaiOS UaiOS)
        {
            _UaiOS = UaiOS;
        }

        public void Ejecutar(string[] pArgumentos)
        {
            if (pArgumentos.Length != 0)
            {
                Console.WriteLine("Uso: MD nombreDirectorio");
                return;
            }

            // Obtenemos el directorio Actual
            DirectorioComposite mDirectorioActual = (DirectorioComposite)_UaiOS.ObtenerDirectorioActual();

            //validamos si el directorio actual tiene padre para retroceder
            if (mDirectorioActual.PadreId == null)
            {
                Console.WriteLine("Ya se encuentra en el directorio raiz.");
            }
            else
            {
                //si tiene un directorio padre, verificamos en la base de datos
                DirectorioComposite mDirectorioPadre = new DirectorioBL().ObtenerPorId((int)mDirectorioActual.PadreId);

                // Validamos si realmente existe en la base de datos el directorio padre
                if(mDirectorioPadre == null)
                {
                    Console.WriteLine("Ya se encuentra en el directorio raiz.");
                }
                else
                {

                    _UaiOS.CambiarDirectorio(mDirectorioPadre);
                    Console.WriteLine("Retrocedio un directorio");
                }
            }

        }
    }
}
