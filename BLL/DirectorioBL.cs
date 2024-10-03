using BEL;
using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class DirectorioBL
    {
        public int CrearDirectorio(DirectorioComposite pDirectorio, int pUsuarioId)
        {
            //verifico si existe en la Base de datos
            if (DirectorioCompositeDAL.Obtener(pDirectorio.Id, (int)pDirectorio.PadreId) == null)
            {
                return DirectorioCompositeDAL.Guardar(pDirectorio, pUsuarioId);
            }
            else
            {
                throw new Exception("El directorio ya existe en este nivel");
            }
        }

        public int CrearDirectorioRaiz(DirectorioComposite pDirectorio, int pUsuarioId)
        {
            // Si no tiene un nombre asignado, de forma predeterminado ponemos uno
            if (pDirectorio.Nombre == null)
                pDirectorio.Nombre = "U:"; 
             return DirectorioCompositeDAL.GuardarRaiz(pDirectorio, pUsuarioId);
        }

        public void Eliminar(DirectorioComposite pDirectorio)
        {
            DirectorioCompositeDAL.Eliminar(pDirectorio);
            return;
        }

        // Se obtienen los directorios del parametro DirectorioId
        public List<DirectorioComponente> ListarDirectorios(int pDirectorioPadreId)
        {
            return DirectorioCompositeDAL.ListarDirectorios(pDirectorioPadreId);
        }

        // Se obtienen los directorios y archivos del parametro DirectorioId
        public List<DirectorioComponente> ListarComponentes(int pDirectorioPadreId)
        {
            List<DirectorioComponente> mListaComponentes = DirectorioCompositeDAL.ListarDirectorios(pDirectorioPadreId);
            List<Archivo> mListaArchivos = new ArchivoBL().Listar(pDirectorioPadreId);

            mListaComponentes.AddRange(mListaArchivos);

            return mListaComponentes;
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
            if (mDirectorio != null)
            {
                throw new Exception("Directorio no encontrado.");
            }
        }
    }
}