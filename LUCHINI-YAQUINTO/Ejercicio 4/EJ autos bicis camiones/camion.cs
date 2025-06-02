public class Camion : IVehiculo
{
    private int posicion;
    private readonly int posicionOriginal;
    private const int VelocidadMaxima = 30; // m/s

    public Camion()
    {
        posicion = 0;
        posicionOriginal = 0;
    }

    public void Mover(int tiempo)
    {
        if (tiempo > 0)
        {
            posicion += VelocidadMaxima * tiempo;
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