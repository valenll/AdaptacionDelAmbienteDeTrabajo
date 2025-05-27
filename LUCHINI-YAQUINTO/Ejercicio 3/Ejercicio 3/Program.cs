using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Prueba de Jugadores Cansados");

        IJugador amateur = new JugadorAmateur();
        IJugador profesional = new JugadorProfesional();

        Console.WriteLine("\nProbando jugador amateur:");
        ProbarJugador(amateur);

        Console.WriteLine("\nProbando jugador profesional:");
        ProbarJugador(profesional);

        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }

    static void ProbarJugador(IJugador jugador)
    {
        bool exito = jugador.Correr(15);
        Console.WriteLine($"Intentó correr 15 minutos: {(exito ? "Éxito" : "Falló")}");
        Console.WriteLine($"¿Está cansado? {jugador.Cansado()}");

        exito = jugador.Correr(10);
        Console.WriteLine($"Intentó correr 10 minutos más: {(exito ? "Éxito" : "Falló")}");
        Console.WriteLine($"¿Está cansado? {jugador.Cansado()}");

        Console.WriteLine("\nDescansando 15 minutos...");
        jugador.Descansar(15);
        Console.WriteLine($"¿Está cansado después de descansar? {jugador.Cansado()}");

        exito = jugador.Correr(10);
        Console.WriteLine($"Intentó correr 10 minutos más: {(exito ? "Éxito" : "Falló")}");
        Console.WriteLine($"¿Está cansado? {jugador.Cansado()}");
    }
}