using BEL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ArchivoBL
    {
        public int Guardar(Archivo pArchivo)
        {
            return ArchivoDAL.Guardar(pArchivo);
        }

        public void Eliminar(Archivo pArchivo)
        {
            ArchivoDAL.Eliminar(pArchivo);

            return;
        }

        public List<Archivo> Listar(int pDirectorioId)
        {
            return ArchivoDAL.Listar(pDirectorioId);
        }


        public Archivo Obtener(int pId, int pPadreId)
        {
            return ArchivoDAL.Obtener(pId, pPadreId);
        }

        public Archivo ObtenerPorNombre(string pArchivo, int pPadreId)
        {
            return ArchivoDAL.ObtenerPorNombre(pArchivo, pPadreId); 
        }
    }
}
