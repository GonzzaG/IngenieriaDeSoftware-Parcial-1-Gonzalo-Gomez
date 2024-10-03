using System;

namespace BEL
{
    public class Archivo : DirectorioComponente
    {
        public float Tamano { get; set; }
        public int DirectorioId { get; set; }

        public Archivo()
        { }

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

        public override float ObtenerTamano()
        {
            return Tamano;
        }
    }
}