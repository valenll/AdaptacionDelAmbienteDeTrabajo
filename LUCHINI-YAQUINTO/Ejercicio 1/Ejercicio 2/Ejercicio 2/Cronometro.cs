public class Cronometro
{
    private int minutos;
    private int segundos;

    public Cronometro()
    {
        Reiniciar();
    }

    public void Reiniciar()
    {
        minutos = 0;
        segundos = 0;
    }

    public void IncrementarTiempo()
    {
        segundos++;
        if (segundos >= 60)
        {
            minutos++;
            segundos = 0;
        }
    }

    public string MostrarTiempo()
    {
        return $"{minutos} minutos, {segundos} segundos";
    }

    // Propiedades para acceder a los valores (opcional)
    public int Minutos => minutos;
    public int Segundos => segundos;
}