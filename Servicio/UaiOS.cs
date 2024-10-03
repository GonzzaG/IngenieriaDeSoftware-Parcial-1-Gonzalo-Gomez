using BEL;
using BLL;
using System;
using System.Collections.Generic;

namespace Servicio
{
    public class UaiOS
    {
        private static UaiOS _Instancia;
        private Usuario _UsuarioConectado;
        private DirectorioComponente _DirectorioActual;

        private UaiOS()
        { }

        public static UaiOS ObtenerInstancia()
        {
            if (_Instancia == null) // Verificamos que no haya una instancia creada
            {
                lock (typeof(UaiOS)) // Se bloquea el tipo UaiOS para garantizar exclusividad en esta creacion
                {
                    if (_Instancia == null) // Doble verificacion de bloqueo
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
                _DirectorioActual = new DirectorioBL().ObtenerDirectorioRaizDelUsuario(_UsuarioConectado.Id); //Primer Directorio no tiene Padre_Id, por ende es el directorio raiz del usuario
                
                if(_DirectorioActual == null)
                {
                    _DirectorioActual = new DirectorioComposite { PadreId = null};
                   new DirectorioBL().CrearDirectorioRaiz((DirectorioComposite)_DirectorioActual, _UsuarioConectado.Id);
                   _DirectorioActual = new DirectorioBL().ObtenerDirectorioRaizDelUsuario(_UsuarioConectado.Id);
                }

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

        public DirectorioComponente ObtenerDirectorioActual()
        {
            return _DirectorioActual;
        }

        private string ConstruirRutaDesdeDirectorio(DirectorioComponente pNuevoDirectorioActual)
        {
            var ruta = new List<string>();

            DirectorioComponente mDirectorioPuntero = pNuevoDirectorioActual;

            // Recorremos los directorios desde el actual hasta el directorio que no tiene padre (raiz)
            while (mDirectorioPuntero != null)
            {
                // Insertamos el nombre del directorio actual al inicio de la lista
                ruta.Insert(0, mDirectorioPuntero.Nombre);

                // Subimos al directorio padre
                if (mDirectorioPuntero is DirectorioComposite mDirectorioComposite && mDirectorioComposite.PadreId != null)
                {
                    mDirectorioPuntero = new DirectorioBL().ObtenerPorId((int)mDirectorioComposite.PadreId);
                }
                else
                {
                    // En caso de ser DirectorioComponente o archivo, se termina el bucle
                    break;
                }
            }

            // Unimos los nombres de los directorios con '/' para la ruta
            return string.Join(@"\", ruta);
        }

        public string ObtenerRuta()
        {
            // Construir la ruta desde el directorio raíz hasta el directorio actual
            return $@"{_UsuarioConectado.Nombre}\{ConstruirRutaDesdeDirectorio(_DirectorioActual)}> ";
        }

        public void CambiarDirectorio(DirectorioComponente pNuevoDirectorioActual)
        {
            _DirectorioActual = pNuevoDirectorioActual;
        }

        public void LogOut()
        {
            _UsuarioConectado = null;
            _DirectorioActual = null;

            Console.WriteLine("Sesion cerrada");
        }
    }
}