using BEL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DirectorioBL
    {
        public int CrearDirectorio(DirectorioComposite pDirectorio)
        {
            //verifico si existe en la Base de datos
            if(DirectorioCompositeDAL.Obtener(pDirectorio.Id, (int)pDirectorio.PadreId) == null)
            {
                return DirectorioCompositeDAL.Guardar(pDirectorio);
            }
            else
            {
                throw new Exception("El directorio ya existe en este nivel");
            }
        }

        public void Eliminar(DirectorioComposite pDirectorio)
        {
            DirectorioCompositeDAL.Eliminar(pDirectorio);
            return;
        }

        public List<DirectorioComposite> ListarDirectorios(int pDirectorioPadreId)
        {
            return DirectorioCompositeDAL.Listar(pDirectorioPadreId);
        }

        public DirectorioComposite Obtener(int pId, int pPadreId)
        {
            return DirectorioCompositeDAL.Obtener(pId, pPadreId); 
        }

        public void CambiarDirectorio(string pNombreDirectorio, int pDirectorioActualId)
        {
            DirectorioComposite mDirectorio = DirectorioCompositeDAL.ObtenerPorNombre(pNombreDirectorio, pDirectorioActualId);    
            
            //si no existe se lanza una excepcion
            if(mDirectorio != null)
            {
                throw new Exception("Directorio no encontrado.");
            }

        }


    }

}
