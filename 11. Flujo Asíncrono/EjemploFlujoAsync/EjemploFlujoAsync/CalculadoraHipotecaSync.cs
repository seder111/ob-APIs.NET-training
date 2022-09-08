using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploFlujoAsync
{
    public static class CalculadoraHipotecaSync
    {
        public static int ObtenerAniosVidaLaboral()
        {
            Console.WriteLine("\nObteniendo años de vida laboral...");
            Task.Delay(2000).Wait(); // Esperamos 2 segundos
            return new Random().Next(1, 35); // Devolvemos un valor aleatorio entre 1 y 35
        }

        public static bool EsTipoContratoIndefinido()
        {
            Console.WriteLine("\nVerificando si el tipo de contrato es indefinido...");
            Task.Delay(2000).Wait(); // Esperamos 2 segundos
            return new Random().Next(1, 10) % 2 == 0; // Devolvemos true o false si el valor es par o impar
        }

        public static int ObtenerSueldoNeto()
        {
            Console.WriteLine("\nObteniendo sueldo neto...");
            Task.Delay(2000).Wait(); // Esperamos 2 segundos
            return new Random().Next(800, 6000); // Devolvemos un número entre 800 y 6000
        }


        public static int ObtenerGastosMensuales()
        {
            Console.WriteLine("\nObteniendo gastos mensuales del usuario...");
            Task.Delay(2000).Wait();
            return new Random().Next(400, 1000);
        }

        public static bool AnalizarInformacionPacaCondecerHipoteca(int aniosVidaLaboral, bool tipoContratoEsIndefinido, int sueldoNeto, int gastosMensuales, int cantidadSolicitada, int aniosPagar)
        {
            Console.WriteLine("\nAnalizando información para conceder hipoteca...");
            
            // Si la vida laboral es menor a 2 años devolvemos false.
            if(aniosVidaLaboral < 2)
            {
                return false;
            }

            var cuota = (cantidadSolicitada / aniosPagar) / 12;

            if(cuota >= sueldoNeto || cuota >= (sueldoNeto / 2))
            {
                return false;
            }


            var porcentajeGastosSobreSueldo = ((gastosMensuales * 100) / sueldoNeto);

            if (porcentajeGastosSobreSueldo > 30)
            {
                return false;
            }


            if((cuota + gastosMensuales) >= sueldoNeto)
            {
                return false;
            }

            if (!tipoContratoEsIndefinido)
            {
                if((cuota + gastosMensuales) >= (sueldoNeto / 3))
                {
                    return false;
                } else
                {
                    return true;
                }
            }

            // Si no entra en ninguna de las condiciones anteriores concedemos la Hipoteca.
            return true;
        }
    }
}
