using BEL;
using BLL;
using Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UIOS
{
    internal class ComandoCD : IComando
    {
        private readonly UaiOS _UaiOS;

        public ComandoCD(UaiOS UaiOS)
        {
            _UaiOS = UaiOS;
        }

        public void Ejecutar(string[] pArgumentos)
        {
            if (pArgumentos.Length != 1)
            {
                Console.WriteLine("Uso: CD nombreDirectorio");
                return;
            }

            string mNombreDirectorio = pArgumentos[0];

            DirectorioComposite mDirectorioActual = (DirectorioComposite)_UaiOS.ObtenerDirectorioActual();
            DirectorioComposite mNuevoDirectorio = null;

            mDirectorioActual.ListarComponentes(new DirectorioBL().ListarComponentes(mDirectorioActual.Id));
            // Se recorren los componentes del directorio actual para verificar si existe el archivo buscado
            // con ese nombre
            foreach (DirectorioComposite pComponente in mDirectorioActual.ObtenerComponentes())
            {
                if(pComponente is DirectorioComposite mDir)
                { 
                    // Se compara si los nombres son iguales, sin importar mayusculas ni minusculas
                    if(mDir.Nombre.Equals(mNombreDirectorio, StringComparison.OrdinalIgnoreCase))
                    {
                        // Se guarda el directorio con el nombre buscado en el nuevo directorio
                        mNuevoDirectorio = mDir;
                        break;
                    }
                }
            }

            // Si no existe, notificamos
            if(mNuevoDirectorio == null)
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
