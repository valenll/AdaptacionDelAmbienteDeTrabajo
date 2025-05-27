using System;

public class Semaforo
{
    private string _colorActual;
    private int _tiempoTranscurrido;
    private bool _intermitente;
    private bool _mostrarColor;

    public Semaforo(string colorInicial)
    {
        _colorActual = colorInicial;
        _tiempoTranscurrido = 0;
        _intermitente = false;
        _mostrarColor = true;
    }

    public void PasoDelTiempo(int segundos)
    {
        _tiempoTranscurrido += segundos;

        CambiarColorSegunTiempo();

        if (_intermitente)
        {
            _mostrarColor = !_mostrarColor;
        }
    }

    private void CambiarColorSegunTiempo()
    {
        int tiempoCiclo = _tiempoTranscurrido % 56;

        if (tiempoCiclo < 30)
        {
            _colorActual = "Rojo";
        }
        else if (tiempoCiclo < 32)
        {
            _colorActual = "Rojo - Amarillo";
        }
        else if (tiempoCiclo < 52)
        {
            _colorActual = "Verde";
        }
        else
        {
            _colorActual = "Amarillo";
        }
    }

    public string MostrarColor()
    {
        if (_intermitente)
        {
            return _mostrarColor ? _colorActual : "apagado";
        }
        return _colorActual;
    }

    public void PonerIntermitente()
    {
        _intermitente = true;
    }

    public void SacarIntermitente()
    {
        _intermitente = false;
        _mostrarColor = true;
    }
}