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
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public DirectorioComponente DirectorioRaiz {  get; set; }

        public Usuario() { }
        public Usuario(string pNombre, string pContrasena)
        {
            Nombre = pNombre;
            Contrasena = pContrasena;
            DirectorioRaiz = new DirectorioComposite("Raiz");
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

    }
}
