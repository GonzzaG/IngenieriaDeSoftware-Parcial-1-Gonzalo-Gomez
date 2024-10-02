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
    public class ComandoMD : IComando
    {
        private readonly UaiOS _uaiOS;

        public ComandoMD(UaiOS uaiOS)
        {
            _uaiOS = uaiOS;
        }

        public void Ejecutar(string[] pArgumentos)
        {
            if(pArgumentos.Length != 1)
            {
                Console.WriteLine("Uso: MD nombredirectorio");
                return;
            }

            string mNombreDirectorio = pArgumentos[0];
            Usuario mUsuario = _uaiOS.ObtenerUsuarioConectado();

            DirectorioComponente mDirectorioActual = _uaiOS.ObtenerDirectorioActual();

            // validamos si existe ya un directorio con ese nombre    
            if (new DirectorioBL().ObtenerPorNombre(mNombreDirectorio, mDirectorioActual.Id) != null)
            {
                Console.WriteLine("Error: El directorio ya existe.");
                return;
            }

            // Se crea el nuevo directorio
            DirectorioComponente mNuevoDirectorio = new DirectorioComposite
            {
                Nombre = mNombreDirectorio,
                PadreId = mDirectorioActual.Id
            };

            // guardamos el directorio creado en la base de datos
            var mDirectorioBL = new DirectorioBL().CrearDirectorio(mNuevoDirectorio as DirectorioComposite, mUsuario.Id);
            mDirectorioActual.Agregar(mNuevoDirectorio);
            Console.WriteLine($"Directorio '{mNombreDirectorio}' creado satisfactoriamente.");
        }
    }
}
