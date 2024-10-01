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

        public static void Eliminar(Archivo pArchivo)
        {
            ArchivoDAL.Eliminar(pArchivo);

            return;
        }

        public static List<Archivo> Listar()
        {
            return ArchivoDAL.Listar();
        }


        public static Archivo Obtener(int pId, int pPadreId)
        {
            return ArchivoDAL.Obtener(pId, pPadreId);
        }


    }
}
