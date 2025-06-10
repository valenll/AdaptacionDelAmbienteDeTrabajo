using System;
using System.Collections.Generic;
using System.Linq;
public class Paciente
{
    public string DocumentoIdentidad { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Telefono { get; set; }
    public string ObraSocial { get; set; }
    public decimal MontoCobertura { get; set; }
    public List<IntervencionRealizada> Intervenciones { get; set; } = new List<IntervencionRealizada>();

    public Paciente(string documentoIdentidad, string nombre, string apellido, string telefono, string obraSocial = null, decimal montoCobertura = 0)
    {
        DocumentoIdentidad = documentoIdentidad;
        Nombre = nombre;
        Apellido = apellido;
        Telefono = telefono;
        ObraSocial = obraSocial ?? "-";
        MontoCobertura = montoCobertura;
    }

    public string NombreCompleto => $"{Apellido}, {Nombre}";

    public void AgregarIntervencion(IntervencionRealizada intervencion)
    {
        Intervenciones.Add(intervencion);
    }

    public decimal CalcularTotalPendientePago()
    {
        return Intervenciones
            .Where(i => !i.Pagado)
            .Sum(i => i.CalcularCostoFinal(this));
    }

    public List<IntervencionRealizada> ObtenerIntervencionesPendientes()
    {
        return Intervenciones.Where(i => !i.Pagado).ToList();
    }
}

public class IntervencionRealizada
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public Intervencion Intervencion { get; set; }
    public Doctor Medico { get; set; }
    public bool Pagado { get; set; }

    public IntervencionRealizada(int id, DateTime fecha, Intervencion intervencion, Doctor medico, bool pagado = false)
    {
        Id = id;
        Fecha = fecha;
        Intervencion = intervencion;
        Medico = medico;
        Pagado = pagado;
    }

    public decimal CalcularCostoFinal(Paciente paciente)
    {
        decimal costo = Intervencion.CalcularCosto();
        if (paciente.ObraSocial != "-")
        {
            costo -= paciente.MontoCobertura;
            if (costo < 0) costo = 0;
        }
        return costo;
    }
}