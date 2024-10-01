using BEL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioBL
    {
        public int Guardar(Usuario pUsuario)
        {
            return UsuarioDAL.Guardar(pUsuario);
        }

        public void Eliminar(Usuario pUsuario)
        {
            UsuarioDAL.Eliminar(pUsuario);
            return;
        }

        public List<Usuario> Listar()
        {
            return UsuarioDAL.Listar(); 
        }

        public Usuario Obtener(int pId)
        {
            return UsuarioDAL.Obtener(pId);
        }

        public Usuario ObtenerPorNombreYContrasena(string pNombre, string pContrasena)
        {
            return UsuarioDAL.ObtenerPorNombreYContrasena(pNombre, pContrasena);
        }


    }
}
