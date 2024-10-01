using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
    public class Usuario
    {
        public int Id { get; set; }
        public string _nombre { get; set; }
        public string _contrasena { get; set; }
        public DirectorioComponente DirectorioRaiz {  get; set; }

        public string Nombre
        {
            get { return _nombre; }
            private set { _nombre = value; }
        }

        public string Contrasena
        {
            get { return _contrasena; }
            private set { _contrasena = value; }
        }

        public Usuario() { }
        public Usuario(string pNombre, string pContrasena)
        {
            Nombre = pNombre;
            Contrasena = pContrasena;
            DirectorioRaiz = new DirectorioComposite("Raiz");
        }

        public void EstablecerNombre(string pNombre)
        {
            _nombre = pNombre;
        }

        public void EstablecerContrasena(string pContrasena)
        {
            _contrasena = pContrasena;
        }

        public bool Validar (string pContrasena)
        {
            return Contrasena == pContrasena;
        }

        public void MostrarEstructura()
        {
            if (DirectorioRaiz != null)
            {
                DirectorioRaiz.MostrarEstructura(0);
            }
        }
        


        public void CambiarContrasena()
        {

        }

    }
}
