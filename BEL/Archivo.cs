using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BEL
{
    public class Archivo : DirectorioComponente
    {
        public float Tamano {  get; set; }
        public int DirectorioId { get; set; }

        public Archivo() { }
        public Archivo(string pNombre, int pTamano, int pDirectorioId)
        {
            Nombre = pNombre;
            Tamano = pTamano;
            DirectorioId = pDirectorioId;
        }

        public override void MostrarEstructura(int pNivel)
        {
            Console.WriteLine($"{new string(' ', pNivel * 2)}- {Nombre} (Tamaño: {Tamano} bytes)");
        }

        public override int ObtenerTamano()
        {
            throw new NotImplementedException();
        }
    }
}
