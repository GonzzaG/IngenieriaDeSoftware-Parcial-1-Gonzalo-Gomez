using System;
using System.Collections.Generic;

namespace BEL
{
    public class DirectorioComposite : DirectorioComponente
    {
        // Agregacion de componentes con mas directorios o archivos
        private List<DirectorioComponente> _Componentes = new List<DirectorioComponente>();

        public int? PadreId { get; set; }

        public DirectorioComposite()
        { }

        public DirectorioComposite(string pNombre)
        {
            Nombre = pNombre;
        }

        public override void MostrarEstructura(int pNivel)
        {
            Console.WriteLine($"{new string(' ', pNivel * 2)}+ {Nombre}/");
            foreach (var Componente in _Componentes)
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

        public List<DirectorioComponente> ListarComponentes(List<DirectorioComponente> mLista)
        {
            return _Componentes = mLista;
        }

        public override float ObtenerTamano(List<DirectorioComponente> mLista)
        {
            float Tamano = 0;
            return CalcularTamano(mLista, Tamano);
        }

        public float CalcularTamano(List<DirectorioComponente> mLista, float pTamano)
        {
            foreach (DirectorioComponente mComponente in mLista)
            {
                // Validamos si es un Archivo (hoja) sumamos su tamano al total
                if (mComponente is Archivo a)
                {
                    pTamano += a.Tamano;
                }
                //Si no es archivo, es un directorio, lo que significa que puede contener mas directorios y archivos,
                //asi que realizamos recursividad
                else
                {
                    //Si es composite, recursividad para calcular tamano de sus componentes
                    if (mComponente is DirectorioComposite DC)
                    {
                        CalcularTamano(DC._Componentes, pTamano);
                    }
                }
            }

            return pTamano;
        }
    }
}