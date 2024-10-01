using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
    public class Archivo : DirectorioComponente
    {
        public int tamano {  get; set; } 

        public override void MostrarEstructura(int pNivel)
        {
            throw new NotImplementedException();
        }
    }
}
