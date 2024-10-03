using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
    internal class BL
    {

        // Interfaz Observador
        public interface IObservador
        {
            void Actualizar();
        }

        // Interfaz Sujeto
        public interface ISujeto
        {
            void RegistrarObservador(IObservador observador);
            void EliminarObservador(IObservador observador);
            void NotificarObservadores();
        }

        // Clase Auto que implementa la interfaz ISujeto
        public class Auto : ISujeto
        {
            private List<IObservador> observadores = new List<IObservador>();
            private bool encendido;

            public void RegistrarObservador(IObservador observador)
            {
                observadores.Add(observador);
            }

            public void EliminarObservador(IObservador observador)
            {
                observadores.Remove(observador);
            }

            public void NotificarObservadores()
            {
                foreach (var observador in observadores)
                {
                    observador.Actualizar();
                }
            }

            public void Arrancar()
            {
                encendido = true;
                Console.WriteLine("El auto ha arrancado.");
                NotificarObservadores();
            }

            public void Detener()
            {
                encendido = false;
                Console.WriteLine("El auto se ha detenido.");
                NotificarObservadores();
            }

            public bool EstaEncendido()
            {
                return encendido;
            }
        }

        // Clase SistemaDeAudio que implementa la interfaz IObservador
        public class SistemaDeAudio : IObservador
        {
            public void Actualizar()
            {
                Console.WriteLine("El sistema de audio está listo para reproducir música.");
            }
        }

        // Clase SistemaDeNavegacion que implementa la interfaz IObservador
        public class SistemaDeNavegacion : IObservador
        {
            public void Actualizar()
            {
                Console.WriteLine("El sistema de navegación está listo.");
            }
        }

        // Clase LuzDeAdvertencia que implementa la interfaz IObservador
        public class LuzDeAdvertencia : IObservador
        {
            public void Actualizar()
            {
                Console.WriteLine("La luz de advertencia está encendida.");
            }
        }

        // Ejemplo de uso
        public class Program
        {
            public static void Main(string[] args)
            {
                Auto auto = new Auto();

                SistemaDeAudio audio = new SistemaDeAudio();
                SistemaDeNavegacion navegacion = new SistemaDeNavegacion();
                LuzDeAdvertencia luzAdvertencia = new LuzDeAdvertencia();

                // Registrando los observadores
                auto.RegistrarObservador(audio);
                auto.RegistrarObservador(navegacion);
                auto.RegistrarObservador(luzAdvertencia);

                // Arrancando el auto
                auto.Arrancar();

                // Deteniendo el auto
                auto.Detener();
            }
        }
    }

    // Violación del SRP: La clase tiene más de una responsabilidad.
    public class Reporte
    {
        public void GenerarReporte()
        {
            // Lógica para generar el informe
        }

        public void EnviarReportePorEmail(string pEmail)
        {
            // Lógica para enviar el informe por correo electrónico
        }
    }

    // Cumple con el SRP
    public class GeneradorReporte
    {
        public void GenerarReporte()
        {
            // Lógica para generar el informe
        }
    }



    public class EmailEmisor
    {
        public void EnviarReportePorEmail(string pEmail)
        {
            // Lógica para enviar el informe por correo electrónico
        }
    }



    // Sin OCP: Cada vez que agregamos un tipo de descuento, modificamos la clase.
    public class CalculadoraDescuento
    {
        public decimal CalcularDescuento(string pTipo)
        {
            if (pTipo == "Verano")
                return 10;
            else if (pTipo == "Invierno")
                return 20;
            else
                return 0;
        }
    }


    // Aplicando OCP
    public abstract class Descuento
    {
        public abstract decimal ObtenerDescuento();
    }

    public class VeranoDescuento : Descuento
    {
        public override decimal ObtenerDescuento() => 10;
    }

    public class InviernoDescuento : Descuento
    {
        public override decimal ObtenerDescuento() => 20;
    }


    // Violación del LSP
    public class Rectangulo
    {
        public virtual void SetAncho(int Ancho) { /* lógica */ }
        public virtual void SetAltura(int Altura) { /* lógica */ }
    }

    public class Cuadrado : Rectangulo
    {
        public override void SetAncho(int width)
        {
            base.SetAncho(width);
            base.SetAltura(width); // Violación: Cambiar el ancho afecta al alto.
        }
    }


    // Violación del ISP: Interfaz muy grande
    public interface ITrabajador
    {
        void Trabajar();
        void Comer();
    }

    public class Robot : ITrabajador
    {
        public void Trabajar() { /* lógica de trabajo */ }
        public void Comer() { /* No aplica para un robot */ } // Violación
    }

    // Cumple con ISP: Interfaces específicas para cada acción.
    public interface ITrabajador2
    {
        void Trabajar2();
    }

    public interface IComer
    {
        void Comer();
    }

    public class Robot2 : ITrabajador2
    {
        public void Trabajar2() { /* lógica de trabajo */ }
    }

    // Violación del DIP: La clase ReportManager depende directamente de EnviadorDeEmail.
    public class EnviadorDeEmail
    {
        public void EnviarEmail() { /* Lógica de envío de correo */ }
    }

    public class GestorDeReportes
    {
        private EnviadorDeEmail enviadorDeEmail = new EnviadorDeEmail();

        public void EnviarReporte()
        {
            enviadorDeEmail.EnviarEmail();
        }
    }

    // Aplicando DIP
    public interface IEnviadorDeEmail
    {
        void EnviarEmail2();
    }

    public class EnviadorDeEmail2 : IEnviadorDeEmail
    {
        public void EnviarEmail2() { /* Lógica de envío de correo */ }
    }

    public class GestorDeReportes2
    {
        private IEnviadorDeEmail enviadorDeEmail;

        public GestorDeReportes2(IEnviadorDeEmail enviadorDeEmail)
        {
            this.enviadorDeEmail = enviadorDeEmail;
        }

        public void EnviarReporte()
        {
            enviadorDeEmail.EnviarEmail2();
        }
    }





}
