public class Auto : IVehiculo
{
    private int posicion;
    private readonly int posicionOriginal;
    private readonly int velocidadMaxima;
    private const int VelocidadPorDefecto = 40; // m/s

    public Auto(int? velocidadPersonalizada = null)
    {
        posicion = 0;
        posicionOriginal = 0;
        velocidadMaxima = velocidadPersonalizada ?? VelocidadPorDefecto;
    }

    public void Mover(int tiempo)
    {
        if (tiempo > 0)
        {
            posicion += velocidadMaxima * tiempo;
        }
    }

    public int Posicion()
    {
        return posicion;
    }

    public void SimularPosicion()
    {
        posicion = posicionOriginal;
    }
}