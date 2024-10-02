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
        public int CrearDirectorio(DirectorioComposite pDirectorio, int pUsuarioId)
        {
            //verifico si existe en la Base de datos
            if(DirectorioCompositeDAL.Obtener(pDirectorio.Id, (int)pDirectorio.PadreId) == null)
            {
                return DirectorioCompositeDAL.Guardar(pDirectorio, pUsuarioId);
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

        public List<DirectorioComponente> ListarDirectorios(int pDirectorioPadreId)
        {
            return DirectorioCompositeDAL.Listar(pDirectorioPadreId);
        }

        public DirectorioComposite Obtener(int pId, int pPadreId)
        {
            return DirectorioCompositeDAL.Obtener(pId, pPadreId); 
        }

        public DirectorioComposite ObtenerPorId(int pId)
        {
            return DirectorioCompositeDAL.ObtenerPorId(pId);
        }

        public DirectorioComposite ObtenerDirectorioRaizDelUsuario(int pUsuarioId)
        {
            return DirectorioCompositeDAL.ObtenerDirectorioRaizDelUsuario(pUsuarioId);
        }

        public DirectorioComposite ObtenerPorNombre(string pNombredirectorio, int pDirectorioActualId)
        {
            return DirectorioCompositeDAL.ObtenerPorNombre(pNombredirectorio, pDirectorioActualId);
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
