using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Prueba de Vehículos");

        // Crear vehículos según el ejemplo
        Auto flat = new Auto(45); // El auto se mueve a 45 m/s
        Bicicleta bici = new Bicicleta();
        Camion camion = new Camion();

        // Prueba con bicicleta
        Console.WriteLine("\nPrueba con bicicleta:");
        bici.Mover(20);
        Console.WriteLine($"Posición después de 20 segundos: {bici.Posicion()} metros");
        bici.Mover(10);
        Console.WriteLine($"Posición después de otros 10 segundos: {bici.Posicion()} metros");

        // Carrera entre auto y camión
        Console.WriteLine("\nCarrera entre auto y camión:");
        Carrera.Competir(flat, camion, 60);

        // Carrera entre bicicleta y auto
        Console.WriteLine("\nCarrera entre bicicleta y auto:");
        Carrera.Competir(bici, flat, 30);

        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }
}