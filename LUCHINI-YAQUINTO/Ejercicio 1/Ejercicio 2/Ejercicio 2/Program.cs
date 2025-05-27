using System;

class Program
{
    static void Main(string[] args)
    {
        // Crear un nuevo cronómetro
        Cronometro miCronometro = new Cronometro();

        Console.WriteLine("Prueba del cronómetro");
        Console.WriteLine("Estado inicial: " + miCronometro.MostrarTiempo());

        // Simular el paso del tiempo (125 segundos)
        Console.WriteLine("\nIncrementando 125 segundos...");
        for (int i = 0; i < 125; i++)
        {
            miCronometro.IncrementarTiempo();
        }

        Console.WriteLine("Tiempo actual: " + miCronometro.MostrarTiempo());

        // Reiniciar el cronómetro
        Console.WriteLine("\nReiniciando el cronómetro...");
        miCronometro.Reiniciar();
        Console.WriteLine("Tiempo después de reiniciar: " + miCronometro.MostrarTiempo());

        // Prueba adicional
        Console.WriteLine("\nIncrementando 45 segundos más...");
        for (int i = 0; i < 45; i++)
        {
            miCronometro.IncrementarTiempo();
        }
        Console.WriteLine("Tiempo actual: " + miCronometro.MostrarTiempo());

        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }
}