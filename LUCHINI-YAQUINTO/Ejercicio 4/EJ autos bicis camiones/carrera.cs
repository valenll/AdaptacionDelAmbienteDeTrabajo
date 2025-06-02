using System;
public class Carrera
{
    public static void Competir(IVehiculo vehiculo1, IVehiculo vehiculo2, int segundos)
    {
        // Simular posición original
        vehiculo1.SimularPosicion();
        vehiculo2.SimularPosicion();

        // Mover los vehículos
        vehiculo1.Mover(segundos);
        vehiculo2.Mover(segundos);

        // Mostrar resultados
        Console.WriteLine($"Resultados después de {segundos} segundos:");
        Console.WriteLine($"Vehículo 1 - Posición: {vehiculo1.Posicion()} metros");
        Console.WriteLine($"Vehículo 2 - Posición: {vehiculo2.Posicion()} metros");

        // Determinar ganador
        if (vehiculo1.Posicion() > vehiculo2.Posicion())
            Console.WriteLine("¡Vehículo 1 es el ganador!");
        else if (vehiculo2.Posicion() > vehiculo1.Posicion())
            Console.WriteLine("¡Vehículo 2 es el ganador!");
        else
            Console.WriteLine("¡Es un empate!");
    }
}