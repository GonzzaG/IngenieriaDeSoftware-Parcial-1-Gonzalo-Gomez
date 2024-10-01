using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
    public abstract class DirectorioComponente
    {
        public int Id {  get; set; }
        public string Name { get; set; }

        // Mostrar archivos y directorios
        public abstract void MostrarEstructura(int pNivel);

        // Metodos para gestionar los elementos del DirectorioComponente
        // En virtual ya que los archivos no implementan estos metodos.
        public virtual void Agregar(DirectorioComponente pComponente)
        {
            throw new NotSupportedException("Operacion no soportada en hojas");
        }
        public virtual void Eliminar(DirectorioComponente pComponente)
        {
            throw new NotSupportedException("Operacion no soportada en hojas");
        }
    }
}
