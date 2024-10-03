using Servicio;
using UIOS.Abstracciones;

namespace UIOS.Comandos
{
    internal class ComandoDI : IComando
    {
        private UaiOS _UaiOS;

        public ComandoDI(UaiOS UaiOS)
        {
            _UaiOS = UaiOS;
        }

        public void Ejecutar(string[] pArgumentos)
        {
            Console.WriteLine("Desconectandose del UAI Sistema Operativo...");

            // Limpiamos el usuario y directorio actual del sistema operativo
            _UaiOS.LogOut();
        }
    }
}