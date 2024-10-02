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
            if (pArgumentos.Length != 1)
            {
                Console.WriteLine("Uso: CD.. nombredirectorio");
                return;
            }

            string mNombreDirectorio = pArgumentos[0];

            Usuario usuarioActual = _UaiOS.ObtenerUsuarioConectado();
           

             //implementacion para verificar si tiene padre, si tiene se va al directorio de arriba y se carga ruta

            // Si no existe, notificamos
            if (mNuevoDirectorio == null)
            {
                Console.WriteLine($"Error: El directorio '{mNombreDirectorio}' no existe.");
                return;
            }

            // Si existe, cambiamos el directorio, pasando como parametro un nuevo directorio actual
            _UaiOS.CambiarDirectorio(mNuevoDirectorio);
            Console.WriteLine($"Cambiado al directorio '{mNombreDirectorio}'.");
        }
    }
}
