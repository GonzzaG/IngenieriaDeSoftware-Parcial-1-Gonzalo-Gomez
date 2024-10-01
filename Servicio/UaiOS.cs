using BEL;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio
{
    public class UaiOS
    {
        private static UaiOS _Instancia;
        private Usuario _UsuarioConectado;

        private UaiOS() { } 
        public static UaiOS ObtenerInstancia()
        {
            if(_Instancia == null) // Verificamos que no haya una instancia creada
            {
                lock(typeof(UaiOS)) // Se bloquea el tipo UaiOS para garantizar exclusividad en esta creacion
                {
                    if( _Instancia == null) // Doble verificacion de bloqueo
                    {
                        _Instancia = new UaiOS();
                    }
                }
            }
            return _Instancia;
        }

        public bool IniciarSesion(string nombre, string contrasena)
        { 
            Usuario mUsuarioEncontrado = new UsuarioBL().ObtenerPorNombreYContrasena(nombre, contrasena);

            if (mUsuarioEncontrado != null && mUsuarioEncontrado.Validar(contrasena))
            {
                _UsuarioConectado = mUsuarioEncontrado; // Establecer el usuario conectado
                return true; // Inicio de sesión exitoso
            }
            return false; // Error de autenticación
        }

        // Metodo para conectar un usuario
        public void ConectarUsuario(Usuario pUsuario)
        {
            _UsuarioConectado = pUsuario;    
        }

        // Metodo que obtiene el usuario conectado
        public Usuario ObtenerUsuarioConectado()
        {
            return _UsuarioConectado;
        }


    }
}
