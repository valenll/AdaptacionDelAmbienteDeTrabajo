using System;
public class JugadorAmateur : IJugador
{
    private int minutosCorridos;
    private const int MaxMinutosAntesDescanso = 20;

    public bool Correr(int minutos)
    {
        if (minutos <= 0)
            return true;

        if (Cansado())
            return false;

        if (minutosCorridos + minutos > MaxMinutosAntesDescanso)
        {
            minutosCorridos = MaxMinutosAntesDescanso;
            return false;
        }

        minutosCorridos += minutos;
        return true;
    }

    public bool Cansado()
    {
        return minutosCorridos >= MaxMinutosAntesDescanso;
    }

    public void Descansar(int minutos)
    {
        if (minutos <= 0) return;

        minutosCorridos = Math.Max(0, minutosCorridos - minutos);
    }

    public int MinutosCorridos => minutosCorridos;
}