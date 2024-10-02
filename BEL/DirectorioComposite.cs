using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BEL
{
    public class DirectorioComposite : DirectorioComponente
    {
        // Agregacion de componentes con mas directorios o archivos
        private List<DirectorioComponente> _Componentes = new List<DirectorioComponente>();
        public int? PadreId { get; set; }

        public DirectorioComposite() { }
        public DirectorioComposite(string pNombre)
        {
            Nombre = pNombre;
        }

        public override void MostrarEstructura(int pNivel)
        {
            Console.WriteLine($"{new string(' ', pNivel * 2)}+ {Nombre}/");
            foreach(var Componente in _Componentes)
            {
                Componente.MostrarEstructura(pNivel + 1);
            }
        }

        // Se agrega un componente al composite
        public override void Agregar(DirectorioComponente pComponente)
        {
            _Componentes.Add(pComponente);
        }

        // Se elimina un componente al composite
        public override void Eliminar(DirectorioComponente pComponente)
        {
            _Componentes.Remove(pComponente);
        }

        public List<DirectorioComponente> ObtenerComponentes()
        {
            return _Componentes;
        }
        public override int ObtenerTamano()
        {
            return _Componentes.Sum(c => c.ObtenerTamano());
        }
        public List<DirectorioComponente> ListarComponentes(List<DirectorioComponente> mLista)
        {
            return _Componentes = mLista;
        }

    }
}
