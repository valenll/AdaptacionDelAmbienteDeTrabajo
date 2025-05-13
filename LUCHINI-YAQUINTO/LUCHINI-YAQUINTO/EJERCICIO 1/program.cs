using System;
using System.Threading;

namespace SemaforoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Semaforo semaforo = new Semaforo("Rojo");

            Console.WriteLine($"Estado inicial del semáforo: {semaforo.MostrarColor()}");

            bool continuar = true;
            int tiempo = 0;

            Console.WriteLine("\nPresiona Ctrl+C para salir...\n");

            while (continuar)
            {
                Thread.Sleep(1000); 
                tiempo++;

                semaforo.PasoDelTiempo(1);
                Console.WriteLine($"Segundo {tiempo}: {semaforo.MostrarColor()}");

                if (tiempo == 10)
                {
                    Console.WriteLine("\n--- Activando intermitente ---");
                    semaforo.PonerIntermitente();
                }
                else if (tiempo == 20)
                {
                    Console.WriteLine("\n--- Desactivando intermitente ---");
                    semaforo.SacarIntermitente();
                }
            }
        }
    }
}