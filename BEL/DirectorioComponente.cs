using System;
using System.Collections.Generic;

namespace BEL
{
    public abstract class DirectorioComponente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Mostrar archivos y directorios
        public abstract void MostrarEstructura(int pNivel);

        public virtual float ObtenerTamano()
        {
            throw new Exception("Si es archivo se obtiene su tamano");
        }

        public virtual float ObtenerTamano(List<DirectorioComponente> mLista)
        {
            throw new Exception("Si es Directorio se obtiene el tamano de todos sus componentes");
        }

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